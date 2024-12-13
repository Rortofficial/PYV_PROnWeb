namespace Client.Models.Gets
{
    public class GetSeriesJournalVoucher
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public int NextNumber { get; set; }
    }
}
