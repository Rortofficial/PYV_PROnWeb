using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class ListMasterPettyCash
    {
        public string GetMaxJournalVoucherNumber { get; set; }
        public List<GetSeriesJournalVoucher> GetSeriesJournalVouchers { get; set; }
        public List<GetBPCodeJournalVoucher> GetBPCodeJournalVouchers { get; set; }
        public List<GetAccountCodeJournalVoucher> GetAccountCodeJournalVouchers { get; set; }
        public List<GetVatGroupJournalVoucher> GetVatGroupJournalVouchers { get; set; }
        public ListPurchaseRequestConfirm ListAdvancePaymentConfirms { get; set; }
        public List<GetProjectManagmentList> getProjectManagmentList { get; set; }
        public List<GetDistributionRules> GetDistributionRuless { get; set; }
        public ResponseData<GetAccountByEmployeeBudget> GetEmployeeBudgets { get; set; }
        public ResponseData<PettyCashDetail> GetPettyCashViewDetail { get; set; }
    }
}
