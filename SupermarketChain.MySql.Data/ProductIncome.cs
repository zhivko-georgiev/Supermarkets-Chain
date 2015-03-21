using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketsChain.MySql.Data
{
    public class ProductIncome
    {
        MySqlConnection mySqlDb;
        public ProductIncome()
        {
            mySqlDb = DbContext.Get();
        }

        public void UpdateIncomes(Dictionary<int, decimal> msSqlIncomes)
        {
            mySqlDb.Open();
            var formattedValues = msSqlIncomes.Select(x => FormatIncomeValues(x));
            var values = string.Join(",", formattedValues);
            var query = string.Format(
                @"INSERT INTO product_income (id, income)
                  VALUES {0} 
                  ON DUPLICATE KEY UPDATE income=VALUES(income);", values);
            var command = mySqlDb.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            mySqlDb.Close();
        }

        private string FormatIncomeValues(KeyValuePair<int, decimal> x)
        {
            var formattedValue = string.Format("('{0}', '{1}')", x.Key, x.Value);
            return formattedValue;
        }
    }
}
