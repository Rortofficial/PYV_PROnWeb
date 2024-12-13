using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Lib.Purchase;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class PurchaseRequest : IPurchaseRequest
    {
        private int ErrCode;
        private string ErrMsg = null!;
        #region Get
        public Task<ResponseData<List<GetAccountCodePurchaseRequest>>> GetAccountCodePurchaseRequestResponsesAsync()
        {
            try
            {
                var listGetAccountCodePurchaseRequest = new List<GetAccountCodePurchaseRequest>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetAccountCodePurchaseRequest",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetAccountCodePurchaseRequest.Add(new GetAccountCodePurchaseRequest
                    {
                        AccountCode = dataRow["ACCOUNTCODE"].ToString()!,
                        AccountName = dataRow["ACCOUNTNAME"].ToString()!,
                        AccountBalance = Convert.ToDouble(dataRow["ACCOUNTBALANCE"].ToString()),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetAccountCodePurchaseRequest>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetAccountCodePurchaseRequest
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAccountCodePurchaseRequest>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetVendor>>> GetVendorResponseAsync(string bpType)
        {
            try
            {
                var listGetVendor = new List<GetVendor>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "GetVendor",
                             bpType,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetVendor.Add(new GetVendor
                    {
                        CustomerCode = dataRow["CUSTOMERCODE"].ToString()!,
                        CustomerName = dataRow["CUSTOMERNAME"].ToString()!,
                        BankAccount = dataRow["BANKACCOUNT"].ToString()!,
                        Branch = dataRow["BRANCH"].ToString()!,
                        BankCountry = dataRow["BANKCOUNTRY"].ToString()!,
                        BankName = dataRow["BANKNAME"].ToString()!,
                        SwiftCode = dataRow["SWIFTCODE"].ToString()!,
                        CreditTermDay = Convert.ToInt32(dataRow["CreditTermDay"].ToString()!),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetVendor>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetVendor
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetVendor>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetItemCodePurchaseRequest>>> GetItemCodePurchaseRequestResponsesAsync(string type, string department)
        {
            try
            {
                var listGetItemCodePurchaseRequest = new List<GetItemCodePurchaseRequest>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetItemCodePurchaseRequest",
                             type,
                             department,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetItemCodePurchaseRequest.Add(new GetItemCodePurchaseRequest
                    {
                        ItemCode = dataRow["ITEMCODE"].ToString()!,
                        ItemName = dataRow["ITEMNAME"].ToString()!,
                        Price = Convert.ToDouble(dataRow["PRICE"].ToString()),
                        ServiceType = dataRow["SERVICETYPE"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetItemCodePurchaseRequest>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetItemCodePurchaseRequest
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
                        DocEntryJobSheet = dataRow["DocEntry"].ToString()!,
                        Active = dataRow["ACTIVE"].ToString()!,
                        AbsEntry = dataRow["AbsEntry"].ToString()!,
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
        public Task<ResponseData<List<GetPurchaseRequest>>> GetPurchaseRequestResponseAsync(string type, string dateFrom, string dateTo, string storetype, string userID)
        {
            try
            {
                var listGetPurchaseRequest = new List<GetPurchaseRequest>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetListPurchaseRequest",
                             type,
                             dateFrom,
                             dateTo,
                             storetype,
                             userID).GetResultDataTable().Rows)
                {
                    listGetPurchaseRequest.Add(new GetPurchaseRequest
                    {
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        Status = dataRow["STATUS"].ToString()!,
                        UserRequest = dataRow["USERNAME"].ToString()!,
                        IssueDate = dataRow["ISSUEDATE"].ToString()!,
                        UpdateBy = dataRow["UpdateBy"].ToString()!,
                        RequireDate = dataRow["REQUIREDATE"].ToString()!,
                        Remark = dataRow["REMARK"].ToString()!,
                        Reason = dataRow["REASON"].ToString()!,
                        DocTotal = Convert.ToDouble(dataRow["DOCTOTAL"].ToString()),
                        ProjectNumber = dataRow["JOBNUMBER"].ToString(),
                        DocNumPR = dataRow["DOCNUMPR"].ToString(),
                        VendorCode = dataRow["VENDORCODE"].ToString(),
                        VendorName = dataRow["VENDORNAME"].ToString(),
                        UpdateStatus = dataRow["UpdateStatus"].ToString(),
                        CancelStatus = dataRow["CancelStatus"].ToString(),
                        UpdateTime = dataRow["UPDATETIME"].ToString(),
                        CreateTime = dataRow["CREATETIME"].ToString(),
                        CreateDate = dataRow["CreateDate"].ToString(),
                        UpdateDate = dataRow["UpdateDate"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetPurchaseRequest>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetPurchaseRequest
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetPurchaseRequest>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<GetRefNoPurchaseRequestResponse> GetRefNoPurchaseRequestResponsesAsync()
        {
            try
            {
                var refNo = (DateTime.Now.Day.ToString()
                                            + DateTime.Now.Month.ToString()
                                            + DateTime.Now.Year.ToString()
                                            + DateTime.Now.DayOfYear.ToString()
                                            + DateTime.Now.Hour.ToString()
                                            + DateTime.Now.Minute.ToString()
                                            + DateTime.Now.Second.ToString()
                                            + DateTime.Now.Millisecond.ToString());
                string docNum = "";
                foreach (DataRow dataRow in new GetRecordByDataTable(
                            "GetMaxDocNumPurchaseRequest",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    docNum = dataRow["MAXDOCNUM"].ToString();
                }
                return Task.FromResult(new GetRefNoPurchaseRequestResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = refNo,
                    DocNum = docNum
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetRefNoPurchaseRequestResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetItemPriceList>> GetPriceListByItemCodeResponseAsync(string itemCode, string cardCode, string priceList)
        {
            try
            {
                var getItemPriceList = new GetItemPriceList();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetPriceByItem",
                             itemCode,
                             cardCode,
                             priceList,
                             "",
                             "").GetResultDataTable().Rows)
                {
                    getItemPriceList = new GetItemPriceList
                    {
                        ItemCode = dataRow["ITEMCODE"].ToString()!,
                        Price = Convert.ToDouble(dataRow["PRICE"].ToString()),
                        IsEnable = dataRow["ISENABLE"].ToString()!,
                    };
                }
                return Task.FromResult(new ResponseData<GetItemPriceList>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getItemPriceList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetItemPriceList>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetCurrencyByCustomer>>> GetGetCurrencyByCustomerAsync(string CardCode)
        {
            try
            {
                var getCurrencyByCustomerList = new List<GetCurrencyByCustomer>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetCurrencyByCardCode",
                             CardCode,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    getCurrencyByCustomerList.Add(new GetCurrencyByCustomer
                    {
                        CurrencyCode = dataRow["CurrencyCode"].ToString()!,
                        CurrencyName = dataRow["CurrencyName"].ToString()!,
                        ExchangeRateSystemCurrency = Convert.ToDecimal(dataRow["ExchangeRateSystemCurrency"].ToString()!),
                        ExchangeRateLocalCurrency = Convert.ToDecimal(dataRow["ExchangeRateLocalCurrency"].ToString()!),
                        EXCHANGERATE = Convert.ToDecimal(dataRow["EXCHANGERATE"].ToString()!),
                        Defualt = dataRow["Defualt"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetCurrencyByCustomer>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getCurrencyByCustomerList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCurrencyByCustomer>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync()
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.PurchaseRequestLayoutPrinter,
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
        public Task<ResponseData<List<GetDistributionRules>>> GetDistributionRulesResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetDistributionRules>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetDistributionRules>(new GetRecordByDataTable(
                             "GetDistributionRules",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetDistributionRules>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetTaxCode>>> GetTaxCodes(string type="")
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetTaxCode>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetTaxCode>(new GetRecordByDataTable(
                             (type=="")?"GetTaxCodePurchases":type,
                             "",
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
        public Task<ResponseData<List<GetVendorByJobNoCOGS>>> GetVendorByJobNoResponseAsync(string confirmBooking, string ActionType)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<GetVendorByJobNoCOGS>(new GetRecordByDataTable(
                             "GetConfirmBookingByProjectID",
                             confirmBooking,
                             "",
                             "",
                             "",
                             "").GetResultDataTable());
                foreach (var a in obj)
                {
                    if (ActionType == "ADD")
                    {
                        a.ItemByJobNo = QueryData.ConvertDataTable<GetItemByJobNo>(new GetRecordByDataTable(
                                     "GetConfirmBookingLineByProjectID",
                                     confirmBooking,
                                     a.VendorCode,
                                     "",
                                     "",
                                     "").GetResultDataTable());
                    }
                    else if (ActionType == "UPDATE")
                    {
                        a.ItemByJobNo = QueryData.ConvertDataTable<GetItemByJobNo>(new GetRecordByDataTable(
                                     "GetConfirmBookingLineByProjectIDUpdate",
                                     confirmBooking,
                                     a.VendorCode,
                                     "",
                                     "",
                                     "").GetResultDataTable());
                    }
                }
                return Task.FromResult(new ResponseData<List<GetVendorByJobNoCOGS>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = obj
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetVendorByJobNoCOGS>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetTruckNoByJob>>> GetTruckNoByJobNoResponseAsync(string jobNo)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetTruckNoByJob>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetTruckNoByJob>(new GetRecordByDataTable(
                             "GetTruckNoByJobNo",
                             jobNo,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetTruckNoByJob>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetPaymentTerm>>> GetPaymentTerms()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetPaymentTerm>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetPaymentTerm>(new GetRecordByDataTable(
                             "GetPaymentTerm",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetPaymentTerm>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetCheckingRefInvPR>> GetCheckingRefInvPr(string RefInv)
        {
            try
            {
                var a = QueryData.ConvertDataTable<GetCheckingRefInvPR>(new GetRecordByDataTable(
                             "GetCheckingRefInvPR",
                             RefInv,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())[0];
                if (a.MessageCode != 0) throw new ArgumentException(a.Message, nameof(RefInv));
                return Task.FromResult(new ResponseData<GetCheckingRefInvPR>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = a
                }); ;
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetCheckingRefInvPR>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Post
        public Task<PostResponse> PostPurchaseRequestAsync(Models.Request.PurchaseRequest postPurchaseRequestRequest, string typePurchaseRequest)
        {
            try
            {
                #region Create Purchase Request
                var rs = new AddPurchaseOrAdvancePayment(postPurchaseRequestRequest, SAP_Driver_oCompany._CheckingStatusOCompany(), 0, typePurchaseRequest);
                if (rs.ErroreCode == 0)
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = rs.DocEntry.ToString(),
                    });
                }
                else
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = rs.ErroreCode,
                        ErrorMsg = rs.ErroreMessage,
                        DocEntry = "0"
                    });
                }
                #endregion
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
        #region Put
        public Task<PostResponse> PutPurchaseRequestAsync(UpdatePurchaseRequest putPurchaseRequestRequest, string typePurchaseRequest)//For Update Status of PR that has Approve Already
        {
            try
            {
                //Documents oGoodReturn;
                //Company oCompany;
                //var Retval = 0;
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                //    oCompany = ;
                #region Create Purchase Request
                var rs = new UpdatePurchaseOrAdvancePayment(putPurchaseRequestRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
                if (rs.ErroreCode == 0)
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = rs.DocEntry.ToString(),
                    });
                }
                else
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = ErrCode,
                        ErrorMsg = ErrMsg,
                        DocEntry = "0"
                    });
                }
                #endregion

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

        public Task<PostResponse> EditPurchaseRequestAsync(string docEntry, EditPurchaseRequest purchaseRequests, int projectNum, string remarks)
        {
            try
            {
                #region Create Purchase Request
                var rs = new EditPurchaseOrAdvancePayment(docEntry, purchaseRequests, SAP_Driver_oCompany._CheckingStatusOCompany(), projectNum, "", remarks);
                if (rs.ErroreCode == 0)
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = rs.DocEntry.ToString(),
                    });
                }
                else
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = rs.ErroreCode,
                        ErrorMsg = rs.ErroreMessage,
                        DocEntry = "0"
                    });
                }
                #endregion
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
        #region Cancel
        public Task<PostResponse> CancelPurchaseRequestAsync(string docNum)
        {
            try
            {
                #region Create Purchase Request
                var rs = new CancelPurchaseOrAdvancePayment(docNum, SAP_Driver_oCompany._CheckingStatusOCompany());
                if (rs.ErroreCode == 0)
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = 0,
                        ErrorMsg = "",
                        DocEntry = rs.DocEntry.ToString(),
                    });
                }
                else
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = rs.ErroreCode,
                        ErrorMsg = rs.ErroreMessage,
                        DocEntry = "0"
                    });
                }
                #endregion
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
    }
}
