using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IContainer
    {
        Task<PrintViewLayoutResponse> GetContainerStatusResponseAsync(IWebHostEnvironment _webHostEnvironment, string dateFrom, string dateTo);
        Task<ResponseData<List<GetContainerUpdateStatus>>> GetLoadExcelPathAsync(List<IFormFile> MyUploader);
        Task<PostResponse> InventoryTransfer(PostInventoryTransfer postInventoryTransfer);
        Task<PostResponse> DeleteInventoryTransfer(string docEntry);
        Task<ResponseData<List<GetListOfInventoryTransfer>>> GetListInventoryTransfer();
        Task<ResponseData<GetInventoryTransferDetail>> GetInventoryTransferDetailByDocEntry(string docEntry);
    }
}
