namespace SupermarketsChain.ConsoleClient
{
    using System;
    using SupermarketsChain.Data;
    using SupermarketsChain.Helpers;
    using SupermarketsChain.Models;

    public static class ConsoleClient
    {
        public static void Main()
        {
            //OracleDbManager.Populate();
            //XmlImporter.ImportExpenses();
            //PdfExporter.ExportSales("20-Jul-2014", "22-Jul-2014");
            //XmlExporter.ExportSales("20-Jul-2014", "22-Jul-2014");
            //ReportsFromZipIntoSQL.ExportSales();

            var jReport = new JSONReport("20-Jul-2014", "21-Jul-2014");
            jReport.Configure("JSONReport", "192.168.1.106", 27017);
            jReport.Generate();
        }
    }
}
