using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class ListMasterPurchaseRequest
    {
        public string GetRefNoPurchaseRequest { get; set; } = null!;
        public string DocNum { get; set; }
        public List<GetAccountCodePurchaseRequest> ListGetAccountCodePurchaseRequest { get; set; } = null!;
        public List<GetItemCodePurchaseRequest> ListGetItemCodePurchaseRequest { get; set; } = null!;
        public List<GetProjectManagmentList> GetProjectManagmentLists { get; set; } = null!;
        public List<GetVendor> GetVendorLists { get; set; } = null!;
        public ListPurchaseRequestConfirm ListPurchaseRequestConfirms { get; set; } = null!;
        public List<GetSeriesJournalVoucher> GetSeriesJournalVouchers { get; set; } = null!;
        public List<GetDistributionRules> GetDistributionRuless { get; set; }
        public List<GetTaxCode> GetTaxCodes { get; set; }
        public List<GetPaymentTerm> GetPaymentTerms { get; set; }
        public List<GetTruckNoByJob> GetTruckNoAll { get; set; }
        public PurchaseRequest getDetailPurchaseRequest { get; set; }
    }
}
