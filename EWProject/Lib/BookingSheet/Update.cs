using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheet
{
    public class Update
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Update(PostBookingSheetRequest postBookingSheetRequest, Company _company)
        {
            _Update(postBookingSheetRequest, _company);
        }
        private void _Update(PostBookingSheetRequest postBookingSheetRequest, Company _company)
        {
            try
            {
                //_company.StartTransaction();
                if (GetQuery.GetValueByID("CHECKINGLOADINGDATEHASCHANGEORNOT", "Condition", postBookingSheetRequest.BookingID.ToString()) != Convert.ToDateTime(postBookingSheetRequest.LoadingDate).ToString("yyyy-MM"))
                {
                    var objRemove = BookingSheets._Remove(postBookingSheetRequest.BookingID.ToString(), _company);
                    if (objRemove.ErroreCode != 0)
                    {
                        _ErroreCode = objRemove.ErroreCode;
                        _MessageErrore = objRemove.ErroreMessage;
                        _DocEntry = objRemove.DocEntry;
                        return;
                    }
                    var objBookingSheet = BookingSheets._Add(postBookingSheetRequest, _company);
                    _ErroreCode = objBookingSheet.ErroreCode;
                    _MessageErrore = objBookingSheet.ErroreMessage;
                    _DocEntry = objBookingSheet.DocEntry;
                }
                else
                {
                    GeneralService oConfirmBookingGeneralServiceUpdate;
                    GeneralData oConfirmBookingGeneralDataUpdate;
                    GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                    CompanyService companyService;
                    companyService = _company.GetCompanyService();
                    oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("BOOKINGSHEET");
                    oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", postBookingSheetRequest.BookingID);
                    oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_EWSeries", ClearEmptyString.clearEmptyString(postBookingSheetRequest.EWSereis));
                    //oGeneralData.SetProperty("U_JOBNO", (GetDocNumBookingSheetResponseAsync().GetAwaiter().GetResult()).Data.JOBNO);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_CO", ClearEmptyString.clearEmptyString(postBookingSheetRequest.CO));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_SALEEMPLOYEE", postBookingSheetRequest.SaleEmployee);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_IMPORTTYPE", ClearEmptyString.clearEmptyString(postBookingSheetRequest.ImportType));
                    //oConfirmBookingGeneralDataUpdate.SetProperty("U_BOOKINGDATE", DateTime.Now);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_ORIGIN", postBookingSheetRequest.Origin.ToString());
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_DESTINATION", postBookingSheetRequest.Destination.ToString());
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_GOODSDESCRIPTION", ClearEmptyString.clearEmptyString(postBookingSheetRequest.GoodDescription));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALPACKAGE", ClearEmptyString.clearEmptyString(postBookingSheetRequest.TotalPackage));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_NETWEIGHT", postBookingSheetRequest.NetWeight);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_GROSSWEIGHT", postBookingSheetRequest.GrossWeight);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_LOADINGDATE", postBookingSheetRequest.LoadingDate);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_CROSSBORDERDATE", (postBookingSheetRequest.EWSereis == "EWST") ? ClearEmptyString.clearEmptyString(postBookingSheetRequest.CorssBorderDate) : postBookingSheetRequest.CorssBorderDate);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_ETAREQUIREMENT", (postBookingSheetRequest.EWSereis == "EWST") ? ClearEmptyString.clearEmptyString(postBookingSheetRequest.ETARequirement) : postBookingSheetRequest.ETARequirement);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_DG", ClearEmptyString.clearEmptyString(postBookingSheetRequest.DG));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_OtherRemark", ClearEmptyString.clearEmptyString(postBookingSheetRequest.OtherRemark));
                    if (postBookingSheetRequest.EWSereis == "EWST")
                    {
                        if (ClearEmptyString.clearEmptyString(postBookingSheetRequest.LoloYardOrUnloading.ToString()) != "0")
                        {
                            oConfirmBookingGeneralDataUpdate.SetProperty("U_LOLOYARDORUNLOADING", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LoloYardOrUnloading.ToString()));
                        }
                    }
                    else
                    {
                        oConfirmBookingGeneralDataUpdate.SetProperty("U_LOLOYARDORUNLOADING", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LoloYardOrUnloading.ToString()));
                    }
                    if (postBookingSheetRequest.EWSereis == "EWST")
                    {
                        if (ClearEmptyString.clearEmptyString(postBookingSheetRequest.LCLOrFCL.ToString()) != "0")
                        {
                            oConfirmBookingGeneralDataUpdate.SetProperty("U_LCLORFCL", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LCLOrFCL.ToString()));
                        }
                    }
                    else
                    {
                        oConfirmBookingGeneralDataUpdate.SetProperty("U_LCLORFCL", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LCLOrFCL.ToString()));
                    }
                    if (postBookingSheetRequest.EWSereis == "EWST")
                    {
                        if (ClearEmptyString.clearEmptyString(postBookingSheetRequest.CYOrCFS.ToString()) != "0")
                        {
                            oConfirmBookingGeneralDataUpdate.SetProperty("U_CYORCFS", postBookingSheetRequest.CYOrCFS.ToString());
                        }
                    }
                    else
                    {
                        oConfirmBookingGeneralDataUpdate.SetProperty("U_CYORCFS", ClearEmptyString.clearEmptyString(postBookingSheetRequest.CYOrCFS.ToString()));
                    }
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_LOLOYARDRemark", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LOLOYARDRemark));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_SERVICETYPE", ClearEmptyString.clearEmptyString(postBookingSheetRequest.ServiceType));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(postBookingSheetRequest.CreateUser.ToString()));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_SPECIALREQUEST", ClearEmptyString.clearEmptyString(postBookingSheetRequest.SpeacialRequest));
                    oConfirmBookingGeneralDataUpdate.SetProperty("Remark", ClearEmptyString.clearEmptyString(postBookingSheetRequest.Remarks));
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.Commodities, oConfirmBookingGeneralDataUpdate, "COMMODITY", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.PlaceOfLoadings, oConfirmBookingGeneralDataUpdate, "PLACEOFLOADING", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.PlaceOfDeliveries, oConfirmBookingGeneralDataUpdate, "PLACEOFDELIVERY", "LineId", postBookingSheetRequest.BookingID.ToString());
                    //UpdateChildBookingSheet.UpdateChild(postBookingSheetRequest.PlaceOfDeliveries, oConfirmBookingGeneralDataUpdate, "PLACEOFDELIVERY", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.Volumes, oConfirmBookingGeneralDataUpdate, "VOLUME", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.ThaiBorders, oConfirmBookingGeneralDataUpdate, "THAIBORDER", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.OverseaTrucker, oConfirmBookingGeneralDataUpdate, "TBOVERSEATRUCKER", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.OverseaForwarder, oConfirmBookingGeneralDataUpdate, "TBOVERSEAFORWARDER", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.SaleQuotation, oConfirmBookingGeneralDataUpdate, "TBSALESQUOTATION", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.ThaiForwarder, oConfirmBookingGeneralDataUpdate, "THAIFORWARDER", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.Shipper, oConfirmBookingGeneralDataUpdate, "TBSHIPPER", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.Consignee, oConfirmBookingGeneralDataUpdate, "TBCONSIGNEE", "LineId", postBookingSheetRequest.BookingID.ToString());
                    UpdateChild.UpdateChildObject(postBookingSheetRequest.TruckType, oConfirmBookingGeneralDataUpdate, "TBTRUCKTYPEROW", "LineId", postBookingSheetRequest.BookingID.ToString());
                    oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                    #region Update DocEntry Of BookingSheet link with Confirm BookingSheet
                    GeneralService oBookingGeneralServiceUpdate;
                    GeneralData oBookingGeneralDataUpdate;
                    //GeneralData oBookingChildUpdate;
                    //GeneralDataCollection oBookingChildrenUpdate;
                    GeneralDataParams oBookingGeneralParamsUpdate;
                    CompanyService oBookingCmpSrvUpdate;
                    oBookingCmpSrvUpdate = _company.GetCompanyService();
                    oBookingGeneralServiceUpdate = oBookingCmpSrvUpdate.GetGeneralService("BOOKINGSHEET");
                    oBookingGeneralParamsUpdate = (GeneralDataParams)oBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oBookingGeneralParamsUpdate.SetProperty("DocEntry", postBookingSheetRequest.BookingID);
                    oBookingGeneralDataUpdate = oBookingGeneralServiceUpdate.GetByParams(oBookingGeneralParamsUpdate);
                    oBookingGeneralDataUpdate.SetProperty("U_JOBNO", GetQuery.GetValueByID("CONVERTBOOKINGDOCNUMTOJOBNO_1", "JOBNO", postBookingSheetRequest.BookingID.ToString()));
                    oBookingGeneralServiceUpdate.Update(oBookingGeneralDataUpdate);
                    #endregion
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = postBookingSheetRequest.BookingID;
                }
                //_company.EndTransaction(BoWfTransOpt.wf_Commit);
                //if (_company.InTransaction)
                //{
                //    _company.EndTransaction(BoWfTransOpt.wf_Commit);
                //    _ErroreCode = 0;
                //    _MessageErrore = "";
                //    _DocEntry = postBookingSheetRequest.BookingID;
                //}
                //else
                //{
                //    _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                //    _company.GetLastError(out _ErroreCode, out _MessageErrore);
                //}
            }
            catch (Exception ex)
            {
                _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }

}
