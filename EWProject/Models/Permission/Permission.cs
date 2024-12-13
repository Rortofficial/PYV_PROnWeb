namespace Client.Models.Permission
{
    public class Permission
    {
        public string Key { get; set; }
        public PropertiesPermission Properties { get; set; }
    }
    public class PropertiesPermission
    {
        public string Code { get; set; }
        public string IsPermission { get; set; }
        public string IsAllowReadOnly { get; set; }
        public string IsAllowAdd { get; set; }
        public string IsAllowUpdate { get; set; }
        public string IsAllowCancel { get; set; }
        public int CBTCount { get; set; }
        public int S_A_Count { get; set; }
        public int OtherCount { get; set; }
    }
}
