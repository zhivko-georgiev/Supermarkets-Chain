namespace SupermarketsChain.Helpers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using SupermarketsChain.Data;
    using System.IO;
    using Ionic.Zip;
    using Excel = Microsoft.Office.Interop.Excel;
    using System.Collections.Generic;
    using System.Data.OleDb;

    public class ReportsFromZipIntoSQL
    {
        public static void Generate()
        {
            string FILEPATH = @"C:\SoftUni\Database\Teamwork\trunk\Sales-Reports\test.xls";
            string CONNECTION_STRING = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FILEPATH + ";Extended Properties=Excel 12.0;Persist Security Info=False";  

            try
            {
                using (ZipFile zip = ZipFile.Read(@"C:\SoftUni\Database\Teamwork\trunk\Sales-Reports.zip"))
                {
                    ZipEntry xml = zip["Bourgas-Plaza-Sales-Report-20-Jul-2014.xls"];
                    using (OleDbConnection connection = new OleDbConnection(CONNECTION_STRING))
                    {
                        OleDbCommand command = new OleDbCommand("select * from [sales$]", connection);
                        connection.Open();
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            for (int i = 0; i <= 3; i++)
                            {
                                Console.WriteLine(reader.GetValue(i).ToString());
                            }
                        }
                        reader.Close();
                    } 
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Directory not found: ");
            }    
        }
    }
}
