namespace Client.Models.Gets
{
    public class GetShipper
    {
        public string CardCode { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public double Balance { get; set; }
        public string Country { get; set; } = null!;
    }
}
