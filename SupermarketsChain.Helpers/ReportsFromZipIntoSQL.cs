namespace SupermarketsChain.Helpers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using SupermarketsChain.Data;
    using System.IO;
    using Ionic.Zip;

    public class ReportsFromZipIntoSQL
    {
        public static void Generate()
        {
            using (ZipFile zip = ZipFile.Read(@"C:\SoftUni\Database\Teamwork\trunk\Sales-Reports.zip"))
            {
                ZipEntry e = zip["Bourgas-Plaza-Sales-Report-20-Jul-2014.xls"];
                Console.WriteLine("Successfully open file.");
            }
        }
    }
}
