using Client.Connection;
using Client.Lib.AdvancePayment;
using Client.Lib.IncomingPayment;
using Client.Lib.JournalEntry;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class AdvancePayment : IAdvancePayment
    {
        #region Get
        public Task<ResponseData<List<GetAdvancePayment>>> GetAdvancePaymentByUserResponseAsync()
        {
            try
            {
                var listGetAdvancePayment = new List<GetAdvancePayment>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETADVANCEPAYMENTLIST",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetAdvancePayment.Add(new GetAdvancePayment
                    {
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        Status = dataRow["STATUS"].ToString()!,
                        StatusApprove = dataRow["STATUS"].ToString()!,
                        DocDate = dataRow["CREATEDATE"].ToString()!,
                        Remark = dataRow["REMARK"].ToString()!,
                        Total = Convert.ToDouble(dataRow["DOCTOTAL"].ToString()),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetAdvancePayment>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetAdvancePayment
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAdvancePayment>>
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
                             "GETPROJECTMANAGEMENTLIST",
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
        public Task<GetDetailAdvancePaymentByDocEntryResponse> GetDetailAdvancePaymentByDocEntryResponse(string docEntry)
        {
            try
            {
                var getDetailAdvancePaymentByDocEntryList = new PostAdvancePaymentRequest();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "Header",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var listAdvancePaymentRequestLines = new List<AdvancePaymentRequestLine>();
                    foreach (DataRow rowLine in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "Lines",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        listAdvancePaymentRequestLines.Add(new AdvancePaymentRequestLine
                        {
                            BpCode = rowLine["EMPLOYEECODE"].ToString()!,
                            bpCodeOrBpName = rowLine["EMPLOYEENAME"].ToString()!,
                            AccountCode = rowLine["ACCOUNTCODE"].ToString()!,
                            accountCodeOrAccountName = rowLine["ACCOUNTNAME"].ToString()!,
                            amount = Convert.ToDouble(rowLine["AMOUNT"].ToString()),
                        });
                    }
                    getDetailAdvancePaymentByDocEntryList = new PostAdvancePaymentRequest
                    {
                        SeriesName = dataRow["SERIES"].ToString()!,
                        DocDate = Convert.ToDateTime(dataRow["DOCDATE"].ToString()),
                        Remark = dataRow["REMARK"].ToString()!,
                        Ref1 = dataRow["REF1"].ToString()!,
                        Ref2 = dataRow["REF2"].ToString()!,
                        Ref3 = dataRow["REF3"].ToString()!,
                        ProjectManagement = dataRow["PROJECTMANAGEMENT"].ToString()!,
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        UserID = dataRow["USERCODE"].ToString()!,
                        Lines = listAdvancePaymentRequestLines,
                    };
                }
                return Task.FromResult(new GetDetailAdvancePaymentByDocEntryResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDetailAdvancePaymentByDocEntryList,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetDetailAdvancePaymentByDocEntryResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAdvancePayment>>> GetAdvancePaymentApprovalResponseAsync()
        {
            try
            {
                var listGetAdvancePayment = new List<GetAdvancePayment>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETAPPROVALLISTADVANCEPAYMENT",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetAdvancePayment.Add(new GetAdvancePayment
                    {
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        Status = dataRow["STATUS"].ToString()!,
                        StatusApprove = dataRow["STATUSAPPROVE"].ToString()!,
                        DocDate = dataRow["DOCDATE"].ToString()!,
                        Remark = dataRow["REMARK"].ToString()!,
                        Total = Convert.ToDouble(dataRow["TOTAL"].ToString()),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetAdvancePayment>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetAdvancePayment
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAdvancePayment>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAdvancePaymentClearing>>> GetAdvancePaymentClearingResponseAsync()
        {
            try
            {
                var listGetAdvancePaymentClearing = new List<GetAdvancePaymentClearing>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetListClearingAdvancePayment",
                             "",
                             "",
                             "",
                             "",
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
        public Task<ResponseData<List<GetSeries>>> GetSeriesResponseAsync()
        {
            try
            {
                var listGetSeries = new List<GetSeries>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetMaxDocNumIncomingPayment",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetSeries.Add(new GetSeries
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                        NextNumber = dataRow["MAXDOCNUM"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetSeries>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetSeries
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetSeries>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetClearingAdvance>>> GetClearingAdvanceTypeReponseAsync()
        {
            try
            {
                var listGetClearingAdvance = new List<GetClearingAdvance>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetClearingAdvanceType",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetClearingAdvance.Add(new GetClearingAdvance
                    {
                        ClearingCode = dataRow["CLEARINGCODE"].ToString()!,
                        ClearingName = dataRow["CLEARINGNAME"].ToString()!,
                        AccountName = dataRow["ACCOUNTNAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetClearingAdvance>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetClearingAdvance
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetClearingAdvance>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAdvancePaymentClearing>>> GetAdvancePaymentClearingResponseByTransIdAsync(string transID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetAdvancePaymentClearing>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    //Data = listGetAdvancePaymentClearing
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
        #endregion
        #region Post
        public Task<PostResponse> PostAdvancePayment(PostAdvancePaymentRequest postAdvancePaymentRequest)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = AdvancePayments._Add(postAdvancePaymentRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
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
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        public Task<PostResponse> PostJE(PostAdvancePaymentRequest postAdvancePaymentRequest)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = JournalEntrys._Add(postAdvancePaymentRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
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
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        public Task<PostResponse> PostEmployeeClearingAdvance(EmployeeClearingAdvanceRequest postAdvancePaymentRequest)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = IncomingPayments._Add(postAdvancePaymentRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
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
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        #endregion
        #region Delete
        public Task<DeleteResponse> CancelAdvancePaymentAsync(string docNum)
        {
            try
            {
                if (GetQuery.GetValueByID("GETSTATUSCANCELADVANCEPAYMENT", "Condition", docNum) == "0")
                {
                    //SapConnection login = new();
                    //if (login.LErrCode == 0)
                    //{
                    var a = AdvancePayments._Cancel(docNum, SAP_Driver_oCompany._CheckingStatusOCompany());
                    return Task.FromResult(new DeleteResponse
                    {
                        ErrorCode = a.ErroreCode,
                        ErrorMsg = a.ErroreMessage,
                        DocEntry = a.DocEntry.ToString()
                    });
                    //}
                    //return Task.FromResult(new DeleteResponse
                    //{
                    //    ErrorCode = login.LErrCode,
                    //    ErrorMsg = login.SErrMsg
                    //});
                }
                else
                {
                    return Task.FromResult(new DeleteResponse
                    {
                        ErrorCode = -99,
                        ErrorMsg = "Document already link with Other Document"
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new DeleteResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        public Task<DeleteResponse> RejectAdvancePayment(string DocEntry)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = AdvancePayments._Reject(docNum: DocEntry, _company: SAP_Driver_oCompany._CheckingStatusOCompany());
                return Task.FromResult(new DeleteResponse
                {
                    ErrorCode = a.ErroreCode,
                    ErrorMsg = a.ErroreMessage,
                    DocEntry = a.DocEntry.ToString()
                });
                //}
                //return Task.FromResult(new DeleteResponse
                //{
                //    ErrorCode = login.LErrCode,
                //    ErrorMsg = login.SErrMsg
                //});
            }
            catch (Exception ex)
            {
                return Task.FromResult(new DeleteResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        #endregion
        #region Function
        private Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync()
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.AdvancePaymentLayoutPrinter!,
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
    }
}
