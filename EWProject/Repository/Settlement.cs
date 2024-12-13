using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Lib.Settlement;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using SAPbobsCOM;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class Settlement : ISettlement
    {
        private int ErrCode;
        private string ErrMsg = null!;
        #region Post
        public Task<PostResponse> AddSettlementDraftAsync(AddSettlementDraftRequest addSettlementDraftRequest)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = Settlements._Add(addSettlementDraftRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
            //}
            //return Task.FromResult(new PostResponse
            //{
            //    ErrorCode = login.LErrCode,
            //    ErrorMsg = login.SErrMsg
            //});
        }
        #endregion
        #region Get

        public Task<ResponseData<List<GetListCustomerJobSheetTrucking>>> getListCustomerJobSheetTruckingResponse(string JobNumber)
        {
            try
            {
                var listGetListCustomerJobSheetTrucking = new List<GetListCustomerJobSheetTrucking>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetListCustomerJobSheetTrucking",
                             JobNumber,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetListCustomerJobSheetTrucking.Add(new GetListCustomerJobSheetTrucking
                    {
                        CardCode = dataRow["CUSTOMERCODE"].ToString()!,
                        CardName = dataRow["CUSTOMERNAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetListCustomerJobSheetTrucking>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetListCustomerJobSheetTrucking
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListCustomerJobSheetTrucking>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetListRefInvByJobSheet>>> getListRefInvByJobSheetResponse(string CardCode, string JobNumber)
        {
            try
            {
                var listGetListRefInvByJobSheet = new List<GetListRefInvByJobSheet>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetListInvoiceRefBaseOnCustomerAndJobNumber",
                             CardCode,
                             JobNumber,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetListRefInvByJobSheet.Add(new GetListRefInvByJobSheet
                    {
                        DocNum = Convert.ToInt32(dataRow["DOCNUM"].ToString()),
                        DocEntry = Convert.ToInt32(dataRow["DOCENTRY"].ToString())
                    });
                }
                return Task.FromResult(new ResponseData<List<GetListRefInvByJobSheet>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetListRefInvByJobSheet
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListRefInvByJobSheet>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetProjectManagmentList>>> GetProjectManagmentListResponseAsync()
        {
            try
            {
                var listGetProjectManagment = new List<GetProjectManagmentList>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETPROJECTMANAGEMENTLISTJOBSHEETTRUCKING",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetProjectManagment.Add(new GetProjectManagmentList
                    {
                        ProjectCode = dataRow["PROJECTCODE"].ToString()!,
                        ProjectName = dataRow["PROJECTNAME"].ToString()!,
                        Active = dataRow["ACTIVE"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetProjectManagmentList>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetProjectManagment
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetProjectManagmentList>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<GetDetailInformationDocumentApproveResponse> GetDetailPurchaseOrderResponseAsync(string docNum)
        {
            try
            {
                var listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailPurchaseOrderByDocEntry",
                             "Header",
                             docNum,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var lines = new List<PurchaseRequestLine>();
                    foreach (DataRow dataRowLine in new GetRecordByDataTable(
                             "GetDetailPurchaseOrderByDocEntry",
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
                            remark = dataRowLine["REMARKS"].ToString()!,
                            ServiceType = dataRowLine["SERVICETYPE"].ToString()!
                        });
                    }
                    listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest
                    {
                        RefNo = dataRow["DOCNUM"].ToString(),
                        PRDocNum = dataRow["DOCNUMPR"].ToString(),
                        DocEntry = dataRow["DOCENTRY"].ToString(),
                        DueDate = dataRow["DUEDATE"].ToString(),
                        IssueDate = dataRow["ISSUEDATE"].ToString(),
                        VendorCode = dataRow["VENDORCODE"].ToString(),
                        VendorName = dataRow["VENDORNAME"].ToString(),
                        AmountCurrency = dataRow["CURRENCY"].ToString(),
                        IssueBy = Convert.ToInt32(dataRow["EMPLOYEEID"].ToString()),
                        IssueByName = dataRow["EMPLOYEENAME"].ToString(),
                        AmountTHB = Convert.ToDecimal(dataRow["AMOUNT"].ToString()),
                        Remarks = dataRow["REMARKS"].ToString(),
                        Lines = lines
                    };
                }
                return Task.FromResult(new GetDetailInformationDocumentApproveResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDetailViewSalesQuotation,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
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
        public Task<ResponseData<List<GetAdvancePaymentClearing>>> GetPurchaseOrderListResponseAsync(string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                var listGetAdvancePaymentClearing = new List<GetAdvancePaymentClearing>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetPurchaseOrderListSettlement",
                             dateFrom,
                             dateTo,
                             type,
                             userID,
                             "").GetResultDataTable().Rows)
                {
                    listGetAdvancePaymentClearing.Add(new GetAdvancePaymentClearing
                    {
                        Row = dataRow["ROWNUM"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        RefNo = dataRow["REFNO"].ToString()!,
                        JobNumber = dataRow["JOBNUMBER"].ToString()!,
                        VendorCode = dataRow["VENDORCODE"].ToString()!,
                        VendorName = dataRow["VENDORNAME"].ToString()!,
                        IssueDate = dataRow["ISSUEDATE"].ToString()!,
                        DocTotal = Convert.ToDouble(dataRow["DOCTOTAL"].ToString()),
                        Remarks = dataRow["REMARKS"].ToString(),
                        PODocEntry = dataRow["PODOCENTRY"].ToString(),
                        Status = dataRow["Status"].ToString(),
                        IsUpdate = dataRow["IsUpdate"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetAdvancePaymentClearing>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetAdvancePaymentClearing
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAdvancePaymentClearing>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<GetDetailInformationDocumentApproveResponse> GetDetailSettlementByDocEntryResponseAsync(string docNum)
        {
            try
            {
                var listGetDetailViewSalesQuotation = QueryData.ConvertDataTable<Models.Request.PurchaseRequest>(new GetRecordByDataTable(
                         "SETTLEMENTDETAILBYDOCENTRY",
                         "Header",
                         docNum,
                         "",
                         "",
                         "").GetResultDataTable())[0];
                listGetDetailViewSalesQuotation.Lines = QueryData.ConvertDataTable<PurchaseRequestLine>(new GetRecordByDataTable(
                            "SETTLEMENTDETAILBYDOCENTRY",
                            "Lines",
                            docNum,
                            "",
                            "",
                            "").GetResultDataTable()); ;
                return Task.FromResult(new GetDetailInformationDocumentApproveResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDetailViewSalesQuotation,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
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
        #endregion
        #region Update
        public Task<PostResponse> PutPurchaseRequestAsync(UpdatePurchaseRequest updatePurchaseRequest, string CostCenter)
        {
            Documents oPurchaseOrder;
            Company oCompany;
            int Retval = 0;
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            oCompany = SAP_Driver_oCompany._CheckingStatusOCompany();
            if(oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            oCompany.StartTransaction();
            try
            {
                updatePurchaseRequest.MaxLineNum = Convert.ToInt32(GetValueByID("GetMaxLineNumInPOForSettlement", "DocEntry", updatePurchaseRequest.DocEntryPO.ToString()));
                oPurchaseOrder = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                if (oPurchaseOrder.GetByKey(updatePurchaseRequest.DocEntryPO) == true)
                {
                    //oPurchaseOrder.Project = "EWTE2340217";
                    oPurchaseOrder.UserFields.Fields.Item("U_IsUpdate").Value = "Y";
                    for (var i = 0; i <= updatePurchaseRequest.MaxLineNum; i++)
                    {
                        oPurchaseOrder.Lines.SetCurrentLine(i);
                        oPurchaseOrder.Lines.DiscountPercent = 100;
                        //oPurchaseOrder.Lines.ProjectCode = "EWTE2340217";
                    }
                    //Retval = oPurchaseOrder.Update();
                    //if (Retval != 0)
                    //{
                    //    oCompany.GetLastError(out ErrCode, out ErrMsg);
                    //    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    //    return Task.FromResult(new PostPurchaseRequestResponse
                    //    {
                    //        ErrorCode = ErrCode,
                    //        ErrorMsg = ErrMsg,
                    //        DocEntry = updatePurchaseRequest.DocEntryPO.ToString()
                    //    });
                    //}
                    //else
                    //{
                    //    oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    //}
                    var projectListnew = new List<CheckProjectList>();
                    foreach (var a in updatePurchaseRequest.Lines)
                    {
                        //oPurchaseOrder.Lines.SetCurrentLine(a.LineNumPO);
                        //oPurchaseOrder.Lines.Delete();
                        //oPurchaseOrder.Lines.SetCurrentLine(a.LineNumPO);
                        //oPurchaseOrder.Lines.UnitPrice = a.Paid;
                        //oPurchaseOrder.Lines.BaseLine = a.LineNumPR;
                        //oPurchaseOrder.Lines.BaseEntry = Convert.ToInt32(updatePurchaseRequest.DocEntryPR);
                        //oPurchaseOrder.Lines.BaseType = 1470000113;
                        oPurchaseOrder.Lines.Add();

                        updatePurchaseRequest.MaxLineNum = updatePurchaseRequest.MaxLineNum + 1;
                        //oPurchaseOrder.Lines.SetCurrentLine(updatePurchaseRequest.MaxLineNum);
                        oPurchaseOrder.Lines.ItemCode = a.ItemCode;
                        oPurchaseOrder.Lines.Quantity = 1;
                        oPurchaseOrder.Lines.UnitPrice = a.Paid;
                        oPurchaseOrder.Lines.VatGroup = "P00";
                        oPurchaseOrder.Lines.UserFields.Fields.Item("U_CustomerCode").Value = ClearEmptyString.clearEmptyString(a.CustomerCode);
                        oPurchaseOrder.Lines.UserFields.Fields.Item("U_TransportationNo").Value = ClearEmptyString.clearEmptyString(a.DeclarationNo);
                        oPurchaseOrder.Lines.UserFields.Fields.Item("U_LinkToARInvoice").Value = ClearEmptyString.clearEmptyString(a.InvNumber);
                        oPurchaseOrder.Lines.UserFields.Fields.Item("U_Remark").Value = ClearEmptyString.clearEmptyString(a.RemarkOrRisk);
                        oPurchaseOrder.Lines.ProjectCode = a.JobNumber;
                        oPurchaseOrder.Lines.COGSCostingCode = CostCenter;
                        a.LineNumPO = updatePurchaseRequest.MaxLineNum;
                        projectListnew = CheckProjectManagement(projectListnew, a.JobNumber, a.LineNumPO);
                    }
                    Retval = oPurchaseOrder.Update();
                    if (Retval != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        //oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        return Task.FromResult(new PostResponse
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = updatePurchaseRequest.DocEntryPO.ToString()
                        });
                    }
                    foreach (var a in projectListnew)
                    {
                        #region AddLine Project Management Purchase Request
                        var absEntry = Convert.ToInt32(GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", a.ProjectManagment));
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
                        stage.Description = "Purchase Order";
                        stage.IsFinished = BoYesNoEnum.tNO;
                        PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                        foreach (var z in a.Lines)
                        {
                            PM_DocumentData document = documentsCollection.Add();
                            document.StageID = Convert.ToInt32(GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                            document.DocType = PMDocumentTypeEnum.pmdt_PurchaseOrder;
                            document.DocEntry = Convert.ToInt32(updatePurchaseRequest.DocEntryPO);
                            document.LineNumber = Convert.ToInt32(z);
                        }

                        //End Sale Order
                        pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                        #endregion
                    }

                    oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = updatePurchaseRequest.DocEntryPO.ToString(),
                    });
                }
                else
                {
                    if(oCompany.InTransaction)
                    {
                        oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    }
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = -1,
                        ErrorMsg = "Invalid DocNum"
                    });
                }
            }
            catch (Exception ex)
            {
                if(oCompany.InTransaction)
                {
                    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                }   
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
            //}
            //else
            //{
            //    return Task.FromResult(new PostResponse
            //    {
            //        ErrorCode = login.LErrCode,
            //        ErrorMsg = login.SErrMsg
            //    });
            //}
        }
        public Task<PostResponse> UpdateSettlementDraftAsync(AddSettlementDraftRequest addSettlementDraftRequest)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{                
            var a = Settlements._Update(addSettlementDraftRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
            //}
            //return Task.FromResult(new PostResponse
            //{
            //    ErrorCode = login.LErrCode,
            //    ErrorMsg = login.SErrMsg
            //});
        }
        #endregion
        #region Function
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
        private List<CheckProjectList> CheckProjectManagement(List<CheckProjectList> checkProjectLists, string projectName, int LineNum)
        {
            if (checkProjectLists == null)
            {
                var a = new List<int>();
                a.Add(LineNum);
                checkProjectLists.Add(new CheckProjectList
                {
                    Count = 1,
                    ProjectManagment = projectName,
                    Lines = a
                });
            }
            else
            {
                var a = checkProjectLists.FirstOrDefault(x => x.ProjectManagment == projectName);
                if (a == null)
                {
                    var listLine = new List<int>();
                    listLine.Add(LineNum);
                    checkProjectLists.Add(new CheckProjectList
                    {
                        Count = 1,
                        ProjectManagment = projectName,
                        Lines = listLine
                    });
                }
                else
                {
                    checkProjectLists.Remove(a);
                    a.Count = a.Count + 1;
                    a.Lines.Add(LineNum);
                    checkProjectLists.Add(a);
                }
            }
            return checkProjectLists;
        }
        private Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync()
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.PRSettlement!,
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetLayoutShowByType.Add(new GetLayoutShowByType
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!
                    });
                }
                return Task.FromResult(new ResponseData<List<GetLayoutShowByType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetLayoutShowByType
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetLayoutShowByType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Cancel
        public Task<PostResponse> CancelSettlementDraftAsync(string docNum)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = Settlements._Cancel(docNum, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
            //}
            //return Task.FromResult(new PostResponse
            //{
            //    ErrorCode = login.LErrCode,
            //    ErrorMsg = login.SErrMsg
            //});
        }
        #endregion
    }
}
