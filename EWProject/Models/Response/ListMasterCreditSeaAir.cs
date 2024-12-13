using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterCreditSeaAir
    {
        public List<GetSeriesJournalVoucher> GetSeriesJournalVouchers { get; set; }
        public List<GetItemCodePurchaseRequest> ListGetItemCodePurchaseRequest { get; set; }
        public List<GetProjectManagmentList> GetProjectManagmentLists { get; set; }
        public List<GetVendor> GetVendorSeaAndAirs { get; set; }
        public List<GetDistributionRules> GetDistributionRuless { get; set; }
        public List<GetTaxCode> GetTaxCodes { get; set; }
        public List<GetArCreditNoteRefInvSeaAndAir> GetArCreditNoteRefInvSeaAndAirs { get; set; }
    }
}
