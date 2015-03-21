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
	    }
        public Vendors Vendors { get; set; }
        public Measures Measures { get; set; }
    }
}
