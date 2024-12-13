using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.CreditNote
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry="";
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
                Documents oCreditNotes = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
                oCreditNotes.DocObjectCode = BoObjectTypes.oCreditNotes;
                oCreditNotes.CardCode = postARCreditMemoRequest.CustomerCode;
                oCreditNotes.DocCurrency = postARCreditMemoRequest.BpCurrency;
                oCreditNotes.DocDate = DateTime.Today;
                oCreditNotes.Series = Convert.ToInt32(GetQuery.GetValueByID("GetSeriesARCreditNoteByType", "SERIES", "CreditNote", DateTime.UtcNow.ToString("yyyy-MM-dd"), storeType: Connection.GetRecordByDataTable.StoreType.Transaction));
                oCreditNotes.UserFields.Fields.Item("U_UserCreate").Value = postARCreditMemoRequest.CreateUser;
                oCreditNotes.UserFields.Fields.Item("U_REFINV").Value = postARCreditMemoRequest.BaseEntry;
                oCreditNotes.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString()
                                                                             + DateTime.Now.Month.ToString()
                                                                             + DateTime.Now.Year.ToString()
                                                                             + DateTime.Now.DayOfYear.ToString()
                                                                             + DateTime.Now.Hour.ToString()
                                                                             + DateTime.Now.Minute.ToString()
                                                                             + DateTime.Now.Second.ToString()
                                                                             + DateTime.Now.Millisecond.ToString());

                foreach (var a in postARCreditMemoRequest.Lines)
                {
                    //oCreditNotes.Lines.BaseEntry = Convert.ToInt32(postARCreditMemoRequest.BaseEntry);
                    //oCreditNotes.Lines.BaseType = 13;
                    //oCreditNotes.Lines.BaseLine = a.LineNum;
                    oCreditNotes.Lines.ItemCode = a.ItemCode;
                    oCreditNotes.Lines.TaxCode = a.Vat;
                    oCreditNotes.Lines.UnitPrice = a.Paid;
                    oCreditNotes.Lines.Price = a.Paid;
                    oCreditNotes.Lines.Quantity = 1;
                    oCreditNotes.Lines.DiscountPercent = 0;
                    oCreditNotes.Lines.CostingCode = postARCreditMemoRequest.CostCenter;
                    oCreditNotes.Lines.UserFields.Fields.Item("U_Remark").Value = ClearEmptyString.clearEmptyString(a.Remark);
                    oCreditNotes.Lines.UserFields.Fields.Item("U_RefInv").Value = ClearEmptyString.clearEmptyString(a.LineNum.ToString());
                    oCreditNotes.Lines.WTLiable = BoYesNoEnum.tYES;
                    oCreditNotes.Lines.Add();
                }
                //if (Convert.ToInt32(GetQuery.GetValueByID("GetWTAmount", "WTCount", postARCreditMemoRequest.BaseEntry)) != 0)
                //{
                //    oCreditNotes.WithholdingTaxData.BaseDocEntry = Convert.ToInt32(postARCreditMemoRequest.BaseEntry);
                //    oCreditNotes.WithholdingTaxData.BaseDocType = 13;
                //    oCreditNotes.WithholdingTaxData.Add();
                //}

                var retval = oCreditNotes.Add();
                if (retval == 0)
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
                var SODocEntry= oCompany.GetNewObjectKey();
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
                stage.Description = "AR Credit Note";
                stage.IsFinished = BoYesNoEnum.tNO;
                PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                PM_DocumentData document = documentsCollection.Add();
                document.StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                document.DocType = PMDocumentTypeEnum.pmdt_DocumentDraft;
                document.DocEntry = Convert.ToInt32(SODocEntry);
                pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                #endregion
                #endregion
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                _ErroreCode = 0;
                _MessageErrore = "";
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                if (oCompany.InTransaction)
                    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
