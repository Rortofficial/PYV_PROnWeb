using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Response;
using Microsoft.Reporting.NETCore;
using System.Data;

namespace Client.Repository
{
    public class Report : IReport
    {
        public Task<PrintViewLayoutResponse> CallReport(string fileName, string type, string cmd, string dataset, string layoutName, IWebHostEnvironment _webHostEnvironment)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\Report\\{fileName}";
                LocalReport lr = new LocalReport();
                var typeExtension = getTypeExport(type.ToString()!);
                Stream reportDefinition = System.IO.File.OpenRead(path);
                lr.LoadReportDefinition(reportDefinition);
                DataTable dt = GetValueFromStore(cmd).Result;
                lr.DataSources.Add(new ReportDataSource(dataset, dt));
                lr.Refresh();
                var result = lr.Render(type);
                return Task.FromResult(new PrintViewLayoutResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = result,
                    ApplicationType = typeExtension.Item2,
                    FileName = (typeExtension.Item1 == "PDF" && layoutName == "") ? "" : $"{layoutName + typeExtension.Item3}"
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
        private Task<DataTable> GetValueFromStore(string cmd)
        {
            return Task.FromResult(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.Custom,
                             Type: cmd).GetResultDataTable());
        }
        private Tuple<string, string, string> getTypeExport(string type)
        {
            if (type == "PDF")
            {
                return Tuple.Create("PDF", "application/pdf", ".pdf");
            }
            else if (type == "WORD")
            {
                return Tuple.Create("WORD", "application/msword", ".doc");
            }
            else if (type == "EXCEL")
            {
                return Tuple.Create("EXCEL", "application/xlsx", ".xls");
            }
            return Tuple.Create("PDF", "application/pdf", ".pdf");
        }
        public Task<ResponseData<List<GetReportByUser>>> GetReportByUserAsync(string userID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetReportByUser>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetReportByUser>(new GetRecordByDataTable(
                             "GetReportByUser",
                             userID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetReportByUser>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetReportByID>>> GetReportByIDAsync(string reportID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetReportByID>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetReportByID>(new GetRecordByDataTable(
                             "GetReportByID",
                             reportID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetReportByID>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<GetReportViewDetailByID>> GetReportViewDetailByIDAsync(string reportID)
        {
            try
            {
                var dt = QueryData.ConvertDataTable<GetReportViewDetailByID>(new GetRecordByDataTable(
                             "GetReportViewDetailHeaderByID",
                             reportID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())[0];
                dt.Lines = QueryData.ConvertDataTable<ReportViewDetailLine>(new GetRecordByDataTable(
                             "GetReportViewDetailLineByID",
                             reportID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable());
                return Task.FromResult(new ResponseData<GetReportViewDetailByID>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = dt,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetReportViewDetailByID>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetSubAttribute>>> GetSubAttributeAsync(string user, string query)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetSubAttribute>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetSubAttribute>(new GetRecordByDataTable(
                        GetRecordByDataTable.StoreType.Custom,
                        Type: $"DO BEGIN DECLARE par1 NVARCHAR(255); par1:='{user}';{query} END;").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetSubAttribute>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
    }
}
