using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.SalesCommission
{
    public class Approve
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Approve(PostSalesCommissions postSalesCommissions, string CostCenter, Company _company)
        {
            _Approve(postSalesCommissions, CostCenter, _company);
        }
        private void _Approve(PostSalesCommissions postSalesCommissions, string CostCenter, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Code
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                oCompany.StartTransaction();
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                #region Purchase Request
                var webID = (DateTime.Now.Day.ToString()
                                        + DateTime.Now.Month.ToString()
                                        + DateTime.Now.Year.ToString()
                                        + DateTime.Now.DayOfYear.ToString()
                                        + DateTime.Now.Hour.ToString()
                                        + DateTime.Now.Minute.ToString()
                                        + DateTime.Now.Second.ToString()
                                        + DateTime.Now.Millisecond.ToString()).ToString();
                Documents PurchaseRequest = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseRequest);
                PurchaseRequest.DocType = BoDocumentTypes.dDocument_Items;
                PurchaseRequest.ReqType = 171;
                PurchaseRequest.ReqCode = postSalesCommissions.UserID.ToString();
                PurchaseRequest.DocDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                PurchaseRequest.TaxDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                PurchaseRequest.DocDueDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                PurchaseRequest.RequriedDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                PurchaseRequest.DocType = BoDocumentTypes.dDocument_Service;
                PurchaseRequest.UserFields.Fields.Item("U_Commission").Value = postSalesCommissions.DocEntry;
                PurchaseRequest.UserFields.Fields.Item("U_WEBID").Value = webID;
                foreach (var a in postSalesCommissions.Lines)
                {
                    PurchaseRequest.Lines.AccountCode = a.AccountCode;
                    PurchaseRequest.Lines.LineVendor = postSalesCommissions.VendorCode;
                    PurchaseRequest.Lines.LineTotal = a.TotalSaleCommission;
                    PurchaseRequest.Lines.RequiredDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                    PurchaseRequest.Lines.VatGroup = "P00";
                    PurchaseRequest.Lines.ProjectCode = a.JobNumber;
                    PurchaseRequest.Lines.COGSCostingCode = CostCenter;
                    PurchaseRequest.Lines.Add();
                }
                int RetVal = PurchaseRequest.Add();
                if (RetVal != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                    if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    return;
                }
                var PRDocEntry = oCompany.GetNewObjectKey();
                #endregion
                #region Purchase Order
                Documents SO_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                SO_invoice.CardCode = postSalesCommissions.VendorCode;
                SO_invoice.DocDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                SO_invoice.TaxDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                SO_invoice.DocDueDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                SO_invoice.RequriedDate = Convert.ToDateTime(postSalesCommissions.IssueDate);
                SO_invoice.DocType = BoDocumentTypes.dDocument_Service;
                SO_invoice.UserFields.Fields.Item("U_Commission").Value = postSalesCommissions.DocEntry;
                SO_invoice.UserFields.Fields.Item("U_WEBID").Value = webID;
                SO_invoice.UserFields.Fields.Item("U_PROJECT_NO").Value = ClearEmptyString.clearEmptyString(postSalesCommissions.NumAtCard);
                var i = 0;
                foreach (var a in postSalesCommissions.Lines)
                {
                    SO_invoice.Lines.BaseLine = i;
                    SO_invoice.Lines.BaseEntry = Convert.ToInt32(PRDocEntry);
                    SO_invoice.Lines.BaseType = 1470000113;
                    SO_invoice.Lines.AccountCode = a.AccountCode;
                    SO_invoice.Lines.LineTotal = a.TotalSaleCommission;
                    SO_invoice.Lines.VatGroup = "P00";
                    SO_invoice.Lines.ProjectCode = a.JobNumber;
                    SO_invoice.Lines.COGSCostingCode = CostCenter;
                    a.LineNumPO = i;
                    SO_invoice.Lines.Add();
                    i++;
                }
                int RetValSO = SO_invoice.Add();
                if (RetValSO != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                    if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    return;
                }
                var soDocEntry = oCompany.GetNewObjectKey();
                #endregion
                #region Add PO to Project Managment
                foreach (var a in postSalesCommissions.Lines)
                {
                    #region AddLine Project Management Purchase Request
                    var absEntry = GetQuery.GetValueByID("GETPROJECTNUMBERBYDOCENTRYAPPROVE", "DOCENTRY", a.JobNumber);
                    CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                    ProjectManagementService pmgServiceUpdateProjectManagement = (ProjectManagementService)oCompServUpdateProject.GetBusinessService(ServiceTypes.ProjectManagementService);
                    PM_ProjectDocumentParams projectToUpdateParam = (PM_ProjectDocumentParams)pmgServiceUpdateProjectManagement.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentParams);
                    projectToUpdateParam.AbsEntry = Convert.ToInt32(absEntry);
                    PM_ProjectDocumentData projectUpdateProjectManagement = pmgServiceUpdateProjectManagement.GetProject(projectToUpdateParam);
                    projectUpdateProjectManagement.ProjectStatus = ProjectStatusTypeEnum.pst_Finished;
                    PM_StagesCollection stagesCollection = projectUpdateProjectManagement.PM_StagesCollection;
                    PM_StageData stage = stagesCollection.Add();
                    stage.StageType = 1;
                    stage.StartDate = DateTime.Now;
                    stage.Task = 1;
                    stage.Description = "Purchase Request Commission";
                    stage.IsFinished = BoYesNoEnum.tNO;
                    PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                    PM_DocumentData document = documentsCollection.Add();
                    var StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry));
                    document.StageID = StageID;
                    document.DocType = PMDocumentTypeEnum.pmdt_PurchaseRequest;
                    document.DocEntry = Convert.ToInt32(PRDocEntry);
                    document.LineNumber = a.LineNumPO;

                    PM_DocumentData documentPO = documentsCollection.Add();
                    documentPO.StageID = StageID;
                    documentPO.DocType = PMDocumentTypeEnum.pmdt_PurchaseOrder;
                    documentPO.DocEntry = Convert.ToInt32(soDocEntry);
                    documentPO.LineNumber = a.LineNumPO;
                    pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                    #endregion
                }
                #endregion
                #region Update Approve Draft in TBComission
                GeneralService oGeneralServiceUpdate;
                GeneralData oGeneralDataUpdate;
                GeneralDataParams oGeneralParamsUpdate;
                companyService = oCompany.GetCompanyService();
                oGeneralServiceUpdate = companyService.GetGeneralService("TBCOMMISSION");
                oGeneralParamsUpdate = (GeneralDataParams)oGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParamsUpdate.SetProperty("DocEntry", postSalesCommissions.DocEntry);
                oGeneralDataUpdate = oGeneralServiceUpdate.GetByParams(oGeneralParamsUpdate);
                oGeneralDataUpdate.SetProperty("U_ApproveStatus", "A");
                oGeneralDataUpdate.SetProperty("U_PODocEntry", soDocEntry);
                oGeneralDataUpdate.SetProperty("Remark", ClearEmptyString.clearEmptyString(postSalesCommissions.Reason));
                oGeneralServiceUpdate.Update(oGeneralDataUpdate);
                #endregion
                #endregion
                _ErroreCode = 0;
                _MessageErrore = "";
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
