﻿namespace SupermarketsChain.ConsoleClient
{
    using System.Data.Entity;
    using Data;
    using Data.Migrations;
    using Helpers;
    using Helpers.DataExporters;
    using Helpers.DataImporters;

    public static class ConsoleClient
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketsChainEntities, Configuration>());

            MysqlDbManager.MSSQLToMySql();

            //OracleDbManager.PopulateDb();
            //OracleDbManager.ExportDbToSqlServer();
            //XlsImporter.ImportSales();
            //PdfExporter.ExportSales("01-Jan-2015", "25-Feb-2015");
            //XmlExporter.ExportSales("01-Jan-2015", "25-Feb-2015");
            //JsonExporter.ExportSalesToMongoDb("20-Jul-2014", "22-Jul-2014");
            //JsonExporter.ExportSalesToJson("20-Jul-2014", "22-Jul-2014");
            //XmlImporter.ImportExpenses();
            //SqLiteDbManager.PopulateDb();
        }
    }
}
