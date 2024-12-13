using Client.Connection;
using Client.Lib.CreditNoteSeaAir;
using Client.Lib.DebitSeaAir;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class CreditSeaAir : ICreditSeaAir
    {
        #region Get
        public Task<ResponseData<Models.Request.PurchaseRequest>> GetCreditSeaAirDeatailByDocEntry(string docEntry, string type)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<Models.Request.PurchaseRequest>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetCreditSeaAirDetailByDocEntry",
                            type,
                            docEntry,
                            "",
                            "",
                            "").GetResultDataTable())[0];
                obj.Lines = QueryData.ConvertDataTable<Models.Request.PurchaseRequestLine>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetCreditSeaAirDetailByDocEntry",
                            "Lines",
                            docEntry,
                            "",
                            "",
                            "").GetResultDataTable());
                return Task.FromResult(new ResponseData<Models.Request.PurchaseRequest>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = obj,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<Models.Request.PurchaseRequest>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetItemCodePurchaseRequest>>> GetItemCodeCreditSeaAirResponsesAsync(string type, string department)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetItemCodePurchaseRequest>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetItemCodePurchaseRequest>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetItemCodePurchaseRequest",
                            type,
                            department,
                            "",
                            "",
                            "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetItemCodePurchaseRequest>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAdvancePaymentClearing>>> GetListOfCreditNote(string dateFrom, string dateTo, string type, string userID)
        {
            //try
            //{
            //    return Task.FromResult(new ResponseData<List<GetAdvancePaymentClearing>>
            //    {
            //        ErrorCode = 0,
            //        ErrorMessage = "",
            //        Data = QueryData.ConvertDataTable<GetAdvancePaymentClearing>(new GetRecordByDataTable(
            //                StoreType.TransactionSeaAir,
            //                "GetListOfCreditNoteSeaAir",
            //                dateFrom,
            //                dateTo,
            //                type,
            //                userID,
            //                "").GetResultDataTable())
            //    });
            //}
            //catch (Exception ex)
            //{
            //    return Task.FromResult(new ResponseData<List<GetAdvancePaymentClearing>>
            //    {
            //        ErrorCode = ex.HResult,
            //        ErrorMessage = ex.Message,
            //        Data = null!
            //    });
            //}
            try
            {
                var listGetAdvancePaymentClearing = new List<GetAdvancePaymentClearing>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetListOfCreditNoteSeaAir",
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
        public Task<ResponseData<List<GetAdvancePaymentClearing>>> GetListOfDebitNote(string dateFrom, string dateTo, string type, string userID)
        {
            //try
            //{
            //    return Task.FromResult(new ResponseData<List<GetListCreditNoteSeaAir>>
            //    {
            //        ErrorCode = 0,
            //        ErrorMessage = "",
            //        Data = QueryData.ConvertDataTable<GetListCreditNoteSeaAir>(new GetRecordByDataTable(
            //                StoreType.TransactionSeaAir,
            //                "GetListOfDebitNoteSeaAir",
            //                dateFrom,
            //                dateTo,
            //                type,
            //                userID,
            //                "").GetResultDataTable())
            //    });
            //}
            //catch (Exception ex)
            //{
            //    return Task.FromResult(new ResponseData<List<GetListCreditNoteSeaAir>>
            //    {
            //        ErrorCode = ex.HResult,
            //        ErrorMessage = ex.Message,
            //        Data = null!
            //    });
            //}
            try
            {
                var listGetAdvancePaymentClearing = new List<GetAdvancePaymentClearing>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.TransactionSeaAir,
                             "GetListOfDebitNoteSeaAir",
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
        public Task<ResponseData<List<GetProjectManagmentList>>> GetProjectManagmentListResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetProjectManagmentList>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetProjectManagmentList>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GETPROJECTMANAGEMENTLIST",
                            "",
                            "",
                            "",
                            "",
                            "").GetResultDataTable())
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
        public Task<ResponseData<List<GetTaxCode>>> GetTaxCodes(string type)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetTaxCode>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetTaxCode>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetTaxCode",
                            type,
                            "",
                            "",
                            "",
                            "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetTaxCode>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetArCreditNoteRefInvSeaAndAir>>> GetArCreditNoteRefInvSeaAndAir()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetArCreditNoteRefInvSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetArCreditNoteRefInvSeaAndAir>(new GetRecordByDataTable(
                        StoreType.TransactionSeaAir,
                        "GetArCreditNoteRefInvSeaAndAir",
                        "",
                        "",
                        "",
                        "",
                        "").GetResultDataTable())
                }); ;
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetArCreditNoteRefInvSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Post
        public Task<PostResponse> PostCreditSeaAir(Models.Request.PurchaseRequest postPurchaseRequestRequest, string CostCenter)
        {
            var a = CreditNoteSeaAirs._Add(postPurchaseRequestRequest, CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }

        public Task<PostResponse> PostDebitSeaAir(Models.Request.PurchaseRequest postPurchaseRequestRequest, string CostCenter)
        {
            var a = DebitSeaAirs._Add(postPurchaseRequestRequest, CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        #endregion
        #region Remove
        public Task<PostResponse> RemoveCreditSeaAir(int docEntry)
        {
            var a = CreditNoteSeaAirs._Remove(docEntry, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
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
                             Configure.CreditNoteSA!,
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
