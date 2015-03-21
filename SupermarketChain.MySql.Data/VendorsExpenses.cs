using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketsChain.Models;

namespace SupermarketsChain.MySql.Data
{
    public class VendorExpenses
    {
        MySqlConnection mySqlDb;

        public VendorExpenses()
        {
            mySqlDb = DbContext.Get();
        }

        public void UpdateExpenses(Dictionary<int, decimal> msSqlExpenses)
        {
            mySqlDb.Open();
            var formattedValues = msSqlExpenses.Select(x => FormatIncomeValues(x));
            var values = string.Join(",", formattedValues);
            var query = string.Format(
                @"INSERT INTO vendor_expenses (vendor_id, value)
                  VALUES {0} 
                  ON DUPLICATE KEY UPDATE value=VALUES(value);", values);
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
