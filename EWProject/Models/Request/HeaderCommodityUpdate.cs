namespace Client.Models.Request
{
    public class HeaderCommodityUpdate
    {
        public double GROSSWEIGHT { get; set; }
        public double NETWEIGHT { get; set; }
        public string TOTALPACKAGE { get; set; }
        public string GOODSDESCRIPTION { get; set; }
        public string LOADINGDATE { get; set; }
        public string ETAREQUIREMENT { get; set; }
        public string CROSSBORDERDATE { get; set; }
        public string LOLOYARDORUNLOADINGRemark { get; set; }
        public int LOLOYARDORUNLOADING { get; set; }
        public int LCLORFLC { get; set; }
        public int CYORCFS { get; set; }
    }
}
