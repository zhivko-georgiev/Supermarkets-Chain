namespace SupermarketsChain.Helpers
{
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Data;
    using System.Collections.Generic;


    public class JSONReport
    {
        private DateTime begingingDate = new DateTime(1, 1, 1);
        private DateTime endingDate = new DateTime(5000, 1, 1);

        private string mongoDbName;
        private string serverAdress;
        private int port;

        public JSONReport(string begingingDate, string endingDate)
        {
            this.begingingDate = DateTime.Parse(begingingDate);
            this.endingDate = DateTime.Parse(endingDate);
        }

        public void Generate()
        {
            var report = new List<JEntity>();

            using (SupermarketsChainEntities db = new SupermarketsChainEntities())
            {
                report = MakeTheReportQuery(db);
            }

            var mdb = InitializeMongoDatabase();

            var collection = mdb.GetCollection<BsonDocument>("JSONReport" + DateTime.Now);

            foreach (var entity in report)
            {
                BsonDocument bson = new BsonDocument();
                bson.Add("product-id", entity.Id);
                bson.Add("product-name", entity.ProductName);
                bson.Add("vendor-name", entity.VendorName);
                bson.Add("total-quantity-sold", entity.QuantitySold.ToString());
                bson.Add("total-incomes", entity.TotalIncomes.ToString());

                collection.Insert(bson);
            }

        }

        /// <summary>
        ///     Configures database name in MongoDb,
        ///     Server IP adress and Port 
        /// </summary>
        /// <param name="mongoDbName">Datebase Name</param>
        /// <param name="serverAdress">Server IP Adress</param>
        /// <param name="port">Server Port</param>
        public void Configure(string mongoDbName, string serverAdress, int port)
        {
            this.mongoDbName = mongoDbName;
            this.serverAdress = serverAdress;
            this.port = port;
        }

        private List<JEntity> MakeTheReportQuery(SupermarketsChainEntities db)
        {
            var report = new List<JEntity>();
            //db.Database.Log = Console.Write;
            var queryForSales = (from sales in db.Sales
                                 join product in db.Products on sales.ProductId equals product.Id
                                 join vendor in db.Vendors on product.VendorId equals vendor.Id
                                 where sales.DateOfSale >= this.begingingDate && sales.DateOfSale <= this.endingDate
                                 group sales by new { product.Id, ProductName = product.Name, VendorName = vendor.Name }
                                     into g
                                     select new
                                     {
                                         g.Key.Id,
                                         ProductName = g.Key.ProductName,
                                         VendorName = g.Key.VendorName,
                                         QuantitySold = g.Sum(a => a.Quantity),
                                         TotalIncomes = g.Sum(a => a.PricePerUnit * a.Quantity)
                                     }).ToList();

            foreach (var row in queryForSales)
            {
                var jEntity = new JEntity();

                jEntity.Id = row.Id;
                jEntity.ProductName = row.ProductName;
                jEntity.VendorName = row.VendorName;
                jEntity.QuantitySold = row.QuantitySold;
                jEntity.TotalIncomes = row.TotalIncomes;

                report.Add(jEntity);
            }

            return report;
        }

        private MongoDatabase InitializeMongoDatabase()
        {
            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(this.serverAdress, this.port);
            var client = new MongoClient(settings);
            var server = client.GetServer();
            var mdb = server.GetDatabase(this.mongoDbName);

            return mdb;
        }
    }
}
