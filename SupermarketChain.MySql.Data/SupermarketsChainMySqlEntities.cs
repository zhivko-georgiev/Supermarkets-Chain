﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketChain.MySql.Data
{
    public class SupermarketsChainMySqlEntities
    {
        MySqlConnection mySqlDb;
        public SupermarketsChainMySqlEntities ()
	    {
            DbContext.Init();
            Vendors = new Vendors();
	    }
        public Vendors Vendors { get; set; }
    }
}