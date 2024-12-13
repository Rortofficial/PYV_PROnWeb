using Client.Models.Gets;

namespace Client.Models.Response
{
    public class GetDetailViewSalesQuotationResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public GetDetailViewSalesQuotation Data { get; set; } = null!;
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; } = null!;
    }
    public class GetDetailViewSalesQuotation
    {
        public string DocEntry { get; set; } = null!;
        public string CustomerType { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string ReftNo { get; set; } = null!;
        public string ATTN { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Validity { get; set; } = null!;
        public string ValidityCount { get; set; }
        public string Tel { get; set; } = null!;
        public string PaymentTerm { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string SalePerson { get; set; }
        public string DG { get; set; }
        public List<GetDetailViewSalesQuotationLine> Lines { get; set; } = null!;
    }
    public class GetDetailViewSalesQuotationLine
    {
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Remarks { get; set; } = null!;
    }
}
