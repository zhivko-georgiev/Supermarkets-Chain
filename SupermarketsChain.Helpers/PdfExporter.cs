namespace SupermarketsChain.Helpers
{
    using System;
    using System.IO;
    using System.Linq;
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

                PdfPTable table = new PdfPTable(5);
                table.TotalWidth = 550f;
                table.LockedWidth = true;
                float[] widths = new float[] { 150f, 70f, 70f, 230f, 70f };
                table.SetWidths(widths);

                PdfPCell cell = new PdfPCell();

                BaseFont baseFont = BaseFont.CreateFont("../../../verdana.ttf", BaseFont.CP1252, false);
                Font normal = new Font(baseFont, 10);
                Font bold = new Font(baseFont, 12, Font.BOLD);
                Font extraBold = new Font(baseFont, 14, Font.BOLD);

                cell = new PdfPCell(new Phrase("Aggregated Sales Report", extraBold));
                cell.Colspan = 5;
                cell.HorizontalAlignment = 1;
                cell.BackgroundColor = new BaseColor(255, 255, 255);
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                var query = (
                     from prod in db.Products
                     join meas in db.Measures on prod.MeasureId equals meas.Id
                     join sale in db.Sales on prod.Id equals sale.ProductId
                     join loc in db.Locations on sale.LocationId equals loc.Id
                     where sale.DateOfSale >= start && sale.DateOfSale <= end
                     select new
                     {
                         name = prod.Name,
                         quantity = sale.Quantity,
                         pricePerUnit = sale.PricePerUnit,
                         location = loc.Name,
                         dateOfSale = sale.DateOfSale,
                         total = sale.Quantity * sale.PricePerUnit
                     });

                var dates = query.Select(x => x.dateOfSale).Distinct().ToList();

                decimal totalSum = 0m;
                decimal grandTotal = 0m;

                foreach (var date in dates)
                {
                    cell = new PdfPCell(new Phrase("Date: " + date.ToString("d-MMM-yyyy"), bold));
                    cell.Colspan = 5;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BackgroundColor = new BaseColor(242, 242, 242);
                    cell.PaddingTop = 10f;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Product", bold));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BackgroundColor = new BaseColor(217, 217, 217);
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Quantity", bold));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BackgroundColor = new BaseColor(217, 217, 217);
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Unit Price", bold));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BackgroundColor = new BaseColor(217, 217, 217);
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Location", bold));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BackgroundColor = new BaseColor(217, 217, 217);
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Sum", bold));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BackgroundColor = new BaseColor(217, 217, 217);
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                    var products = query.Where(x => x.dateOfSale == date);

                    foreach (var item in products)
                    {
                        cell = new PdfPCell(new Phrase(item.name, normal));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BackgroundColor = new BaseColor(255, 255, 255);
                        cell.PaddingTop = 5f;
                        cell.PaddingBottom = 5f;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.quantity.ToString("F2"), normal));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BackgroundColor = new BaseColor(255, 255, 255);
                        cell.PaddingTop = 5f;
                        cell.PaddingBottom = 5f;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.pricePerUnit.ToString("F2"), normal));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BackgroundColor = new BaseColor(255, 255, 255);
                        cell.PaddingTop = 5f;
                        cell.PaddingBottom = 5f;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.location, normal));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BackgroundColor = new BaseColor(255, 255, 255);
                        cell.PaddingTop = 5f;
                        cell.PaddingBottom = 5f;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.total.ToString("F2"), normal));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.BackgroundColor = new BaseColor(255, 255, 255);
                        cell.PaddingTop = 5f;
                        cell.PaddingBottom = 5f;
                        table.AddCell(cell);
                        
                        totalSum += item.total;
                    }

                    cell = new PdfPCell(new Phrase("Total sum for " + date.ToString("d-MMM-yyyy"), bold));
                    cell.Colspan = 4;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.BackgroundColor = new BaseColor(255, 255, 255);
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(totalSum.ToString("F2"), bold));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.BackgroundColor = new BaseColor(255, 255, 255);
                    cell.PaddingTop = 5f;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                    grandTotal += totalSum;

                }

                cell = new PdfPCell(new Phrase("Grand Total:", extraBold));
                cell.Colspan = 4;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = new BaseColor(180, 190, 231);
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(grandTotal.ToString("F2"), extraBold));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = new BaseColor(180, 190, 231);
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                table.AddCell(cell);

                doc.Add(table);
                doc.Close();
            }
        }
    }
}