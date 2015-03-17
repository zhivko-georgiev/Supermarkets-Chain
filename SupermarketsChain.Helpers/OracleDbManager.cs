namespace SupermarketsChain.Helpers
{
    using System;
    using System.IO;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;
    using Oracle.ManagedDataAccess.Client;

    public static class OracleDbManager
    {
        public static void Populate()
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

        public static void ExportToSqlServer()
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
                    ExportProducts(connection, sqlServerDb);
                    ExportSales(connection, sqlServerDb);

                    sqlServerDb.SaveChanges();
                }
            }
        }

        private static void ExportMeasures(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT * FROM MEASURES", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Measures.Add(new Measure
                        {
                            Id = (int)(decimal)reader["MEASURE_ID"],
                            Name = (string)reader["MEASURE_NAME"]
                        });
                    }
                }
            }
        }

        private static void ExportVendors(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT * FROM VENDORS", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Vendors.Add(new Vendor
                        {
                            Id = (int)(decimal)reader["VENDOR_ID"],
                            Name = (string)reader["VENDOR_NAME"]
                        });
                    }
                }
            }
        }
        
        private static void ExportLocations(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT * FROM LOCATIONS", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Locations.Add(new Location
                        {
                            Id = (int)(decimal)reader["LOCATION_ID"],
                            Name = (string)reader["LOCATION_NAME"]
                        });
                    }
                }
            }
        }

        private static void ExportProducts(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT * FROM PRODUCTS", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Products.Add(new Product
                        {
                            Id = (int)(decimal)(reader["PRODUCT_ID"]),
                            Name = (string)reader["PRODUCT_NAME"],
                            VendorId = (int)(decimal)reader["VENDOR_ID"],
                            MeasureId = (int)(decimal)reader["MEASURE_ID"]
                        });
                    }
                }
            }
        }

        private static void ExportSales(OracleConnection connection, SupermarketsChainEntities db)
        {
            using (var command = new OracleCommand("SELECT * FROM SALES", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Sales.Add(new Sale
                        {
                            ProductId = (int)(decimal)(reader["PRODUCT_ID"]),
                            LocationId = (int)(decimal)(reader["LOCATION_ID"]),
                            Quantity = (decimal)(reader["QUANTITY"]),
                            DateOfSale = (DateTime)(reader["DATE_SALE"]),
                            PricePerUnit = (decimal)(double)(reader["PRICE_PER_UNIT"])
                        });
                    }
                }
            }
        }
    }
}
