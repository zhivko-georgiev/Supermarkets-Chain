using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketsChain.Helpers
{
    internal class JEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public decimal QuantitySold { get; set; }
        public decimal TotalIncomes { get; set; }
    }
}
