using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketsChain.Models;

namespace SupermarketsChain.MySql.Data
{
    public class Vendors
    {
        MySqlConnection mySqlDb;

        public Vendors()
        {
            mySqlDb = DbContext.Get();
        }

        public List<string> GetAll()
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

        public void SaveVendors(List<Vendor> msSqlVendors)
        {
            mySqlDb.Open();
            var values = string.Join(",", msSqlVendors.Select(x => "('" + x.Id + "', '" + x.Name + "')").ToList());
            var query = string.Format("INSERT INTO vendor (id, name) VALUES {0};", values);
            var command = mySqlDb.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            mySqlDb.Close();
        }
    }
}
