using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using SAPbobsCOM;
using System.Data;

namespace Client.Lib.CreditNoteSeaAir
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PurchaseRequest postPurchaseRequestRequest, string CostCenter, Company _company)
        {
            _Add(postPurchaseRequestRequest, CostCenter, _company);
        }
        private void _Add(PurchaseRequest postPurchaseRequestRequest, string CostCenter, Company oCompany)
        {
            try
            {
                if (oCompany.InTransaction)
                    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                oCompany.StartTransaction();
                JournalVouchers JVEntry = (JournalVouchers)oCompany.GetBusinessObject(BoObjectTypes.oJournalVouchers);
                JVEntry.JournalEntries.Series = Convert.ToInt32(postPurchaseRequestRequest.Series);
                JVEntry.JournalEntries.DueDate = Convert.ToDateTime(postPurchaseRequestRequest.IssueDate);
                JVEntry.JournalEntries.ReferenceDate = Convert.ToDateTime(postPurchaseRequestRequest.IssueDate);
                JVEntry.JournalEntries.Memo = postPurchaseRequestRequest.Remarks;
                JVEntry.JournalEntries.TransactionCode = "R003";
                JVEntry.JournalEntries.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString()).ToString();
                JVEntry.JournalEntries.UserFields.Fields.Item("U_USERID").Value = postPurchaseRequestRequest.IssueByName;
                JVEntry.JournalEntries.UserFields.Fields.Item("U_REFINV").Value = postPurchaseRequestRequest.ArInvoice;
                JVEntry.JournalEntries.Lines.ShortName = postPurchaseRequestRequest.VendorCode;
                JVEntry.JournalEntries.Lines.Credit = Convert.ToDouble(postPurchaseRequestRequest.Lines.Sum(x => x.Amount));
                if (string.IsNullOrEmpty(postPurchaseRequestRequest.Lines.FirstOrDefault().DistributionRule))
                    JVEntry.JournalEntries.Lines.CostingCode = postPurchaseRequestRequest.Lines.FirstOrDefault().DistributionRule;
                else
                    JVEntry.JournalEntries.Lines.CostingCode = CostCenter;
                JVEntry.JournalEntries.Lines.ProjectCode = postPurchaseRequestRequest.JobNo;
                JVEntry.JournalEntries.Lines.Add();
                foreach (var a in postPurchaseRequestRequest.Lines)
                {
                    JVEntry.JournalEntries.Lines.AccountCode = a.ServiceType;
                    JVEntry.JournalEntries.Lines.UserFields.Fields.Item("U_CardCode").Value = a.ItemCode;
                    JVEntry.JournalEntries.Lines.Debit = Convert.ToDouble(a.Amount);
                    JVEntry.JournalEntries.Lines.LineMemo = a.remark;
                    if (string.IsNullOrEmpty(a.DistributionRule))
                        JVEntry.JournalEntries.Lines.CostingCode = a.DistributionRule;
                    else
                        JVEntry.JournalEntries.Lines.CostingCode = CostCenter;
                    JVEntry.JournalEntries.Lines.TaxGroup = a.VatCode;
                    JVEntry.JournalEntries.Lines.ProjectCode = postPurchaseRequestRequest.JobNo;
                    JVEntry.JournalEntries.Lines.Add();
                }
                int RetVal = JVEntry.Add();
                if (RetVal != 0)
                {
                    if (oCompany.InTransaction)
                        oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                    return;
                }
                _DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey().ToString().Replace("\t1", "").Trim());
                #region Can not add to Stage in Project because it's Journal Entry
                //var absEntry = GetQuery.GetValueByID("GETPROJECTNUMBERBYDOCENTRYAPPROVE", "DOCENTRY", postPurchaseRequestRequest.JobNo);
                //CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                //ProjectManagementService pmgServiceUpdateProjectManagement = (ProjectManagementService)oCompServUpdateProject.GetBusinessService(ServiceTypes.ProjectManagementService);
                //PM_ProjectDocumentParams projectToUpdateParam = (PM_ProjectDocumentParams)pmgServiceUpdateProjectManagement.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentParams);
                //projectToUpdateParam.AbsEntry = Convert.ToInt32(absEntry);
                //PM_ProjectDocumentData projectUpdateProjectManagement = pmgServiceUpdateProjectManagement.GetProject(projectToUpdateParam);
                //PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                //var StageID = Convert.ToInt32(GetQuery.GetValueByID("GETSTAGEBYJOBNUMBER", "STAGEID", absEntry));
                //PM_DocumentData document;
                //document = documentsCollection.Add();
                //document.StageID = StageID;
                //document.DocType = PMDocumentTypeEnum.pmdt_ManualJournalEntry;
                //document.DocEntry = Convert.ToInt32(_DocEntry);
                //pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                #endregion
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            catch (Exception ex) 
            { 
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack); 
                _ErroreCode = ex.GetHashCode(); 
                _MessageErrore = ex.Message; 
            }
        }
    }
}
