using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.SalesQuotation
{
    public class SalesQuotations
    {
        public static Add _Add(PostSalesQuotationRequest _postSalesQuotationRequest, Company _company) => new Add(_postSalesQuotationRequest, _company);
        public static Cancel _Cancel(int DocEntry, Company _company) => new Cancel(DocEntry, _company);
    }
}
