using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using SAPbobsCOM;
using System.Data;
using System.Dynamic;

namespace Client.Lib.JobSheetTruckingSeaAir
{
    public class Update
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Update(PostJobSheetTruckingSeaAirRequest postBookingSheetRequestAirAndSea, string docEntry, string CostCenter, Company _company)
        {
            _Update(postBookingSheetRequestAirAndSea, docEntry,CostCenter, _company);
        }
        private void _Update(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string JobdocEntry, string CostCenter, Company oCompany)
        {
            try
            {
                var absEntry = Convert.ToInt32(GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO));
                if (oCompany.InTransaction == true) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                oCompany.StartTransaction();
                CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                if (postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.COSTINGCONFIRM == "Y")
                {
                    #region Create Purchase Request
                    Documents PR_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseRequest);
                    PR_invoice.DocDate = DateTime.Today;
                    PR_invoice.DocDueDate = DateTime.Today;
                    PR_invoice.TaxDate = DateTime.Today;
                    PR_invoice.RequriedDate = DateTime.Today;
                    PR_invoice.DocType = BoDocumentTypes.dDocument_Items;
                    PR_invoice.DocCurrency = "THS";
                    PR_invoice.Comments = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMARKSCOSTING;
                    var SOstatus=GetQuery.GetValueByID(type: "GetStatusSOForAddPRByDocEntry", field: "DocStatus", par1: postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.SALESORDERDOCNUM);
                    foreach (var a in postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting)
                    {
                        if (a.Qty != 0)
                        {
                            if (string.IsNullOrEmpty(SOstatus))
                            {
                                if (!String.IsNullOrEmpty(a.RefLineId))
                                {
                                    PR_invoice.Lines.BaseEntry = Convert.ToInt32(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.SALESORDERDOCNUM);
                                    PR_invoice.Lines.BaseLine = Convert.ToInt32(a.RefLineId);
                                    PR_invoice.Lines.BaseType = 17;
                                }
                            }                                                        
                            PR_invoice.Lines.LineVendor = a.VendorCode;
                            PR_invoice.Lines.ItemCode = a.ITEMCODE;
                            PR_invoice.Lines.Quantity = a.Qty;
                            if (a.Currency != "THB" && a.Currency != "THS")
                            {
                                PR_invoice.Lines.Currency = a.Currency;
                                PR_invoice.Lines.Rate = a.ExchangeRate;
                                PR_invoice.Lines.UnitPrice = Math.Round(a.TOTALPRICE, 7);
                            }
                            else
                            {
                                PR_invoice.Lines.UnitPrice = Math.Round(a.TotalBaht / a.Qty, 7);
                            }
                            PR_invoice.Lines.TaxCode = "P00";
                            PR_invoice.Lines.COGSCostingCode = CostCenter;
                            if (a.ContainerType != "")
                            {
                                PR_invoice.Lines.UoMEntry = Convert.ToInt32(a.ContainerType);
                            }
                            PR_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                            a.LineId = PR_invoice.Lines.LineNum;
                            //a.RefLineId = PR_invoice.Lines.LineNum.ToString();
                            PR_invoice.Lines.Add();
                        }
                    }
                    int RetValPR = PR_invoice.Add();
                    if (RetValPR != 0)
                    {
                        oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                        _DocEntry = "0";
                        if (oCompany.InTransaction)
                        {
                            oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        }
                        return;
                    }
                    postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.PurchaseOrder = oCompany.GetNewObjectKey();
                    var tempListPO = new List<object>();
                    #endregion
                    var ListObjectVendor = postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.Where(x => !String.IsNullOrWhiteSpace(x.VendorCode)).Select(x => x.VendorCode).Distinct();
                    foreach (var ObjVendor in ListObjectVendor)
                    {
                        #region Create Purchase Order
                        if (!String.IsNullOrEmpty(ObjVendor))
                        {
                            Documents PO_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                            PO_invoice.CardCode = ObjVendor;
                            PO_invoice.DocDate = DateTime.Today;
                            PO_invoice.DocDueDate = DateTime.Today;
                            PO_invoice.TaxDate = DateTime.Today;
                            PO_invoice.RequriedDate = DateTime.Today;
                            PO_invoice.DocType = BoDocumentTypes.dDocument_Items;
                            PO_invoice.Comments = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMARKSCOSTING;
                            PO_invoice.NumAtCard = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                            PO_invoice.DocCurrency = "THS";
                            foreach (var a in postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.Where(x => x.VendorCode == ObjVendor && x.Qty!=0))
                            {
                                PO_invoice.Lines.BaseEntry = Convert.ToInt32(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.PurchaseOrder);
                                PO_invoice.Lines.BaseLine = a.LineId;
                                PO_invoice.Lines.BaseType = 1470000113;
                                PO_invoice.Lines.ItemCode = a.ITEMCODE;
                                PO_invoice.Lines.Quantity = a.Qty;
                                if (a.Currency != "THB" && a.Currency != "THS")
                                {
                                    PO_invoice.Lines.Currency = a.Currency;
                                    PO_invoice.Lines.Rate = a.ExchangeRate;
                                    PO_invoice.Lines.UnitPrice = Math.Round(a.TOTALPRICE, 7);
                                }
                                else
                                {
                                    PO_invoice.Lines.UnitPrice = Math.Round(a.TotalBaht / a.Qty, 7);
                                }
                                PO_invoice.Lines.TaxCode = "P00";
                                if (a.ContainerType != "")
                                {
                                    PO_invoice.Lines.UoMEntry = Convert.ToInt32(a.ContainerType);
                                }
                                PO_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                                PO_invoice.Lines.COGSCostingCode = CostCenter;
                                PO_invoice.Lines.Add();
                            }
                            int RetValPO = PO_invoice.Add();
                            if (RetValPO != 0)
                            {
                                oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                                _DocEntry = "0";
                                if (oCompany.InTransaction == true) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                                return;
                            }
                            //PurchaseOrder = oCompany.GetNewObjectKey();
                            dynamic obj = new ExpandoObject();
                            obj.CardCode = ObjVendor;
                            obj.DocEntry = oCompany.GetNewObjectKey();
                            tempListPO.Add(obj);
                        }
                        #endregion
                    }
                    foreach (dynamic tmp in tempListPO)
                    {
                        postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.ForEach(
                            (x) => { if (x.VendorCode == tmp.CardCode) x.PurchaseOrder = tmp.DocEntry; });
                    }
                }
                #region Code Update Job Sheet Trucking
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("TB_JOBSHEET_SEA_AIR");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", JobdocEntry.ToString());
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_REMAKRSSELLING", ClearEmptyString.clearEmptyString(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMAKRSSELLING));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_REMARKSCOSTING", ClearEmptyString.clearEmptyString(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMARKSCOSTING));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_COSTINGCONFIRM", ClearEmptyString.clearEmptyString(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.COSTINGCONFIRM));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALCOSTING", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.TOTALCOSTING);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALSELLING", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.TOTALSELLING);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_REBATE", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REBATE);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_GRANDTOTALCOSTING", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.GRANDTOTALCOSTING);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_GRANDTOTALSELLING", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.GRANDTOTALSELLING);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALPROFIT", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.TOTALPROFIT);
                UpdateChild.UpdateChildObject(postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting, oConfirmBookingGeneralDataUpdate, "TB_JOBSHEET_COSTING", "LineId", JobdocEntry);
                UpdateChild.UpdateChildObject(postJobSheetTruckingSeaAirRequestAirAndSea.Attachment, oConfirmBookingGeneralDataUpdate, "ATTACHMENT_SEA_AIR", "LineId", JobdocEntry);
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                #endregion 
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = JobdocEntry;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                _DocEntry = JobdocEntry;
                if(oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
        #region Function Private
        public string GetValueByID(string type, string field, string id)
        {
            try
            {
                var getBookingSheetByUsersList = new List<GetBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             type,
                             id,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    return dataRow[field].ToString()!;
                }
                return "-1";
            }
            catch
            {
                return "-1";
            }
        }
        #endregion
    }
}
