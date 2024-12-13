using Client.Connection;
using Client.Lib.Customer;
using Client.Lib.OtherFunction;
using Client.Lib.SalesQuotation;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using SAPbobsCOM;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class SalesQuotation : ISalesQuotation
    {
        #region GET
        public Task<ResponseData<List<GetCreditTerm>>> GetCreditTermResponses()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetCreditTerm>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetCreditTerm>(new GetRecordByDataTable(
                             "GETCREDITTERM",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCreditTerm>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetRefNo>> GetRefNoResponses(string UserID)
        {
            try
            {
                return Task.FromResult(new ResponseData<GetRefNo>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetRefNo>(new GetRecordByDataTable(
                             "GETREFTNOSALEQUOTATION",
                             UserID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())[0],
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetRefNo>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetServiceType>>> GetServiceTypeResponses()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetServiceType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetServiceType>(new GetRecordByDataTable(
                             "GETSERVICESALESQUOTATION",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetServiceType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetDestination>>> GetDestinationResponses()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetDestination>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetDestination>(new GetRecordByDataTable(
                             "GETDESTINATION",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetDestination>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetItemSaleQuotation>>> GetItemSalesQuotationResponses(string department)
        {
            try
            {
                var listGetItemSaleQuotation = new List<GetItemSaleQuotation>();
                //var str = "";
                //var type = "CBT";
                //var arr = type.Split(',');
                //if (arr == null)
                //{
                //    str = "''" + arr + "''";
                //}
                //else
                //{
                //    for (var i = 0; i < arr.Length; i++)
                //    {
                //        if (arr.Length == i + 1)
                //        {
                //            str += "''" + arr[i] + "''";
                //        }
                //        else
                //        {
                //            str += "''" + arr[i] + "'',";
                //        }
                //    }
                //}
                return Task.FromResult(new ResponseData<List<GetItemSaleQuotation>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetItemSaleQuotation>(new GetRecordByDataTable(
                             "GETITEMSALESQUOTATION",
                             department,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetItemSaleQuotation>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetOrigin>>> GetOriginResponses()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetOrigin>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetOrigin>(new GetRecordByDataTable(
                             "GETORIGIN",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetOrigin>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetListSaleQuotation>>> GetListSaleQuotationResponse(string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetListSaleQuotation>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetListSaleQuotation>(new GetRecordByDataTable(
                             "GETLISTSALEQUOTATION",
                             "SALEQUOTATION",
                             dateFrom,
                             dateTo,
                             type,
                             userID).GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListSaleQuotation>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetListSaleQuotation>>> GetListSaleQuotationBookingResponse()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetListSaleQuotation>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetListSaleQuotation>(new GetRecordByDataTable(
                             "GETLISTSALEQUOTATION",
                             "LISTSALEQUOTATIONBOOKING",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListSaleQuotation>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetCO>>> GetCOResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetCO>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetCO>(new GetRecordByDataTable(
                             "GETCO",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCO>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<GetDetailViewSalesQuotationResponse> GetDetailViewSalesQuotationAsync(string docEntry)
        {
            try
            {
                var listGetDetailViewSalesQuotation = new GetDetailViewSalesQuotation();
                listGetDetailViewSalesQuotation = QueryData.ConvertDataTable<GetDetailViewSalesQuotation>(new GetRecordByDataTable(
                             "GETVIEWDETAILSALESQUOTATIONHEADER",
                             docEntry,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())[0];
                listGetDetailViewSalesQuotation.Lines = QueryData.ConvertDataTable<GetDetailViewSalesQuotationLine>(new GetRecordByDataTable(
                             "GETVIEWDETAILSALESQUOTATIONLINE",
                             docEntry,
                             "",
                             "",
                             "",
                             "").GetResultDataTable());
                return Task.FromResult(new GetDetailViewSalesQuotationResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDetailViewSalesQuotation,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetDetailViewSalesQuotationResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAddressCodeByCustomer>>> GetCustomerAddress(string cardCode)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetAddressCodeByCustomer>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetAddressCodeByCustomer>(new GetRecordByDataTable(
                             "GetAddressByCustomer",
                             cardCode,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAddressCodeByCustomer>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        private Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetLayoutShowByType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetLayoutShowByType>(new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.SalesQuotationLayoutPrinter!,
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
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
        public Task<ResponseData<List<GetSaleEmployee>>> GetSaleEmployeeResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetSaleEmployee>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetSaleEmployee>(new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             "GETSALEPERSON",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetSaleEmployee>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        
        public Task<ResponseData<List<GetContactPerson>>> GetContactPersonByCardCode(string CardCode)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetContactPerson>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetContactPerson>(new GetRecordByDataTable(
                             "GetContactPersonByCardCode",
                             CardCode,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetContactPerson>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetInformationBP>> GetInformationBPResponses(string CardCode)
        {
            try
            {
                return Task.FromResult(new ResponseData<GetInformationBP>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetInformationBP>(new GetRecordByDataTable(
                             "GetInformationBP",
                             CardCode,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())[0]
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetInformationBP>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetCurrencyByCustomer>>> GetBPCurrencyResponses(string CardCode)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetCurrencyByCustomer>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetCurrencyByCustomer>(new GetRecordByDataTable(
                             "GetCurrencyByCardCode",
                             CardCode,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
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
        #endregion
        #region POST  
        public Task<PostResponse> PostSalesQuotations(PostSalesQuotationRequest postSalesQuotationRequest)
        {
            Company oCompany;
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            oCompany = SAP_Driver_oCompany._CheckingStatusOCompany();
            oCompany.StartTransaction();
            try
            {
                #region Create Sales Quotation
                if (postSalesQuotationRequest.CustomerType == "-1")
                {
                    var objAddCustomer = Customers._AddCustomer(postSalesQuotationRequest, oCompany);
                    if (objAddCustomer.ErroreCode != 0)
                    {
                        return Task.FromResult(new PostResponse
                        {
                            ErrorCode = objAddCustomer.ErroreCode,
                            ErrorMsg = objAddCustomer.ErroreMessage,
                            DocEntry = objAddCustomer.CustomerCode
                        });
                    }
                    else
                    {
                        postSalesQuotationRequest.U_CUSTOMERTYPE = "NEW";
                        postSalesQuotationRequest.BPCurrency = "THS";
                        postSalesQuotationRequest.CustomerCode = objAddCustomer.CustomerCode;
                        postSalesQuotationRequest.AddressCode = objAddCustomer.AddressID;
                    }
                }
                else
                {
                    if (postSalesQuotationRequest.ContactPersons.Code == "-1")
                    {
                        var objContactPer = Customers._AddContactPersion(postSalesQuotationRequest.ContactPersons, oCompany, postSalesQuotationRequest.CustomerCode);
                        if (objContactPer.ErroreCode != 0)
                        {
                            return Task.FromResult(new PostResponse
                            {
                                ErrorCode = objContactPer.ErroreCode,
                                ErrorMsg = objContactPer.ErroreMessage,
                                DocEntry = objContactPer.DocEntry.ToString()
                            });
                        }
                        postSalesQuotationRequest.ContactPersons.Code = objContactPer.DocEntry.ToString();
                    }
                    postSalesQuotationRequest.U_CUSTOMERTYPE = "EXIST";
                }
                var oSaleOrder = SalesQuotations._Add(postSalesQuotationRequest, oCompany);
                if (oSaleOrder.ErroreCode == 0)
                {
                    if (oCompany.InTransaction)
                    {
                        oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                        QueryData.ConvertDataTable<PostResponse>(new Connection.GetRecordByDataTable(
                             Connection.GetRecordByDataTable.StoreType.Add,
                             "UPDATENUMATCARDSQ",
                             postSalesQuotationRequest.ReftNo,
                             oSaleOrder.DocEntry,
                             "",
                             "",
                             "").GetResultDataTable());
                        return Task.FromResult(new PostResponse
                        {
                            ErrorCode = 0,
                            ErrorMsg = "",
                            DocEntry = oSaleOrder.DocEntry
                        });
                    }
                    else
                    {
                        return Task.FromResult(new PostResponse
                        {
                            ErrorCode = oSaleOrder.ErroreCode,
                            ErrorMsg = oSaleOrder.ErroreMessage,
                            DocEntry = oSaleOrder.DocEntry
                        });
                    }
                }
                else
                {
                    return Task.FromResult(new PostResponse
                    {
                        ErrorCode = oSaleOrder.ErroreCode,
                        ErrorMsg = oSaleOrder.ErroreMessage,
                        DocEntry = oSaleOrder.DocEntry
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
            //}
            //return Task.FromResult(new PostResponse
            //{
            //    ErrorCode = login.LErrCode,
            //    ErrorMsg = login.SErrMsg
            //});
        }
        #endregion
        #region Cancel
        public Task<DeleteResponse> DeleteSalesQuotations(int DocEntry)
        {
            try
            {
                //Documents oGoodReturn;
                //var Retval = 0;
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var objSalesQuotation = SalesQuotations._Cancel(DocEntry, SAP_Driver_oCompany._CheckingStatusOCompany());
                //}
                return Task.FromResult(new DeleteResponse
                {
                    ErrorCode = objSalesQuotation.ErroreCode,
                    ErrorMsg = objSalesQuotation.ErroreMessage
                });
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
    }
}
