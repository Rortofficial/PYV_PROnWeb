using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Lib.Reimbursement;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public class Reimbursement : IReimbursement
    {
        #region Update
        public Task<PostResponse> ReimbursementPut(ReimbursementRequest reimbursementRequest)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<PostResponse>(new Connection.GetRecordByDataTable(
                             Connection.GetRecordByDataTable.StoreType.Add,
                             "UpdateReimbursement",
                             reimbursementRequest.CardCode,
                             reimbursementRequest.Amount.ToString(),
                             ClearEmptyString.clearEmptyString(reimbursementRequest.Remark.ToString()),
                             reimbursementRequest.DocEntry.ToString(),
                             "").GetResultDataTable()).FirstOrDefault();
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorMsg = ex.Message,
                });
            }
        }
        #endregion
        #region Delete
        public Task<PostResponse> ReimbursementDelete(string docEntry)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<PostResponse>(new Connection.GetRecordByDataTable(
                             Connection.GetRecordByDataTable.StoreType.Add,
                             "DeleteReimbursement",
                             docEntry,
                             "",
                             "",
                             "",
                             "").GetResultDataTable()).FirstOrDefault();
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorMsg = ex.Message,
                });
            }
        }
        #endregion
        #region POST
        public Task<PostResponse> ApproveReimbursementPost(List<ListApproveReimbursement> listApproveReimbursements, string CostCenter)
        {
            try
            {
                var objCondition = new List<ReimbursementRequest>();
                foreach (var objReimbursement in listApproveReimbursements)
                {
                    if (objReimbursement.ApproveType == "Approve")
                    {
                        objCondition.Add(QueryData.ConvertDataTable<ReimbursementRequest>(new GetRecordByDataTable(
                                         "GetReimbursementDocEntry",
                                         objReimbursement.DocEntry,
                                         "1999-01-01",
                                         DateTime.Now.ToString("yyyy-MM-dd"),
                                         "ALL",
                                         "").GetResultDataTable()).FirstOrDefault());
                    }
                }
                foreach (var a in listApproveReimbursements)
                {
                    if (a.ApproveType == "Approve")
                    {
                        var testAmount = objCondition.Where(x => x.CardCode == a.CardCode).Sum(x => x.Amount);
                        var totalBpAmountLimit = objCondition.Where(x => x.CardCode == a.CardCode).FirstOrDefault().AmountLimit - objCondition.Where(x => x.CardCode == a.CardCode).Sum(x => x.Amount);
                        if ((totalBpAmountLimit < 0))
                        {
                            return Task.FromResult(new PostResponse
                            {
                                ErrorCode = -1,
                                ErrorMsg = "Customer Code:" + a.CardCode + " Can not Approve becuase Amount has Equal CreidtLimit or more than CreditLimit!",
                            });
                        }
                    }
                }

                foreach (var objReimbursement in listApproveReimbursements)
                {
                    if (objReimbursement.ApproveType == "Approve")
                    {
                        var obj = QueryData.ConvertDataTable<ReimbursementRequest>(new GetRecordByDataTable(
                                        "GetReimbursementDocEntry",
                                        objReimbursement.DocEntry,
                                        "1999-01-01",
                                        DateTime.Now.ToString("yyyy-MM-dd"),
                                        "ALL",
                                        "").GetResultDataTable()).FirstOrDefault();
                        //SapConnection login = new();
                        //if (login.LErrCode == 0)
                        //{

                        obj.AccountCode = objReimbursement.AccountCode;
                        obj.Reason = objReimbursement.Reason;
                        obj.UserID = objReimbursement.UserID;
                        var a = Reimbursements._Approve(obj, CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
                        if (a.ErroreCode == 0)
                        {
                            var rs = QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                                     GetRecordByDataTable.StoreType.Add,
                                     "UpdateStatusReimbursement",
                                     "A",
                                     objReimbursement.Reason,
                                     objReimbursement.UserID,
                                     objReimbursement.DocEntry,
                                     "").GetResultDataTable()).FirstOrDefault();
                        }
                        else
                        {
                            return Task.FromResult(new PostResponse
                            {
                                ErrorCode = a.ErroreCode,
                                ErrorMsg = a.ErroreMessage,
                                DocEntry = a.DocEntry.ToString()
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
                    else
                    {
                        var rs = QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                                 GetRecordByDataTable.StoreType.Add,
                                 "UpdateStatusReimbursement",
                                 "R",
                                 objReimbursement.Reason,
                                 objReimbursement.UserID,
                                 objReimbursement.DocEntry,
                                 "").GetResultDataTable()).FirstOrDefault();
                    }
                }
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = 0,
                    ErrorMsg = "",
                    DocEntry = ""
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorMsg = ex.Message
                });
            }
        }

        public Task<PostResponse> ReimbursementPost(ReimbursementRequest reimbursementRequest)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<PostResponse>(new Connection.GetRecordByDataTable(
                             Connection.GetRecordByDataTable.StoreType.Add,
                             "AddReimbursement",
                             reimbursementRequest.CardCode,
                             reimbursementRequest.Amount.ToString(),
                             ClearEmptyString.clearEmptyString(reimbursementRequest.Remark),
                             "",
                             "").GetResultDataTable()).FirstOrDefault();
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorMsg = ex.Message,
                });
            }
        }
        #endregion
        #region Get
        public Task<ResponseData<List<ReimbursementRequest>>> ReimbursementGet(string docEntry, string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<ReimbursementRequest>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<ReimbursementRequest>(new GetRecordByDataTable(
                             "GetReimbursementDocEntry",
                             docEntry,
                             dateFrom,
                             dateTo,
                             type,
                             userID).GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<ReimbursementRequest>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAccountByEmployeeBudget>>> GetEmployeeBudget()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetAccountByEmployeeBudget>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetAccountByEmployeeBudget>(new GetRecordByDataTable(
                             "GetEmployeeBudget",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAccountByEmployeeBudget>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<ReimbursementRequest>>> ListReimbursementApprove()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<ReimbursementRequest>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<ReimbursementRequest>(new GetRecordByDataTable(
                             "GetListReimbursementApprove",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<ReimbursementRequest>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAccountReimbursment>>> GetAccountCode()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetAccountReimbursment>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetAccountReimbursment>(new GetRecordByDataTable(
                             "GetAccountReimbursment",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAccountReimbursment>>
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
