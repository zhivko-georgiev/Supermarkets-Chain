using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SupermarketsChain.Data;
using SupermarketsChain.Models;
using SupermarketsChain.MySql.Data;

namespace SupermarketsChain.Helpers
{
    public class MysqlDbManager
    {

        private static SupermarketsChainMySqlEntities mySqlDb;
        private static SupermarketsChainEntities msSqlDb;
        public static void Initialize()
        {
            mySqlDb = new SupermarketsChainMySqlEntities();
            msSqlDb = new SupermarketsChainEntities();
        }

        public static void MSSQLToMySql(){
            if(mySqlDb == null){ Initialize(); }

            MergeVendors();
            MergeMeasures();
            MergeProducts();
            MergeProductIncomes();
            MergeProductExpenses();

            Console.ReadLine();
        }

        private static void MergeProductExpenses()
        {
            var msSqlExpenses = msSqlDb.Expenses.GroupBy(x => x.VendorId)
                .ToDictionary(x => x.Key, x => x.Sum(r => r.Value));
            mySqlDb.VendorExpenses.UpdateExpenses(msSqlExpenses);
        }

        private static void MergeProductIncomes()
        {
            var msSqlIncomes = msSqlDb.Sales.GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(r => r.PricePerUnit * r.Quantity));
            mySqlDb.ProductIncome.UpdateIncomes(msSqlIncomes);
        }

        private static void MergeProducts()
        {
            var mySqlProducts = mySqlDb.Products.GetAll();
            var msSqlProducts = msSqlDb.Products.Where(x => !mySqlProducts.Contains(x.Name)).ToList();
            if (msSqlProducts.Count() > 0)
            {
                mySqlDb.Products.SaveProducts(msSqlProducts);
            }
        }

        private static void MergeMeasures()
        {
            var mySqlMeasures = mySqlDb.Measures.GetAll();
            var msSqlMeasures = msSqlDb.Measures.Where(x => !mySqlMeasures.Contains(x.Name)).ToList();
            if (msSqlMeasures.Count() > 0)
            {
                mySqlDb.Measures.SaveMeasures(msSqlMeasures);
            }
        }

        private static void MergeVendors()
        {
            var mySqlVendors = mySqlDb.Vendors.GetAll();
            var msSqlVendors = msSqlDb.Vendors.Where(x => !mySqlVendors.Contains(x.Name)).ToList();
            if (msSqlVendors.Count() > 0)
            {
                mySqlDb.Vendors.SaveVendors(msSqlVendors);
            }
        }
    }
}
