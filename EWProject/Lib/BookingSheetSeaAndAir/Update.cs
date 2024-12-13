using Client.Lib.BookingSheet;
using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheetSeaAndAir
{
    public class Update
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Update(AddBookingSheetSeaAndAir postBookingSheetRequestAirAndSea, Company _company)
        {
            _Update(postBookingSheetRequestAirAndSea, _company);
        }
        private void _Update(AddBookingSheetSeaAndAir postBookingSheetRequestAirAndSea, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Code
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                if (
                        GetQuery.GetValueByID("CHECKINGLOADINGDATEHASCHANGEORNOT"
                                              , "Condition"
                                              , postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO.ToString()
                                              ,storeType:Connection.GetRecordByDataTable.StoreType.TransactionSeaAir) != 
                        Convert.ToDateTime(postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGDATE).ToString("yyyy-MM"))
                {
                    var objRemove = BookingSheetSeaAndAirs._Remove(postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO, oCompany);
                    if (objRemove.ErroreCode != 0)
                    {
                        _ErroreCode = objRemove.ErroreCode;
                        _MessageErrore = objRemove.ErroreMessage;
                        _DocEntry = objRemove.DocEntry.ToString();
                        return;
                    }
                    var objBookingSheet = BookingSheetSeaAndAirs._Add(postBookingSheetRequestAirAndSea, oCompany);
                    _ErroreCode = objBookingSheet.ErroreCode;
                    _MessageErrore = objBookingSheet.ErroreMessage;
                    _DocEntry = objBookingSheet.DocEntry.ToString();
                }
                else
                {
                    companyService = oCompany.GetCompanyService();
                    oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("BOOKING_SEA_AIR");
                    oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                    UpdateHeader.UpdateHeaderObject(postBookingSheetRequestAirAndSea.HeaderObj, oConfirmBookingGeneralDataUpdate);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListCOLoader, oConfirmBookingGeneralDataUpdate, "TBCOLOADER", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListCustomer, oConfirmBookingGeneralDataUpdate, "TBCUSTOMER", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListShipper, oConfirmBookingGeneralDataUpdate, "TBSHIPPER_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListConsignee, oConfirmBookingGeneralDataUpdate, "TBCONSIGNEE_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListShippingLine, oConfirmBookingGeneralDataUpdate, "TBSHIPPINGLINE", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListDestAgent, oConfirmBookingGeneralDataUpdate, "TBDESTAGENT", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListOverseaTransportSeaAndAir, oConfirmBookingGeneralDataUpdate, "TBOVERSEATRANSPORT", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListPortOfReceiptSeaAndAir, oConfirmBookingGeneralDataUpdate, "TBPORTOFRECEIPT", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListPortOfDischargeSeaAndAir, oConfirmBookingGeneralDataUpdate, "TBPORTOFDISCHARGE", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListPlaceOfDeliverySeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_POD_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListPlaceOfLoadingSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_POL_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListThaiForwarderSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_TFW_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListOverSeaForwarderSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_OSFW_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListThaiBorderSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_THAI_B_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.FlightsInformations, oConfirmBookingGeneralDataUpdate, "TB_FLY_INFO", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListCommodities, oConfirmBookingGeneralDataUpdate, "COMMODITY_SEA_AIR", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListContainerTypeSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_CON_S_A", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListTruckTypeSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_TRUCK_S_A", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListCYAt_ContactSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_CY_AT_S_A", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListReturnAt_ContactSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_RETURNAT_S_A", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    UpdateChild.UpdateChildObject(postBookingSheetRequestAirAndSea.ListLoadingAtSeaAndAir, oConfirmBookingGeneralDataUpdate, "TB_LOADINGAT_S_A", "LineId", postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO);
                    oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                    string docEntry = postBookingSheetRequestAirAndSea.HeaderObj.BOOKINGNO;
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = docEntry;
                }                
                #endregion
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
