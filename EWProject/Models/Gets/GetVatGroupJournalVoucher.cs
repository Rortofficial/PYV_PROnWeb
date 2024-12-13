namespace Client.Models.Gets
{
    public class GetVatGroupJournalVoucher
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Rate { get; set; }
    }
}
