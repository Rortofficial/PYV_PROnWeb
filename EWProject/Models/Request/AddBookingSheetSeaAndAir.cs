namespace Client.Models.Request
{
    public class AddBookingSheetSeaAndAir
    {
        public Header HeaderObj { get; set; }
        public List<COLoaderSeaAndAir> ListCOLoader { get; set; }
        public List<ListCustomerSeaAndAir> ListCustomer { get; set; }
        public List<ShipperSeaAndAir> ListShipper { get; set; }
        public List<ConsigneeSeaAndAir> ListConsignee { get; set; }
        public List<ShippingLineSeaAndAir> ListShippingLine { get; set; }
        public List<DestAgentSeaAndAir> ListDestAgent { get; set; }
        public List<CommoditySeaAndAir> ListCommodities { get; set; }
        public List<OverseaTransportSeaAndAir> ListOverseaTransportSeaAndAir { get; set; }
        public List<PortOfReceiptSeaAndAir> ListPortOfReceiptSeaAndAir { get; set; }
        public List<PortOfDischargeSeaAndAir> ListPortOfDischargeSeaAndAir { get; set; }
        public List<PlaceOfLoadingSeaAndAir> ListPlaceOfLoadingSeaAndAir { get; set; }
        public List<PlaceOfDeliverySeaAndAir> ListPlaceOfDeliverySeaAndAir { get; set; }
        public List<ThaiForwarderSeaAndAir> ListThaiForwarderSeaAndAir { get; set; }
        public List<ContainerTypeSeaAndAir> ListContainerTypeSeaAndAir { get; set; }
        public List<OverSeaForwarderSeaAndAir> ListOverSeaForwarderSeaAndAir { get; set; }
        public List<ThaiBorderSeaAndAir> ListThaiBorderSeaAndAir { get; set; }
        public List<TruckTypeSeaAndAir> ListTruckTypeSeaAndAir { get; set; }
        public List<CYAt_ContactSeaAndAir> ListCYAt_ContactSeaAndAir { get; set; }
        public List<ReturnAt_ContactSeaAndAir> ListReturnAt_ContactSeaAndAir { get; set; }
        public List<LoadingAtSeaAndAir> ListLoadingAtSeaAndAir { get; set; }
        #region Air
        public List<FlightsInformation> FlightsInformations { get; set; }
        #endregion
    }
    public class Header
    {
        public string JOBSERIES { get; set; }
        public DateTime BOOKINGDATE { get; set; }
        public int SALEEMPLOYEE { get; set; }
        public string ORIGIN { get; set; }
        public string DESTINATION { get; set; }
        public string IMPORTTYPE { get; set; }
        public string SERVICETYPE { get; set; }
        public string BOOKINGNO { get; set; }
        public string FREIGHT { get; set; }
        public string FEEDER { get; set; }
        public string FEEDERVOY { get; set; }
        public string VESSEL { get; set; }
        public string VESSELVOY { get; set; }
        public string GOODSDESCRIPTION { get; set; }
        public string TOTALPACKAGE { get; set; }
        public double NETWEIGHT { get; set; }
        public double GROSSWEIGHT { get; set; }
        public DateTime LOADINGDATE { get; set; }
        public DateTime CROSSBORDERDATE { get; set; }
        public DateTime ETAREQUIREMENT { get; set; }
        public string REMARKLOLOYARD { get; set; }
        public string LOLOYARD_UNLOADING { get; set; }
        public double CBM { get; set; }
        public DateTime CYDATE { get; set; }
        public string LCL_FCL_CY_CFS { get; set; }
        public DateTime RETURNDATE { get; set; }
        public DateTime LASTRETURNDATE { get; set; }
        public DateTime RETURNBEFORE { get; set; }
        public string DOCUMENTREQUIREMENT { get; set; }
        public string SPECIALREQUEST { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime ETDREQUIREMENT { get; set; }
        public DateTime CLOSINGDATE { get; set; }
        public DateTime CLOSINGTIME { get; set; }
        public string DG { get; set; }
        #region Air
        public string MAWB { get; set; }
        public DateTime LoadingDateAir { get; set; }
        public DateTime CuffOfDateAir { get; set; }
        public DateTime CuffOfTimeAir { get; set; }
        public string LoadingPlaceAir { get; set; }
        public string Warehouse { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        #endregion
    }
    public class COLoaderSeaAndAir
    {
        public int LineId { get; set; }
        public string COLOADER { get; set; }
    }
    public class ListCustomerSeaAndAir
    {
        public int LineId { get; set; }
        public string CUSTOMERCODE { get; set; }
    }
    public class ShipperSeaAndAir
    {
        public int LineId { get; set; }
        public string SHIPPER { get; set; }
    }
    public class ConsigneeSeaAndAir
    {
        public int LineId { get; set; }
        public string CONSIGNEE { get; set; }
    }
    public class ShippingLineSeaAndAir
    {
        public int LineId { get; set; }
        public string SHIPPINGLINE { get; set; }
    }
    public class DestAgentSeaAndAir
    {
        public int LineId { get; set; }
        public string DESTAGENT { get; set; }
    }
    public class CommoditySeaAndAir
    {
        public int LineId { get; set; }
        public string Invoice { get; set; }
    }
    public class OverseaTransportSeaAndAir
    {
        public int LineId { get; set; }
        public string OVEARSEATRANSPORT { get; set; }
    }
    public class PortOfReceiptSeaAndAir
    {
        public int LineId { get; set; }
        public string PORTOFRECEIPT { get; set; }
    }
    public class PortOfDischargeSeaAndAir
    {
        public int LineId { get; set; }
        public string PORTOFDISCHARGE { get; set; }
    }
    public class PlaceOfLoadingSeaAndAir
    {
        public int LineId { get; set; }
        public string PLACEOFLOADING { get; set; }
    }
    public class PlaceOfDeliverySeaAndAir
    {
        public int LineId { get; set; }
        public string PLACEOFDELIVERY { get; set; }
    }
    public class ThaiForwarderSeaAndAir
    {
        public int LineId { get; set; }
        public string THAIFORWARDER { get; set; }
    }
    public class ContainerTypeSeaAndAir
    {
        public int LineId { get; set; }
        public int Qty { get; set; }
        public string ContainerType { get; set; }
        public double Weight { get; set; }
        public string ListInvoice { get; set; }
    }
    public class OverSeaForwarderSeaAndAir
    {
        public int LineId { get; set; }
        public string OVERSEAFORWARDER { get; set; }
    }
    public class ThaiBorderSeaAndAir
    {
        public int LineId { get; set; }
        public string THAIBORDER { get; set; }
    }
    public class TruckTypeSeaAndAir
    {
        public int LineId { get; set; }
        public int Qty { get; set; }
        public string ContainerType { get; set; }
        public double Weight { get; set; }
        public string ListInvoice { get; set; }
    }
    public class CYAt_ContactSeaAndAir
    {
        public int LineId { get; set; }
        public string CYAt_Contact { get; set; }
    }
    public class ReturnAt_ContactSeaAndAir
    {
        public int LineId { get; set; }
        public string ReturnAt_Contact { get; set; }
    }
    public class LoadingAtSeaAndAir
    {
        public int LineId { get; set; }
        public string LoadingAt { get; set; }
    }
    public class FlightsInformation
    {
        public int LineId { get; set; }
        public string FlightFromTo { get; set; }
        public string FlightNo { get; set; }
        public string FlightArrival { get; set; }
    }
}
