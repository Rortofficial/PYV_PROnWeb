using Client.Models.Response;

namespace Client.Repository
{
    public interface IReportLayout
    {
        Task<PrintViewLayoutResponse> CallViewLayout(string Code, string docEntry, IWebHostEnvironment _webHostEnvironment);
    }
}
