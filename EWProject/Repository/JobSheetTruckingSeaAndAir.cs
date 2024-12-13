using Client.Connection;
using Client.Lib.ConfirmBookingSheet;
using Client.Lib.JobSheetTruckingSeaAir;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class JobSheetTruckingSeaAndAir : IJobSheetTruckingSeaAndAir
    {
        #region Get
        public Task<ResponseData<List<GetConfirmBookingSheetByUser>>> GetConfirmBookingSheetByUserResponse()
        {
            try
            {
                var getConfirmBookingSheetByUserList = new List<GetConfirmBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                    GetRecordByDataTable.StoreType.TransactionSeaAir,
                    "ListBookingSheet",
                    "ConfirmBookingSheetJobSheet",
                    "",
                    "",
                    "",
                    "").GetResultDataTable().Rows)
                {
                    getConfirmBookingSheetByUserList.Add(new GetConfirmBookingSheetByUser
                    {
                        BookingID = dataRow["BOOKINGID"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        BookingDate = dataRow["BOOKINGDATE"].ToString(),
                        ImportType = dataRow["IMPORTYPE"].ToString()!,
                        CO = dataRow["CO"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        CreateBy = dataRow["CREATEBY"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetConfirmBookingSheetByUser>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getConfirmBookingSheetByUserList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetConfirmBookingSheetByUser>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetDetailJobSheetTrucking>>> GetConfirmBookingSheetDetailByUserResponseAsync(int confirmBookingSheetID)
        {
            try
            {
                var listGetDetailJobSheetTrucking = new List<GetDetailJobSheetTrucking>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                    StoreType.TransactionSeaAir,
                    "GetConfirmBookingSheetDetail",
                    confirmBookingSheetID.ToString(),
                    "",
                    "",
                    "",
                    "").GetResultDataTable().Rows)
                {
                    var lsInvoice = new List<string>();
                    foreach (DataRow dataRowInvoice in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetConfirmBookingSheetListInvoice",
                             dataRow["CONFIRMBOOKINGID"].ToString()!,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        lsInvoice.Add(dataRowInvoice["CODE"].ToString()!);
                    }
                    var lsTruckInformations = new List<TruckInformationJobSheet>();
                    foreach (DataRow dataRowTruck in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetConfirmBookingSheetListTruck",
                             confirmBookingSheetID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        lsTruckInformations.Add(new TruckInformationJobSheet
                        {
                            CONTAINERTRUCK = dataRowTruck["CONTAINERNO"].ToString()!,
                            ContainerWeight = dataRowTruck["CONTAINERWEIGHT"].ToString(),
                            TRUCKNO = dataRowTruck["TRUCKNO"].ToString(),
                            TruckWeight = dataRowTruck["TRUCKWEIGHT"].ToString(),
                            TRANSPORTER = dataRowTruck["TRANSPORTER"].ToString()!,
                        });
                    }
                    var shipper = new List<string>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                 "GETSHIPPERCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        shipper.Add(dataRowString["SHIPPER"].ToString());
                    }
                    var consignee = new List<string>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                 "GETCONSIGNEECONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        consignee.Add(dataRowString["CONSIGNEE"].ToString());
                    }
                    var forwarder = new List<string>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                 "GETTHAIFORWARDERCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        forwarder.Add(dataRowString["FORWARDER"].ToString());
                    }
                    var overseaforwarder = new List<string>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                 "GETOVERSEAFORWARDERCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        overseaforwarder.Add(dataRowString["FORWARDER"].ToString());
                    }
                    var thaiborder = new List<string>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                 "GETTHAIBORDERCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        thaiborder.Add(dataRowString["ThaiBorder"].ToString());
                    }
                    var oveartransporter = new List<string>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                 "GETOverseaTruckerCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        oveartransporter.Add(dataRowString["OverseaTrucker"].ToString());
                    }
                    var cardCode = "";
                    var cardName = "";
                    foreach (DataRow dataRowInvoice in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetConfirmBookingSheetListCustomer",
                             dataRow["CONFIRMBOOKINGID"].ToString()!,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        cardCode = dataRowInvoice["CODE"].ToString();
                        cardName = dataRowInvoice["CARDNAME"].ToString();
                    }
                    listGetDetailJobSheetTrucking.Add(new GetDetailJobSheetTrucking
                    {
                        BOOKINGID = Convert.ToInt32(dataRow["BOOKINGID"].ToString()),
                        CONFIRMBOOKINGID = Convert.ToInt32(dataRow["CONFIRMBOOKINGID"].ToString()),
                        JOBNO = dataRow["JOBNO"].ToString()!,
                        ROUTE = dataRow["ROUTE"].ToString()!,
                        BOOKINGDATE = dataRow["BOOKINGDATE"].ToString()!,
                        SALEEMPLOYEE = dataRow["SALEEMPLOYEE"].ToString()!,
                        IMPORTTYPE = dataRow["IMPORTTYPE"].ToString()!,
                        SHIPPERNAME = shipper,
                        TOTALPACKAGE = dataRow["TOTALPACKAGE"].ToString()!,
                        CO = cardName!,
                        COCODE = cardCode,//dataRow["COCODE"].ToString()!,
                        ShippingLine = dataRow["ShippingLine"].ToString(),//dataRow["COCODE"].ToString()!,
                        NETWEIGHT = Convert.ToDouble(dataRow["NETWEIGHT"].ToString()),
                        GROSSWEIGHT = Convert.ToDouble(dataRow["GROSSWEIGHT"].ToString()),
                        CONSIGNEE = consignee,
                        LOADINGDATE = dataRow["LOADINGDATE"].ToString()!,
                        CORSSBORDERDATE = dataRow["CROSSBORDERDATE"].ToString()!,
                        ETAREQUIREMENT = dataRow["ETAREQUIREMENT"].ToString()!,
                        PLACEOFLOADING = dataRow["PLACEOFLOADING"].ToString()!,
                        PLACEOFDELIVERY = dataRow["PLACEOFDELIVERY"].ToString()!,
                        GOODSDESCRIPTION = dataRow["GOODSDESCRIPTION"].ToString()!,
                        VOLUME = dataRow["VOLUME"].ToString()!,
                        THAIFORWARDER = forwarder,
                        OVERSEAFORWARDER = overseaforwarder,
                        THAIBORDER = thaiborder,
                        OVERSEATRANSPORT = oveartransporter,
                        Invoices = lsInvoice,
                        TRUCKINFORMATIONS = lsTruckInformations,
                        CurrencyByCustomers = GetCurrecnyByCardCode("").Result.Data,
                        //SalesQuotationInvoiceLists = invoiceList,
                        Layout = GetLayoutShowByTypeResponsenAsync(Configure.TruckWayBillLayoutPrinter)
                    });
                }
                return Task.FromResult(new ResponseData<List<GetDetailJobSheetTrucking>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDetailJobSheetTrucking
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetDetailJobSheetTrucking>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetCurrencyByCustomer>>> GetCurrecnyByCardCode(string CardCode)
        {
            try
            {
                var listGetCurrencyByCustomer = new List<GetCurrencyByCustomer>();
                foreach (DataRow dataRowCurrency in new GetRecordByDataTable(
                             "GetCurrencyByCardCode",
                             CardCode,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetCurrencyByCustomer.Add(new GetCurrencyByCustomer
                    {
                        CurrencyCode = dataRowCurrency["CurrencyCode"].ToString()!,
                        CurrencyName = dataRowCurrency["CurrencyName"].ToString()!,
                        ExchangeRateSystemCurrency = Convert.ToDecimal(dataRowCurrency["ExchangeRateSystemCurrency"].ToString()),
                        ExchangeRateLocalCurrency = Convert.ToDecimal(dataRowCurrency["ExchangeRateLocalCurrency"].ToString()),
                        EXCHANGERATE = Convert.ToDecimal(dataRowCurrency["EXCHANGERATE"].ToString()),
                        Defualt = dataRowCurrency["Defualt"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetCurrencyByCustomer>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetCurrencyByCustomer
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

        public Task<ResponseData<List<GetItemDetailByItemType>>> GetItemDetailByItemTypeResponseAsync(string userID, string department)
        {
            try
            {
                var listGetItemDetailByItemType = new List<GetItemDetailByItemType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetItemDetailByType",
                             userID,
                             department,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    //var uomlist = JsonSerializer.Serialize(GetGetUomGroups(dataRow["UomEntry"].ToString()));
                    listGetItemDetailByItemType.Add(new GetItemDetailByItemType
                    {
                        LineNum = Convert.ToInt32(dataRow["ROWNUM"].ToString()),
                        ItemCode = dataRow["ITEMCODE"].ToString()!,
                        ItemName = dataRow["ITEMNAME"].ToString()!,
                        ItemType = dataRow["ITEMTYPE"].ToString()!,
                        PriceSelling = Convert.ToDouble(dataRow["SELLINGPRICE"].ToString()),
                        PriceCosting = Convert.ToDouble(dataRow["COSTINGPRICE"].ToString()),
                        uomCodeList = dataRow["UomEntry"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetItemDetailByItemType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetItemDetailByItemType
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetItemDetailByItemType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<PostJobSheetTruckingSeaAirRequest>> GetJobSheetTruckingDetailByDocEntry(int docEntry)
        {
            try
            {
                var listGetPostJobSheetTrucking = new PostJobSheetTruckingSeaAirRequest();
                listGetPostJobSheetTrucking.ObjectHeader = QueryData.ConvertDataTable<PostJobSheetTruckingSeaAir>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                                            "GetDetailJobSheetByDocEntry",
                                                            docEntry.ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            "").GetResultDataTable())[0];
                listGetPostJobSheetTrucking.ItemLineRevenue = GetLineJobSheetTruckingByDocEntry("GetDetailJobSheetLineRevenueByDocEntry", docEntry).Result;
                listGetPostJobSheetTrucking.ItemLineCosting = QueryData.ConvertDataTable<JobSheetRuckingLineForItemCostingSeaAir>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                                            "GetDetailJobSheetLineCostingByDocEntry",
                                                            docEntry.ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            "").GetResultDataTable());
                    //GetLineJobSheetTruckingByDocEntry("GetDetailJobSheetLineCostingByDocEntry", docEntry).Result;
                listGetPostJobSheetTrucking.Attachment = QueryData.ConvertDataTable<Attachments>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                                            "GetDetailJobSheetLineAttachmentByDocEntry",
                                                            docEntry.ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            "").GetResultDataTable());
                return Task.FromResult(new ResponseData<PostJobSheetTruckingSeaAirRequest>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetPostJobSheetTrucking,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync(Configure.JobSheetTruckingLayoutSeaAirPrinter).Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<PostJobSheetTruckingSeaAirRequest>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<GetJobSheetTruckingEdit>> GetJobSheetTruckingEditResponsesAsync(int confirmBookingSheetID, int jobSheetDocEntry)
        {
            try
            {
                var JobSheetTruckingEdit = QueryData.ConvertDataTable<GetJobSheetTruckingEdit>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetDetailJobSheetTruckingDetail",
                             confirmBookingSheetID.ToString(),
                             jobSheetDocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable())[0];
                JobSheetTruckingEdit.LinesCosting = QueryData.ConvertDataTable<GetJobSheetTruckingLineView>(new GetRecordByDataTable(
                             "GetLineCostingJobSheetTruckingEdit",
                             JobSheetTruckingEdit.DocNum.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable()); ;
                JobSheetTruckingEdit.LinesSelling = QueryData.ConvertDataTable<GetJobSheetTruckingLineView>(new GetRecordByDataTable(
                             "GetLineSellingJobSheetTruckingEdit",
                             JobSheetTruckingEdit.DocNum.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable()); ;
                JobSheetTruckingEdit.Attachments = QueryData.ConvertDataTable<Models.Gets.Attachments>(new GetRecordByDataTable(
                             "GetAttachmentJobSheetTruckingEdit",
                             JobSheetTruckingEdit.DocNum.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable());
                return Task.FromResult(new ResponseData<GetJobSheetTruckingEdit>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = JobSheetTruckingEdit
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetJobSheetTruckingEdit>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetListJobSheetTrucking>>> GetListJobSheetTruckingResponses(string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                var getListJobSheetTruckingList = new List<GetListJobSheetTrucking>();
                foreach (DataRow dataRow in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetListJobSheetSeaAir",
                             dateFrom,
                             dateTo,
                             type,
                             userID,
                             "").GetResultDataTable().Rows)
                {
                    getListJobSheetTruckingList.Add(new GetListJobSheetTrucking
                    {
                        JobSheetID = dataRow["JOBSHEETID"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        BookingDate = dataRow["JOBSHEETDATE"].ToString()!,
                        UpdateBy = dataRow["UpdateBy"].ToString()!,
                        UPDATEDATE = dataRow["UPDATEDATE"].ToString()!,
                        CREATETIME = dataRow["CREATETIME"].ToString()!,
                        UPDATETIME = dataRow["UPDATETIME"].ToString()!,
                        LoadingDate = dataRow["LoadingDate"].ToString()!,
                        ETADate = dataRow["ETADate"].ToString()!,
                        ImportType = dataRow["IMPORTYPE"].ToString()!,
                        Shipper = dataRow["Shipper"].ToString()!,
                        Consignee = dataRow["Consignee"].ToString()!,
                        CO = dataRow["CO"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        CreateBy = dataRow["CREATEBY"].ToString()!,
                        ConfirmBookingID = dataRow["CONFIRMBOOKINGID"].ToString()!,
                        CostingConfirmStatus = dataRow["COSTINGCONFIRMSTATUS"].ToString()!,
                        Status = dataRow["Status"].ToString()!,
                        CancelStatus = dataRow["CancelStatus"].ToString()!,
                        DocEntrySO = dataRow["DocEntrySO"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetListJobSheetTrucking>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getListJobSheetTruckingList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListJobSheetTrucking>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<List<GetSaleQuotationJobSheet>>> GetSaleQuotationJobSheetResponse(int ConfirmBooking, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseData<List<GetVendorByConfirmJobSheetSeaAir>>> GetVendorByConfirmJobSheetSeaAirResponseAsync(int confirmBookingSheetID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetVendorByConfirmJobSheetSeaAir>>
                {
                    Data= QueryData.ConvertDataTable<GetVendorByConfirmJobSheetSeaAir>(new GetRecordByDataTable(
                    StoreType.TransactionSeaAir,
                    "GetVendorByConfirmJobSheetSeaAir",
                    confirmBookingSheetID.ToString(),
                    "",
                    "",
                    "",
                    "").GetResultDataTable()),
                    ErrorCode = 0,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetVendorByConfirmJobSheetSeaAir>>
                {
                    ErrorCode=ex.HResult,
                    ErrorMessage=ex.Message,
                });
            }
        }

        #endregion
        #region Post
        public Task<PostResponse> PostJobSheetTrucking(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingRequest, string draftdocument, string CostCenter)
        {
            var a = JobSheetTruckingSeaAirs._Add(postJobSheetTruckingRequest, draftdocument, CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        #endregion
        #region Put
        public Task<PostResponse> PutJobSheetTrucking(PostJobSheetTruckingSeaAirRequest updateJobSheetTruckingEdit, string jobDocEntry, string CostCenter)
        {
            var a = JobSheetTruckingSeaAirs._Update(updateJobSheetTruckingEdit, jobDocEntry,CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        public Task<PostResponse> PutDraftJobSheetTrucking(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingRequest, string draftdocument,string jobSheetDocEntry,string CostCenter)
        {
            var a = JobSheetTruckingSeaAirs._UpdateDraft(postJobSheetTruckingRequest,draftdocument, jobSheetDocEntry,CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        #endregion
        #region Delete
        public Task<PostResponse> DeleteJobSheetTrucking(string jobDocEntry,int soDocEntry, string UpdateBy)
        {
            try
            {
                var a = JobSheetTruckingSeaAirs._Cancel(Convert.ToInt32(jobDocEntry), soDocEntry,
                    (soDocEntry == 0) ? null : QueryData.ConvertDataTable<GetPurchaseOrderCancelJobSheetSeaAir>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                    "GetListPurchaseOrderCancelJobSheetTrucking",
                    jobDocEntry,
                    "",
                    "",
                    "",
                    "").GetResultDataTable()), UpdateBy, SAP_Driver_oCompany._CheckingStatusOCompany());
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = a.ErroreCode,
                    ErrorMsg = a.ErroreMessage,
                    DocEntry = a.DocEntry.ToString()
                });
            }catch (Exception ex)
            {
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        #endregion
        #region Function
        public Task<List<JobSheetRuckingLineForItemSeaAir>> GetLineJobSheetTruckingByDocEntry(string type, int DocEntry)
        {
            try
            {
                return Task.FromResult(QueryData.ConvertDataTable<JobSheetRuckingLineForItemSeaAir>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                            type,
                            DocEntry.ToString(),
                            "",
                            "",
                            "",
                            "").GetResultDataTable()));
            }
            catch //(Exception ex)
            {
                return Task.FromResult(new List<JobSheetRuckingLineForItemSeaAir>());
            }
        }
        private ResponseData<List<GetLayoutShowByType>> GetLayoutShowByTypeResponsenAsync(string typeLayout)
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             typeLayout!,
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
                return new ResponseData<List<GetLayoutShowByType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetLayoutShowByType
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<List<GetLayoutShowByType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                };
            }
        }
        #endregion
    }
}
