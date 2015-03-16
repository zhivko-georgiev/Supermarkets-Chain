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
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);
            ExportSales(start, end);
        }

        public static void ExportSales(DateTime startDate, DateTime endDate)
        {
            var encoding = Encoding.GetEncoding("utf-8");
            using (var writer = new XmlTextWriter(Settings.Default.XmlSalesByVendorLocation, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("sales");

                using (var db = new SupermarketsChainEntities())
                {
                    foreach (var vendor in db.Vendors.ToList())
                    {
                        writer.WriteStartElement("sale");
                        writer.WriteAttributeString("vendor", vendor.Name);

                        var totalSalesSumByDate = db.Sales
                            .Where(sale =>
                                sale.Product.VendorId == vendor.Id &&
                                sale.DateOfSale >= startDate &&
                                sale.DateOfSale <= endDate)
                            .OrderBy(sale => sale.DateOfSale)
                            .GroupBy(sale => sale.DateOfSale)
                            .Select(group => new
                            {
                                Date = group.Key,
                                TotalSum = group.Sum(g => g.PricePerUnit * g.Quantity)
                            });

                        foreach (var sale in totalSalesSumByDate)
                        {
                            WriteSaleToVendor(writer, sale.Date, sale.TotalSum);
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
