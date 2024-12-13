using Client.Connection;
using Client.Lib.JobSheetTrucking;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using SAPbobsCOM;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class JobSheetTrucking : IJobSheetTrucking
    {
        #region DataMember
        private int ErrCode;
        private string ErrMsg;
        #endregion
        #region GET
        public Task<ResponseData<List<GetCurrencyByCustomer>>> GetCurrecnyByCardCode(string CardCode, string confirmBookingID)
        {
            try
            {
                var listGetCurrencyByCustomer = new List<GetCurrencyByCustomer>();
                foreach (DataRow dataRowCurrency in new GetRecordByDataTable(
                             "GetCurrencyByCardCodeJobSheetTruckingCBT",
                             CardCode,
                             confirmBookingID,
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
        public Task<ResponseData<List<GetConfirmBookingSheetByUser>>> GetConfirmBookingSheetByUserResponse()
        {
            try
            {
                var getConfirmBookingSheetByUserList = new List<GetConfirmBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
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
                             "GetConfirmBookingSheetDetail",
                             confirmBookingSheetID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var lsInvoice = new List<string>();
                    foreach (DataRow dataRowInvoice in new GetRecordByDataTable(
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
                    foreach (DataRow dataRowTruck in new GetRecordByDataTable(
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
                    foreach (DataRow dataRowString in new GetRecordByDataTable(
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
                    foreach (DataRow dataRowString in new GetRecordByDataTable(
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
                    foreach (DataRow dataRowString in new GetRecordByDataTable(
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
                    foreach (DataRow dataRowString in new GetRecordByDataTable(
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
                    foreach (DataRow dataRowString in new GetRecordByDataTable(
                                 "GETTHAIBORDERCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        thaiborder.Add(dataRowString["ThaiBorder"].ToString());
                    }
                    var invoiceList = new List<SalesQuotationInvoiceList>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(
                                 "GETSALESQUOTATIONCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        invoiceList.Add(new SalesQuotationInvoiceList
                        {
                            InvoiceNumber = dataRowString["DOCENTRY"].ToString(),
                            RefNo = dataRowString["REFNO"].ToString(),
                            CardCode = dataRowString["CARDCODE"].ToString(),
                        });
                    }
                    var oveartransporter = new List<string>();
                    foreach (DataRow dataRowString in new GetRecordByDataTable(
                                 "GETOverseaTruckerCONFIRMBOOKINGDETAIL",
                                 confirmBookingSheetID.ToString(),
                                 "",
                                 "",
                                 "",
                                 "").GetResultDataTable().Rows)
                    {
                        oveartransporter.Add(dataRowString["OverseaTrucker"].ToString());
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
                        CO = dataRow["CO"].ToString()!,
                        COCODE = dataRow["COCODE"].ToString()!,
                        NETWEIGHT = Convert.ToDouble(dataRow["NETWEIGHT"].ToString()),
                        GROSSWEIGHT = Convert.ToDouble(dataRow["GROSSWEIGHT"].ToString()),
                        CONSIGNEE = consignee,
                        LOADINGDATE = dataRow["LOADINGDATE"].ToString()!,
                        CORSSBORDERDATE = dataRow["CROSSBORDERDATE"].ToString()!,
                        ETAREQUIREMENT = dataRow["ETAREQUIREMENT"].ToString()!,
                        PLACEOFLOADING = dataRow["PLACEOFLOADING"].ToString()!,
                        PLACEOFDELIVERY = dataRow["PLACEOFDELIVERY"].ToString()!,
                        DistrictOfLoading = dataRow["DistrictOfLoading"].ToString()!,
                        DistrictOfDelivery = dataRow["DistrictOfDelivery"].ToString()!,
                        GOODSDESCRIPTION = dataRow["GOODSDESCRIPTION"].ToString()!,
                        VOLUME = dataRow["VOLUME"].ToString()!,
                        THAIFORWARDER = forwarder,
                        OVERSEAFORWARDER = overseaforwarder,
                        THAIBORDER = thaiborder,
                        OVERSEATRANSPORT = oveartransporter,
                        Invoices = lsInvoice,
                        TRUCKINFORMATIONS = lsTruckInformations,
                        CurrencyByCustomers = GetCurrecnyByCardCode("", confirmBookingSheetID.ToString()).Result.Data,
                        SalesQuotationInvoiceLists = invoiceList,
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
        public Task<ResponseData<List<GetItemDetailByItemType>>> GetItemDetailByItemTypeResponseAsync(string userID, string department)
        {
            try
            {
                var listGetItemDetailByItemType = new List<GetItemDetailByItemType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
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
        public Task<ResponseData<List<GetSaleQuotationJobSheet>>> GetSaleQuotationJobSheetResponse(int ConfirmBooking, int UserID)
        {
            try
            {
                var listGetSaleQuotationJobSheet = new List<GetSaleQuotationJobSheet>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETSALEQUOTATIONJOBSHEET",
                             ConfirmBooking.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var a = GetGetItemSaleQuotationJobSheetResponse(Convert.ToInt32(dataRow["DOCENTRY"].ToString()), UserID);
                    listGetSaleQuotationJobSheet.Add(new GetSaleQuotationJobSheet
                    {
                        CustomerCode = dataRow["CARDCODE"].ToString()!,
                        CustomerName = dataRow["CUSTOMERNAME"].ToString()!,
                        SaleQuotationDate = dataRow["QUOTATIONDATE"].ToString()!,
                        Tel = dataRow["TEL"].ToString()!,
                        CustomerRef = dataRow["CustomerRefNo"].ToString()!,
                        Email = dataRow["EMAIL"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        Total = Convert.ToDouble(dataRow["TOTAL"].ToString()),
                        DocNum = dataRow["DOCUMENT"].ToString()!,
                        DocEntry = Convert.ToInt32(dataRow["DOCENTRY"].ToString()),
                        Currency = dataRow["CURRENCY"].ToString(),
                        Lines = a.Result.Data
                    });
                }
                return Task.FromResult(new ResponseData<List<GetSaleQuotationJobSheet>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetSaleQuotationJobSheet
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetSaleQuotationJobSheet>>
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
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetListJobSheet",
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
                        CancelStatus = dataRow["CancelStatus"].ToString()!,
                        DocEntrySO = dataRow["DocEntrySO"].ToString()!,
                        Commodities = dataRow["Commodities"].ToString()!,
                        InvoiceNumber = dataRow["InvoiceNumber"].ToString()!,
                        DownPaymentInvoice = dataRow["DownPaymentInvoice"].ToString()!,
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
        public Task<ResponseData<List<GetJobSheetTruckingEditDraftByDocEntry>>> GetJobSheetTruckingEditDraftByDocEntry(int docEntry)
        {
            try
            {
                var listGetPostJobSheetTrucking = new List<GetJobSheetTruckingEditDraftByDocEntry>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailJobSheetByDocEntry",
                             docEntry.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var a = GetLineJobSheetTruckingByDocEntry(Convert.ToInt32(dataRow["DOCENTRY"].ToString()));
                    listGetPostJobSheetTrucking.Add(new GetJobSheetTruckingEditDraftByDocEntry
                    {
                        DocEntry = Convert.ToInt32(dataRow["DOCENTRY"]),
                        DocNum = Convert.ToInt32(dataRow["DOCNUM"]),
                        ConfirmBookingID = Convert.ToInt32(dataRow["CONFIRMBOOKINGID"]),
                        SaleQuotationDocNum = dataRow["SALEQUOTATIONDOCNUM"].ToString(),
                        CurrencyCosting = dataRow["CURRENCYCOSTING"].ToString(),
                        CurrencySelling = dataRow["CURRENCYSELLING"].ToString(),
                        RemarksCosting = dataRow["REMARKSCOSTING"].ToString(),
                        RemarksSelling = dataRow["REMARKSSELLING"].ToString(),
                        TotalCosting = Convert.ToDouble(dataRow["TOTALCOSTING"]),
                        Rebate = Convert.ToDouble(dataRow["REBATE"]),
                        GrandTotalCosting = Convert.ToDouble(dataRow["GRANDTOTALCOSTING"]),
                        GrandTotalCostingUSD = Convert.ToDouble(dataRow["GRANDTOTALCOSTINGUSD"]),
                        TotalSelling = Convert.ToDouble(dataRow["TOTALSELLING"]),
                        GrandTotalSelling = Convert.ToDouble(dataRow["GRANDTOTALSELLING"]),
                        GrandTotalSellingUSD = Convert.ToDouble(dataRow["GRANDTOTALSELLINGUSD"]),
                        TotalProfit = Convert.ToDouble(dataRow["TOTALPROFIT"]),
                        DutyTaxAmountCosting = Convert.ToDouble(dataRow["DutyTaxAmountCosting"]),
                        DutyTaxAmountSelling = Convert.ToDouble(dataRow["DutyTaxAmountSelling"]),
                        AdvanceAmountCosting = Convert.ToDouble(dataRow["AdvanceAmountCosting"]),
                        AdvanceAmountSelling = Convert.ToDouble(dataRow["AdvanceAmountSelling"]),
                        UserCreate = dataRow["USERCREATE"].ToString(),
                        UpdateBy = dataRow["UpdateBy"].ToString(),
                        CardCode = dataRow["USERCREATE"].ToString(),
                        JobNo = dataRow["JOBNO"].ToString(),
                        CardName = dataRow["CARDNAME"].ToString(),
                        SQREF = dataRow["SQREF"].ToString(),
                        ItemLine = QueryData.ConvertDataTable<GetJobSheetTruckingEditDraftLine>(new GetRecordByDataTable(
                            "GetJobSheetTruckingDetailLineByDocEntry",
                            docEntry.ToString(),
                            "",
                            "",
                            "",
                            "").GetResultDataTable()),
                        Attachment = QueryData.ConvertDataTable<Models.Gets.Attachments>(new GetRecordByDataTable(
                            "GetAttachmentJobSheetTruckingEdit",
                            docEntry.ToString(),
                            "",
                            "",
                            "",
                            "").GetResultDataTable())
                    });
                }
                return Task.FromResult(new ResponseData<List<GetJobSheetTruckingEditDraftByDocEntry>>
                {
                    Data = listGetPostJobSheetTrucking
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetJobSheetTruckingEditDraftByDocEntry>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<GetJobSheetTruckingDetailResponse> GetJobSheetTruckingDetailByDocEntry(int docEntry)
        {
            try
            {
                var listGetPostJobSheetTrucking = new List<PostJobSheetTruckingRequest>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailJobSheetByDocEntry",
                             docEntry.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var a = GetLineJobSheetTruckingByDocEntry(Convert.ToInt32(dataRow["DOCENTRY"].ToString()));
                    listGetPostJobSheetTrucking.Add(new PostJobSheetTruckingRequest
                    {
                        DocEntry = Convert.ToInt32(dataRow["DOCENTRY"]),
                        DocNum = Convert.ToInt32(dataRow["DOCNUM"]),
                        ConfirmBookingID = Convert.ToInt32(dataRow["CONFIRMBOOKINGID"]),
                        SaleQuotationDocNum = dataRow["SALEQUOTATIONDOCNUM"].ToString(),
                        CurrencyCosting = dataRow["CURRENCYCOSTING"].ToString(),
                        CurrencySelling = dataRow["CURRENCYSELLING"].ToString(),
                        RemarksCosting = dataRow["REMARKSCOSTING"].ToString(),
                        RemarksSelling = dataRow["REMARKSSELLING"].ToString(),
                        TotalCosting = Convert.ToDouble(dataRow["TOTALCOSTING"]),
                        Rebate = Convert.ToDouble(dataRow["REBATE"]),
                        GrandTotalCosting = Convert.ToDouble(dataRow["GRANDTOTALCOSTING"]),
                        GrandTotalCostingUSD = Convert.ToDouble(dataRow["GRANDTOTALCOSTINGUSD"]),
                        TotalSelling = Convert.ToDouble(dataRow["TOTALSELLING"]),
                        GrandTotalSelling = Convert.ToDouble(dataRow["GRANDTOTALSELLING"]),
                        GrandTotalSellingUSD = Convert.ToDouble(dataRow["GRANDTOTALSELLINGUSD"]),
                        TotalProfit = Convert.ToDouble(dataRow["TOTALPROFIT"]),
                        DutyTaxAmountCosting = Convert.ToDouble(dataRow["DutyTaxAmountCosting"]),
                        DutyTaxAmountSelling = Convert.ToDouble(dataRow["DutyTaxAmountSelling"]),
                        AdvanceAmountCosting = Convert.ToDouble(dataRow["AdvanceAmountCosting"]),
                        AdvanceAmountSelling = Convert.ToDouble(dataRow["AdvanceAmountSelling"]),
                        UserCreate = dataRow["USERCREATE"].ToString(),
                        UpdateBy = dataRow["UpdateBy"].ToString(),
                        CardCode = dataRow["USERCREATE"].ToString(),
                        JobNo = dataRow["JOBNO"].ToString(),
                        CardName = dataRow["CARDNAME"].ToString(),
                        SQREF = dataRow["SQREF"].ToString(),
                        ItemLine = a.Result,
                        Attachment = QueryData.ConvertDataTable<Models.Gets.Attachments>(new GetRecordByDataTable(
                            "GetAttachmentJobSheetTruckingEdit",
                            docEntry.ToString(),
                            "",
                            "",
                            "",
                            "").GetResultDataTable())
                    });
                }
                return Task.FromResult(new GetJobSheetTruckingDetailResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetPostJobSheetTrucking,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync(Configure.JobSheetTruckingLayoutPrinter).Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetJobSheetTruckingDetailResponse
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
                var JobSheetTruckingEdit = QueryData.ConvertDataTable<GetJobSheetTruckingEdit>(new GetRecordByDataTable(
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
        #endregion
        #region POST
        public Task<PostResponse> PostJobSheetTrucking(PostJobSheetTruckingRequest postJobSheetTruckingRequest, string draftDocument)
        {

            //Documents oGoodReturn;
            Company oCompany;
            //var Retval = 0;
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            oCompany = SAP_Driver_oCompany._CheckingStatusOCompany();
            if (oCompany.InTransaction == true) 
                oCompany.EndTransaction(BoWfTransOpt.wf_RollBack); 
            oCompany.StartTransaction();
            try
            {
                var SODocEntry = "";
                if (draftDocument == "1")
                {
                    #region Create Sale Order
                    Documents SO_invoice = (Documents)oCompany.GetBusinessObject(BoObjectTypes.oOrders);
                    SO_invoice.CardCode = postJobSheetTruckingRequest.CardCode;
                    var loadingDate = DateTime.Parse(GetQuery.GetValueByID("GetLoadingDate",
                        "LoadingDate",
                        postJobSheetTruckingRequest.ConfirmBookingID.ToString()));
                    SO_invoice.DocDate = loadingDate;
                    SO_invoice.DocDueDate = loadingDate;
                    SO_invoice.TaxDate = loadingDate;
                    SO_invoice.RequriedDate = loadingDate;
                    SO_invoice.DocType = BoDocumentTypes.dDocument_Items;
                    SO_invoice.DocCurrency = postJobSheetTruckingRequest.CurrencySelling;
                    //SO_invoice.Comments = postJobSheetTruckingRequest.RemarksSelling;
                    //SO_invoice.DocTotal = postJobSheetTruckingRequest.GrandTotalSelling;
                    SO_invoice.ShipToCode = "";
                    SO_invoice.NumAtCard = postJobSheetTruckingRequest.JobNo;
                    SO_invoice.UserFields.Fields.Item("U_RemarkCosting").Value = ClearEmptyString.clearEmptyString(postJobSheetTruckingRequest.RemarksCosting);
                    SO_invoice.UserFields.Fields.Item("U_Remarks").Value = ClearEmptyString.clearEmptyString(postJobSheetTruckingRequest.RemarksSelling);
                    decimal exchangeRate = Convert.ToDecimal(GetQuery.GetValueByID("GetExchangeRateByLoadingDate", "ExchangeRate", postJobSheetTruckingRequest.ConfirmBookingID.ToString(), postJobSheetTruckingRequest.CurrencySelling));
                    if (postJobSheetTruckingRequest.CurrencySelling != "THS" && postJobSheetTruckingRequest.CurrencySelling != "THB")
                    {
                        SO_invoice.DocRate = Math.Round((double)exchangeRate, 6);
                    }
                    else
                    {
                        SO_invoice.DocCurrency = postJobSheetTruckingRequest.CurrencySelling;
                    }
                    foreach (var a in postJobSheetTruckingRequest.ItemLine)
                    {
                        if (a.qtySelling != 0)
                        {
                            if (a.itemType == "Q")
                            {
                                SO_invoice.Lines.BaseLine = a.lineNum - 1;
                                SO_invoice.Lines.BaseEntry = Convert.ToInt32(postJobSheetTruckingRequest.SaleQuotationDocNum);
                                SO_invoice.Lines.BaseType = 23;
                                var index=postJobSheetTruckingRequest.ItemLine.IndexOf(a);
                                postJobSheetTruckingRequest.ItemLine.Where(x => x.lineNum == a.lineNum).ToList().ForEach(c=>{
                                    c.lineNum = 0;
                                    c.itemType = "I";
                                });
                                postJobSheetTruckingRequest.ItemLine[index].lineNum = a.lineNum;
                                postJobSheetTruckingRequest.ItemLine[index].itemType = "Q";
                            }
                            SO_invoice.Lines.ItemCode = a.itemCode;
                            SO_invoice.Lines.Quantity = a.qtySelling;
                            if (postJobSheetTruckingRequest.CurrencySelling != "THB" && postJobSheetTruckingRequest.CurrencySelling != "THS")
                            {
                                SO_invoice.Lines.Currency = postJobSheetTruckingRequest.CurrencySelling;
                                SO_invoice.Lines.Rate = Math.Round((double)exchangeRate, 7);
                                SO_invoice.Lines.UnitPrice = Math.Round(a.priceSelling, 7);
                            }
                            else
                            {
                                SO_invoice.Lines.UnitPrice = Math.Round(a.priceSelling, 7);
                            }
                            SO_invoice.Lines.ProjectCode = postJobSheetTruckingRequest.JobNo;
                            SO_invoice.Lines.CostingCode = postJobSheetTruckingRequest.CostCenter;
                            SO_invoice.Lines.TaxCode = "S00";
                            if (a.UomCode != 0)
                            {
                                SO_invoice.Lines.UoMEntry = a.UomCode;
                            }
                            a.lineNum = SO_invoice.Lines.LineNum;
                            SO_invoice.Lines.Add();
                        }
                    }
                    if (postJobSheetTruckingRequest.DutyTaxAmountSelling != 0)
                    {
                        SO_invoice.SpecialLines.LineType = BoDocSpecialLineType.dslt_Text;
                        SO_invoice.SpecialLines.LineText = "Duty Tax";
                        //SO_invoice.SpecialLines.SetCurrentLine(postJobSheetTruckingRequest.ItemLine.Last().lineNum);
                        SO_invoice.SpecialLines.AfterLineNumber = postJobSheetTruckingRequest.ItemLine.Last(x => x.qtySelling != 0).lineNum;
                        SO_invoice.SpecialLines.Add();
                        //SO_invoice.Lines.Add();
                        SO_invoice.Lines.ItemCode = Configure.DutyTaxCode;
                        SO_invoice.Lines.Quantity = 1;
                        SO_invoice.Lines.UnitPrice = postJobSheetTruckingRequest.DutyTaxAmountSelling;
                        SO_invoice.Lines.LineTotal = postJobSheetTruckingRequest.DutyTaxAmountSelling;
                        SO_invoice.Lines.ProjectCode = postJobSheetTruckingRequest.JobNo;
                        SO_invoice.Lines.COGSCostingCode = postJobSheetTruckingRequest.CostCenter;
                        postJobSheetTruckingRequest.ItemLine.Add(new JobSheetRuckingLineForItem
                        {
                            lineNum = SO_invoice.Lines.LineNum,
                            itemType = "DutyTax",
                            qtySelling = 1,
                        });
                        SO_invoice.Lines.Add();
                    }
                    int RetValSO = SO_invoice.Add();
                    if (RetValSO != 0)
                    {
                        oCompany.GetLastError(out ErrCode, out ErrMsg);
                        if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        return Task.FromResult(new PostResponse
                        {
                            ErrorCode = ErrCode,
                            ErrorMsg = ErrMsg,
                            DocEntry = null
                        });
                    }
                    SODocEntry = oCompany.GetNewObjectKey();
                    #endregion
                }
                #region Add JOB SHEET TRUCKING
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                GeneralData oChild;
                GeneralDataCollection oChildren;
                GeneralData oChild1;
                GeneralData oChild2;
                GeneralDataCollection oChildren1;
                GeneralDataCollection oChildren2;
                //GeneralDataParams oGeneralParams;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("JOBSHEETRUCKING");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                oGeneralData.SetProperty("U_CONFIRMBOOKINGID", postJobSheetTruckingRequest.ConfirmBookingID);
                oGeneralData.SetProperty("U_CARDCODE", postJobSheetTruckingRequest.CardCode);
                oGeneralData.SetProperty("U_SALEQUOTATIONDOCNUM", postJobSheetTruckingRequest.SaleQuotationDocNum);
                if (draftDocument == "1")
                {
                    oGeneralData.SetProperty("U_SALESORDERDOCNUM", SODocEntry);
                }
                oGeneralData.SetProperty("U_TRUCKWAYBILLBY", (postJobSheetTruckingRequest.TuckWayBillBy == null) ? "" : postJobSheetTruckingRequest.TuckWayBillBy);
                oGeneralData.SetProperty("U_CURRENCYCOSTING", postJobSheetTruckingRequest.CurrencyCosting);
                oGeneralData.SetProperty("U_CURRENCYSELLING", postJobSheetTruckingRequest.CurrencySelling);
                oGeneralData.SetProperty("U_REMARKSCOSTING", (postJobSheetTruckingRequest.RemarksCosting == null) ? "" : postJobSheetTruckingRequest.RemarksCosting);
                oGeneralData.SetProperty("U_REMAKRSSELLING", (postJobSheetTruckingRequest.RemarksSelling == null) ? "" : postJobSheetTruckingRequest.RemarksSelling);
                oGeneralData.SetProperty("U_TOTALCOSTING", postJobSheetTruckingRequest.TotalCosting);
                oGeneralData.SetProperty("U_REBATE", postJobSheetTruckingRequest.Rebate);
                oGeneralData.SetProperty("U_GRANDTOTALCOSTING", postJobSheetTruckingRequest.GrandTotalCosting);
                oGeneralData.SetProperty("U_GRANDTOTALCOSTINGUSD", postJobSheetTruckingRequest.GrandTotalCostingUSD);
                oGeneralData.SetProperty("U_TOTALSELLING", postJobSheetTruckingRequest.TotalSelling);
                oGeneralData.SetProperty("U_GRANDTOTALSELLING", postJobSheetTruckingRequest.GrandTotalSelling);
                oGeneralData.SetProperty("U_GRANDTOTALSELLINGUSD", postJobSheetTruckingRequest.GrandTotalSellingUSD);
                oGeneralData.SetProperty("U_DutyTaxAmountCosting", postJobSheetTruckingRequest.DutyTaxAmountCosting);
                oGeneralData.SetProperty("U_DutyTaxAmountSelling", postJobSheetTruckingRequest.DutyTaxAmountSelling);
                oGeneralData.SetProperty("U_AdvanceAmountCosting", postJobSheetTruckingRequest.AdvanceAmountCosting);
                oGeneralData.SetProperty("U_AdvanceAmountSelling", postJobSheetTruckingRequest.AdvanceAmountSelling);
                oGeneralData.SetProperty("U_TOTALPROFIT", postJobSheetTruckingRequest.TotalProfit);
                oGeneralData.SetProperty("U_USERCREATE", ClearEmptyString.clearEmptyString(postJobSheetTruckingRequest.UserCreate));
                oGeneralData.SetProperty("U_JOBNO", postJobSheetTruckingRequest.JobNo);
                oGeneralData.SetProperty("U_SELLINGCONFIRM", "Y");
                oGeneralData.SetProperty("U_COSTINGCONFIRM", ClearEmptyString.clearEmptyString(postJobSheetTruckingRequest.ConfirmCostingButton));
                //Add Line
                oChildren = oGeneralData.Child("JOBTRUCKCOSTING");
                foreach (var a in postJobSheetTruckingRequest.ItemLine)
                {
                    if (a.itemType != "DutyTax")
                    {
                        oChild = oChildren.Add();
                        oChild.SetProperty("U_ITEMCODE", a.itemCode);
                        oChild.SetProperty("U_Qty", a.qtyCosting);
                        oChild.SetProperty("U_TOTALPRICE", a.priceCosting);
                        oChild.SetProperty("U_ContainerType", a.UomCode);
                    }
                }
                oChildren1 = oGeneralData.Child("JOBTRUCKINGSELLING");
                foreach (var a in postJobSheetTruckingRequest.ItemLine)
                {
                    if (a.itemType != "DutyTax")
                    {
                        oChild1 = oChildren1.Add();
                        oChild1.SetProperty("U_ITEMCODE", a.itemCode);
                        oChild1.SetProperty("U_Qty", a.qtySelling);
                        oChild1.SetProperty("U_TOTALPRICE", a.priceSelling);
                        oChild1.SetProperty("U_ContainerType", a.UomCode);
                        oChild1.SetProperty("U_ItemType", a.itemType);
                        oChild1.SetProperty("U_LineNum", a.lineNum.ToString());
                    }
                }
                if (postJobSheetTruckingRequest.Attachment != null)
                {
                    oChildren2 = oGeneralData.Child("ATTACHMENT");
                    foreach (var a in postJobSheetTruckingRequest.Attachment)
                    {
                        oChild2 = oChildren2.Add();
                        oChild2.SetProperty("U_ATTACHMENTNAME", a.ATTACHMENTNAME);
                    }
                }

                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                int DocEntryConfirmBookingSheet = (int)responseoGeneralService.GetProperty("DocEntry");
                #endregion
                #region Update DocEntry Of ConfirmBookingSheet link with JobSheetTrucking
                GeneralService oConfrimBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                //GeneralData oBookingChildUpdate;
                //GeneralDataCollection oBookingChildrenUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService oConfirmBookingCmpSrvUpdate;
                oConfirmBookingCmpSrvUpdate = oCompany.GetCompanyService();
                oConfrimBookingGeneralServiceUpdate = oConfirmBookingCmpSrvUpdate.GetGeneralService("CONFIRMBOOKINGSHEET");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfrimBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", postJobSheetTruckingRequest.ConfirmBookingID);
                oConfirmBookingGeneralDataUpdate = oConfrimBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_JOBSHEETID", DocEntryConfirmBookingSheet);
                //Add Childrence
                oConfrimBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                #endregion
                if (draftDocument == "1")
                {
                    #region AddLine Project Management Purchase Request
                    var absEntry = Convert.ToInt32(GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", postJobSheetTruckingRequest.JobNo));
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
                    stage.Description = "Job Sheet Trucking";
                    stage.IsFinished = BoYesNoEnum.tNO;
                    PM_DocumentsCollection documentsCollection = projectUpdateProjectManagement.PM_DocumentsCollection;
                    PM_DocumentData document = documentsCollection.Add();
                    document.StageID = Convert.ToInt32(GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                    document.DocType = PMDocumentTypeEnum.pmdt_SalesOrder;
                    document.DocEntry = Convert.ToInt32(SODocEntry);
                    //foreach (var a in postJobSheetTruckingRequest.ItemLine)
                    //{
                    //    if (a.qtySelling != 0)
                    //    {
                    //        PM_DocumentData document = documentsCollection.Add();
                    //        document.StageID = Convert.ToInt32(GetValueByID("GETLASTROWSTAGE", "LASTROWSTAGE", absEntry.ToString()));
                    //        document.DocType = PMDocumentTypeEnum.pmdt_SalesOrder;
                    //        document.DocEntry = Convert.ToInt32(SODocEntry);
                    //        //document.LineNumber = a.lineNum;
                    //    }
                    //}
                    //End Sale Order
                    pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                    #endregion
                }
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = 0,
                    ErrorMsg="",
                    DocEntry = DocEntryConfirmBookingSheet.ToString(),
                });
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                return Task.FromResult(new PostResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        #endregion
        #region Put
        public Task<PostResponse> PutJobSheetTrucking(GetJobSheetTruckingEdit updateJobSheetTruckingEdit)
        {
            var a = JobSheetTruckings._Update(updateJobSheetTruckingEdit, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        public Task<PostResponse> PutAttachmentJobSheetTrucking(GetJobSheetTruckingEdit updateJobSheetTruckingEdit)
        {
            var a = JobSheetTruckings._UpdateAttachment(updateJobSheetTruckingEdit, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        public Task<PostResponse> PutJobSheetTruckingDraft(UpdateDraftJobSheetTrucking updateJobSheetTruckingEdit, string draftDocument)
        {
            var a = JobSheetTruckings._UpdateDraft(updateJobSheetTruckingEdit, draftDocument, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        #endregion
        #region Delete
        public Task<PostResponse> CancelJobSheetTrucking(string docEntry, string docEntrySO, string updateBy)
        {
            var a = JobSheetTruckings._Cancel(Convert.ToInt32(docEntry), Convert.ToInt32(docEntrySO), updateBy, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
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
        public Task<ResponseData<List<GetItemSaleQuotationJobSheet>>> GetGetItemSaleQuotationJobSheetResponse(int DocEntry, int UserID)
        {
            try
            {
                var listGetItemSaleQuotationJobSheet = new List<GetItemSaleQuotationJobSheet>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETITEMSALEQUOTATIONJOBSHEET",
                             DocEntry.ToString(),
                             UserID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    //var uomlist= JsonSerializer.Serialize(GetGetUomGroups(dataRow["UomEntry"].ToString()));
                    //GetGetUomGroups
                    listGetItemSaleQuotationJobSheet.Add(new GetItemSaleQuotationJobSheet
                    {
                        lineNum = Convert.ToInt32(dataRow["ROWNUM"].ToString()),
                        itemCode = dataRow["ITEMCODE"].ToString()!,
                        itemName = dataRow["ITEMNAME"].ToString()!,
                        itemType = dataRow["ITEMTYPE"].ToString()!,
                        priceSelling = Convert.ToDouble(dataRow["PRICESELLING"].ToString()),
                        priceCosting = Convert.ToDouble(dataRow["PRICECOSTING"].ToString()),
                        uomCodeList = dataRow["UomEntry"].ToString(),
                        remarks = dataRow["Remarks"].ToString()
                    });
                }
                return Task.FromResult(new ResponseData<List<GetItemSaleQuotationJobSheet>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetItemSaleQuotationJobSheet
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetItemSaleQuotationJobSheet>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetUomGroup>>> GetUomGroups(string uomEntry)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetUomGroup>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetUomGroup>(new GetRecordByDataTable(
                             "GetUomGroupByUomEntry",
                             uomEntry,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetUomGroup>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<List<JobSheetRuckingLineForItem>> GetLineJobSheetTruckingByDocEntry(int DocEntry)
        {
            try
            {
                var listGetJobSheetRuckingLineForItem = new List<JobSheetRuckingLineForItem>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailJobSheetLineByDocEntry",
                             DocEntry.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetJobSheetRuckingLineForItem.Add(new JobSheetRuckingLineForItem
                    {
                        itemCode = dataRow["ITEMCODE"].ToString(),
                        priceCosting = Convert.ToDouble(dataRow["COSTINGTOTALPRICE"].ToString()),
                        qtyCosting = Convert.ToDouble(dataRow["COSTINGQTY"].ToString()),
                        priceSelling = Convert.ToDouble(dataRow["SELLINGTOTALPRICE"].ToString()),
                        qtySelling = Convert.ToDouble(dataRow["SELLINGQTY"].ToString()),
                        UomName = dataRow["UomName"].ToString(),
                        itemType = dataRow["Type"].ToString(),
                        LineId = Convert.ToInt32(dataRow["LineId"]),
                    });
                }
                return Task.FromResult(listGetJobSheetRuckingLineForItem);
            }
            catch //(Exception ex)
            {
                return Task.FromResult(new List<JobSheetRuckingLineForItem>());
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
