namespace SupermarketsChain.Helpers.DbManagers
{
    using System;
    using System.IO;
    using System.Linq;
    using Data;
    using Models;
    using Oracle.ManagedDataAccess.Client;

    public static class OracleDbManager
    {
        public static void PopulateDb()
        {
            var queries = File.ReadAllText(Settings.Default.OracleSqlScriptLocation)
                .Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var connection = new OracleConnection(Settings.Default.OracleConnectionString);
            connection.Open();
            using (connection)
            {
                foreach (var query in queries)
                {
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void ExportDbToSqlServer()
        {
            var connection = new OracleConnection(Settings.Default.OracleConnectionString);
            connection.Open();
            using (connection)
            {
                using (var sqlServerDb = new SupermarketsChainEntities())
                {
                    ExportMeasures(connection, sqlServerDb);
                    ExportVendors(connection, sqlServerDb);
                    ExportLocations(connection, sqlServerDb);
                    sqlServerDb.SaveChanges();

                    ExportProducts(connection, sqlServerDb);
                    sqlServerDb.SaveChanges();

                    ExportSales(connection, sqlServerDb);
                    sqlServerDb.SaveChanges();
                }
            }
        }

        private static void ExportMeasures(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT MEASURE_NAME FROM MEASURES", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Measures.Add(new Measure
                        {
                            Name = (string)reader["MEASURE_NAME"]
                        });
                    }
                }
            }
        }

        private static void ExportVendors(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT VENDOR_NAME FROM VENDORS", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Vendors.Add(new Vendor
                        {
                            Name = (string)reader["VENDOR_NAME"]
                        });
                    }
                }
            }
        }
        
        private static void ExportLocations(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT LOCATION_NAME FROM LOCATIONS", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Locations.Add(new Location
                        {
                            Name = (string)reader["LOCATION_NAME"]
                        });
                    }
                }
            }
        }

        private static void ExportProducts(OracleConnection connection, SupermarketsChainEntities db)
        {
            const string query = "SELECT PRODUCT_NAME, VENDOR_NAME, MEASURE_NAME FROM PRODUCTS P " + 
                "JOIN VENDORS V ON V.VENDOR_ID = P.VENDOR_ID JOIN MEASURES M ON M.MEASURE_ID = P.MEASURE_ID";
            using (var command = new OracleCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vendorName = (string)reader["VENDOR_NAME"];
                        var measureName = (string)reader["MEASURE_NAME"];
                        db.Products.Add(new Product
                        {
                            Name = (string)reader["PRODUCT_NAME"],
                            Vendor = db.Vendors.FirstOrDefault(v => v.Name == vendorName),
                            Measure = db.Measures.FirstOrDefault(m => m.Name == measureName)
                        });
                    }
                }
            }
        }

        private static void ExportSales(OracleConnection connection, SupermarketsChainEntities db)
        {
            const string query = "SELECT PRODUCT_NAME, LOCATION_NAME, QUANTITY, DATE_SALE, PRICE_PER_UNIT FROM SALES S " +
                "JOIN PRODUCTS P ON P.PRODUCT_ID = S.PRODUCT_ID JOIN LOCATIONS L ON L.LOCATION_ID = S.LOCATION_ID";
            using (var command = new OracleCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productName = (string)reader["PRODUCT_NAME"];
                        var locationName = (string)reader["LOCATION_NAME"];
                        db.Sales.Add(new Sale
                        {
                            Product = db.Products.FirstOrDefault(p => p.Name == productName),
                            Location = db.Locations.FirstOrDefault(l => l.Name == locationName),
                            Quantity = (decimal)reader["QUANTITY"],
                            DateOfSale = (DateTime)reader["DATE_SALE"],
                            PricePerUnit = (decimal)(double)reader["PRICE_PER_UNIT"]
                        });
                    }
                }
            }
        }
    }
}
