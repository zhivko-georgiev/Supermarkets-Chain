using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SupermarketsChain.Data;
using SupermarketsChain.Models;

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
            
            MergeVendors();

            Console.ReadLine();
        }

        private static void MergeVendors()
        {
            var mySqlVendors = GetAllVendors();
            var msSqlVendors = msSqlDb.Vendors.Where(x => !mySqlVendors.Contains(x.Name)).ToList();
            if (msSqlVendors.Count() > 0)
            {
                SaveVendors(msSqlVendors);
            }
        }

        private static void SaveVendors(List<Vendor> msSqlVendors)
        {
            mySqlDb.Open();
            var values = string.Join(",", msSqlVendors.Select(x=>"('" + x.Id + "', '" + x.Name + "')").ToList());
            var query = string.Format("INSERT INTO vendor (id, name) VALUES {0};", values);
            var command = mySqlDb.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            mySqlDb.Close();
        }

        private static List<string> GetAllVendors()
        {
            mySqlDb.Open();
            var command = mySqlDb.CreateCommand();
            var vendors = new List<string>();

            command.CommandText = "SELECT name FROM vendor;";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                vendors.Add(reader["name"].ToString());
            }
            mySqlDb.Close();
            return vendors;
        }
    }
}
