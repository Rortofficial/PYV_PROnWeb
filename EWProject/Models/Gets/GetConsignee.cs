namespace Client.Models.Gets
{
    public class GetConsignee
    {
        public string CardCode { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public double Balance { get; set; }
        public string TaxID { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
