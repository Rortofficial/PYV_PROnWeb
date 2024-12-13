using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface ISalesQuotation
    {
        Task<ResponseData<GetRefNo>> GetRefNoResponses(string UserID);
        Task<ResponseData<List<GetCreditTerm>>> GetCreditTermResponses();
        Task<ResponseData<List<GetServiceType>>> GetServiceTypeResponses();
        Task<PostResponse> PostSalesQuotations(PostSalesQuotationRequest postSalesQuotationRequest);
        Task<DeleteResponse> DeleteSalesQuotations(int DocEntry);
        Task<ResponseData<List<GetOrigin>>> GetOriginResponses();
        Task<ResponseData<List<GetDestination>>> GetDestinationResponses();
        Task<ResponseData<List<GetItemSaleQuotation>>> GetItemSalesQuotationResponses(string department);
        Task<ResponseData<List<GetListSaleQuotation>>> GetListSaleQuotationResponse(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<List<GetListSaleQuotation>>> GetListSaleQuotationBookingResponse();
        Task<ResponseData<List<GetCO>>> GetCOResponseAsync();
        Task<ResponseData<List<GetContactPerson>>> GetContactPersonByCardCode(string CardCode);
        Task<GetDetailViewSalesQuotationResponse> GetDetailViewSalesQuotationAsync(string docEntry);
        Task<ResponseData<List<GetAddressCodeByCustomer>>> GetCustomerAddress(string cardCode);
        Task<ResponseData<List<GetSaleEmployee>>> GetSaleEmployeeResponseAsync();
        Task<ResponseData<List<GetCurrencyByCustomer>>> GetBPCurrencyResponses(string cardCode);
        public Task<ResponseData<GetInformationBP>> GetInformationBPResponses(string cardCode);
        //Task<GetLayoutShowByTypeResponse> GetLayoutShowByTypeResponsenAsync();
    }
}
