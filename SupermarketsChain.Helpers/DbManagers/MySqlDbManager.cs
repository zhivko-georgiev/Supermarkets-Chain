namespace SupermarketsChain.Helpers.DbManagers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using MySql.Data.MySqlClient;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class MySqlDbManager
    {
        public static void CreateDbSchema()
        {
            var queries = File.ReadAllText(Settings.Default.MySqlScriptLocation);
            var connection = new MySqlConnection(Settings.Default.MySqlConnectionString);
            connection.Open();
            using (connection)
            {
                using (var command = new MySqlCommand(queries, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ImportDataFromSqlServer()
        {
            var mySqlConnection = new MySqlConnection(Settings.Default.MySqlConnectionString);
            mySqlConnection.Open();
            using (mySqlConnection)
            {
                using (var sqlServerDb = new SupermarketsChainEntities())
                {
                    ImportVendors(mySqlConnection, sqlServerDb);
                    ImportExpenses(mySqlConnection, sqlServerDb);
                    ImportProducts(mySqlConnection, sqlServerDb);
                }
            }
        }

        public static IDictionary<string, decimal> GetVendorsWithExpenses()
        {
            var results = new Dictionary<string, decimal>();
            var connection = new MySqlConnection(Settings.Default.MySqlConnectionString);
            connection.Open();
            using (connection)
            {
                const string query = "USE supermarketschain; select v.name, " +
                    "ifnull(sum(e.value), 0) as expenses " +
                    "FROM vendors v " +
                    "LEFT JOIN expenses e on e.vendor_id = v.id " +
                    "GROUP BY v.id";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add((string)reader["name"], (decimal)reader["expenses"]);
                        }
                    }
                }
            }

            return results;
        }

        public static IDictionary<string, decimal> GetProductsByVendor(string vendorName)
        {
            var results = new Dictionary<string, decimal>();
            var connection = new MySqlConnection(Settings.Default.MySqlConnectionString);
            connection.Open();
            using (connection)
            {
                var query = string.Format(
                    "USE supermarketschain; select p.name, p.total_income " +
                    "FROM products p JOIN vendors v on v.id = p.vendor_id " +
                    "WHERE v.name='{0}' GROUP BY p.name", 
                    vendorName);

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add((string)reader["name"], (decimal)reader["total_income"]);
                        }
                    }
                }
            }

            return results;
        } 

        private static void ImportVendors(MySqlConnection mySqlConnection, SupermarketsChainEntities db)
        {
            var vendorNames = db.Vendors.Select(v => "('" + v.Name + "')");
            var query = string.Format(
                "USE supermarketschain; INSERT IGNORE INTO vendors (name) VALUES {0};",
                string.Join(",", vendorNames));

            using (var command = new MySqlCommand(query, mySqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void ImportExpenses(MySqlConnection mySqlConnection, SupermarketsChainEntities db)
        {
            foreach (var expense in db.Expenses.Select(e => new { VendorName = e.Vendor.Name, e.Value }))
            {
                var query = string.Format(
                    "USE supermarketschain; INSERT INTO expenses (vendor_id,value) VALUES({0},{1});", 
                    GetVendorId(expense.VendorName, mySqlConnection), 
                    expense.Value);

                using (var command = new MySqlCommand(query, mySqlConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void ImportProducts(MySqlConnection mySqlConnection, SupermarketsChainEntities db)
        {
            var products = db.Products.Select(p => new
            {
                p.Name,
                VendorName = p.Vendor.Name,
                TotalIncome = db.Sales
                    .Where(s => s.ProductId == p.Id)
                    .Select(s => s.Quantity * s.PricePerUnit)
                    .DefaultIfEmpty(0)
                    .Sum()
            });

            foreach (var product in products)
            {
                var query = string.Format(
                    "USE supermarketschain; INSERT INTO products (name,vendor_id,total_income) VALUES('{0}',{1},{2});", 
                    product.Name,
                    GetVendorId(product.VendorName, mySqlConnection),
                    product.TotalIncome);

                using (var command = new MySqlCommand(query, mySqlConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static int GetVendorId(string vendorName, MySqlConnection mySqlConnection)
        {
            var query = string.Format("USE supermarketschain; SELECT id FROM vendors WHERE name='{0}'", vendorName);
            using (var command = new MySqlCommand(query, mySqlConnection))
            {
                return (int)command.ExecuteScalar();
            }
        }
    }
}
