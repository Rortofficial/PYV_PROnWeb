using Client.Models.Response;
using Microsoft.Reporting.NETCore;
using System.Data;

namespace Client.Lib.OtherFunction
{
    public class ExportDataFromList
    {
        public enum AppicationType
        {
            Excel,
            Word,
            PDF
        }
        public static Task<PrintViewLayoutResponse> ExportExcel<T>(List<T> objectList, IWebHostEnvironment _webHostEnvironment, string DataSetName, string ReportName, AppicationType ApplicationExportType, string FileName)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\Report\\{ReportName}";
                LocalReport lr = new LocalReport();
                Stream reportDefinition = System.IO.File.OpenRead(path);
                lr.LoadReportDefinition(reportDefinition);
                DataTable dt = new DataTable();
                dt = ConvertListToDataTable.ToDataTable(objectList);
                lr.DataSources.Add(new ReportDataSource(DataSetName, dt));
                lr.Refresh();
                var reportType = ChckingTypeApplication(ApplicationExportType);
                var result = lr.Render(reportType.Item1);
                return Task.FromResult(new PrintViewLayoutResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = result,
                    ApplicationType = reportType.Item2,
                    FileName = FileName + reportType.Item3
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PrintViewLayoutResponse
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorMessage = ex.Message
                });
            }
        }
        private static Tuple<string, string, string> ChckingTypeApplication(AppicationType type)
        {
            if (type == AppicationType.PDF)
            {
                return Tuple.Create("PDF", "application/pdf", ".pdf");
            }
            else if (type == AppicationType.Word)
            {
                return Tuple.Create("WORD", "application/msword", ".doc");
            }
            else if (type == AppicationType.Excel)
            {
                return Tuple.Create("EXCEL", "application/xlsx", ".xls");
            }
            return Tuple.Create("PDF", "application/pdf", "");
        }
    }
}
