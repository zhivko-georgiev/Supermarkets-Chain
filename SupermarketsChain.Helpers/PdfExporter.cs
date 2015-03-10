namespace SupermarketsChain.Helpers
{
    using System;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using SupermarketsChain.Data;
    
    public class PdfExporter
    {
        public static void Export(string startDate, string endDate)
        {
            using (var db = new SupermarketsChainEntities())
            {
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);

                Document doc = new Document();
                FileStream file = File.Create("../../../Aggregated-Sales-Report.pdf");
                PdfWriter.GetInstance(doc, file);
                doc.Open();

                PdfPTable table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                float[] widths = new float[] { 40f, 180f, 110f, 80f };
                table.SetWidths(widths);

                PdfPCell cell = new PdfPCell();

                BaseFont baseFont = BaseFont.CreateFont("../../../verdana.ttf", BaseFont.CP1252, false);
                Font normal = new Font(baseFont, 10);
                Font bold = new Font(baseFont, 12, Font.BOLD);
                Font extraBold = new Font(baseFont, 14, Font.BOLD);

                // Header
                cell = new PdfPCell(new Phrase("Aggregated Sales Report", extraBold));
                cell.Colspan = 4;
                cell.HorizontalAlignment = 1;
                cell.BackgroundColor = new BaseColor(255, 255, 255);
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Date: " + start.ToString("d-MMM-yyyy"), bold));
                cell.Colspan = 4;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.BackgroundColor = new BaseColor(242, 242, 242);
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                // Query to the DB
                var query = (
                  from prod in db.Products
                  join meas in db.Measures on prod.MeasureId equals meas.Id
                  join sale in db.Sales on prod.Id equals sale.ProductId
                  join loc in db.Locations on sale.LocationId equals loc.Id
                  where sale.DateOfSale >= start && sale.DateOfSale <= end
                  select new
                  {
                      productName = prod.Name,
                      quantity = sale.Quantity,
                      pricePerUnit = sale.PricePerUnit,
                      location = loc.Name,
                      total = sale.Quantity * sale.PricePerUnit
                  });

                doc.Add(table);
                doc.Close();
            }
        }
    }
}