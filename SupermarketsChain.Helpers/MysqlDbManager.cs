using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SupermarketsChain.Data;

namespace SupermarketsChain.Helpers
{
    public class MysqlDbManager
    {

        private static MySqlConnection mySqlDb;
        private static SupermarketsChainEntities msSqlDb;
        public static void Initialize()
        {
            //init MySQL connection
            mySqlDb = new MySqlConnection();
            var connetionString = "Data Source=localhost;Initial Catalog=supermarketschaindb;User ID=root;Password=";
            mySqlDb.ConnectionString = connetionString;
            
            //init MSSQL connection
            msSqlDb = new SupermarketsChainEntities();
        }

        public static void MSSQLToMySql(){
            if(mySqlDb == null){ Initialize(); }
            
            msSqlDb.Vendors.ToList();

            try{
                mySqlDb.Open();
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }

            MergeVendors();

            mySqlDb.Close();
            Console.ReadLine();
        }

        private static void MergeVendors()
        {
            var mySqlVendors = GetAllVendors();
            var msSqlVendors = msSqlDb.Vendors.Where(x => !mySqlVendors.Contains(x.Name)).Select(x => x.Name).ToList();
            if (msSqlVendors.Count() > 0)
            {
                SaveVendors(msSqlVendors);
            }
        }

        private static void SaveVendors(List<string> msSqlVendors)
        {
            var values = string.Format("'{0}'",string.Join("', '", msSqlVendors));
            var query = string.Format("INSERT INTO vendor (name) VALUES ({0});", values);
            var command = mySqlDb.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private static List<string> GetAllVendors()
        {
            var command = mySqlDb.CreateCommand();
            var vendors = new List<string>();

            command.CommandText = "SELECT name FROM vendor;";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                vendors.Add(reader["name"].ToString());
            }
            return vendors;
        }
    }
}
