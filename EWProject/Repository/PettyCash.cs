using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Lib.PettyCash;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class PettyCash : IPettyCash
    {
        #region Get
        public Task<ResponseData<List<GetAccountCodeJournalVoucher>>> GetAccountCodeJournalVoucherResponseAsync()
        {
            try
            {
                var listGetAccountCodeJournalVoucher = new List<GetAccountCodeJournalVoucher>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetAccountCodeJournalVoucherEntry",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetAccountCodeJournalVoucher.Add(new GetAccountCodeJournalVoucher
                    {
                        AccountNumber = dataRow["ACCOUNTCODE"].ToString()!,
                        AccountName = dataRow["ACCOUNTNAME"].ToString()!,
                        AccountBalance = Convert.ToDouble(dataRow["ACCOUNTBALANCE"].ToString()),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetAccountCodeJournalVoucher>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetAccountCodeJournalVoucher
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAccountCodeJournalVoucher>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetBPCodeJournalVoucher>>> GetBPCodeJournalVoucherResponseAsync()
        {
            try
            {
                var listGetBPCodeJournalVoucher = new List<GetBPCodeJournalVoucher>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetBpCodeJournalVoucherEntry",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetBPCodeJournalVoucher.Add(new GetBPCodeJournalVoucher
                    {
                        CardCode = dataRow["CUSTOMERCODE"].ToString()!,
                        CardName = dataRow["CUSTOMERNAME"].ToString()!,
                        Balance = Convert.ToDouble(dataRow["BALANCE"].ToString()),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetBPCodeJournalVoucher>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetBPCodeJournalVoucher
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetBPCodeJournalVoucher>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<GetAccountByEmployeeBudget>> GetEmployeeBudgetByIDAsync(string empID)
        {
            try
            {
                return Task.FromResult(new ResponseData<GetAccountByEmployeeBudget>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetAccountByEmployeeBudget>(new GetRecordByDataTable(
                             "GetAccountByEmployeeByID",
                             empID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable()).FirstOrDefault()
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetAccountByEmployeeBudget>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetListJournalVoucher>>> GetListJournalVoucherResponseAsync(string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                var listGetListJournalVoucher = new List<GetListJournalVoucher>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetListJournalVoucher",
                             dateFrom,
                             dateTo,
                             type,
                             userID,
                             "").GetResultDataTable().Rows)
                {
                    listGetListJournalVoucher.Add(new GetListJournalVoucher
                    {
                        DocEntry = dataRow["DOCENTRY"].ToString(),
                        DocDate = dataRow["DOCDATE"].ToString(),
                        Remarks = dataRow["REMARKS"].ToString(),
                        Ref1 = dataRow["REF1"].ToString(),
                        Total = Convert.ToDouble(dataRow["TOTAL"].ToString()),
                        CreateBy = dataRow["CREATEBY"].ToString(),
                        UpdateBy = dataRow["UpdateBy"].ToString(),
                        Type = dataRow["TYPE"].ToString(),
                        Status = dataRow["Status"].ToString(),
                        StatusUpdate = dataRow["StatusUpdate"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetListJournalVoucher>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetListJournalVoucher
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListJournalVoucher>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<string>> GetMaxJournalVoucherNumberResponseAsync()
        {
            try
            {
                var GetMaxJournalVoucherNumber = (DateTime.Now.Day.ToString()
                                            + DateTime.Now.Month.ToString()
                                            + DateTime.Now.Year.ToString()
                                            + DateTime.Now.DayOfYear.ToString()
                                            + DateTime.Now.Hour.ToString()
                                            + DateTime.Now.Minute.ToString()
                                            + DateTime.Now.Second.ToString()
                                            + DateTime.Now.Millisecond.ToString());
                //foreach (DataRow dataRow in new GetRecordByDataTable(
                //             "GetMaxJournalVoucherNumber",
                //             "",
                //             "",
                //             "",
                //             "",
                //             "").GetResultDataTable().Rows)
                //{
                //    GetMaxJournalVoucherNumber = dataRow["MAXDOCNUM"].ToString()!;
                //}
                return Task.FromResult(new ResponseData<string>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = GetMaxJournalVoucherNumber
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<string>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetSeriesJournalVoucher>>> GetSeriesJournalVoucherResponseAsync(string type)
        {
            try
            {
                var listGetSeriesJournalVoucher = new List<GetSeriesJournalVoucher>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             type,
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetSeriesJournalVoucher.Add(new GetSeriesJournalVoucher
                    {
                        Code = Convert.ToInt32(dataRow["SERIES"].ToString()),
                        Name = dataRow["SERIESNAME"].ToString()!,
                        NextNumber = Convert.ToInt32(dataRow["MAXDOCNUM"].ToString()!),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetSeriesJournalVoucher>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetSeriesJournalVoucher
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetSeriesJournalVoucher>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetVatGroupJournalVoucher>>> GetVatGroupJournalVoucherResponseAsync()
        {
            try
            {
                var listGetVatGroupJournalVoucher = new List<GetVatGroupJournalVoucher>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetVatGroupJournalVoucherNumber",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetVatGroupJournalVoucher.Add(new GetVatGroupJournalVoucher
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                        Rate = Convert.ToDouble(dataRow["RATE"].ToString())
                    });
                }
                return Task.FromResult(new ResponseData<List<GetVatGroupJournalVoucher>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetVatGroupJournalVoucher
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetVatGroupJournalVoucher>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<PettyCashDetail>> GetViewDetailPettyCashResponseByDocEntryAsync(string docEntry, string type)
        {
            try
            {
                var obj = new PettyCashDetail();
                obj.Header = QueryData.ConvertDataTable<PettyCashHeaderDetail>(new GetRecordByDataTable(
                             "GetViewDetailPettyCashByDocEntry",
                             docEntry,
                             type,
                             "",
                             "",
                             "").GetResultDataTable()).FirstOrDefault();
                obj.Lines = QueryData.ConvertDataTable<PettyCashLine>(new GetRecordByDataTable(
                             "GetViewDetailPettyCashByDocEntryLine",
                             docEntry,
                             type,
                             "",
                             "",
                             "").GetResultDataTable());
                return Task.FromResult(new ResponseData<PettyCashDetail>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = obj,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<PettyCashDetail>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region POST
        public Task<PostResponse> PostPettyCash(PostPettyCashRequest postPettyCashRequest, string draftJE,string CostCenter)
        {
            var obj = PettyCashs._Add(postPettyCashRequest, draftJE, CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = obj.ErroreCode,
                ErrorMsg = obj.ErroreMessage,
                DocEntry = obj.DocEntry.ToString()
            });
        }
        public Task<PostResponse> PostPettyCashDraft(PostPettyCashRequest postPettyCashRequest)
        {
            var obj = PettyCashs._AddDraft(postPettyCashRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = obj.ErroreCode,
                ErrorMsg = obj.ErroreMessage,
                DocEntry = obj.DocEntry.ToString()
            });
        }
        #endregion
        #region Delete
        public Task<PostResponse> CancelPettyCashDraft(string docEntry)
        {
            var obj = PettyCashs._CancelDraft(docEntry, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = obj.ErroreCode,
                ErrorMsg = obj.ErroreMessage,
                DocEntry = obj.DocEntry.ToString()
            });
        }
        #endregion
        #region Close
        public Task<PostResponse> ClosePettyCashDraft(string docEntry)
        {
            var obj = PettyCashs._CloseDraft(docEntry, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = obj.ErroreCode,
                ErrorMsg = obj.ErroreMessage,
                DocEntry = obj.DocEntry.ToString()
            });
        }
        #endregion
        #region Put
        public Task<PostResponse> PutPettyCash(string docEntry, string JEDocEntry, PostPettyCashRequest postPettyCashRequest)
        {
            var obj = PettyCashs._UpdateDraft(docEntry, JEDocEntry, postPettyCashRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = obj.ErroreCode,
                ErrorMsg = obj.ErroreMessage,
                DocEntry = obj.DocEntry.ToString()
            });
        }
        #endregion
        #region Private Function
        private Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync()
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.PettyCashLayoutPrint!,
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
