using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheet
{
    public class UpdateCommodities
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public UpdateCommodities(string CreateUser, int docEntry, List<Commodity> commodityRequest, HeaderCommodityUpdate headerCommodityUpdate, List<OverTruckers> overseaTrucker
                                                                                , List<ThaiForwarders> thaiForwarders, List<OverForwarders> overForwarders
                                                                                , List<ThaiBorders> thaiBorders, PlaceOfLoading placeOfLoadings, PlaceOfLoading placeOfDeliveries, Company _company)
        {
            _Update(CreateUser, docEntry, commodityRequest, headerCommodityUpdate, overseaTrucker, thaiForwarders, overForwarders, thaiBorders, placeOfLoadings, placeOfDeliveries, _company);
        }
        private void _Update(string CreateUser, int docEntry, List<Commodity> commodityRequest, HeaderCommodityUpdate headerCommodityUpdate, List<OverTruckers> overseaTrucker
                                                                                , List<ThaiForwarders> thaiForwarders, List<OverForwarders> overForwarders
                                                                                , List<ThaiBorders> thaiBorders, PlaceOfLoading placeOfLoadings, PlaceOfLoading placeOfDeliveries, Company _company)
        {
            try
            {
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                companyService = _company.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("BOOKINGSHEET");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", docEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                if (headerCommodityUpdate != null)
                {
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_GROSSWEIGHT", headerCommodityUpdate.GROSSWEIGHT);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_NETWEIGHT", headerCommodityUpdate.NETWEIGHT);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALPACKAGE", ClearEmptyString.clearEmptyString(String.IsNullOrEmpty(headerCommodityUpdate.TOTALPACKAGE) ? "" : headerCommodityUpdate.TOTALPACKAGE));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_GOODSDESCRIPTION", ClearEmptyString.clearEmptyString(String.IsNullOrEmpty(headerCommodityUpdate.GOODSDESCRIPTION) ? "" : headerCommodityUpdate.GOODSDESCRIPTION));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_LOLOYARDRemark", ClearEmptyString.clearEmptyString(String.IsNullOrEmpty(headerCommodityUpdate.LOLOYARDORUNLOADINGRemark) ? "" : headerCommodityUpdate.LOLOYARDORUNLOADINGRemark));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_LOLOYARDORUNLOADING", ClearEmptyString.clearEmptyString(headerCommodityUpdate.LOLOYARDORUNLOADING.ToString()));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_LCLORFCL", ClearEmptyString.clearEmptyString(headerCommodityUpdate.LCLORFLC.ToString()));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_CYORCFS", ClearEmptyString.clearEmptyString(headerCommodityUpdate.CYORCFS.ToString()));
                }
                oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(CreateUser.ToString()));
                UpdateChild.UpdateChildObject(commodityRequest, oConfirmBookingGeneralDataUpdate, "COMMODITY", "LineId", docEntry.ToString());
                UpdateChild.UpdateChildObject(overseaTrucker, oConfirmBookingGeneralDataUpdate, "TBOVERSEATRUCKER", "LineId", docEntry.ToString());
                UpdateChild.UpdateChildObject(thaiForwarders, oConfirmBookingGeneralDataUpdate, "THAIFORWARDER", "LineId", docEntry.ToString());
                UpdateChild.UpdateChildObject(overForwarders, oConfirmBookingGeneralDataUpdate, "TBOVERSEAFORWARDER", "LineId", docEntry.ToString());
                UpdateChild.UpdateChildObject(thaiBorders, oConfirmBookingGeneralDataUpdate, "THAIBORDER", "LineId", docEntry.ToString());
                if (GetQuery.GetValueByID("GetPlaceOfLoadingCheckingUpdateValue", "PlaceOfLoading", docEntry.ToString()) != placeOfLoadings.PLACELOADING
                    || GetQuery.GetValueByID("GetPlaceOfLoadingDistrictCheckingUpdateValue", "LoadingOfDistrict", docEntry.ToString()) != placeOfLoadings.District
                    || GetQuery.GetValueByID("GetPlaceOfDeliveryCheckingUpdateValue", "PlaceOfDelivery", docEntry.ToString()) != placeOfDeliveries.PLACELOADING
                    || GetQuery.GetValueByID("GetPlaceOfDeliveryDistrictCheckingUpdateValue", "DeliveryOfDistrict", docEntry.ToString()) != placeOfDeliveries.District)
                {
                    GeneralService oLoadingAndDistrict;
                    GeneralData oDataLoadingAndDelivery;
                    oLoadingAndDistrict = companyService.GetGeneralService("TB_PLACE_L_D");
                    oDataLoadingAndDelivery = (GeneralData)oLoadingAndDistrict.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                    oDataLoadingAndDelivery.SetProperty("Code", (DateTime.Now.Day.ToString()
                                            + DateTime.Now.Month.ToString()
                                            + DateTime.Now.Year.ToString()
                                            + DateTime.Now.DayOfYear.ToString()
                                            + DateTime.Now.Hour.ToString()
                                            + DateTime.Now.Minute.ToString()
                                            + DateTime.Now.Second.ToString()
                                            + DateTime.Now.Millisecond.ToString()).ToString());
                    oDataLoadingAndDelivery.SetProperty("U_BOOKINGID", docEntry);
                    oDataLoadingAndDelivery.SetProperty("U_PLACEOFLOADING", (GetQuery.GetValueByID("GetPlaceOfLoadingCheckingUpdateValue", "PlaceOfLoading", docEntry.ToString()) != placeOfLoadings.PLACELOADING) ? ClearEmptyString.clearEmptyString(placeOfLoadings.PLACELOADING) : "");
                    oDataLoadingAndDelivery.SetProperty("U_DISTRICTOFLOADING", (GetQuery.GetValueByID("GetPlaceOfLoadingDistrictCheckingUpdateValue", "LoadingOfDistrict", docEntry.ToString()) != placeOfLoadings.District) ? ClearEmptyString.clearEmptyString(placeOfLoadings.District) : "");
                    oDataLoadingAndDelivery.SetProperty("U_PLACEOFDELIVERY", (GetQuery.GetValueByID("GetPlaceOfDeliveryCheckingUpdateValue", "PlaceOfDelivery", docEntry.ToString()) != placeOfDeliveries.PLACELOADING) ? ClearEmptyString.clearEmptyString(placeOfDeliveries.PLACELOADING) : "");
                    oDataLoadingAndDelivery.SetProperty("U_DISTRICTOFDELIVERY", (GetQuery.GetValueByID("GetPlaceOfDeliveryDistrictCheckingUpdateValue", "DeliveryOfDistrict", docEntry.ToString()) != placeOfDeliveries.District) ? ClearEmptyString.clearEmptyString(placeOfDeliveries.District) : "");
                    oDataLoadingAndDelivery.SetProperty("U_STATUS", "P");
                    oDataLoadingAndDelivery.SetProperty("U_CreateBy", ClearEmptyString.clearEmptyString(CreateUser.ToString()));
                    oLoadingAndDistrict.Add(oDataLoadingAndDelivery);
                }
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
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
