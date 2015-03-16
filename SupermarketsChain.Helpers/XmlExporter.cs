namespace SupermarketsChain.Helpers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using SupermarketsChain.Data;

    public static class XmlExporter
    {
        public static void ExportSales(string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            ExportSales(start, end);
        }

        public static void ExportSales(DateTime startDate, DateTime endDate)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            using (XmlTextWriter writer = new XmlTextWriter(Settings.Default.XmlSalesByVendorLocation, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("sales");

                using (var db = new SupermarketsChainEntities())
                {
                    var vendors = db.Vendors.ToArray();
                    var dates = db.Sales
                        .Select(x => x.DateOfSale)
                        .Where(x => x >= startDate && x <= endDate)
                        .Distinct()
                        .ToList();

                    decimal total = 0m;

                    foreach (var vendor in vendors)
                    {
                        writer.WriteStartElement("sale");
                        writer.WriteAttributeString("vendor", vendor.Name);

                        foreach (var date in dates)
                        {
                            var productSales = db.Sales
                                .Select(x =>
                                    new
                                    {
                                        x.DateOfSale,
                                        VendorId = x.Product.Vendor.Id,
                                        TotalValue = x.Quantity * x.PricePerUnit
                                    })
                                .ToList();

                            foreach (var productSale in productSales)
                            {
                                if (vendor.Id == productSale.VendorId && productSale.DateOfSale == date)
                                {
                                    total += productSale.TotalValue;
                                }
                            }

                            WriteSaleToVendor(writer, date, total);
                            total = 0m;
                        }

                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndDocument();
            }
        }

        private static void WriteSaleToVendor(XmlWriter writer, DateTime date, decimal totalSum)
        {
            writer.WriteStartElement("summary");
            writer.WriteAttributeString("date", date.ToString("d-MMM-yyyy"));
            writer.WriteAttributeString("total-sum", totalSum.ToString("F2"));
            writer.WriteEndElement();
        }
    }
}
