namespace Client.Models.Gets
{
    public class GetBookingSheetSeaAndAir
    {
        public GetBookingSheetSeaAndAirHeader HeaderObj { get; set; }
        public List<GetCOLoaderSeaAndAirViewDetail> ListCOLoader { get; set; }
        public List<GetListCustomerSeaAndAirViewDetail> ListCustomer { get; set; }
        public List<GetShipperSeaAndAirViewDetail> ListShipper { get; set; }
        public List<GetConsigneeSeaAndAirViewDetail> ListConsignee { get; set; }
        public List<GetShippingLineSeaAndAirViewDetail> ListShippingLine { get; set; }
        public List<GetDestAgentSeaAndAirViewDetail> ListDestAgent { get; set; }
        public List<GetCommoditySeaAndAirViewDetail> ListCommodities { get; set; }
        public List<GetOverseaTransportSeaAndAirViewDetail> ListOverseaTransportSeaAndAir { get; set; }
        public List<GetPortOfReceiptSeaAndAirViewDetail> ListPortOfReceiptSeaAndAir { get; set; }
        public List<GetPortOfDischargeSeaAndAirViewDetail> ListPortOfDischargeSeaAndAir { get; set; }
        public List<GetPlaceOfLoadingSeaAndAirViewDetail> ListPlaceOfLoadingSeaAndAir { get; set; }
        public List<GetPlaceOfDeliverySeaAndAirViewDetail> ListPlaceOfDeliverySeaAndAir { get; set; }
        public List<GetThaiForwarderSeaAndAirViewDetail> ListThaiForwarderSeaAndAir { get; set; }
        public List<GetContainerTypeSeaAndAirViewDetail> ListContainerTypeSeaAndAir { get; set; }
        public List<GetOverSeaForwarderSeaAndAirViewDetail> ListOverSeaForwarderSeaAndAir { get; set; }
        public List<GetThaiBorderSeaAndAirViewDetail> ListThaiBorderSeaAndAir { get; set; }
        public List<GetTruckTypeSeaAndAirViewDetail> ListTruckTypeSeaAndAir { get; set; }
        public List<GetCYAt_ContactSeaAndAirViewDetail> ListCYAt_ContactSeaAndAir { get; set; }
        public List<GetReturnAt_ContactSeaAndAirViewDetail> ListReturnAt_ContactSeaAndAir { get; set; }
        public List<GetLoadingAtSeaAndAirViewDetail> ListLoadingAtSeaAndAir { get; set; }
        #region Air
        public List<GetFlightsInformationViewDetail> FlightsInformations { get; set; }
        #endregion
    }
    public class GetBookingSheetSeaAndAirHeader
    {
        public string BookingID { get; set; }
        public string JobNo { get; set; }
        public string BookingDate { get; set; }
        public string Route { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string SalePerson { get; set; }
        public string SalePersonCode { get; set; }
        public string JobType { get; set; }
        public string ServiceType { get; set; }
        public string ServiceTypeCode { get; set; }
        public string BookingNo { get; set; }
        public string Freight { get; set; }
        public string Feeder { get; set; }
        public string FeederVoy { get; set; }
        public string Vessel { get; set; }
        public string VesselVoy { get; set; }
        public string GoodsDescription { get; set; }
        public string TotalPackage { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string LoadingDate { get; set; }
        public string CrossBorderDate { get; set; }
        public string ETARequirement { get; set; }
        public string RemarkLOLOYard { get; set; }
        public string LOLOYARD_UNLOADING { get; set; }
        public string CBM { get; set; }
        public string CYDate { get; set; }
        public string LCL_FCL_CY_CFS { get; set; }
        public string ReturnDate { get; set; }
        public string LastReturnDate { get; set; }
        public string ReturnBefore { get; set; }
        public string DocumentRequest { get; set; }
        public string SpecialRequest { get; set; }
        public string ETDREQUIREMENT { get; set; }
        public string CLOSINGDATE { get; set; }
        public string CLOSINGTIME { get; set; }
        public string DG { get; set; }
        #region Air
        public string MAWB { get; set; }
        public string LoadingDateAir { get; set; }
        public string CutOffTime { get; set; }
        public string CutOffDate { get; set; }
        public string LoadingPlaceAir { get; set; }
        public string Warehouse { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string EWSeries { get; set; }
        #endregion
    }
    public class GetCOLoaderSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string COLOADER { get; set; }
        public string CardCode { get; set; }
    }
    public class GetListCustomerSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string CardCode { get; set; }

    }
    public class GetShipperSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string SHIPPER { get; set; }
        public string CardCode { get; set; }

    }
    public class GetConsigneeSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string CONSIGNEE { get; set; }
        public string CardCode { get; set; }

    }
    public class GetShippingLineSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string SHIPPINGLINE { get; set; }
        public string CardCode { get; set; }

    }
    public class GetDestAgentSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string DESTAGENT { get; set; }
        public string CardCode { get; set; }

    }
    public class GetCommoditySeaAndAirViewDetail
    {
        public string Invoice { get; set; }
    }
    public class GetOverseaTransportSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string OVEARSEATRANSPORT { get; set; }
        public string CardCode { get; set; }

    }
    public class GetPortOfReceiptSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string PORTOFRECEIPT { get; set; }
        public string Code { get; set; }
    }
    public class GetPortOfDischargeSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string PORTOFDISCHARGE { get; set; }
        public string Code { get; set; }

    }
    public class GetPlaceOfLoadingSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string PLACEOFLOADING { get; set; }
        public string Code { get; set; }

    }
    public class GetPlaceOfDeliverySeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string PLACEOFDELIVERY { get; set; }
        public string Code { get; set; }

    }
    public class GetThaiForwarderSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string THAIFORWARDER { get; set; }
        public string CardCode { get; set; }

    }
    public class GetContainerTypeSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public int Qty { get; set; }
        public string ContainerType { get; set; }
        public double Weight { get; set; }
        public string ListInvoice { get; set; }
    }
    public class GetOverSeaForwarderSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string OVERSEAFORWARDER { get; set; }
        public string CardCode { get; set; }

    }
    public class GetThaiBorderSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string THAIBORDER { get; set; }
        public string Code { get; set; }

    }
    public class GetTruckTypeSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public int Qty { get; set; }
        public string ContainerType { get; set; }
        public double Weight { get; set; }
        public string ListInvoice { get; set; }
    }
    public class GetCYAt_ContactSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string CYAt_Contact { get; set; }
        public string CardCode { get; set; }

    }
    public class GetReturnAt_ContactSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string ReturnAt_Contact { get; set; }
        public string CardCode { get; set; }

    }
    public class GetLoadingAtSeaAndAirViewDetail
    {
        public int LineId { get; set; }
        public string LoadingAt { get; set; }
        public string CardCode { get; set; }

    }
    public class GetFlightsInformationViewDetail
    {
        public string FlightFromTo { get; set; }
        public string FlightNo { get; set; }
        public string FlightArrival { get; set; }
    }
}
