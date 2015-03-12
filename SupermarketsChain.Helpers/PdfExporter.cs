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
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            Export(start, end);
        }

        public static void Export(DateTime startDate, DateTime endDate)
        {
            using (var pdfDocument = new Document())
            {
                var file = File.Create(Settings.Default.SalesReportLocation);
                PdfWriter.GetInstance(pdfDocument, file);
                pdfDocument.Open();

                PdfPTable table = new PdfPTable(5)
                {
                    TotalWidth = 550f,
                    LockedWidth = true
                };

                table.SetWidths(new[] { 150f, 70f, 70f, 230f, 70f });

                var baseFont = BaseFont.CreateFont("../../../verdana.ttf", BaseFont.CP1252, false);
                var normalFont = new Font(baseFont, 10);
                var boldFont = new Font(baseFont, 12, Font.BOLD);
                var extraBoldFont = new Font(baseFont, 14, Font.BOLD);

                table.AddCell(new PdfPCell(new Phrase("Aggregated Sales Report", extraBoldFont))
                {
                    Colspan = 5,
                    HorizontalAlignment = 1,
                    BackgroundColor = new BaseColor(255, 255, 255),
                    PaddingTop = 10f,
                    PaddingBottom = 10f
                });

                decimal totalSum = 0m;
                decimal grandTotal = 0m;

                using (var db = new SupermarketsChainEntities())
                {
                    var dates = db.Sales
                        .Select(sale => sale.DateOfSale)
                        .Where(date => date >= startDate && date <= endDate)
                        .Distinct()
                        .ToList();

                    foreach (var date in dates)
                    {
                        table.AddCell(new PdfPCell(new Phrase("Date: " + date.ToString("d-MMM-yyyy"), boldFont))
                        {
                            Colspan = 5,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            BackgroundColor = new BaseColor(242, 242, 242),
                            PaddingTop = 10f,
                            PaddingBottom = 10f
                        });

                        table.AddCell(new PdfPCell(new Phrase("Product", boldFont))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            BackgroundColor = new BaseColor(217, 217, 217),
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        });

                        table.AddCell(new PdfPCell(new Phrase("Quantity", boldFont))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            BackgroundColor = new BaseColor(217, 217, 217),
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        });

                        table.AddCell(new PdfPCell(new Phrase("Unit Price", boldFont))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            BackgroundColor = new BaseColor(217, 217, 217),
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        });

                        table.AddCell(new PdfPCell(new Phrase("Location", boldFont))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            BackgroundColor = new BaseColor(217, 217, 217),
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        });

                        table.AddCell(new PdfPCell(new Phrase("Sum", boldFont))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            BackgroundColor = new BaseColor(217, 217, 217),
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        });

                        var sales = db.Sales
                            .Where(sale => sale.DateOfSale == date)
                            .Select(sale => new
                            {
                                sale.Product.Name,
                                sale.Quantity,
                                sale.PricePerUnit,
                                sale.DateOfSale,
                                Location = sale.Location.Name,
                                TotalValue = sale.Quantity * sale.PricePerUnit
                            });

                        foreach (var sale in sales)
                        {
                            table.AddCell(new PdfPCell(new Phrase(sale.Name, normalFont))
                            {
                                Colspan = 1,
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                BackgroundColor = new BaseColor(255, 255, 255),
                                PaddingTop = 5f,
                                PaddingBottom = 5f
                            });

                            table.AddCell(new PdfPCell(new Phrase(sale.Quantity.ToString("F2"), normalFont))
                            {
                                Colspan = 1,
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                BackgroundColor = new BaseColor(255, 255, 255),
                                PaddingTop = 5f,
                                PaddingBottom = 5f
                            });

                            table.AddCell(new PdfPCell(new Phrase(sale.PricePerUnit.ToString("F2"), normalFont))
                            {
                                Colspan = 1,
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                BackgroundColor = new BaseColor(255, 255, 255),
                                PaddingTop = 5f,
                                PaddingBottom = 5f
                            });

                            table.AddCell(new PdfPCell(new Phrase(sale.Location, normalFont))
                            {
                                Colspan = 1,
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                BackgroundColor = new BaseColor(255, 255, 255),
                                PaddingTop = 5f,
                                PaddingBottom = 5f
                            });

                            table.AddCell(new PdfPCell(new Phrase(sale.TotalValue.ToString("F2"), normalFont))
                            {
                                Colspan = 1,
                                HorizontalAlignment = Element.ALIGN_RIGHT,
                                BackgroundColor = new BaseColor(255, 255, 255),
                                PaddingTop = 5f,
                                PaddingBottom = 5f
                            });

                            totalSum += sale.TotalValue;
                        }

                        table.AddCell(new PdfPCell(new Phrase("Total sum for " + date.ToString("d-MMM-yyyy") + ":", boldFont))
                        {
                            Colspan = 4,
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            BackgroundColor = new BaseColor(255, 255, 255),
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        });

                        table.AddCell(new PdfPCell(new Phrase(totalSum.ToString("F2"), boldFont))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            BackgroundColor = new BaseColor(255, 255, 255),
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        });

                        grandTotal += totalSum;
                    }
                }

                table.AddCell(new PdfPCell(new Phrase("Grand Total:", extraBoldFont))
                {
                    Colspan = 4,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    BackgroundColor = new BaseColor(180, 190, 231),
                    PaddingTop = 5f,
                    PaddingBottom = 5f
                });

                table.AddCell(new PdfPCell(new Phrase(grandTotal.ToString("F2"), extraBoldFont))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    BackgroundColor = new BaseColor(180, 190, 231),
                    PaddingTop = 5f,
                    PaddingBottom = 5f
                });

                pdfDocument.Add(table);
                pdfDocument.Close();
            }
        }
    }
}