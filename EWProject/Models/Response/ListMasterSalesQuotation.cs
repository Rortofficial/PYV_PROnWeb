using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterSalesQuotation
    {
        public GetRefNo GetRefNos { get; set; } = null!;
        public List<GetCreditTerm> GetCreditTerms { get; set; } = null!;
        public List<GetServiceType> GetServiceTypes { get; set; } = null!;
        public List<GetOrigin> GetOrigins { get; set; } = null!;
        public List<GetDestination> GetDestinations { get; set; } = null!;
        public List<GetItemSaleQuotation> GetItemSaleQuotations { get; set; } = null!;
        public List<GetCO> GetCOs { get; set; } = null!;
        public List<GetSaleEmployee> GetSaleEmployees { get; set; } = null!;
        public List<GetCurrencyByCustomer> GetCurrencyByCustomers { get; set; } = null!;
    }
}
