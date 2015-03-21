using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketsChain.MySql.Data
{
    internal class MySQLContext
    {
        private static MySqlConnection mySqlDb;
        private static void Init(){
            mySqlDb = new MySqlConnection();
            var connetionString = "Data Source=localhost;Initial Catalog=supermarketschaindb;User ID=root;Password=";
            mySqlDb.ConnectionString = connetionString;
        }

        public static MySqlConnection GetConnection()
        {
            if (mySqlDb == null) { Init(); }
            return mySqlDb;
        }

        public static void PopulateDb()
        {
            var mySqlDb = MySQLContext.GetConnection();
            var query = File.ReadAllText(@"..\..\..\Helper.Files\Populate-Mysql.sql");

            mySqlDb.Open();
            var command = mySqlDb.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            mySqlDb.Close();
        }
    }
}
