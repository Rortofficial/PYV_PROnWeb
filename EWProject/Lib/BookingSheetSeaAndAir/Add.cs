using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheetSeaAndAir
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(AddBookingSheetSeaAndAir postBookingSheetRequestAirAndSea, Company _company)
        {
            _Add(postBookingSheetRequestAirAndSea, _company);
        }
        private void _Add(AddBookingSheetSeaAndAir postBookingSheetRequestAirAndSea, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Code
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("BOOKING_SEA_AIR");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                var GetSeries = GetQuery.GetValueByID("GETEWSERIESBYLOADINGDATE", 
                                                      "GetSeries", 
                                                      postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGDATE.ToString("yyyy-MM-dd"),
                                                      storeType:Connection.GetRecordByDataTable.StoreType.TransactionSeaAir);
                if (GetSeries == "-1")
                {
                    _ErroreCode = 999;
                    _MessageErrore = "Please Define a new Document Numbering for month of Loading Date";
                    return;
                }
                else
                {
                    oGeneralData.SetProperty("Series",GetSeries);
                }
                AddHeader.AddHeaderObject(postBookingSheetRequestAirAndSea.HeaderObj, oGeneralData);
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListCOLoader, oGeneralData, "TBCOLOADER", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListCustomer, oGeneralData, "TBCUSTOMER", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListShipper, oGeneralData, "TBSHIPPER_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListConsignee, oGeneralData, "TBCONSIGNEE_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListShippingLine, oGeneralData, "TBSHIPPINGLINE", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListDestAgent, oGeneralData, "TBDESTAGENT", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListOverseaTransportSeaAndAir, oGeneralData, "TBOVERSEATRANSPORT", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListPortOfReceiptSeaAndAir, oGeneralData, "TBPORTOFRECEIPT", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListPortOfDischargeSeaAndAir, oGeneralData, "TBPORTOFDISCHARGE", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListPlaceOfDeliverySeaAndAir, oGeneralData, "TB_POD_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListPlaceOfLoadingSeaAndAir, oGeneralData, "TB_POL_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListThaiForwarderSeaAndAir, oGeneralData, "TB_TFW_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListOverSeaForwarderSeaAndAir, oGeneralData, "TB_OSFW_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListThaiBorderSeaAndAir, oGeneralData, "TB_THAI_B_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.FlightsInformations, oGeneralData, "TB_FLY_INFO", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListCommodities, oGeneralData, "COMMODITY_SEA_AIR", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListContainerTypeSeaAndAir, oGeneralData, "TB_CON_S_A", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListTruckTypeSeaAndAir, oGeneralData, "TB_TRUCK_S_A", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListCYAt_ContactSeaAndAir, oGeneralData, "TB_CY_AT_S_A", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListReturnAt_ContactSeaAndAir, oGeneralData, "TB_RETURNAT_S_A", "LineId");
                AddChild.AddChildRow(postBookingSheetRequestAirAndSea.ListLoadingAtSeaAndAir, oGeneralData, "TB_LOADINGAT_S_A", "LineId");
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                int docEntry = (int)responseoGeneralService.GetProperty("DocEntry");
                #endregion
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = docEntry;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
