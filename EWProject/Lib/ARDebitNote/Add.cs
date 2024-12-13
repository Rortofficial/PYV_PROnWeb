using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.ARDebitNote
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Add(PostARCreditMemoRequest postARCreditMemoRequest, Company _company)
        {
            _Add(postARCreditMemoRequest, _company);
        }
        private void _Add(PostARCreditMemoRequest postARCreditMemoRequest, Company oCompany)
        {
            try
            {
                if (oCompany.InTransaction)
                    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                oCompany.StartTransaction();
                #region Code
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                oCompany.StartTransaction();
                Documents oCreditNotes = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
                oCreditNotes.DocObjectCode=BoObjectTypes.oCreditNotes;
                oCreditNotes.CardCode = postARCreditMemoRequest.CustomerCode;
                oCreditNotes.UserFields.Fields.Item("U_REFINV").Value = postARCreditMemoRequest.BaseEntry;
                oCreditNotes.UserFields.Fields.Item("U_CreditMemoType").Value = "Debit";
                oCreditNotes.DocDate = DateTime.Today;
                oCreditNotes.Series = Convert.ToInt32(GetQuery.GetValueByID("GetSeriesARCreditNoteByType", "SERIES", "DebitNote", DateTime.UtcNow.ToString("yyyy-MM-dd"), storeType: Connection.GetRecordByDataTable.StoreType.Transaction));
                oCreditNotes.UserFields.Fields.Item("U_UserCreate").Value = ClearEmptyString.clearEmptyString(postARCreditMemoRequest.CreateUser);
                oCreditNotes.DocCurrency = postARCreditMemoRequest.BpCurrency; //= "THS";
                oCreditNotes.NumAtCard = (DateTime.Now.Day.ToString()
                                        + DateTime.Now.Month.ToString()
                                        + DateTime.Now.Year.ToString()
                                        + DateTime.Now.DayOfYear.ToString()
                                        + DateTime.Now.Hour.ToString()
                                        + DateTime.Now.Minute.ToString()
                                        + DateTime.Now.Second.ToString()
                                        + DateTime.Now.Millisecond.ToString());
                var i = 0;
                foreach (var a in postARCreditMemoRequest.Lines)
                {
                    oCreditNotes.Lines.ItemCode = a.ItemCode;
                    oCreditNotes.Lines.Quantity = -1;
                    oCreditNotes.Lines.UnitPrice = a.Paid;
                    oCreditNotes.Lines.Price = a.Paid;
                    //oCreditNotes.Lines.Currency = "THS";
                    if (String.IsNullOrEmpty(a.Vat))
                    {
                        oCreditNotes.Lines.VatGroup = "S00";
                    }
                    else
                    {
                        oCreditNotes.Lines.VatGroup = a.Vat;
                    }
                    oCreditNotes.Lines.ProjectCode = postARCreditMemoRequest.JobNumber;
                    oCreditNotes.Lines.COGSCostingCode = postARCreditMemoRequest.CostCenter;
                    oCreditNotes.Lines.UserFields.Fields.Item("U_Remark").Value = ClearEmptyString.clearEmptyString(a.Remark);
                    a.LineNum = i;
                    oCreditNotes.Lines.Add();
                    i++;
                }
                //oCreditNotes.WithholdingTaxData.BaseDocEntry = Convert.ToInt32(postARCreditMemoRequest.BaseEntry);
                //oCreditNotes.WithholdingTaxData.BaseDocType = 13;
                //oCreditNotes.WithholdingTaxData.Add();
                var RetVal = oCreditNotes.Add();
                var creditNotesDocEntry = oCompany.GetNewObjectKey();
                if (RetVal == 0)
                {
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = oCompany.GetNewObjectKey();
                }
                else
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                    _DocEntry = "0";
                    return;
                }
                #region AddLine Project Management Purchase Request
                var absEntry = Convert.ToInt32(GetQuery.GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", postARCreditMemoRequest.JobNumber));
                CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
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
                stage.Description = "AR Debit Note";
                stage.IsFinished = BoYesNoEnum.tNO;
                PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                PM_DocumentData document = documentsCollection.Add();
                document.StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                document.DocType = PMDocumentTypeEnum.pmdt_DocumentDraft;
                document.DocEntry = Convert.ToInt32(creditNotesDocEntry);
                pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                #endregion
                #endregion
                _DocEntry = oCompany.GetNewObjectKey();
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                _DocEntry = "0";
            }
        }
    }
}
