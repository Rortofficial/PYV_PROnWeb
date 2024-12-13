namespace Client.Models.Request
{
    public class PostSalesQuotationRequest
    {
        public string CustomerType { get; set; } = null!;
        public string CustomerCode { get; set; } = null!;
        public string ReftNo { get; set; } = null!;
        public string ATTN { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Validity { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public int CreditTerm { get; set; }
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string SaleEmployee { get; set; } = null!;
        public string CommodityType { get; set; } = null!;
        public string BPCurrency { get; set; } = null!;
        public string AddressCode { get; set; } = null!;
        public string AddressDetail { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public string U_CUSTOMERTYPE { get; set; }
        public string DG { get; set; }
        public string CostCenter { get; set; }
        public List<SalesQuotationLines> Lines { get; set; } = null!;
        public ContactPerson ContactPersons { get; set; } = null!;
    }
    public class SalesQuotationLines
    {
        public string ItemCode { get; set; }
        public double Price { get; set; }
        public string Remarks { get; set; }
    }
    public class ContactPerson
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
