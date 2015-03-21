using MySql.Data.MySqlClient;
using SupermarketsChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExtensions;

namespace SupermarketsChain.MySql.Data
{
    public class Products
    {
        MySqlConnection mySqlDb;

        public Products()
        {
            mySqlDb = MySQLContext.GetConnection();
        }
        public List<string> GetAll()
        {
            mySqlDb.Open();
            var command = mySqlDb.CreateCommand();
            var products = new List<string>();

            command.CommandText = "SELECT name FROM product;";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(reader["name"].ToString());
            }
            mySqlDb.Close();
            return products;
        }

        public void SaveProducts(List<Product> msSqlProducts)
        {
            mySqlDb.Open();
            var formattedValues = msSqlProducts.Select(x => FormatProductValues(x)).ToList();
            var values = string.Join(",", formattedValues);
            var query = string.Format(
                @"INSERT INTO product (id, name, measure_id, vendor_id)
                  VALUES {0} 
                  ON DUPLICATE KEY UPDATE name=VALUES(name);", values);
            var command = mySqlDb.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            mySqlDb.Close();
        }

        private static string FormatProductValues(Product x)
        {
            var formattedValues = string.Format("('{0}', '{1}', '{2}', '{3}')", x.Id, x.Name.MysqlEscape(), x.MeasureId, x.Vendor.Id);
            return formattedValues;
        }
    }

    
}
