namespace Client.Models.Gets
{
    public class GetCostType
    {
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public double Price { get; set; }
    }
}
