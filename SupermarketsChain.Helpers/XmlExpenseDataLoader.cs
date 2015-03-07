namespace SupermarketsChain.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class XmlExpenseDataLoader
    {
        public static void AddExpensesToDb()
        {
            var expenses = ReadExpensesFromXml();

            using (var db = new SupermarketsChainEntities())
            {
                foreach (var expense in expenses)
                {
                    var existingVendor = db.Vendors.FirstOrDefault(v => v.Name == expense.Vendor.Name);
                    if (existingVendor != null)
                    {
                        expense.Vendor = existingVendor;
                    }
                }

                db.Expenses.AddRange(expenses);
                db.SaveChanges();
            }
        }

        private static ICollection<Expense> ReadExpensesFromXml()
        {
            var expenses = new List<Expense>();

            using (var reader = XmlReader.Create(Settings.Default.XmlExpensesLocation))
            {
                string currentVendor = null;
                while (reader.Read())
                {
                    if (reader.Name == "vendor")
                    {
                        currentVendor = reader.GetAttribute("name");
                    }
                    else if (reader.Name == "expenses")
                    {
                        var currentMonth = DateTime.ParseExact(reader.GetAttribute("month"), "MMM-yyyy", CultureInfo.InvariantCulture);
                        var currentValue = reader.ReadElementContentAsDecimal();
                        var currentExpense = new Expense
                        {
                            Vendor = new Vendor { Name = currentVendor },
                            Month = currentMonth,
                            Value = currentValue
                        };

                        if (!string.IsNullOrWhiteSpace(currentVendor))
                        {
                            expenses.Add(currentExpense);
                        }
                    }
                }
            }

            return expenses;
        }
    }
}
