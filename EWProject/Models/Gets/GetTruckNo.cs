namespace Client.Models.Gets
{
    public class GetTruckNo
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string TruckType { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string TrailerProvince { get; set; } = null!;
        public string TrailerNo { get; set; } = null!;
        public string TrailerType { get; set; } = null!;
        public string OWNERTRUCK { get; set; }
        public string DriverName1 { get; set; }
        public string DriverName2 { get; set; }
        public string Location { get; set; }
    }
}
