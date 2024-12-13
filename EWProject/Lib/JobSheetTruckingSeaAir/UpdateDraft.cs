using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;
using System.Dynamic;

namespace Client.Lib.JobSheetTruckingSeaAir
{
    public class UpdateDraft
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public UpdateDraft(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string draftdocument, string jobSheetDocEntry, string CostCenter, Company oCompany)
        {
            _UpdateDraft(postJobSheetTruckingSeaAirRequestAirAndSea, draftdocument,jobSheetDocEntry, CostCenter, oCompany);
        }
        private void _UpdateDraft(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string draftdocument, string jobSheetDocEntry, string CostCenter, Company oCompany)
        {
            try
            {
                var absEntry = Convert.ToInt32(GetQuery.GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO));
                if (oCompany.InTransaction == true) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                oCompany.StartTransaction();
                CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                if (draftdocument == "1")
                {
                    if (postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.SALESORDERDOCNUM == "" || postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.SALESORDERDOCNUM == "0")
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
                        SO_invoice.ShipToCode = "";
                        var i = 0;
                        foreach (var a in postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineRevenue)
                        {
                            if (a.Qty != 0)
                            {
                                SO_invoice.Lines.ItemCode = a.ITEMCODE;
                                SO_invoice.Lines.Quantity = a.Qty;
                                SO_invoice.Lines.Currency = "THS";
                                SO_invoice.Lines.UnitPrice = a.TotalBaht/a.Qty;
                                SO_invoice.Lines.TaxCode = "S00";
                                SO_invoice.Lines.COGSCostingCode = CostCenter;
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
                            SO_invoice.Lines.Currency = "THS";
                            SO_invoice.Lines.Quantity = 1;
                            SO_invoice.Lines.COGSCostingCode = CostCenter;
                            SO_invoice.Lines.UnitPrice = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.DutyTaxAmountSelling;
                            SO_invoice.Lines.LineTotal = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.DutyTaxAmountSelling;
                            SO_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
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
                            _DocEntry = "0";
                            if (oCompany.InTransaction == true) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
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
                                document.StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
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
                        PR_invoice.Comments = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.REMARKSCOSTING;
                        PR_invoice.DocCurrency = "THS";
                        //PR_invoice.NumAtCard = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
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
                            if (oCompany.InTransaction == true) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                            return;
                        }
                        postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.PurchaseOrder = oCompany.GetNewObjectKey();
                        var tempListPO = new List<object>();
                        #endregion
                        var ListObjectVendor= postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.Where(x=> !String.IsNullOrWhiteSpace(x.VendorCode)).Select(x => x.VendorCode).Distinct();
                        foreach (var ObjVendor in ListObjectVendor)
                        {
                            #region Create Purchase Order
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
                                PO_invoice.Lines.COGSCostingCode = CostCenter;
                                if (a.ContainerType != "")
                                {
                                    PO_invoice.Lines.UoMEntry = Convert.ToInt32(a.ContainerType);
                                }
                                PO_invoice.Lines.ProjectCode = postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader.JOBNO;
                                //a.LineId = PO_invoice.Lines.LineNum;
                                //a.RefLineId = PO_invoice.Lines.LineNum.ToString();
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
                            #endregion
                        }
                        foreach (dynamic tmp in tempListPO)
                        {
                            postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting.ForEach(
                                (x) => { if (x.VendorCode == tmp.CardCode) x.PurchaseOrder = tmp.DocEntry; });
                        }
                    }
                }
                #region Job Sheet Trucking Sea Air
                GeneralService oJobSheetTruckingGeneralServiceUpdate;
                GeneralData oJobSheetTruckingGeneralDataUpdate;
                GeneralDataParams oJobSheetTruckingGeneralParamsUpdate;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oJobSheetTruckingGeneralServiceUpdate = companyService.GetGeneralService("TB_JOBSHEET_SEA_AIR");
                oJobSheetTruckingGeneralParamsUpdate = (GeneralDataParams)oJobSheetTruckingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oJobSheetTruckingGeneralParamsUpdate.SetProperty("DocEntry", jobSheetDocEntry);
                oJobSheetTruckingGeneralDataUpdate = oJobSheetTruckingGeneralServiceUpdate.GetByParams(oJobSheetTruckingGeneralParamsUpdate);
                UpdateHeader.UpdateHeaderObject(postJobSheetTruckingSeaAirRequestAirAndSea.ObjectHeader, oJobSheetTruckingGeneralDataUpdate);
                UpdateChild.UpdateChildObject(postJobSheetTruckingSeaAirRequestAirAndSea.Attachment, oJobSheetTruckingGeneralDataUpdate, "ATTACHMENT_SEA_AIR", "LineId", jobSheetDocEntry);
                UpdateChild.UpdateChildObject(postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineCosting, oJobSheetTruckingGeneralDataUpdate, "TB_JOBSHEET_COSTING", "LineId", jobSheetDocEntry);
                UpdateChild.UpdateChildObject(postJobSheetTruckingSeaAirRequestAirAndSea.ItemLineRevenue, oJobSheetTruckingGeneralDataUpdate, "TB_JOBSHEET_SELLING", "LineId", jobSheetDocEntry);
                oJobSheetTruckingGeneralServiceUpdate.Update(oJobSheetTruckingGeneralDataUpdate);
                #endregion
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = jobSheetDocEntry;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                _DocEntry = "0";
                if (oCompany.InTransaction == true) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
