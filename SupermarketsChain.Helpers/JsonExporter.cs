namespace SupermarketsChain.Helpers
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    using SupermarketsChain.Data;
    using SupermarketsChain.Models;
    using Newtonsoft.Json;
    using MongoDB.Driver;

    public static class JsonExporter
    {
        public static void ExportSalesToMongoDb(string startDate, string endDate)
        {
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);
            ExportSalesToMongoDb(start, end);
        }

        public static void ExportSalesToMongoDb(DateTime startDate, DateTime endDate)
        {
            var mongoCLient = new MongoClient(Settings.Default.MongoDbConnectionString);
            var mongoDb = mongoCLient.GetServer().GetDatabase(Settings.Default.DefaultDbName);
            var mongoSales = mongoDb.GetCollection<JsonSale>("SalesByProductReports");
            var salesByProduct = GetSalesByProduct(startDate, endDate);
            mongoSales.InsertBatch(salesByProduct);
        }

        public static void ExportSalesToJson(string startDate, string endDate)
        {
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);
            ExportSalesToJson(start, end);
        }

        public static void ExportSalesToJson(DateTime startDate, DateTime endDate)
        {
            Directory.CreateDirectory(Settings.Default.JsonReportsFolder);

            var salesByProduct = GetSalesByProduct(startDate, endDate);
            foreach (var sale in salesByProduct)
            {
                var json = JsonConvert.SerializeObject(sale, Formatting.Indented);
                var path = Settings.Default.JsonReportsFolder + sale.ProductId + ".json";
                File.WriteAllText(path, json);
            }
        }

        private static IEnumerable<JsonSale> GetSalesByProduct(DateTime startDate, DateTime endDate)
        {
            using (var sqlServerDb = new SupermarketsChainEntities())
            {
                return sqlServerDb.Sales
                    .Where(sale => sale.DateOfSale >= startDate && sale.DateOfSale <= endDate)
                    .GroupBy(sale => new
                    {
                        sale.ProductId,
                        ProductName = sale.Product.Name,
                        VendorName = sale.Product.Vendor.Name
                    })
                    .Select(group => new JsonSale
                    {
                        ProductId = group.Key.ProductId,
                        ProductName = group.Key.ProductName,
                        VendorName = group.Key.VendorName,
                        QuantitySold = (double)group.Sum(g => g.Quantity),
                        TotalIncomes = (double)group.Sum(g => g.Quantity * g.PricePerUnit)
                    })
                    .ToList();
            }
        }
    }
}
