namespace SupermarketsChain.Helpers
{
    using System;
    using System.IO;
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
        }
    }
}
