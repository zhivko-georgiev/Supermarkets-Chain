namespace PopulateOracleDb
{
    using System;
    using System.IO;
    using Oracle.ManagedDataAccess.Client;

    public class PopulateOracleDb
    {
        public static void Main()
        {
            var queries = File.ReadAllText(@"..\..\..\PopulateOracleDb.sql").Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
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
    }
}
