namespace Client.Models.Gets
{
    public class GetJobNumberSeaAndAir
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string JobTypeName { get; set; }
        public List<GetServiceTypeSeaAndAir> ServiceTypeSeaAndAirByJobNumber { get; set; }
    }
    public class GetServiceTypeSeaAndAir
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
