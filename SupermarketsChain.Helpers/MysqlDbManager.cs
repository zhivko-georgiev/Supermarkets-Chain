using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SupermarketsChain.Data;
using SupermarketsChain.Models;
using SupermarketChain.MySql.Data;

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

            Console.ReadLine();
        }

        private static void MergeMeasures()
        {
            var mySqlMeasures = mySqlDb.Measures.GetAll();
            var msSqlMeasures = msSqlDb.Measures.Where(x => !mySqlVendors.Contains(x.Name)).ToList();
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
