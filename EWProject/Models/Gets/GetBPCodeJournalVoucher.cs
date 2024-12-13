namespace Client.Models.Gets
{
    public class GetBPCodeJournalVoucher
    {
        public string CardCode { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public double Balance { get; set; }
    }
}
