namespace SupermarketsChain.ConsoleClient
{
    using System;
    using SupermarketsChain.Data;
    using SupermarketsChain.Helpers;
    using SupermarketsChain.Models;

    public class ConsoleClient
    {
        public static void Main()
        {
            var db = new SupermarketsChainEntities();
            db.SaveChanges();
            ReportsFromZipIntoSQL.Generate();
            //OracleDbPopulator.Populate();
            //XmlExpenseDataLoader.AddExpensesToDb();
            //PdfExporter.Export("20-Jul-2014", "22-Jul-2014");
            //XmlSalesByVendorGenerator.Generate("20-Jul-2014", "22-Jul-2014");
        }
    }
}
