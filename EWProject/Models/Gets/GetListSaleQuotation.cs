namespace Client.Models.Gets
{
    public class GetListSaleQuotation
    {
        public string DocEntry { get; set; } = null!;
        public string DocNum { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string CustomerCode { get; set; }
        public string QuotationDate { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string CustomerRefNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Route { get; set; } = null!;
        public decimal DocTotal { get; set; }
        public string DocStatus { get; set; } = null!;
        public string SlpCode { get; set; } = null!;
        public string SlpName { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public string ValidityCount { get; set; }
        public string StatusName { get; set; }
    }
}
