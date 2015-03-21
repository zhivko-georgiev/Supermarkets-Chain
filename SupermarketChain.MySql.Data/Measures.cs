using MySql.Data.MySqlClient;
using SupermarketsChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketChain.MySql.Data
{
    public class Measures
    {
        MySqlConnection mySqlDb;

        public Measures()
        {
            mySqlDb = DbContext.Get();
        }

        public List<string> GetAll()
        {
            mySqlDb.Open();
            var command = mySqlDb.CreateCommand();
            var measures = new List<string>();

            command.CommandText = "SELECT name FROM measures;";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                measures.Add(reader["name"].ToString());
            }
            mySqlDb.Close();
            return measures;
        }

        public void SaveMeasures(List<Measure> msSqlVendors)
        {
            mySqlDb.Open();
            var values = string.Join(",", msSqlVendors.Select(x => "('" + x.Id + "', '" + x.Name + "')").ToList());
            var query = string.Format("INSERT INTO measure (id, name) VALUES {0};", values);
            var command = mySqlDb.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            mySqlDb.Close();
        }
    }
}
