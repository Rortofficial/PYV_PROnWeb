namespace Client.Models.Gets
{
    public class GetVendor
    {
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string BankAccount { get; set; } = null!;
        public string Branch { get; set; } = null!;
        public string BankCountry { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string SwiftCode { get; set; } = null!;
        public int CreditTermDay { get; set; }
    }
}
