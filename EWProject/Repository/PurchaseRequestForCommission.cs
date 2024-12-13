using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Lib.SalesCommission;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class PurchaseRequestForCommission : IPurchaseRequestForCommission
    {
        #region Get
        public Task<ResponseData<List<GetCommissionBySaleEmployee>>> getCommissionBySaleEmployee(string slpCode)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetCommissionBySaleEmployee>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetCommissionBySaleEmployee>(new GetRecordByDataTable(
                             "GetCommissionBySaleEmployee",
                             slpCode,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCommissionBySaleEmployee>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<PostSalesCommissions>> getSalesCommissionDetailByDocEntry(string docEntry)
        {
            try
            {
                var a = QueryData.ConvertDataTable<PostSalesCommissions>(new GetRecordByDataTable(
                             "GetCommissionDetailByDocEntry",
                             "Header",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable())[0];
                a.Lines = QueryData.ConvertDataTable<PostSalesCommissionsRow>(new GetRecordByDataTable(
                             "GetCommissionDetailByDocEntry",
                             "Lines",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable());
                return Task.FromResult(new ResponseData<PostSalesCommissions>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = a,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<PostSalesCommissions>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<PostSalesCommissionsRow>> CheckCommissionDetailByJobNumber(PostInvoiceListSalesCommission jobNumber)
        {
            try
            {
                return Task.FromResult(new ResponseData<PostSalesCommissionsRow>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<PostSalesCommissionsRow>(new GetRecordByDataTable(
                             "CheckCommissionDetailByJobNumber",
                             jobNumber.ARInvoice,
                             jobNumber.JobNumber,
                             "",
                             "",
                             "").GetResultDataTable())[0]
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<PostSalesCommissionsRow>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetListSalesCommission>>> GetListSalesCommissions(string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetListSalesCommission>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetListSalesCommission>(new GetRecordByDataTable(
                             "GetListSalesCommission",
                             dateFrom,
                             dateTo,
                             type,
                             userID,
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListSalesCommission>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Post
        public Task<PostResponse> PostCommissionBySaleEmployee(PostSalesCommissions postSalesCommissions)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = SalesCommissions._Add(postSalesCommissions, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        public Task<DeleteResponse> RejectCommissionBySaleEmployee(string docNum, string remark)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = SalesCommissions._Reject(docNum, remark, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        public Task<PostResponse> ApproveCommissionBySaleEmployee(PostSalesCommissions postSalesCommissions, string CostCenter)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = SalesCommissions._Approve(postSalesCommissions, CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        public Task<DeleteResponse> CancelCommissionBySaleEmployee(string docNum)
        {
            try
            {
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = SalesCommissions._Cancel(docNum, SAP_Driver_oCompany._CheckingStatusOCompany());
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
                             Configure.PurchaseRequestCommissionLayoutPrint!,
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
