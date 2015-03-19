namespace SupermarketsChain.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using Data;
    using Data.Migrations;
    using Helpers;
    using Helpers.DataExporters;
    using Helpers.DataImporters;
    using Models;

    public static class ConsoleClient
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketsChainEntities, Configuration>());

            //OracleDbManager.PopulateDb();
            //OracleDbManager.ExportDbToSqlServer();
            //ReportsFromZipIntoSQL.ExportSales();
            //PdfExporter.ExportSales("20-Jul-2014", "22-Jul-2014");
            //XmlExporter.ExportSales("20-Jul-2014", "22-Jul-2014");
            //JsonExporter.ExportSalesToMongoDb("01-Feb-2015", "25-Feb-2015");
            //JsonExporter.ExportSalesToJson("01-Feb-1900", "25-Feb-2200");
            //XmlImporter.ImportExpenses();
            //SqLiteDbManager.PopulateDb();
        }
    }
}
