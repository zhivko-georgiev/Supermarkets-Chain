namespace SupermarketsChain.Helpers.DataImporters
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Data;
    using Excel;
    using Ionic.Zip;
    using Models;

    public class XlsImporter
    {
        public static void ImportSales()
        {
            using (var zip = ZipFile.Read(Settings.Default.SalesReportsZipFile))
            {
                foreach (var entry in zip.Entries.Where(e => Path.GetExtension(e.FileName) == ".xls"))
                {
                    using (var stream = new MemoryStream())
                    {
                        entry.Extract(stream);
                        using (var excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
                        {
                            var currentDate = DateTime.Parse(entry.FileName.Split('/').First());
                            AddSalesToDb(excelReader, currentDate);
                        }
                    }
                }
            }
        }

        private static void AddSalesToDb(IExcelDataReader excelReader, DateTime currentDate)
        {
            using (var db = new SupermarketsChainEntities())
            {
                var salesTable = excelReader.AsDataSet().Tables["Sales"];

                var locationName = (string)salesTable.Rows[1].ItemArray[1];
                var currentLocation = GetOrCreateLocation(locationName, db);

                for (var i = 3; i < salesTable.Rows.Count; i++)
                {
                    if (((string)salesTable.Rows[i].ItemArray[1]).Contains("Total sum"))
                    {
                        break;
                    }

                    var productName = (string)salesTable.Rows[i].ItemArray[1];
                    productName = Regex.Replace(productName, @"[^\w'\. ]", string.Empty);
                    var currentProduct = GetOrCreateProduct(productName, db);

                    var quantity = (double)salesTable.Rows[i].ItemArray[2];
                    var pricePerUnit = (double)salesTable.Rows[i].ItemArray[3];

                    db.Sales.Add(new Sale
                    {
                        Location = currentLocation,
                        DateOfSale = currentDate,
                        Product = currentProduct,
                        Quantity = (decimal)quantity,
                        PricePerUnit = (decimal)pricePerUnit
                    });
                }

                db.SaveChanges();
            }
        }

        private static Product GetOrCreateProduct(string productName, SupermarketsChainEntities db)
        {
            var product = db.Products.FirstOrDefault(p => p.Name == productName);
            if (product == null)
            {
                product = new Product
                {
                    Name = productName,
                    Vendor = new Vendor { Name = productName.Split(' ').Last() + " Corp." }
                };

                db.Products.Add(product);
                db.SaveChanges();
            }

            return product;
        }

        private static Location GetOrCreateLocation(string locationName, SupermarketsChainEntities db)
        {
            var location = db.Locations.FirstOrDefault(l => l.Name == locationName);
            if (location == null)
            {
                location = new Location { Name = locationName };
                db.Locations.Add(location);
                db.SaveChanges();
            }

            return location;
        }
    }
}
