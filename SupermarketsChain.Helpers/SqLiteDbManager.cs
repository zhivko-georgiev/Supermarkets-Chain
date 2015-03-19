namespace SupermarketsChain.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;

    public static class SqLiteDbManager
    {
        public static void PopulateDb()
        {
            var queries = File.ReadAllText(Settings.Default.SqLiteSqlScriptLocation)
                .Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var connection = new SQLiteConnection(Settings.Default.SqLiteConnectionString);
            connection.Open();
            using (connection)
            {
                foreach (var query in queries)
                {
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static IDictionary<string, int> GetProductTaxes()
        {
            var productTaxes = new Dictionary<string, int>();

            var connection = new SQLiteConnection(Settings.Default.SqLiteConnectionString);
            connection.Open();
            using (connection)
            {
                using (var command = new SQLiteCommand("SELECT Name, TaxPercent FROM Products", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productTaxes.Add((string)reader["Name"], (int)(long)reader["TaxPercent"]);
                        }
                    }
                }
            }

            return productTaxes;
        } 
    }
}
