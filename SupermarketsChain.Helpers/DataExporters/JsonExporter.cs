namespace SupermarketsChain.Helpers.DataExporters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Data;
    using Models;
    using MongoDB.Driver;
    using Newtonsoft.Json;

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
            var mongoClient = new MongoClient(Settings.Default.MongoDbConnectionString);
            var mongoDb = mongoClient.GetServer().GetDatabase(Settings.Default.DefaultDbName);
            var mongoSales = mongoDb.GetCollection<ProductTotalSale>("SalesByProductReports");
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

        private static IEnumerable<ProductTotalSale> GetSalesByProduct(DateTime startDate, DateTime endDate)
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
                    .Select(group => new ProductTotalSale
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
