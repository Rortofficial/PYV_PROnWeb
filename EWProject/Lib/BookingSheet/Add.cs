using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheet
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PostBookingSheetRequest postBookingSheetRequest, Company _company)
        {
            _Add(postBookingSheetRequest, _company);
        }
        private void _Add(PostBookingSheetRequest postBookingSheetRequest, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Code
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                GeneralData oChild;
                GeneralData oChild1;
                GeneralData oChild2;
                GeneralData oChild3;
                GeneralData oChild4;
                GeneralData oChild5;
                GeneralData oChild6;
                GeneralData oChild7;
                GeneralData oChild8;
                GeneralData oChild9;
                GeneralData oChild10;
                GeneralData oChild11;
                GeneralDataCollection oChildren;
                GeneralDataCollection oChildren1;
                GeneralDataCollection oChildren2;
                GeneralDataCollection oChildren3;
                GeneralDataCollection oChildren4;
                GeneralDataCollection oChildren5;
                GeneralDataCollection oChildren6;
                GeneralDataCollection oChildren7;
                GeneralDataCollection oChildren8;
                GeneralDataCollection oChildren9;
                GeneralDataCollection oChildren10;
                GeneralDataCollection oChildren11;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("BOOKINGSHEET");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                oGeneralData.SetProperty("U_EWSeries", ClearEmptyString.clearEmptyString(postBookingSheetRequest.EWSereis));
                //oGeneralData.SetProperty("U_JOBNO", (GetDocNumBookingSheetResponseAsync().GetAwaiter().GetResult()).Data.JOBNO);
                oGeneralData.SetProperty("U_CO", ClearEmptyString.clearEmptyString(postBookingSheetRequest.CO));
                oGeneralData.SetProperty("U_SALEEMPLOYEE", postBookingSheetRequest.SaleEmployee);
                oGeneralData.SetProperty("U_IMPORTTYPE", ClearEmptyString.clearEmptyString(postBookingSheetRequest.ImportType));
                oGeneralData.SetProperty("U_BOOKINGDATE", DateTime.Now);
                oGeneralData.SetProperty("U_ORIGIN", postBookingSheetRequest.Origin.ToString());
                oGeneralData.SetProperty("U_DESTINATION", postBookingSheetRequest.Destination.ToString());
                oGeneralData.SetProperty("U_GOODSDESCRIPTION", ClearEmptyString.clearEmptyString(postBookingSheetRequest.GoodDescription));
                oGeneralData.SetProperty("U_TOTALPACKAGE", ClearEmptyString.clearEmptyString(postBookingSheetRequest.TotalPackage));
                oGeneralData.SetProperty("U_NETWEIGHT", postBookingSheetRequest.NetWeight);
                oGeneralData.SetProperty("U_GROSSWEIGHT", postBookingSheetRequest.GrossWeight);
                //Add Job For Upload Transaction
                //oGeneralData.SetProperty("U_JOBNO", postBookingSheetRequest.JobNumber);
                //End Add Job For Upload Transaction
                oGeneralData.SetProperty("U_LOADINGDATE", postBookingSheetRequest.LoadingDate);
                oGeneralData.SetProperty("U_CROSSBORDERDATE", (postBookingSheetRequest.EWSereis == "EWST") ? ClearEmptyString.clearEmptyString(postBookingSheetRequest.CorssBorderDate) : postBookingSheetRequest.CorssBorderDate);
                oGeneralData.SetProperty("U_ETAREQUIREMENT", (postBookingSheetRequest.EWSereis == "EWST") ? ClearEmptyString.clearEmptyString(postBookingSheetRequest.ETARequirement) : postBookingSheetRequest.ETARequirement);
                if (postBookingSheetRequest.EWSereis == "EWST")
                {
                    if (ClearEmptyString.clearEmptyString(postBookingSheetRequest.LoloYardOrUnloading.ToString()) != "0")
                    {
                        oGeneralData.SetProperty("U_LOLOYARDORUNLOADING", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LoloYardOrUnloading.ToString()));
                    }
                }
                else
                {
                    oGeneralData.SetProperty("U_LOLOYARDORUNLOADING", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LoloYardOrUnloading.ToString()));
                }
                if (postBookingSheetRequest.EWSereis == "EWST")
                {
                    if (ClearEmptyString.clearEmptyString(postBookingSheetRequest.LCLOrFCL.ToString()) != "0")
                    {
                        oGeneralData.SetProperty("U_LCLORFCL", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LCLOrFCL.ToString()));
                    }
                }
                else
                {
                    oGeneralData.SetProperty("U_LCLORFCL", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LCLOrFCL.ToString()));
                }
                if (postBookingSheetRequest.EWSereis == "EWST")
                {
                    if (ClearEmptyString.clearEmptyString(postBookingSheetRequest.CYOrCFS.ToString()) != "0")
                    {
                        oGeneralData.SetProperty("U_CYORCFS", postBookingSheetRequest.CYOrCFS.ToString());
                    }
                }
                else
                {
                    oGeneralData.SetProperty("U_CYORCFS", ClearEmptyString.clearEmptyString(postBookingSheetRequest.CYOrCFS.ToString()));
                }
                oGeneralData.SetProperty("U_LOLOYARDRemark", ClearEmptyString.clearEmptyString(postBookingSheetRequest.LOLOYARDRemark));
                oGeneralData.SetProperty("U_SERVICETYPE", ClearEmptyString.clearEmptyString(postBookingSheetRequest.ServiceType));
                oGeneralData.SetProperty("U_UserCreate", ClearEmptyString.clearEmptyString(postBookingSheetRequest.CreateUser.ToString()));
                oGeneralData.SetProperty("U_SPECIALREQUEST", ClearEmptyString.clearEmptyString(postBookingSheetRequest.SpeacialRequest));
                oGeneralData.SetProperty("Remark", ClearEmptyString.clearEmptyString(postBookingSheetRequest.Remarks));
                oGeneralData.SetProperty("U_DG", ClearEmptyString.clearEmptyString(postBookingSheetRequest.DG));
                oGeneralData.SetProperty("U_OtherRemark", ClearEmptyString.clearEmptyString(postBookingSheetRequest.OtherRemark));
                var GetSeries = GetQuery.GetValueByID("GETEWSERIESBYLOADINGDATE", "GetSeries", postBookingSheetRequest.LoadingDate);
                if (GetSeries == "-1")
                {
                    _ErroreCode = 999;
                    _MessageErrore = "Please Define a new Document Numbering for month of Loading Date";
                    return;
                }
                else
                {
                    oGeneralData.SetProperty("Series", GetQuery.GetValueByID("GETEWSERIESBYLOADINGDATE", "GetSeries", postBookingSheetRequest.LoadingDate));
                }
                //Add Place Of Commodity
                if (postBookingSheetRequest.Commodities != null)
                {
                    oChildren = oGeneralData.Child("COMMODITY");
                    foreach (var a in postBookingSheetRequest.Commodities)
                    {
                        oChild = oChildren.Add();
                        oChild.SetProperty("U_INVOICE", ClearEmptyString.clearEmptyString(a.INVOICE));
                    }
                }
                //Add Place Of Loading
                if (postBookingSheetRequest.PlaceOfLoadings != null)
                {
                    oChildren1 = oGeneralData.Child("PLACEOFLOADING");
                    foreach (var a in postBookingSheetRequest.PlaceOfLoadings)
                    {
                        oChild1 = oChildren1.Add();
                        oChild1.SetProperty("U_PLACELOADING", ClearEmptyString.clearEmptyString(a.PLACELOADING));
                        oChild1.SetProperty("U_District", ClearEmptyString.clearEmptyString(a.District));
                    }
                }
                //Add Place Of Delivery
                if (postBookingSheetRequest.PlaceOfDeliveries != null)
                {
                    oChildren2 = oGeneralData.Child("PLACEOFDELIVERY");
                    foreach (var a in postBookingSheetRequest.PlaceOfDeliveries)
                    {
                        oChild2 = oChildren2.Add();
                        oChild2.SetProperty("U_PLACEDELIVERY", ClearEmptyString.clearEmptyString(a.PLACEDELIVERY));
                        oChild2.SetProperty("U_District", ClearEmptyString.clearEmptyString(a.District));
                    }
                }
                //Add Volume
                if (postBookingSheetRequest.Volumes != null)
                {
                    oChildren3 = oGeneralData.Child("VOLUME");
                    foreach (var a in postBookingSheetRequest.Volumes)
                    {
                        oChild3 = oChildren3.Add();
                        oChild3.SetProperty("U_QTY", a.QTY);
                        oChild3.SetProperty("U_VOLUMECODE", ClearEmptyString.clearEmptyString(a.VOLUMECODE));
                        oChild3.SetProperty("U_GROSSWEIGHT", a.GROSSWEIGHT);
                        oChild3.SetProperty("U_InvoiceList", ClearEmptyString.clearEmptyString(a.InvoiceList));
                    }
                }
                //Add ThaiBorder
                if (postBookingSheetRequest.ThaiBorders != null)
                {
                    oChildren4 = oGeneralData.Child("THAIBORDER");
                    foreach (var a in postBookingSheetRequest.ThaiBorders)
                    {
                        oChild4 = oChildren4.Add();
                        oChild4.SetProperty("U_ThaiBorder", ClearEmptyString.clearEmptyString(a.ThaiBorder));
                    }
                }
                // TBOVERSEATRUCKER
                if (postBookingSheetRequest.OverseaTrucker != null)
                {
                    oChildren5 = oGeneralData.Child("TBOVERSEATRUCKER");
                    foreach (var a in postBookingSheetRequest.OverseaTrucker)
                    {
                        oChild5 = oChildren5.Add();
                        oChild5.SetProperty("U_OVERSEATRUCKERCODE", ClearEmptyString.clearEmptyString(a.OVERSEATRUCKERCODE));
                    }
                }
                // OVERSEAFORWARDER
                if (postBookingSheetRequest.OverseaForwarder != null)
                {
                    oChildren6 = oGeneralData.Child("TBOVERSEAFORWARDER");
                    foreach (var a in postBookingSheetRequest.OverseaForwarder)
                    {
                        oChild6 = oChildren6.Add();
                        oChild6.SetProperty("U_OVERSEAFORWARDERCODE", ClearEmptyString.clearEmptyString(a.OVERSEAFORWARDERCODE));
                    }
                }
                //oGeneralData.SetProperty("U_SALESQUOTATION", postBookingSheetRequest.SaleQuotation);
                if (postBookingSheetRequest.SaleQuotation != null)
                {
                    oChildren7 = oGeneralData.Child("TBSALESQUOTATION");
                    foreach (var a in postBookingSheetRequest.SaleQuotation)
                    {
                        oChild7 = oChildren7.Add();
                        oChild7.SetProperty("U_DOCENTRY", ClearEmptyString.clearEmptyString(a.DOCENTRY));
                    }
                }
                if (postBookingSheetRequest.ThaiForwarder != null)
                {
                    oChildren8 = oGeneralData.Child("THAIFORWARDER");
                    foreach (var a in postBookingSheetRequest.ThaiForwarder)
                    {
                        oChild8 = oChildren8.Add();
                        oChild8.SetProperty("U_THAIFORWARDER", ClearEmptyString.clearEmptyString(a.THAIFORWARDER));
                    }
                }
                if (postBookingSheetRequest.Shipper != null)
                {
                    oChildren9 = oGeneralData.Child("TBSHIPPER");
                    foreach (var a in postBookingSheetRequest.Shipper)
                    {
                        oChild9 = oChildren9.Add();
                        oChild9.SetProperty("U_SHIPPER", ClearEmptyString.clearEmptyString(a.SHIPPER));
                    }
                }
                if (postBookingSheetRequest.Consignee != null)
                {
                    oChildren10 = oGeneralData.Child("TBCONSIGNEE");
                    foreach (var a in postBookingSheetRequest.Consignee)
                    {
                        oChild10 = oChildren10.Add();
                        oChild10.SetProperty("U_CONSIGNEE", ClearEmptyString.clearEmptyString(a.CONSIGNEE));
                    }
                }
                if (postBookingSheetRequest.TruckType != null)
                {
                    oChildren11 = oGeneralData.Child("TBTRUCKTYPEROW");
                    foreach (var a in postBookingSheetRequest.TruckType)
                    {
                        oChild11 = oChildren11.Add();
                        oChild11.SetProperty("U_QTY", ClearEmptyString.clearEmptyString(a.QTY));
                        oChild11.SetProperty("U_TRUCKTYPE", ClearEmptyString.clearEmptyString(a.TRUCKTYPE));
                        oChild11.SetProperty("U_GROSSWEIGHT", a.GROSSWEIGHT);
                        oChild11.SetProperty("U_InvoiceList", ClearEmptyString.clearEmptyString(a.InvoiceList));
                    }
                }
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                int docEntry = (int)responseoGeneralService.GetProperty("DocEntry");
                #region Update DocEntry Of BookingSheet link with Confirm BookingSheet
                GeneralService oBookingGeneralServiceUpdate;
                GeneralData oBookingGeneralDataUpdate;
                //GeneralData oBookingChildUpdate;
                //GeneralDataCollection oBookingChildrenUpdate;
                GeneralDataParams oBookingGeneralParamsUpdate;
                CompanyService oBookingCmpSrvUpdate;
                oBookingCmpSrvUpdate = oCompany.GetCompanyService();
                oBookingGeneralServiceUpdate = oBookingCmpSrvUpdate.GetGeneralService("BOOKINGSHEET");
                oBookingGeneralParamsUpdate = (GeneralDataParams)oBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oBookingGeneralParamsUpdate.SetProperty("DocEntry", docEntry);
                oBookingGeneralDataUpdate = oBookingGeneralServiceUpdate.GetByParams(oBookingGeneralParamsUpdate);
                oBookingGeneralDataUpdate.SetProperty("U_JOBNO", GetQuery.GetValueByID("CONVERTBOOKINGDOCNUMTOJOBNO_1", "JOBNO", docEntry.ToString()));
                oBookingGeneralServiceUpdate.Update(oBookingGeneralDataUpdate);
                #endregion
                #endregion
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = docEntry;
                //if (oCompany.InTransaction)
                //{
                //    oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                //    _ErroreCode = 0;
                //    _MessageErrore = "";
                //    _DocEntry = docEntry;
                //}
                //else
                //{
                //    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                //}
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
