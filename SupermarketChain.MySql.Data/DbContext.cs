using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketChain.MySql.Data
{
    internal class DbContext
    {
        private static MySqlConnection mySqlDb;
        public static void Init(){
            var mySqlDb = new MySqlConnection();
            var connetionString = "Data Source=localhost;Initial Catalog=supermarketschaindb;User ID=root;Password=";
            mySqlDb.ConnectionString = connetionString;
        }

        public static MySqlConnection Get()
        {
            return mySqlDb;
        }
    }
}
