namespace SupermarketsChain.Helpers
{
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Data;
    using SupermarketsChain.Models;
    using System.Collections.Generic;

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

            using (var sqlServerDb = new SupermarketsChainEntities())
            {
                var salesByProduct = sqlServerDb.Sales
                    .Where(sale => sale.DateOfSale >= startDate && sale.DateOfSale <= endDate)
                    .Select(sale => new JsonSale
                    {
                        ProductId = sale.ProductId,
                        ProductName = sale.Product.Name,
                        VendorName = sale.Product.Vendor.Name,
                        QuantitySold = (double)sale.Quantity,
                        TotalIncomes = (double)(sale.Quantity * sale.PricePerUnit)
                    });

                mongoSales.InsertBatch(salesByProduct);
            }
        }
    }
}
