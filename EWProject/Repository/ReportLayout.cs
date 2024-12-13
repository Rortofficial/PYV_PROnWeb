using Client.Connection;
using Client.Models.Response;
using iText.Kernel.Pdf;
using Microsoft.Reporting.NETCore;
using Newtonsoft.Json;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Client.Repository
{
    public class ReportLayout : IReportLayout
    {
        public ReportLayout()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public Task<PrintViewLayoutResponse> CallViewLayout(string Code, string docEntry, IWebHostEnvironment _webHostEnvironment)
        {
            try
            {
                var reportSetup = GetParamaterLayout(Code).Result;
                var path = $"{_webHostEnvironment.WebRootPath}\\Report\\{reportSetup.Rows[0]["FILENAME"]}";
                var type = getTypeExport(reportSetup.Rows[0]["EXPORTTYPE"].ToString()!);
                var storeName = reportSetup.Rows[0]["STOREPROCEDURE"].ToString();
                var reportBody = JsonConvert.DeserializeObject<List<ReportBodyResponse>>(reportSetup.Rows[0]["PROPERTIES"].ToString());
                //Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport lr = new();
                lr.LoadReportDefinition(File.OpenRead(path));
                foreach (var a in reportBody)
                {
                    DataTable dt = GetValueFromStore(a.TypeOfParameter.ToString()!, docEntry, storeName).Result;
                    lr.DataSources.Add(new ReportDataSource(a.DataSetName.ToString()!, dt));
                }
                lr.Refresh();
                var result = lr.Render(reportSetup.Rows[0]["EXPORTTYPE"].ToString()!);
                return Task.FromResult(new PrintViewLayoutResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = result,
                    ApplicationType = type.Item2,
                    FileName = (type.Item1 == "PDF" && reportSetup.Rows[0]["LAYOUTPRINTNAME"].ToString() == "") ? "" : $"{reportSetup.Rows[0]["LAYOUTPRINTNAME"].ToString() + docEntry + type.Item3}"
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
        private Tuple<string, string, string> getTypeExport(string type)
        {
            if (type == "PDF")
            {
                return Tuple.Create("PDF", "application/pdf", ".pdf");
            }
            else if (type == "WORD")
            {
                //return Tuple.Create("WORD", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx");
                return Tuple.Create("WORD", "application/msword", ".doc");
            }
            else if (type == "EXCEL")
            {
                return Tuple.Create("EXCEL", "application/xlsx", ".xls");
            }
            return Tuple.Create("PDF", "application/pdf", "");
        }
        private Task<DataTable> GetValueFromStore(string type, string docEntry, string storeName)
        {
            return Task.FromResult(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.Layout,
                             storeName,
                             type,
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable());
        }
        private Task<DataTable> GetParamaterLayout(string Code)
        {
            return Task.FromResult(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.Config,
                             Configure.LayoutPrintType!,
                             Code,
                             "",
                             "",
                             "",
                             "").GetResultDataTable());
        }
    }
}
