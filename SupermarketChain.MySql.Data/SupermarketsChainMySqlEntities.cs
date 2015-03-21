using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketsChain.MySql.Data
{
    public class SupermarketsChainMySqlEntities
    {
        public SupermarketsChainMySqlEntities ()
	    {
            Vendors = new Vendors();
            Measures = new Measures();
            Products = new Products();
            ProductIncome = new ProductIncome();
            VendorExpenses = new VendorExpenses();

	    }
        public Vendors Vendors { get; set; }
        public Measures Measures { get; set; }
        public Products Products { get; set; }
        public ProductIncome ProductIncome { get; set; }
        public VendorExpenses VendorExpenses { get; set; }
    }
}
