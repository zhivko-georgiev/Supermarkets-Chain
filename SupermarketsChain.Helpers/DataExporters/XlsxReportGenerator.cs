namespace SupermarketsChain.Helpers.DataExporters
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using SupermarketsChain.Helpers.DbManagers;
    using SupermarketsChain.Models;

    public class XlsxReportGenerator
    {
        public static void GenerateFinancialResultReport()
        {
            using (var excelFile = new ExcelPackage())
            {
                excelFile.Workbook.Properties.Author = "Team Crux";
                excelFile.Workbook.Properties.Title = "Financial Results";
                excelFile.Workbook.Worksheets.Add("Financial Results");
                var workSheet = excelFile.Workbook.Worksheets[1];

                workSheet.Cells[1, 1].Value = "Vendor";
                workSheet.Cells[1, 2].Value = "Incomes";
                workSheet.Cells[1, 3].Value = "Expenses";
                workSheet.Cells[1, 4].Value = "Total Taxes";
                workSheet.Cells[1, 5].Value = "Financial Result";

                var headerStyle = workSheet.Cells[1, 1, 1, 5].Style;                
                headerStyle.WrapText = true;
                headerStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerStyle.VerticalAlignment = ExcelVerticalAlignment.Center;
                headerStyle.Font.Bold = true;

                AddFinancialResultsToWorkSheet(workSheet);

                workSheet.Column(1).Width = 34;
                workSheet.Column(2).Width = 13;
                workSheet.Column(3).Width = 13;
                workSheet.Column(4).Width = 15;
                workSheet.Column(5).Width = 19;
                
                File.WriteAllBytes(Settings.Default.ExcelReportLocation, excelFile.GetAsByteArray());
            }
        }

        private static void AddFinancialResultsToWorkSheet(ExcelWorksheet workSheet)
        {
            var financialResults = GetFinancialResults();
            var row = 2;
            foreach (var result in financialResults)
            {
                var vendorCell = workSheet.Cells[row, 1];
                vendorCell.Value = result.Vendor;

                var incomesCell = workSheet.Cells[row, 2];
                incomesCell.Value = result.Incomes;

                var expensesCell = workSheet.Cells[row, 3];
                expensesCell.Value = result.Expenses;

                var taxesCell = workSheet.Cells[row, 4];
                taxesCell.Value = result.Taxes;

                var financialResultCell = workSheet.Cells[row, 5];
                financialResultCell.Value = result.Total;
                financialResultCell.Style.Font.Bold = true;

                row++;
            }
        }

        private static IEnumerable<FinancialResult> GetFinancialResults()
        {
            var results = new List<FinancialResult>();

            var taxes = SqLiteDbManager.GetProductTaxes();
            var vendors = MySqlDbManager.GetVendorsWithExpenses();
            foreach (var vendor in vendors)
            {
                var products = MySqlDbManager.GetProductsByVendor(vendor.Key);
                results.Add(new FinancialResult
                {
                    Vendor = vendor.Key,
                    Expenses = vendor.Value,
                    Incomes = products.Sum(product => product.Value),
                    Taxes = products.Sum(product => product.Value * taxes[product.Key] / 100m)
                });
            }

            return results;
        }
    }
}
