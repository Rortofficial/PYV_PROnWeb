namespace Client.Models.Request
{
    public class PostPettyCashRequest
    {
        public PettyCashHeader Header { get; set; }
        public List<PettyCashLine> Lines { get; set; }
    }
    public class PettyCashDetail
    {
        public PettyCashHeaderDetail Header { get; set; }
        public List<PettyCashLine> Lines { get; set; }
    }
    public class PettyCashHeaderDetail
    {
        public string Series { get; set; } = null!;
        public string PostingDate { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string Ref1 { get; set; } = null!;
        public string Ref2 { get; set; } = null!;
        public string Ref3 { get; set; } = null!;
        public string UserID { get; set; } = null!;
        public string UpdateBy { get; set; } = null!;
        public string WebID { get; set; }
        public string EmployeeID { get; set; }
        public string DocEntry { get; set; }
    }
    public class PettyCashHeader
    {
        public string Series { get; set; } = null!;
        public string PostingDate { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string Ref1 { get; set; } = null!;
        public string Ref2 { get; set; } = null!;
        public string Ref3 { get; set; } = null!;
        public string UserID { get; set; } = null!;
        public string UpdateBy { get; set; } = null!;
        public string WebID { get; set; }
        public string EmployeeID { get; set; }
        //public string DocEntry { get; set; }
    }
    public class PettyCashLine
    {
        public int LineId { get; set; }
        public string AccountTypeCode { get; set; }
        public string AccountCodeOrBpCode { get; set; }
        public string CardCode { get; set; }
        public string DateLine { get; set; }
        public string Remarks { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
