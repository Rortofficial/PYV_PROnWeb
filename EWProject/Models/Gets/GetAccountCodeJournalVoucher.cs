namespace Client.Models.Gets
{
    public class GetAccountCodeJournalVoucher
    {
        public string AccountNumber { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public double AccountBalance { get; set; }
    }
}
