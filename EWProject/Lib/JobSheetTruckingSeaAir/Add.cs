using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using SAPbobsCOM;
using System.Data;
using System.Dynamic;

namespace Client.Lib.JobSheetTruckingSeaAir
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string draftdocument,string CostCenter, Company _company)
        {
            _Add(postJobSheetTruckingSeaAirRequestAirAndSea,draftdocument, CostCenter, _company);
        }
        private void _Add(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string draftdocument, string CostCenter, Company oCompany)
        {
            try
            {
                var absEntry = Convert.ToInt32(GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO));
                if (oCompany.InTransaction == true) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack); else oCompany.StartTransaction();
                CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                if (draftdocument == "1")
                {
                    #region Create Sale Order
                    Documents SO_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oOrders);
                    SO_invoice.CardCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.CARDCODE;
                    SO_invoice.DocDate = DateTime.Today;
                    SO_invoice.DocDueDate = DateTime.Today;
                    SO_invoice.TaxDate = DateTime.Today;
                    SO_invoice.RequriedDate = DateTime.Today;
                    SO_invoice.DocType = BoDocumentTypes.dDocument_Items;
                    SO_invoice.Comments = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMAKRSSELLING;
                    SO_invoice.NumAtCard = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                    var i = 0;
                    foreach (var a in postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineRevenue)
                    {
                        if (a.Qty != 0)
                        {
                            SO_invoice.Lines.ItemCode = a.ITEMCODE;
                            SO_invoice.Lines.Quantity = a.Qty;
                            SO_invoice.Lines.UnitPrice = Math.Round(a.TotalBaht / a.Qty,7);
                            SO_invoice.Lines.TaxCode = "S00";
                            SO_invoice.Lines.COGSCostingCode = CostCenter;
                            SO_invoice.Lines.Currency = "THS";
                            if (a.ContainerType != "")
                            {
                                SO_invoice.Lines.UoMEntry = Convert.ToInt32(a.ContainerType);
                            }
                            SO_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                            a.LineId = SO_invoice.Lines.LineNum;
                            a.RefLineId = SO_invoice.Lines.LineNum.ToString();
                            postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting[i].RefLineId = SO_invoice.Lines.LineNum.ToString();
                            SO_invoice.Lines.Add();
                        }
                        i++;
                    }
                    if (postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.DutyTaxAmountSelling != 0)
                    {
                        SO_invoice.SpecialLines.LineType = BoDocSpecialLineType.dslt_Text;
                        SO_invoice.SpecialLines.LineText = "Duty Tax";
                        SO_invoice.SpecialLines.AfterLineNumber = postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineRevenue.Last(x => x.Qty != 0).LineId;
                        SO_invoice.SpecialLines.Add();
                        SO_invoice.Lines.ItemCode = Configure.DutyTaxCode;
                        SO_invoice.Lines.Quantity = 1;
                        SO_invoice.Lines.Currency = "THS";
                        SO_invoice.Lines.UnitPrice = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.DutyTaxAmountSelling;
                        SO_invoice.Lines.LineTotal = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.DutyTaxAmountSelling;
                        SO_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                        SO_invoice.Lines.COGSCostingCode = CostCenter;
                        postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineRevenue.Add(new JobSheetRuckingLineForItemSeaAir
                        {
                            LineId = SO_invoice.Lines.LineNum,
                            RefLineId = SO_invoice.Lines.LineNum.ToString(),
                            ITEMCODE = Configure.DutyTaxCode,
                            Qty = 1,
                        });
                        SO_invoice.Lines.Add();
                    }
                    int RetValSO = SO_invoice.Add();
                    if (RetValSO != 0)
                    {
                        oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                        _DocEntry = 0;
                        if(oCompany.InTransaction==true)
                            oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        return;
                    }
                    postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.SALESORDERDOCNUM = oCompany.GetNewObjectKey();
                    #endregion
                    #region AddLine Project Management Sales Order
                    ProjectManagementService pmgServiceUpdateProjectManagement = (ProjectManagementService)oCompServUpdateProject.GetBusinessService(ServiceTypes.ProjectManagementService);
                    PM_ProjectDocumentParams projectToUpdateParam = (PM_ProjectDocumentParams)pmgServiceUpdateProjectManagement.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentParams);
                    projectToUpdateParam.AbsEntry = absEntry;
                    PM_ProjectDocumentData projectUpdateProjectManagement = pmgServiceUpdateProjectManagement.GetProject(projectToUpdateParam);
                    PM_StagesCollection stagesCollection = projectUpdateProjectManagement.PM_StagesCollection;
                    PM_StageData stage = stagesCollection.Add();
                    //Purchase Request
                    stage.StageType = 1;
                    stage.StartDate = DateTime.Now.AddMonths(1);
                    stage.CloseDate = stage.StartDate.AddDays(30);
                    stage.Task = 2;
                    stage.Description = "Sale Order Job Sheet Trucking";
                    stage.IsFinished = BoYesNoEnum.tNO;
                    PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                    foreach (var a in postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineRevenue)
                    {
                        if (a.Qty != 0)
                        {
                            PM_DocumentData document = documentsCollection.Add();
                            document.StageID = Convert.ToInt32(GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                            document.DocType = PMDocumentTypeEnum.pmdt_SalesOrder;
                            document.DocEntry = Convert.ToInt32(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.SALESORDERDOCNUM);
                            document.LineNumber = Convert.ToInt32(a.LineId);
                        }
                    }
                    //End Sale Order
                    pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);

                    #endregion
                }
                if (postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.COSTINGCONFIRM == "Y")
                {
                    #region Create Purchase Request
                    Documents PR_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseRequest);
                    //PR_invoice.CardCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.CARDCODE;
                    PR_invoice.DocDate = DateTime.Today;
                    PR_invoice.DocDueDate = DateTime.Today;
                    PR_invoice.TaxDate = DateTime.Today;
                    PR_invoice.RequriedDate = DateTime.Today;
                    PR_invoice.DocType = BoDocumentTypes.dDocument_Items;
                    PR_invoice.DocCurrency = "THS";
                    PR_invoice.Comments = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMARKSCOSTING;
                    foreach (var a in postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting)
                    {
                        if (a.Qty != 0)
                        {
                            PR_invoice.Lines.BaseEntry = Convert.ToInt32(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.SALESORDERDOCNUM);
                            PR_invoice.Lines.BaseLine = Convert.ToInt32(a.RefLineId);
                            PR_invoice.Lines.BaseType = 17;
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
                            PR_invoice.Lines.COGSCostingCode = CostCenter;
                            PR_invoice.Lines.TaxCode = "P00";
                            if (a.ContainerType != "")
                            {
                                PR_invoice.Lines.UoMEntry = Convert.ToInt32(a.ContainerType);
                            }
                            PR_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                            a.LineId = PR_invoice.Lines.LineNum;
                            PR_invoice.Lines.Add();
                        }
                    }
                    int RetValPR = PR_invoice.Add();
                    if (RetValPR != 0)
                    {
                        oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                        _DocEntry = 0;
                        oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        return;
                    }
                    postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.PurchaseOrder = oCompany.GetNewObjectKey();
                    var tempListPO = new List<object>();
                    var listObject = postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.Where(x => !String.IsNullOrWhiteSpace(x.VendorCode)).Select(x => x.VendorCode).Distinct();
                    #endregion
                    foreach (var ObjVendor in listObject)
                    {
                        #region Create Purchase Order
                        Documents PO_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                        PO_invoice.CardCode = ObjVendor;
                        PO_invoice.DocDate = DateTime.Today;
                        PO_invoice.DocDueDate = DateTime.Today;
                        PO_invoice.TaxDate = DateTime.Today;
                        PO_invoice.RequriedDate = DateTime.Today;
                        PO_invoice.DocType = BoDocumentTypes.dDocument_Items;
                        //Want to use local currency
                        PO_invoice.DocCurrency = "THS";
                        PO_invoice.Comments = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMARKSCOSTING;
                        PO_invoice.NumAtCard = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                        foreach (var a in postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.Where(x => x.VendorCode == ObjVendor && x.Qty!=0))
                        {
                            if (a.Qty != 0)
                            {
                                PO_invoice.Lines.BaseEntry = Convert.ToInt32(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.PurchaseOrder);
                                PO_invoice.Lines.BaseLine = a.LineId;
                                PO_invoice.Lines.BaseType = 1470000113;
                                PO_invoice.Lines.ItemCode = a.ITEMCODE;
                                PO_invoice.Lines.Quantity = a.Qty;
                                if (a.Currency != "THB" && a.Currency!="THS")
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
                                PO_invoice.Lines.COGSCostingCode = CostCenter;
                                if (a.ContainerType != "")
                                {
                                    PO_invoice.Lines.UoMEntry = Convert.ToInt32(a.ContainerType);
                                }
                                PO_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                                PO_invoice.Lines.Add();
                            }
                        }
                        int RetValPO = PO_invoice.Add();
                        if (RetValPO != 0)
                        {
                            oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                            _DocEntry = 0;
                            oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                            return;
                        }
                        //PurchaseOrder = oCompany.GetNewObjectKey();
                        dynamic obj =new ExpandoObject();
                        obj.CardCode = ObjVendor;
                        obj.DocEntry = oCompany.GetNewObjectKey();
                        tempListPO.Add(obj);
                        #endregion
                    }
                    foreach (dynamic tmp in tempListPO)
                    {
                        postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.ForEach(
                            (x) => { if (x.VendorCode == tmp.CardCode) x.PurchaseOrder = tmp.DocEntry; });
                    }
                }
                #region Job Sheet Trucking Sea Air
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("TB_JOBSHEET_SEA_AIR");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                AddHeader.AddHeaderObject(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader, oGeneralData);
                AddChild.AddChildRow(postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineRevenue, oGeneralData, "TB_JOBSHEET_SELLING", "LineId");
                AddChild.AddChildRow(postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting, oGeneralData, "TB_JOBSHEET_COSTING", "LineId");
                if (postJobSheetTruckingSeaAirRequestAirAndSea.Attachment != null)
                {
                    AddChild.AddChildRow(postJobSheetTruckingSeaAirRequestAirAndSea.Attachment, oGeneralData, "ATTACHMENT_SEA_AIR", "LineId");
                }
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                int docEntry = (int)responseoGeneralService.GetProperty("DocEntry");
                #endregion
                #region Update DocEntry Of ConfirmBookingSheet link with JobSheetTrucking Sea Air
                GeneralService oConfrimBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                //GeneralData oBookingChildUpdate;
                //GeneralDataCollection oBookingChildrenUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService oConfirmBookingCmpSrvUpdate;
                oConfirmBookingCmpSrvUpdate = oCompany.GetCompanyService();
                oConfrimBookingGeneralServiceUpdate = oConfirmBookingCmpSrvUpdate.GetGeneralService("CON_BOOKING_S_A");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfrimBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.CONFIRMBOOKINGID);
                oConfirmBookingGeneralDataUpdate = oConfrimBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_JOBSHEETID", docEntry.ToString());
                //Add Childrence
                oConfrimBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                #endregion

                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = Convert.ToInt32(docEntry);
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                _DocEntry = 0;
                if(oCompany.InTransaction==true)
                oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);

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
