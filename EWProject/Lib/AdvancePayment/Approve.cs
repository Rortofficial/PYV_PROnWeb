using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Request;
using Client.Models.Response;
using SAPbobsCOM;
using System.Data;

namespace Client.Lib.AdvancePayment
{
    public class Approve
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry=0;
        public Approve(string docNum, string RemarksReson,string approveBy, string CostCenter, Company _company)
        {
            _Approve(docNum, RemarksReson,approveBy, CostCenter, _company);
        }
        private void _Approve(string docNum, string RemarksReson,string approveBy, string CostCenter, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Code
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);  
                oCompany.StartTransaction();
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                #region Purchae Request
                var purchaseRequest = new Models.Request.PurchaseRequest();
                purchaseRequest = GetDetailInformationDocumentApproveResponseAsync(docNum).Result.Data;
                //var jobNo = "";
                //if (purchaseRequest.JobNo != "") jobNo = GetValueByID("GETPROJECTNUMBERBYDOCENTRY", "DOCENTRY", purchaseRequest.JobNo);
                Documents PurchaseRequest = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseRequest);
                PurchaseRequest.DocType = BoDocumentTypes.dDocument_Items;
                PurchaseRequest.ReqType = 171;
                PurchaseRequest.ReqCode = purchaseRequest.IssueBy.ToString();
                PurchaseRequest.DocDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                PurchaseRequest.TaxDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                PurchaseRequest.DocDueDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                PurchaseRequest.RequriedDate = Convert.ToDateTime(purchaseRequest.DueDate);
                PurchaseRequest.Comments = purchaseRequest.Remarks;
                PurchaseRequest.UserFields.Fields.Item("U_WEBID").Value = purchaseRequest.RefNo;
                PurchaseRequest.UserFields.Fields.Item("U_DEPARTMENT").Value = ClearEmptyString.clearEmptyString(purchaseRequest.Lines.FirstOrDefault().DistributionRule);
                if (purchaseRequest.PRType == "PRAD")
                {
                    if (purchaseRequest.JobNo != "")
                    {
                        PurchaseRequest.UserFields.Fields.Item("U_PRAD_TYPE").Value = "ADV";
                    }
                    else
                    {
                        PurchaseRequest.UserFields.Fields.Item("U_PRAD_TYPE").Value = "LP";
                    }
                }
                else if (purchaseRequest.PRType == "PRCOS")
                {
                    PurchaseRequest.UserFields.Fields.Item("U_PRAD_TYPE").Value = "COS";
                }
                else
                {
                    PurchaseRequest.UserFields.Fields.Item("U_PRAD_TYPE").Value = "AM";
                }
                if (purchaseRequest.Series != "")
                {
                    PurchaseRequest.Series = Convert.ToInt32(GetQuery.GetValueByID(type: "GetSeriesDocNumPurchaseRequestByIssueDate", field: "SERIES", purchaseRequest.IssueDate));
                }
                ////PurchaseRequest.DocCurrency = purchaseRequest.AmountCurrency;
                //PurchaseRequest.Project = purchaseRequest.JobNo;
                foreach (var a in purchaseRequest.Lines)
                {
                    //var z= (Convert.ToDouble(a.Amount) < 0) ? -1 : 1;
                    PurchaseRequest.Lines.ItemCode = a.ItemCode;
                    PurchaseRequest.Lines.LineVendor = purchaseRequest.VendorCode;
                    PurchaseRequest.Lines.Quantity = 1;
                    PurchaseRequest.Lines.Price = Convert.ToDouble(a.Amount);
                    //PurchaseRequest.Lines.Currency = purchaseRequest.AmountCurrency;
                    if (a.VatCode!="")
                    {
                        PurchaseRequest.Lines.VatGroup = a.VatCode;
                    }
                    else
                    {
                        PurchaseRequest.Lines.VatGroup = "P00";
                    }
                    PurchaseRequest.Lines.UserFields.Fields.Item("U_Remark").Value = (a.remark == null) ? "" : a.remark;
                    PurchaseRequest.Lines.UserFields.Fields.Item("U_RefInv").Value = ClearEmptyString.clearEmptyString(a.RefInv);
                    PurchaseRequest.Lines.ProjectCode = a.JobNo == "" ? purchaseRequest.JobNo : a.JobNo;
                    if(!string.IsNullOrEmpty(a.DistributionRule)) PurchaseRequest.Lines.COGSCostingCode = a.DistributionRule; else PurchaseRequest.Lines.COGSCostingCode = CostCenter;
                    if (a.ServiceTypeID != "")
                    {
                        PurchaseRequest.Lines.UoMEntry = Convert.ToInt32(a.ServiceTypeID);
                    }
                    a.LineNumPR = PurchaseRequest.Lines.LineNum;
                    PurchaseRequest.Lines.Add();
                }
                int RetVal = PurchaseRequest.Add();
                if (RetVal != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                    //oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    return;
                }
                var PRDocEntry = oCompany.GetNewObjectKey();
                var PRDocNum = GetDocNum(PRDocEntry, "OPRQ", oCompany);
                #region Purchase Order
                Documents SO_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                SO_invoice.CardCode = purchaseRequest.VendorCode;
                SO_invoice.DocDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                SO_invoice.TaxDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                SO_invoice.DocDueDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                SO_invoice.RequriedDate = Convert.ToDateTime(purchaseRequest.DueDate);
                SO_invoice.DocType = BoDocumentTypes.dDocument_Items;
                SO_invoice.DocCurrency = purchaseRequest.AmountCurrency;
                SO_invoice.Comments = purchaseRequest.Remarks;
                //SO_invoice.Project = purchaseRequest.JobNo;
                SO_invoice.UserFields.Fields.Item("U_PROJECT_NO").Value = ClearEmptyString.clearEmptyString(purchaseRequest.JobNo);
                SO_invoice.Series = Convert.ToInt32(GetQuery.GetValueByID(type: "GetSeriesDocNumPurchaseOrderByIssueDate", field: "SERIES", purchaseRequest.IssueDate));
                //SO_invoice.DocNum = PRDocNum;
                var i = 0;
                foreach (var a in purchaseRequest.Lines)
                {
                    SO_invoice.Lines.BaseEntry = Convert.ToInt32(PRDocEntry);
                    SO_invoice.Lines.BaseType = 1470000113;
                    SO_invoice.Lines.BaseLine = i;
                    SO_invoice.Lines.ItemCode = a.ItemCode;
                    SO_invoice.Lines.Quantity = 1;//(Convert.ToDouble(a.Amount) < 0) ? -1 : 1;
                    SO_invoice.Lines.UnitPrice = Convert.ToDouble(a.Amount); //(Convert.ToDouble(a.Amount) < 0) ? (Convert.ToDouble(a.Amount)) * -1 : Convert.ToDouble(a.Amount);
                    SO_invoice.Lines.VatGroup = a.VatCode;
                    SO_invoice.Lines.UserFields.Fields.Item("U_Remark").Value = (a.remark == null) ? "" : a.remark;
                    SO_invoice.Lines.UserFields.Fields.Item("U_RefInv").Value = ClearEmptyString.clearEmptyString(a.RefInv);
                    SO_invoice.Lines.ProjectCode = a.JobNo == "" ? purchaseRequest.JobNo : a.JobNo;
                    if (!string.IsNullOrEmpty(a.DistributionRule)) SO_invoice.Lines.COGSCostingCode = a.DistributionRule; else PurchaseRequest.Lines.COGSCostingCode = CostCenter;
                    a.LineNumPO = i;
                    SO_invoice.Lines.Add();
                    i++;
                }
                int RetValSO = SO_invoice.Add();
                if (RetValSO != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                    //oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);

                    return;
                }
                var soDocEntry = oCompany.GetNewObjectKey();
                #endregion
                #endregion
                if (purchaseRequest.JobNo != "")
                {
                    #region AddLine Project Management Purchase Request
                    var absEntry = GetQuery.GetValueByID("GETPROJECTNUMBERBYDOCENTRYAPPROVE", "DOCENTRY", purchaseRequest.JobNo);
                    CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                    ProjectManagementService pmgServiceUpdateProjectManagement = (ProjectManagementService)oCompServUpdateProject.GetBusinessService(ServiceTypes.ProjectManagementService);
                    PM_ProjectDocumentParams projectToUpdateParam = (PM_ProjectDocumentParams)pmgServiceUpdateProjectManagement.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentParams);
                    projectToUpdateParam.AbsEntry = Convert.ToInt32(absEntry);
                    PM_ProjectDocumentData projectUpdateProjectManagement = pmgServiceUpdateProjectManagement.GetProject(projectToUpdateParam);
                    PM_StagesCollection stagesCollection = projectUpdateProjectManagement.PM_StagesCollection;
                    PM_StageData stage = stagesCollection.Add();
                    //Purchase Request
                    stage.StageType = 1;
                    stage.StartDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                    //stage.CloseDate = stage.StartDate.AddDays(30);
                    stage.Task = 1;
                    stage.Description = "Purchase Request";
                    //stage.ExpectedCosts = 250;
                    //stage.PercentualCompletness = 8;
                    stage.IsFinished = BoYesNoEnum.tNO;
                    //stage.StageOwner = 5;
                    //stage.DependsOnStage1 = 1;
                    //stage.StageDependency1Type = SAPbobsCOM.StageDepTypeEnum.sdt_Project;
                    //stage.DependsOnStageID1 = 1;
                    PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                    PM_DocumentData document = documentsCollection.Add();
                    //document.StageID = Convert.ToInt32(GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                    //document.DocType = PMDocumentTypeEnum.pmdt_PurchaseRequest;
                    //document.DocEntry = Convert.ToInt32(PRDocEntry);
                    //End Purchase Request
                    //Sale Order
                    //document = documentsCollection.Add();
                    var StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry));
                    document.StageID = StageID;
                    document.DocType = PMDocumentTypeEnum.pmdt_PurchaseRequest;
                    document.DocEntry = Convert.ToInt32(PRDocEntry);

                    foreach (var a in purchaseRequest.Lines)
                    {
                        PM_DocumentData documentPO = documentsCollection.Add();
                        documentPO.StageID = StageID;
                        documentPO.DocType = PMDocumentTypeEnum.pmdt_PurchaseOrder;
                        documentPO.DocEntry = Convert.ToInt32(soDocEntry);
                        documentPO.LineNumber = a.LineNumPO;
                    }
                    //End Sale Order
                    pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                    #endregion
                }
                else if (purchaseRequest.Lines.Where(x => x.JobNo != "" && x.JobNo != null).Count() != 0)
                {
                    #region AddLine Project Management Purchase Request

                    //var az = purchaseRequest.Lines.DistinctBy(x => x.JobNo);
                    foreach (var a in purchaseRequest.Lines.DistinctBy(x => x.JobNo).Where(x => x.JobNo != ""))
                    {
                        var absEntry = GetQuery.GetValueByID("GETPROJECTNUMBERBYDOCENTRYAPPROVE", "DOCENTRY", a.JobNo);
                        CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                        ProjectManagementService pmgServiceUpdateProjectManagement = (ProjectManagementService)oCompServUpdateProject.GetBusinessService(ServiceTypes.ProjectManagementService);
                        PM_ProjectDocumentParams projectToUpdateParam = (PM_ProjectDocumentParams)pmgServiceUpdateProjectManagement.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentParams);
                        projectToUpdateParam.AbsEntry = Convert.ToInt32(absEntry);
                        PM_ProjectDocumentData projectUpdateProjectManagement = pmgServiceUpdateProjectManagement.GetProject(projectToUpdateParam);
                        PM_StagesCollection stagesCollection = projectUpdateProjectManagement.PM_StagesCollection;
                        PM_StageData stage = stagesCollection.Add();
                        //Purchase Request
                        stage.StageType = 1;
                        stage.StartDate = Convert.ToDateTime(purchaseRequest.IssueDate);
                        //stage.CloseDate = stage.StartDate.AddDays(30);
                        stage.Task = 1;
                        stage.Description = "Purchase Request";
                        //stage.ExpectedCosts = 250;
                        //stage.PercentualCompletness = 8;
                        stage.IsFinished = BoYesNoEnum.tNO;
                        //stage.StageOwner = 5;
                        //stage.DependsOnStage1 = 1;
                        //stage.StageDependency1Type = SAPbobsCOM.StageDepTypeEnum.sdt_Project;
                        //stage.DependsOnStageID1 = 1;
                        PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                        PM_DocumentData document = documentsCollection.Add();
                        //document.StageID = Convert.ToInt32(GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                        //document.DocType = PMDocumentTypeEnum.pmdt_PurchaseRequest;
                        //document.DocEntry = Convert.ToInt32(PRDocEntry);
                        //End Purchase Request
                        //Sale Order
                        //document = documentsCollection.Add();
                        var StageID = Convert.ToInt32(GetQuery.GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry));
                        document.StageID = StageID;
                        document.DocType = PMDocumentTypeEnum.pmdt_PurchaseRequest;
                        document.DocEntry = int.Parse(PRDocEntry);
                        document.LineNumber = a.LineNumPO;
                        PM_DocumentData documentPO = documentsCollection.Add();
                        documentPO.StageID = StageID;
                        documentPO.DocType = PMDocumentTypeEnum.pmdt_PurchaseOrder;
                        documentPO.DocEntry = Convert.ToInt32(soDocEntry);
                        documentPO.LineNumber = a.LineNumPO;
                        //End Sale Order
                        pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);

                    }
                    #endregion
                }
                #region Update User Define Field of ConfirmBookingSheet to Blank
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("ADVANCEPAYMENT");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", docNum);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_ApproveStatus", "A");
                oConfirmBookingGeneralDataUpdate.SetProperty("U_PRDocEntry", PRDocEntry);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_REASON", ClearEmptyString.clearEmptyString(RemarksReson));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_ApproveBy", ClearEmptyString.clearEmptyString(approveBy));
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                #endregion
                #endregion
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
        private Task<GetDetailInformationDocumentApproveResponse> GetDetailInformationDocumentApproveResponseAsync(string docNum)
        {
            try
            {
                var listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "Header",
                             docNum,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var lines = new List<PurchaseRequestLine>();
                    foreach (DataRow dataRowLine in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "Lines",
                             docNum,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        lines.Add(new PurchaseRequestLine
                        {
                            ItemCode = dataRowLine["ITEMCODE"].ToString()!,
                            ItemName = dataRowLine["ITEMNAME"].ToString()!,
                            Amount = Convert.ToDecimal(dataRowLine["PRICE"].ToString()),
                            Origin = dataRowLine["ORIGIN"].ToString()!,
                            Destination = dataRowLine["DESTINATION"].ToString()!,
                            remark = dataRowLine["REMARKS"].ToString()!,
                            ServiceType = dataRowLine["SERVICETYPE"].ToString()!,
                            LineNumPO = Convert.ToInt32(dataRowLine["LINENUMPO"].ToString()),
                            LineNumAD = Convert.ToInt32(dataRowLine["LINENUMAD"].ToString()),
                            LineNumPR = Convert.ToInt32(dataRowLine["LINENUMPR"].ToString()),
                            DistributionRule = dataRowLine["DistributionRule"].ToString(),
                            RefInv = dataRowLine["RefInv"].ToString(),
                            VatCode = dataRowLine["TaxCode"].ToString(),
                            JobNo = dataRowLine["JobNo"].ToString(),
                        });
                    }
                    listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest
                    {
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        RefNo = dataRow["DOCNUM"].ToString()!,
                        JobNo = dataRow["PROJECTNUMBER"].ToString()!,
                        IssueDate = dataRow["ISSUEDATE"].ToString(),
                        DueDate = dataRow["DUEDATE"].ToString(),
                        VendorCode = dataRow["VENDORCODE"].ToString()!,
                        VendorName = dataRow["VENDORNAME"].ToString()!,
                        AmountCurrency = dataRow["CURRENCY"].ToString()!,
                        IssueBy = Convert.ToInt32(dataRow["EMPLOYEEID"].ToString()),
                        IssueByName = dataRow["EMPLOYEENAME"].ToString(),
                        GrandTotal = Convert.ToDecimal(dataRow["AMOUNT"].ToString()),
                        AmountTHB = Convert.ToDecimal(dataRow["AMOUNTTHB"].ToString()),
                        BankAccount = dataRow["BANKACCOUNT"].ToString()!,
                        BranchName = dataRow["BRANCH"].ToString()!,
                        BankName = dataRow["BANKCOUNTRY"].ToString()!,
                        Country = dataRow["BANKNAME"].ToString()!,
                        SwiftCode = dataRow["SWIFTCODE"].ToString()!,
                        Remarks = dataRow["REMARKS"].ToString()!,
                        DocNum = dataRow["DOCNUMPR"].ToString()!,
                        Reason = dataRow["REASON"].ToString(),
                        PRDocEntry = dataRow["DOCENTRYPR"].ToString(),
                        PRDocNum = dataRow["DOCNUMPRAD"].ToString(),
                        Series = dataRow["PRSeries"].ToString(),
                        PRType = dataRow["AdvanceType"].ToString(),
                        MaxLineNum = Convert.ToInt32(dataRow["MAXROWPO"].ToString()),
                        DocEntryPurchaseRequest = Convert.ToInt32(dataRow["DOCENTRYPURCHASEREQUEST"].ToString()),
                        Lines = lines
                    };
                }
                return Task.FromResult(new GetDetailInformationDocumentApproveResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDetailViewSalesQuotation,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetDetailInformationDocumentApproveResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        private int GetDocNum(string DocEntry, string sTable, SAPbobsCOM.Company oCompanyGet)
        {
            int result = 0;
            SAPbobsCOM.Company oCompany;
            try
            {

                oCompany = oCompanyGet;
                SAPbobsCOM.Recordset? oRS = null;
                string sqlStr = "SELECT \"DocNum\" FROM \"" + sTable + "\" WHERE \"DocEntry\"='" + DocEntry + "'";
                oRS = (SAPbobsCOM.Recordset?)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery(sqlStr);
                if (oRS.RecordCount > 0)
                {
                    result = (int)oRS.Fields.Item(0).Value;
                }
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                result = 0;
            }
            return result;
        }
    }
}
