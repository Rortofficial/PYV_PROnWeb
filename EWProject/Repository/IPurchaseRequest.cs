using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IPurchaseRequest
    {
        #region Get
        Task<GetRefNoPurchaseRequestResponse> GetRefNoPurchaseRequestResponsesAsync();
        Task<ResponseData<List<GetAccountCodePurchaseRequest>>> GetAccountCodePurchaseRequestResponsesAsync();
        Task<ResponseData<List<GetItemCodePurchaseRequest>>> GetItemCodePurchaseRequestResponsesAsync(string type, string department);
        Task<ResponseData<List<GetProjectManagmentList>>> GetProjectManagmentListResponseAsync();
        Task<ResponseData<List<GetPurchaseRequest>>> GetPurchaseRequestResponseAsync(string type, string dateFrom, string dateTo, string storetype, string userID);
        Task<ResponseData<List<GetVendor>>> GetVendorResponseAsync(string bpType);
        Task<ResponseData<List<GetVendorByJobNoCOGS>>> GetVendorByJobNoResponseAsync(string confirmBooking, string ActionType);
        Task<ResponseData<GetItemPriceList>> GetPriceListByItemCodeResponseAsync(string itemCode, string cardCode, string priceList);
        Task<ResponseData<List<GetCurrencyByCustomer>>> GetGetCurrencyByCustomerAsync(string CardCode);
        Task<ResponseData<List<GetTruckNoByJob>>> GetTruckNoByJobNoResponseAsync(string jobNo);
        Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync();
        Task<ResponseData<List<GetDistributionRules>>> GetDistributionRulesResponseAsync();
        Task<ResponseData<List<GetTaxCode>>> GetTaxCodes(string type = "");
        Task<ResponseData<List<GetPaymentTerm>>> GetPaymentTerms();
        Task<ResponseData<GetCheckingRefInvPR>> GetCheckingRefInvPr(string refInv);
        #endregion
        #region Post
        Task<PostResponse> PostPurchaseRequestAsync(Models.Request.PurchaseRequest postPurchaseRequestRequest, string typePurchaseRequest);
        #endregion
        #region Put
        Task<PostResponse> PutPurchaseRequestAsync(UpdatePurchaseRequest putPurchaseRequestRequest, string typePurchaseRequest);//For Update Status of PR that Approve to SAP
        Task<PostResponse> EditPurchaseRequestAsync(string docEntry, EditPurchaseRequest purchaseRequests, int projectNum, string remarks);// For User Edit or Update PR that are Pending

        #endregion
        #region Delete
        Task<PostResponse> CancelPurchaseRequestAsync(string docNum);
        #endregion
    }
}
