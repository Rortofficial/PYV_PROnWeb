using Client.Models.Gets;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IReport
    {
        Task<ResponseData<List<GetReportByUser>>> GetReportByUserAsync(string userID);
        Task<ResponseData<List<GetReportByID>>> GetReportByIDAsync(string reportID);
        Task<ResponseData<List<GetSubAttribute>>> GetSubAttributeAsync(string user, string query);
        Task<ResponseData<GetReportViewDetailByID>> GetReportViewDetailByIDAsync(string reportID);
        Task<PrintViewLayoutResponse> CallReport(string fileName, string type, string cmd, string dataset, string layoutName, IWebHostEnvironment _webHostEnvironment);
    }
}
