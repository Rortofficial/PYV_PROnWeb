using Client.Models.Response;

namespace Client.Models.Gets
{
    public class GetDetailJobSheetTrucking
    {
        public int BOOKINGID { get; set; }
        public int CONFIRMBOOKINGID { get; set; }
        public string JOBNO { get; set; }
        public string ROUTE { get; set; }
        public string BOOKINGDATE { get; set; }
        public string IMPORTTYPE { get; set; }
        public string SALEEMPLOYEE { get; set; }
        public List<string> SHIPPERNAME { get; set; }
        public string CO { get; set; }
        public string COCODE { get; set; }
        public string ShippingLine { get; set; }
        public List<string> CONSIGNEE { get; set; }
        public string CORSSBORDERDATE { get; set; }
        public string GOODSDESCRIPTION { get; set; }
        public string TOTALPACKAGE { get; set; }
        public double NETWEIGHT { get; set; }
        public double GROSSWEIGHT { get; set; }
        public string LOADINGDATE { get; set; }
        public string ETAREQUIREMENT { get; set; }
        public string PLACEOFLOADING { get; set; }
        public string DistrictOfLoading { get; set; }
        public string PLACEOFDELIVERY { get; set; }
        public string DistrictOfDelivery { get; set; }
        public string VOLUME { get; set; } = null!;
        public List<string> THAIFORWARDER { get; set; } = null!;
        public List<string> THAIBORDER { get; set; } = null!;
        public List<string> OVERSEAFORWARDER { get; set; } = null!;
        public List<string> OVERSEATRANSPORT { get; set; } = null!;
        public List<GetCurrencyByCustomer> CurrencyByCustomers { get; set; } = null!;
        public List<TruckInformationJobSheet> TRUCKINFORMATIONS { get; set; } = null!;
        public List<string> Invoices { get; set; } = null!;
        public List<SalesQuotationInvoiceList> SalesQuotationInvoiceLists { get; set; }
        public ResponseData<List<GetLayoutShowByType>> Layout { get; set; }
    }
    public class GetCurrencyByCustomer
    {
        public string CurrencyCode { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public decimal EXCHANGERATE { get; set; }
        public decimal ExchangeRateSystemCurrency { get; set; }
        public decimal ExchangeRateLocalCurrency { get; set; }
        public string Defualt { get; set; } = null!;
    }
    public class TruckInformationJobSheet
    {
        public string CONTAINERTRUCK { get; set; } = null!;
        public string ContainerWeight { get; set; }
        public string TRUCKNO { get; set; } = null!;
        public string TruckWeight { get; set; }
        public string TRANSPORTER { get; set; } = null!;
    }
    public class SalesQuotationInvoiceList
    {
        public string RefNo { get; set; }
        public string InvoiceNumber { get; set; }
        public string CardCode { get; set; }
    }
}
