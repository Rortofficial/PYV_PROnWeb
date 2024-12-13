using Client.Connection;
using Client.Lib.BookingSheetSeaAndAir;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class BookingSheetSeaAndAir : IBookingSheetSeaAndAir
    {
        #region Get
        public Task<ResponseData<List<GetOrigin>>> GetOriginResponseAsync()
        {
            try
            {
                var listGetOrigin = new List<GetOrigin>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.TransactionSeaAir,
                             "GETORIGIN_SEA_AIR",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetOrigin.Add(new GetOrigin
                    {
                        Code = Convert.ToInt32(dataRow["CODE"].ToString()),
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetOrigin>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetOrigin
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
        public Task<ResponseData<List<GetDestination>>> GetDestinationResponseAsync()
        {
            try
            {
                var listGetDestination = new List<GetDestination>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.TransactionSeaAir,
                             "GETDESTINATION_SEA_AIR",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetDestination.Add(new GetDestination
                    {
                        Code = Convert.ToInt32(dataRow["CODE"].ToString()),
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetDestination>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDestination
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
        public Task<ResponseData<List<GetJobNumberSeaAndAir>>> GetJobNumberSeaAndAirResponseAsync()
        {
            try
            {
                var obj = QueryData.ConvertDataTable<GetJobNumberSeaAndAir>(new GetRecordByDataTable(
                    StoreType.TransactionSeaAir,
                    "GetJobNumberSeaAndAir",
                    "",
                    "",
                    "",
                    "",
                    "").GetResultDataTable());
                for (int i = 0; i < obj.Count(); i++)
                {
                    obj[i].ServiceTypeSeaAndAirByJobNumber = QueryData.ConvertDataTable<GetServiceTypeSeaAndAir>(new GetRecordByDataTable(
                             "GetServiceTypeSeaAndAirByJobNumber",
                             obj[i].Code,
                             "",
                             "",
                             "",
                             "").GetResultDataTable());
                }
                return Task.FromResult(new ResponseData<List<GetJobNumberSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = obj,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetJobNumberSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetCoLoaderSeaAndAir>>> GetCoLoaderSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetCoLoaderSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetCoLoaderSeaAndAir>(new GetRecordByDataTable(
                        StoreType.TransactionSeaAir,
                        "GetCOLoaderSeaAndAir",
                        "",
                        "",
                        "",
                        "",
                        "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCoLoaderSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetCustomerSeaAndAir>>> GetCustomerSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetCustomerSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetCustomerSeaAndAir>(new GetRecordByDataTable(
                        StoreType.TransactionSeaAir,
                        "GetCustomerSeaAndAir",
                        "",
                        "",
                        "",
                        "",
                        "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCustomerSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetShippingLineSeaAndAir>>> GetShippingLineSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetShippingLineSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetShippingLineSeaAndAir>(new GetRecordByDataTable(
                        StoreType.TransactionSeaAir,
                        "GetShippingLineSeaAndAir",
                        "",
                        "",
                        "",
                        "",
                        "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetShippingLineSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<Get_DEST_AGENTSeaAndAir>>> Get_DEST_AGENTSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<Get_DEST_AGENTSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<Get_DEST_AGENTSeaAndAir>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "Get_DEST_AGENTSeaAndAir",
                            "",
                            "",
                            "",
                            "",
                            "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<Get_DEST_AGENTSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetPortOfReceiptSeaAndAir>>> GetPortOfReceiptSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetPortOfReceiptSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetPortOfReceiptSeaAndAir>(new GetRecordByDataTable(
                        StoreType.TransactionSeaAir,
                        "GetPortOfReceiptSeaAndAir",
                        "",
                        "",
                        "",
                        "",
                        "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetPortOfReceiptSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetPortOfDischargeSeaAndAir>>> GetPortOfDischargeSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetPortOfDischargeSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetPortOfDischargeSeaAndAir>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetPortOfDischargeSeaAndAir",
                            "",
                            "",
                            "",
                            "",
                            "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetPortOfDischargeSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetCYAtOrContactPersonSeaAndAir>>> GetCYAtOrContactPersonSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetCYAtOrContactPersonSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetCYAtOrContactPersonSeaAndAir>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetCYAtOrContactPersonSeaAndAir",
                            "",
                            "",
                            "",
                            "",
                            "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCYAtOrContactPersonSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetReturnAtOrContactPersonSeaAndAir>>> GetReturnAtOrContactPersonSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetReturnAtOrContactPersonSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetReturnAtOrContactPersonSeaAndAir>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetReturnAtOrContactPersonSeaAndAir",
                            "",
                            "",
                            "",
                            "",
                            "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetReturnAtOrContactPersonSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetLoadingAtSeaAndAir>>> GetLoadingAtSeaAndAirResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetLoadingAtSeaAndAir>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetLoadingAtSeaAndAir>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetLoadingAtSeaAndAir",
                            "",
                            "",
                            "",
                            "",
                            "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetLoadingAtSeaAndAir>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetBookingSheetByUser>>> GetBookingSheetByUserSeaAndAirResponseAsync(string dateFrom, string dateTo, string field, string userID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetBookingSheetByUser>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetBookingSheetByUser>(new GetRecordByDataTable(
                            StoreType.TransactionSeaAir,
                            "GetListBookingSheetSea_Air",
                            dateFrom,
                            dateTo,
                            field,
                            userID,
                            "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetBookingSheetByUser>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetBookingSheetSeaAndAir>> GetBookingSheetByDocEntrySeaAndAirResponseAsync(string docEntry)
        {
            try
            {
                var obj = new GetBookingSheetSeaAndAir();
                obj.HeaderObj = QueryData.ConvertDataTable<GetBookingSheetSeaAndAirHeader>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetHeaderResponse", par2: docEntry).GetResultDataTable())[0];
                obj.ListCOLoader = QueryData.ConvertDataTable<GetCOLoaderSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetCOLoaderSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListCustomer = QueryData.ConvertDataTable<GetListCustomerSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetCustomerSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListShipper = QueryData.ConvertDataTable<GetShipperSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetShipperSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListConsignee = QueryData.ConvertDataTable<GetConsigneeSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetConsigneeSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListShippingLine = QueryData.ConvertDataTable<GetShippingLineSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetShippingLineSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListDestAgent = QueryData.ConvertDataTable<GetDestAgentSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetDestAgentSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListCommodities = QueryData.ConvertDataTable<GetCommoditySeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetCommoditiesSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListOverseaTransportSeaAndAir = QueryData.ConvertDataTable<GetOverseaTransportSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetOverseaTransportSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListPortOfReceiptSeaAndAir = QueryData.ConvertDataTable<GetPortOfReceiptSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetPortOfReceiptSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListPortOfDischargeSeaAndAir = QueryData.ConvertDataTable<GetPortOfDischargeSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetPortOfDischargeSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListPlaceOfLoadingSeaAndAir = QueryData.ConvertDataTable<GetPlaceOfLoadingSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetPlaceOfLoadingSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListPlaceOfDeliverySeaAndAir = QueryData.ConvertDataTable<GetPlaceOfDeliverySeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetPlaceOfDeliverySeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListThaiForwarderSeaAndAir = QueryData.ConvertDataTable<GetThaiForwarderSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetThaiForwarderSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListContainerTypeSeaAndAir = QueryData.ConvertDataTable<GetContainerTypeSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetContainerTypeSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListOverSeaForwarderSeaAndAir = QueryData.ConvertDataTable<GetOverSeaForwarderSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetOverSeaForwarderSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListThaiBorderSeaAndAir = QueryData.ConvertDataTable<GetThaiBorderSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetThaiBorderSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListTruckTypeSeaAndAir = QueryData.ConvertDataTable<GetTruckTypeSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetTruckTypeSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListCYAt_ContactSeaAndAir = QueryData.ConvertDataTable<GetCYAt_ContactSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetCYAt_ContactSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListReturnAt_ContactSeaAndAir = QueryData.ConvertDataTable<GetReturnAt_ContactSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetReturnAt_ContactSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.ListLoadingAtSeaAndAir = QueryData.ConvertDataTable<GetLoadingAtSeaAndAirViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetLoadingAtSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                obj.FlightsInformations = QueryData.ConvertDataTable<GetFlightsInformationViewDetail>(new GetRecordByDataTable(StoreType.TransactionSeaAir,
                                    Type: "GetViewDetail_Sea_Air", par1: "GetFlightsInformationSeaAndAirResponse", par2: docEntry).GetResultDataTable());
                return Task.FromResult(new ResponseData<GetBookingSheetSeaAndAir>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = obj,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetBookingSheetSeaAndAir>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Post
        public Task<PostResponse> PostBookingSheetSeaAndAir(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir)
        {
            var a = BookingSheetSeaAndAirs._Add(addBookingSheetSeaAndAir, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        #endregion
        #region Put
        public Task<PostResponse> PutBookingSheetSeaAndAir(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir)
        {
            var a = BookingSheetSeaAndAirs._Update(addBookingSheetSeaAndAir, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        public Task<PostResponse> PutCommodityInBookingSheet(string CreateUser, int docEntry, List<CommoditySeaAndAir> commodity, HeaderCommodityUpdate headerCommodityUpdate, List<DestAgentUpdateCommodities> listDestAgent, List<ShipperUpdateCommodities> listShipper, List<ConsigneeUpdateCommodities> listConsignee, List<CustomerUpdateCommodities> listCustomer)
        {
            var a = BookingSheetSeaAndAirs._UpdateCommodities(CreateUser, docEntry, commodity, headerCommodityUpdate,listDestAgent,listShipper,listConsignee, listCustomer, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        #endregion
        #region Delete
        public Task<DeleteResponse> CancelBookingSheetSeaAndAirAsync(string docNum)
        {
            try
            {
                if (GetQuery.GetValueByID("GETSTATUSCANCELBOOKINGSHEETSEA_AND_AIR", "ValueValid", docNum) == "0")
                {
                    var a = BookingSheetSeaAndAirs._Cancel(docNum, SAP_Driver_oCompany._CheckingStatusOCompany());
                    return Task.FromResult(new DeleteResponse
                    {
                        ErrorCode = a.ErroreCode,
                        ErrorMsg = a.ErroreMessage,
                        DocEntry = a.DocEntry.ToString()
                    });
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
        #endregion
        #region Function
        private Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync()
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.BookingSheetSeaAirLayoutPrinter!,
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
