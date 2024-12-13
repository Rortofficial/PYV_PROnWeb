namespace Client.Models.Gets
{
    public class GetCO
    {
        public string CardCode { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public decimal Balance { get; set; }
        public string TaxID { get; set; } = null!;
    }
}
