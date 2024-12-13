using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.JobSheetTrucking
{
    public class UpdateDraft
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public UpdateDraft(UpdateDraftJobSheetTrucking updateJobSheetTruckingEdit, string draftDocument, Company _company)
        {
            _UpdateDraft(updateJobSheetTruckingEdit, draftDocument, _company);
        }
        private void _UpdateDraft(UpdateDraftJobSheetTrucking updateJobSheetTruckingEdit, string draftDocument, Company _company)
        {
            try
            {
                GeneralService oJobSheetTruckingGeneralServiceUpdate;
                GeneralData oJobSheetTruckingGeneralDataUpdate;
                GeneralDataParams oJobSheetTruckingGeneralParamsUpdate;
                CompanyService companyService;
                if (_company.InTransaction) _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                _company.StartTransaction();
                var SODocEntry = "";
                var lineNumDuty = 0;
                if (draftDocument == "1")
                {
                    #region Create Sale Order
                    Documents SO_invoice = (Documents)_company.GetBusinessObject(BoObjectTypes.oOrders);
                    SO_invoice.CardCode = updateJobSheetTruckingEdit.CardCode;
                    var loadingDate = DateTime.Parse(GetQuery.GetValueByID("GetLoadingDate","LoadingDate",updateJobSheetTruckingEdit.ConfirmBookingID.ToString()));
                    SO_invoice.DocDate = loadingDate;
                    SO_invoice.DocDueDate = loadingDate;
                    SO_invoice.TaxDate = loadingDate;
                    SO_invoice.RequriedDate = loadingDate;
                    SO_invoice.DocType = BoDocumentTypes.dDocument_Items;
                    SO_invoice.NumAtCard = updateJobSheetTruckingEdit.JobNo;
                    SO_invoice.ShipToCode = "";
                    SO_invoice.UserFields.Fields.Item("U_RemarkCosting").Value = ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.RemarkForCosting);
                    SO_invoice.UserFields.Fields.Item("U_Remarks").Value = ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.RemarkForSelling);
                    decimal exchangeRate = Convert.ToDecimal(GetQuery.GetValueByID("GetExchangeRateByLoadingDate", "ExchangeRate", updateJobSheetTruckingEdit.ConfirmBookingID.ToString(), updateJobSheetTruckingEdit.CurrencySelling));
                    if (updateJobSheetTruckingEdit.CurrencySelling != "THS" && updateJobSheetTruckingEdit.CurrencySelling != "THB")
                    {
                        SO_invoice.DocRate = Math.Round((double)exchangeRate, 6);
                    }
                    else
                    {
                        SO_invoice.DocCurrency = updateJobSheetTruckingEdit.CurrencySelling;
                    }
                    foreach (var a in updateJobSheetTruckingEdit.JobSheetTruckingSellings)
                    {
                        if (a.Qty != 0)
                        {
                            if (a.ItemType == "Q" &&
                                GetQuery.GetValueByID("GetCurrencyByDocEntrySalesQuotation",
                                                       "SalesQuotationCurrency",
                                                       updateJobSheetTruckingEdit.SaleQuotationDocNum)
                                == updateJobSheetTruckingEdit.CurrencySelling)
                            {
                                SO_invoice.Lines.BaseLine = Convert.ToInt32(a.LineNum) - 1;
                                SO_invoice.Lines.BaseEntry = Convert.ToInt32(updateJobSheetTruckingEdit.SaleQuotationDocNum);
                                SO_invoice.Lines.BaseType = 23;
                                updateJobSheetTruckingEdit.JobSheetTruckingSellings.Where(x => x.LineNum == a.LineNum && x.ItemType == "Q").Select(c => c.ItemType = "").ToList();
                                var index = updateJobSheetTruckingEdit.JobSheetTruckingSellings.IndexOf(a);
                                updateJobSheetTruckingEdit.JobSheetTruckingSellings.Where(x => x.LineNum == a.LineNum).ToList().ForEach(c =>
                                {
                                    c.LineNum = "0";
                                    c.ItemType = "I";
                                });
                                updateJobSheetTruckingEdit.JobSheetTruckingSellings[index].LineNum = a.LineNum;
                                updateJobSheetTruckingEdit.JobSheetTruckingSellings[index].ItemType = "Q";
                            }
                            SO_invoice.Lines.ItemCode = a.ITEMCODE;
                            SO_invoice.Lines.Quantity = a.Qty;
                            if (updateJobSheetTruckingEdit.CurrencySelling != "THB" && updateJobSheetTruckingEdit.CurrencySelling != "THS")
                            {
                                SO_invoice.Lines.Currency = updateJobSheetTruckingEdit.CurrencySelling;
                                SO_invoice.Lines.Rate = Math.Round((double)exchangeRate, 6);
                                SO_invoice.Lines.UnitPrice = Math.Round(a.TOTALPRICE, 7);
                            }
                            else
                            {
                                SO_invoice.Lines.UnitPrice = Math.Round(a.TOTALPRICE, 7);
                            }
                            SO_invoice.Lines.COGSCostingCode = updateJobSheetTruckingEdit.CostCenter;
                            //SO_invoice.Lines.DiscountPercent = 0;
                            SO_invoice.Lines.TaxCode = "S00";
                            if (a.ContainerType != 0)
                            {
                                SO_invoice.Lines.UoMEntry = a.ContainerType;
                            }
                            SO_invoice.Lines.ProjectCode = updateJobSheetTruckingEdit.JobNo;
                            a.LineId = SO_invoice.Lines.LineNum;
                            SO_invoice.Lines.Add();
                        }
                    }
                    if (updateJobSheetTruckingEdit.DutyTaxAmountSelling != 0)
                    {
                        SO_invoice.SpecialLines.LineType = BoDocSpecialLineType.dslt_Text;
                        SO_invoice.SpecialLines.LineText = "Duty Tax";
                        SO_invoice.SpecialLines.AfterLineNumber = updateJobSheetTruckingEdit.JobSheetTruckingSellings.Last(x => x.Qty != 0).LineId;
                        SO_invoice.SpecialLines.Add();
                        SO_invoice.Lines.ItemCode = Configure.DutyTaxCode;
                        SO_invoice.Lines.Quantity = 1;
                        SO_invoice.Lines.Price = updateJobSheetTruckingEdit.DutyTaxAmountSelling;
                        SO_invoice.Lines.ProjectCode = updateJobSheetTruckingEdit.JobNo;
                        SO_invoice.Lines.COGSCostingCode = updateJobSheetTruckingEdit.CostCenter;
                        lineNumDuty = SO_invoice.Lines.LineNum;
                        SO_invoice.Lines.Add();
                    }
                    int RetValSO = SO_invoice.Add();
                    if (RetValSO != 0)
                    {
                        _company.GetLastError(out _ErroreCode, out _MessageErrore);
                        _DocEntry = 0;
                        if (_company.InTransaction) _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                        return;
                    }
                    SODocEntry = _company.GetNewObjectKey();
                    #endregion
                }
                companyService = _company.GetCompanyService();
                oJobSheetTruckingGeneralServiceUpdate = companyService.GetGeneralService("JOBSHEETRUCKING");
                oJobSheetTruckingGeneralParamsUpdate = (GeneralDataParams)oJobSheetTruckingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oJobSheetTruckingGeneralParamsUpdate.SetProperty("DocEntry", updateJobSheetTruckingEdit.DocEntry);
                oJobSheetTruckingGeneralDataUpdate = oJobSheetTruckingGeneralServiceUpdate.GetByParams(oJobSheetTruckingGeneralParamsUpdate);
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_TOTALCOSTING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.TotalCosting.ToString()));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_GRANDTOTALCOSTING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.GrandTotalCosting.ToString()));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_GRANDTOTALCOSTINGUSD", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.GrandTotalCostingUSD.ToString()));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_TOTALSELLING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.TotalSelling.ToString()));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_GRANDTOTALSELLING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.GrandTotalSelling.ToString()));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_GRANDTOTALSELLINGUSD", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.GrandTotalSellingUSD.ToString()));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_TOTALPROFIT", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.Profit.ToString()));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_REMARKSCOSTING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.RemarkForCosting));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_REMAKRSSELLING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.RemarkForSelling));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_COSTINGCONFIRM", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.CostingConfirm));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.UpdateBy));
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_DutyTaxAmountCosting", updateJobSheetTruckingEdit.DutyTaxAmountSelling);
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_DutyTaxAmountSelling", updateJobSheetTruckingEdit.DutyTaxAmountSelling);
                oJobSheetTruckingGeneralDataUpdate.SetProperty("U_REBATE", updateJobSheetTruckingEdit.REBATE);
                if (draftDocument == "1")
                {
                    oJobSheetTruckingGeneralDataUpdate.SetProperty("U_SALESORDERDOCNUM", SODocEntry);
                }
                if (updateJobSheetTruckingEdit.Attachments != null) UpdateChild.UpdateChildObject(updateJobSheetTruckingEdit.Attachments, oJobSheetTruckingGeneralDataUpdate, "ATTACHMENT", "LineId", updateJobSheetTruckingEdit.DocEntry.ToString());
                UpdateChild.UpdateChildObject(updateJobSheetTruckingEdit.JobSheetTruckingCostings, oJobSheetTruckingGeneralDataUpdate, "JOBTRUCKCOSTING", "LineId", updateJobSheetTruckingEdit.DocEntry.ToString());
                UpdateChild.UpdateChildObject(updateJobSheetTruckingEdit.JobSheetTruckingSellings, oJobSheetTruckingGeneralDataUpdate, "JOBTRUCKINGSELLING", "LineId", updateJobSheetTruckingEdit.DocEntry.ToString());
                oJobSheetTruckingGeneralServiceUpdate.Update(oJobSheetTruckingGeneralDataUpdate);
                if (draftDocument == "1")
                {
                    #region AddLine Project Management Purchase Request
                    var absEntry = Convert.ToInt32(GetQuery.GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", updateJobSheetTruckingEdit.JobNo));
                    CompanyService oCompServUpdateProject = _company.GetCompanyService();
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
                    stage.Description = "Job Sheet Trucking";
                    stage.IsFinished = BoYesNoEnum.tNO;
                    PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                    foreach (var a in updateJobSheetTruckingEdit.JobSheetTruckingSellings)
                    {
                        if (a.Qty != 0)
                        {
                            PM_DocumentData document = documentsCollection.Add();
                            document.StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                            document.DocType = PMDocumentTypeEnum.pmdt_SalesOrder;
                            document.DocEntry = Convert.ToInt32(SODocEntry);
                            document.LineNumber = a.LineId;
                        }
                    }
                    if (lineNumDuty != 0)
                    {
                        PM_DocumentData document = documentsCollection.Add();
                        document.StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                        document.DocType = PMDocumentTypeEnum.pmdt_SalesOrder;
                        document.DocEntry = Convert.ToInt32(SODocEntry);
                        document.LineNumber = lineNumDuty;
                    }
                    //End Sale Order
                    pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                    #endregion
                }
                if (_company.InTransaction)
                {
                    _company.EndTransaction(BoWfTransOpt.wf_Commit);
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = updateJobSheetTruckingEdit.DocEntry;
                }
                else
                {
                    _company.GetLastError(out _ErroreCode, out _MessageErrore);
                    _DocEntry = 0;
                    if (_company.InTransaction) _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                if (_company.InTransaction) _company.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
