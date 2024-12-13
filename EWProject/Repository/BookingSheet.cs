using Client.Connection;
using Client.Lib.BookingSheet;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class BookingSheet : IBookingSheet
    {
        #region Get
        public Task<ResponseData<List<GetSaleEmployee>>> GetSaleEmployeeResponseAsync()
        {
            try
            {
                var listGetSaleEmployee = new List<GetSaleEmployee>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "GETSALEPERSON",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetSaleEmployee.Add(new GetSaleEmployee
                    {
                        SlpCode = Convert.ToInt32(dataRow["SLPCODE"].ToString()),
                        SlpName = dataRow["SLPNAME"].ToString()!,
                        VendorCode = dataRow["VendorCode"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetSaleEmployee>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetSaleEmployee
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
        public Task<ResponseData<List<GetConsignee>>> GetConsigneeResponseAsync()
        {
            try
            {
                var listGetConsignee = new List<GetConsignee>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "CONSIGNEE",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetConsignee.Add(new GetConsignee
                    {
                        CardCode = dataRow["CUSTOMERCODE"].ToString()!,
                        CardName = dataRow["CUSTOMERNAME"].ToString()!,
                        Balance = Convert.ToDouble(dataRow["BALANCE"].ToString()),
                        TaxID = dataRow["TAXID"].ToString()!,
                        Country = dataRow["COUNTRY"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetConsignee>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetConsignee
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetConsignee>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetContainerType>>> GetContainerTypeResponseAsync()
        {
            try
            {
                var listGetContainerType = new List<GetContainerType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "CONTAINERTYPE",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetContainerType.Add(new GetContainerType
                    {
                        Code = dataRow["CODE"].ToString(),
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetContainerType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetContainerType
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetContainerType>>
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
                var listGetCO = new List<GetCO>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "GETCO",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetCO.Add(new GetCO
                    {
                        CardCode = dataRow["CUSTOMERCODE"].ToString()!,
                        CardName = dataRow["CUSTOMERNAME"].ToString()!,
                        Balance = Convert.ToDecimal(dataRow["BALANCE"].ToString()),
                        TaxID = dataRow["TAXID"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetCO>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetCO
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
        public Task<ResponseData<List<GetCYORCFS>>> GetCYORCFSResponseAsync()
        {
            try
            {
                var listGetCYORCFS = new List<GetCYORCFS>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "CYORCFS",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetCYORCFS.Add(new GetCYORCFS
                    {
                        Code = Convert.ToInt32(dataRow["CODE"].ToString()),
                        Name = dataRow["NAME"].ToString()!
                    });
                }
                return Task.FromResult(new ResponseData<List<GetCYORCFS>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetCYORCFS
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetCYORCFS>>
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
                             StoreType.Transaction,
                             "GETDESTINATION",
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
        public Task<ResponseData<List<GetImportType>>> GetImportTypeResponseAsync()
        {
            try
            {
                var listGetImportType = new List<GetImportType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "GETIMPORTTYPE",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetImportType.Add(new GetImportType
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetImportType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetImportType
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetImportType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetLCLORFCL>>> GetLCLORFCLResponseAsync()
        {
            try
            {
                var listGetLCLORFCL = new List<GetLCLORFCL>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "LCLORFCL",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetLCLORFCL.Add(new GetLCLORFCL
                    {
                        Code = Convert.ToInt32(dataRow["CODE"].ToString()),
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetLCLORFCL>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetLCLORFCL
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetLCLORFCL>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetLOLOYARDORUNLOADING>>> GetLOLOYARDORUNLOADINGResponseAsync()
        {
            try
            {
                var listGetLOLOYARDORUNLOADING = new List<GetLOLOYARDORUNLOADING>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "LOLOYARDORUNLOADING",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetLOLOYARDORUNLOADING.Add(new GetLOLOYARDORUNLOADING
                    {
                        Code = Convert.ToInt32(dataRow["CODE"].ToString()),
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetLOLOYARDORUNLOADING>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetLOLOYARDORUNLOADING
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetLOLOYARDORUNLOADING>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetOrigin>>> GetOriginResponseAsync()
        {
            try
            {
                var listGetOrigin = new List<GetOrigin>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "GETORIGIN",
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
        public Task<ResponseData<List<GetOverseaForwarder>>> GetOverseaForwarderResponseAsync()
        {
            try
            {
                var listGetOverseaForwarder = new List<GetOverseaForwarder>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "OVERSEAFORWARDER",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetOverseaForwarder.Add(new GetOverseaForwarder
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                        Country = dataRow["COUNTRY"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetOverseaForwarder>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetOverseaForwarder
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetOverseaForwarder>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetOverseaTrucker>>> GetOverseaTruckerResponseAsync()
        {
            try
            {
                var listGetOverseaTrucker = new List<GetOverseaTrucker>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "OVERSEATRUCKER",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetOverseaTrucker.Add(new GetOverseaTrucker
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                        Country = dataRow["COUNTRY"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetOverseaTrucker>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetOverseaTrucker
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetOverseaTrucker>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetPlaceOfDelivery>>> GetPlaceOfDeliveryResponseAsync()
        {
            try
            {
                var listGetPlaceOfDelivery = new List<GetPlaceOfDelivery>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "PLACEOFDELIVERY",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetPlaceOfDelivery.Add(new GetPlaceOfDelivery
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetPlaceOfDelivery>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetPlaceOfDelivery
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetPlaceOfDelivery>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetPlaceOfLoading>>> GetPlaceOfLoadingResponseAsync()
        {
            try
            {
                var listGetPlaceOfLoading = new List<GetPlaceOfLoading>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "PLACEOFLOADING",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetPlaceOfLoading.Add(new GetPlaceOfLoading
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                        Country = dataRow["COUNTRY"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetPlaceOfLoading>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetPlaceOfLoading
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetPlaceOfLoading>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetRoute>>> GetRouteResponseAsync()
        {
            try
            {
                var listGetRoute = new List<GetRoute>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETROUTE",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetRoute.Add(new GetRoute
                    {
                        Code = Convert.ToInt32(dataRow["CODE"].ToString()),
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetRoute>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetRoute
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetRoute>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetShipper>>> GetShipperResponseAsync()
        {
            try
            {
                var listGetShipper = new List<GetShipper>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETSHIPPER",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetShipper.Add(new GetShipper
                    {
                        CardCode = dataRow["CUSTOMERCODE"].ToString()!,
                        CardName = dataRow["CUSTOMERNAME"].ToString()!,
                        Balance = Convert.ToDouble(dataRow["BALANCE"].ToString()),
                        //TaxID = dataRow["TAXID"].ToString()!,
                        Country = dataRow["COUNTRY"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetShipper>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetShipper
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetShipper>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetThaiBorder>>> GetThaiBorderResponseAsync()
        {
            try
            {
                var listGetThaiBorder = new List<GetThaiBorder>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "THAIBORDER",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetThaiBorder.Add(new GetThaiBorder
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetThaiBorder>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetThaiBorder
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetThaiBorder>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetThaiForwarder>>> GetThaiForwarderResponseAsync()
        {
            try
            {
                var listGetThaiForwarder = new List<GetThaiForwarder>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "THAIFORWARDER",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetThaiForwarder.Add(new GetThaiForwarder
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetThaiForwarder>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetThaiForwarder
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetThaiForwarder>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetThaiTrucker>>> GetThaiTruckerResponseAsync()
        {
            try
            {
                var listGetThaiTrucker = new List<GetThaiTrucker>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "THAITRUCKER",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetThaiTrucker.Add(new GetThaiTrucker
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetThaiTrucker>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetThaiTrucker
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetThaiTrucker>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetVolume>>> GetVolumeResponseAsync()
        {
            try
            {
                var listGetVolume = new List<GetVolume>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "VOLUME",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetVolume.Add(new GetVolume
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetVolume>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetVolume
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetVolume>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetDocNumBookingSheet>> GetDocNumBookingSheetResponseAsync()
        {
            try
            {
                var getDocNumBookingSheet = new GetDocNumBookingSheet();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETBOOKINGDOCNUMMAX",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    getDocNumBookingSheet.BookingID = Convert.ToInt32(dataRow["MAXDOCNUM"].ToString());
                    getDocNumBookingSheet.JOBNO = dataRow["JOBNO"].ToString()!;
                }
                return Task.FromResult(new ResponseData<GetDocNumBookingSheet>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDocNumBookingSheet
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetDocNumBookingSheet>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetBookingSheetByUser>>> GetBookingSheetByUserResponseAsync(string dateFrom, string dateTo, string value, string userID)
        {
            try
            {
                var getBookingSheetByUsersList = new List<GetBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "ListBookingSheet",
                             "BookingSheet",
                             dateFrom,
                             dateTo,
                             value,
                             userID).GetResultDataTable().Rows)
                {
                    getBookingSheetByUsersList.Add(new GetBookingSheetByUser
                    {
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        BookingID = dataRow["BOOKINGID"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        BookingDate = dataRow["BOOKINGDATE"].ToString()!,
                        UPDATEDATE = dataRow["UPDATEDATE"].ToString()!,
                        CREATETIME = dataRow["CREATETIME"].ToString()!,
                        UPDATETIME = dataRow["UPDATETIME"].ToString()!,
                        LoadingDate = dataRow["LOADINGDATE"].ToString()!,
                        ETARequirementDate = dataRow["ETARequirementDate"].ToString()!,
                        ImportType = dataRow["IMPORTYPE"].ToString()!,
                        Volumn = dataRow["Volumn"].ToString()!,
                        ContainerType = dataRow["ContainerType"].ToString()!,
                        Consignee = dataRow["Consignee"].ToString()!,
                        Shipper = dataRow["Shipper"].ToString()!,
                        CO = dataRow["CO"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        DocStatus = dataRow["DOCSTATUS"].ToString()!,
                        DocStatusCancel = dataRow["DOCSTATUSCANCEL"].ToString()!,
                        CreateBy = dataRow["CREATEBY"].ToString()!,
                        UpdateBy = dataRow["UpdateBy"].ToString()!,
                        StatusUpdate = dataRow["UPDATESTATUS"].ToString(),
                        COMMODITYS = dataRow["COMMODITYS"].ToString(),
                        SaleEmployee = dataRow["SaleEmployee"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetBookingSheetByUser>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getBookingSheetByUsersList
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
        public Task<GetDetailBookingSheetByDocEntryResponse> GetBookingSheetByDocEntryResponseAsync(string docEntry)
        {
            try
            {
                var getDetailBookingSheetByDocEntryList = new GetDetailBookingSheetByDocEntry();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "Header",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var shipper = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "SHIPPER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        shipper.Add(rowShipper["SHIPPER"].ToString()!);
                    }
                    var consignee = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "CONSIGNEE",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        consignee.Add(rowShipper["CONSIGNEE"].ToString()!);
                    }
                    var salesQuotation = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "SALESQUOTATION",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        salesQuotation.Add(rowShipper["SALESQUOTATION"].ToString()!);
                    }
                    var COMMODITY = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "COMMODITY",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        COMMODITY.Add(rowShipper["INVOICE"].ToString()!);
                    }
                    var OVERSEATRUCKER = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "OVERSEATRUCKER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        OVERSEATRUCKER.Add(rowShipper["OVERSEATRUCKERCODE"].ToString()!);
                    }
                    var PLACEOFLOADING = new List<PlaceOfLoading>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "PLACEOFLOADING",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        PLACEOFLOADING.Add(new PlaceOfLoading
                        {
                            LineId = Convert.ToInt32(rowShipper["CODE"].ToString()!),
                            PLACELOADING = rowShipper["PLACELOADING"].ToString()!,
                            District = rowShipper["DistrictName"].ToString()!
                        });
                    }
                    var PLACEOFDELIVERY = new List<PlaceOfDelivery>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "PLACEOFDELIVERY",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        PLACEOFDELIVERY.Add(new PlaceOfDelivery
                        {
                            PLACEDELIVERY = rowShipper["PLACEOFDELIVERY"].ToString()!,
                            District = rowShipper["DistrictName"].ToString()!,
                        });
                    }
                    var THAIFORWARDER = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "THAIFORWARDER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        THAIFORWARDER.Add(rowShipper["THAIFORWARDER"].ToString()!);
                    }
                    var VOLUME = new List<Volume>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "VOLUME",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        VOLUME.Add(new Volume
                        {
                            QTY = Convert.ToInt32(rowShipper["QTY"].ToString()!),
                            VOLUMECODE = rowShipper["VOLUMECODE"].ToString()!,
                            GROSSWEIGHT = Convert.ToDouble(rowShipper["GROSSWEIGHT"].ToString()),
                            InvoiceList = rowShipper["InvoiceList"].ToString(),
                        });
                    }
                    var TBOVERSEAFORWARDER = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "TBOVERSEAFORWARDER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        TBOVERSEAFORWARDER.Add(rowShipper["OVERSEAFORWARDERCODE"].ToString()!);
                    }
                    var TBTRUCKTYPEROW = new List<TruckType>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "TBTRUCKTYPEROW",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        TBTRUCKTYPEROW.Add(new TruckType
                        {
                            QTY = rowShipper["QTY"].ToString()!,
                            TRUCKTYPE = rowShipper["TRUCKTYPECODE"].ToString()!,
                            GROSSWEIGHT = Convert.ToDouble(rowShipper["GROSSWEIGHT"].ToString()),
                            InvoiceList = rowShipper["InvoiceList"].ToString(),
                        });
                    }
                    var THAIBORDER = new List<string>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "THAIBORDER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        THAIBORDER.Add(rowShipper["THAIBORDER"].ToString()!);
                    }
                    getDetailBookingSheetByDocEntryList = new GetDetailBookingSheetByDocEntry
                    {
                        SlpCode = Convert.ToInt32(dataRow["SlpCode"].ToString()),
                        ServiceTypeCode = dataRow["SERVICETYPECODE"].ToString()!,
                        ImportType = dataRow["IMPORTTYPE"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        NumberJob = dataRow["U_JONO"].ToString(),
                        EWSeries = dataRow["SERIES"].ToString()!,
                        BookingID = dataRow["BOOKINGID"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        BookingDate = dataRow["BOOKINGDATE"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        Origin = dataRow["ORIGIN"].ToString()!,
                        Destination = dataRow["DESTINATION"].ToString()!,
                        SaleEmployee = dataRow["SALESEMPLOYEE"].ToString()!,
                        JobType = dataRow["JOBTYPE"].ToString()!,
                        ServiceType = dataRow["SERVICETYPE"].ToString()!,
                        GoodsDescription = dataRow["GOODSDESCRIPTION"].ToString()!,
                        TotalPackage = dataRow["TOTALPACKAGE"].ToString()!,
                        NetWeight = Convert.ToDouble(dataRow["NETWEIGHT"].ToString()!),
                        GrossWeight = Convert.ToDouble(dataRow["GROSSWEIGHT"].ToString()!),
                        LoadingDate = dataRow["LOADINGDATE"].ToString()!,
                        CrossBorderDate = dataRow["CROSSBORDERDATE"].ToString()!,
                        ETARequirement = dataRow["ETAREQUIREMENT"].ToString()!,
                        Remark = dataRow["REMARK"].ToString()!,
                        SpecialRequest = dataRow["SPECIALREQUEST"].ToString()!,
                        LoloYard = dataRow["LOLOYARD"].ToString()!,
                        LoloYardRemark = dataRow["LOLOYARDREMARK"].ToString()!,
                        FCLLCL = dataRow["LCLFCL"].ToString()!,
                        CYCFS = dataRow["CYCFS"].ToString()!,
                        Shipper = shipper,
                        Consignee = consignee,
                        SalesQuotation = salesQuotation,
                        Commodity = COMMODITY,
                        OverseaTrucker = OVERSEATRUCKER,
                        PlaceOfLoading = PLACEOFLOADING,
                        PlaceOfDelivery = PLACEOFDELIVERY,
                        ThaiForwarder = THAIFORWARDER,
                        Volume = VOLUME,
                        OverseaForwarder = TBOVERSEAFORWARDER,
                        TruckType = TBTRUCKTYPEROW,
                        ThaiBorder = THAIBORDER,
                        DG = dataRow["DG"].ToString()!,
                        OtherRemark = dataRow["OtherRemark"].ToString()!,
                    };
                }
                return Task.FromResult(new GetDetailBookingSheetByDocEntryResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDetailBookingSheetByDocEntryList,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetDetailBookingSheetByDocEntryResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetServiceType>>> GetServiceTypeResponseAsync()
        {
            try
            {
                var getGetServiceType = new List<GetServiceType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETSERVICETYPE",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    getGetServiceType.Add(new GetServiceType
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                        ImportType = dataRow["IMPORTTYPE"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetServiceType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getGetServiceType
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
        public Task<ResponseData<List<GetTruckType>>> GetTruckTypeResponseAsync()
        {
            try
            {
                var getGetTruckType = new List<GetTruckType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETTRUCKTYPEBOOKINGSHEET",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    getGetTruckType.Add(new GetTruckType
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetTruckType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getGetTruckType
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetTruckType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<PostBookingSheetRequest>> GetBookingSheetUpdateByDocEntryResponseAsync(string docEntry)
        {
            try
            {
                var getDetailBookingSheetByDocEntryList = new PostBookingSheetRequest();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "Header",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var shipper = new List<Shippers>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "SHIPPER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        shipper.Add(new Shippers
                        {
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                            SHIPPER = rowShipper["CARDCODE"].ToString()!,
                        });
                    }
                    var consignee = new List<Consignees>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "CONSIGNEE",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        consignee.Add(new Consignees
                        {
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                            CONSIGNEE = rowShipper["CARDCODE"].ToString()!,
                        });
                    }
                    var salesQuotation = new List<SaleQuotations>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "SALESQUOTATION",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        salesQuotation.Add(new SaleQuotations
                        {
                            DOCENTRY = rowShipper["DOCENTRY"].ToString()!,
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                            //CardCode=rowShipper["CARDCODE"].ToString(),
                        });
                    }
                    var COMMODITY = new List<Commodity>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "COMMODITY",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        COMMODITY.Add(new Commodity
                        {
                            INVOICE = rowShipper["INVOICE"].ToString(),
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                        });
                    }
                    var OVERSEATRUCKER = new List<OverTruckers>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "OVERSEATRUCKER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        OVERSEATRUCKER.Add(new OverTruckers
                        {
                            OVERSEATRUCKERCODE = rowShipper["CARDCODE"].ToString(),
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString())
                        });
                    }
                    var PLACEOFLOADING = new List<PlaceOfLoading>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "PLACEOFLOADING",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        PLACEOFLOADING.Add(new PlaceOfLoading
                        {
                            PLACELOADING = rowShipper["CODE"].ToString(),
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                            District = rowShipper["District"].ToString(),
                        });
                    }
                    var PLACEOFDELIVERY = new List<PlaceOfDelivery>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "PLACEOFDELIVERY",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        PLACEOFDELIVERY.Add(new PlaceOfDelivery
                        {
                            PLACEDELIVERY = rowShipper["Code"].ToString(),
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                            District = rowShipper["District"].ToString(),
                        });
                    }
                    var THAIFORWARDER = new List<ThaiForwarders>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "THAIFORWARDER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        THAIFORWARDER.Add(new ThaiForwarders
                        {
                            THAIFORWARDER = rowShipper["CardCode"].ToString()!,
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                        });
                    }
                    var VOLUME = new List<Volume>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "VolumUpdateBookingSheet",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        VOLUME.Add(new Volume
                        {
                            QTY = Convert.ToInt32(rowShipper["QTY"].ToString()!),
                            VOLUMECODE = rowShipper["VOLUMECODE"].ToString()!,
                            GROSSWEIGHT = Convert.ToDouble(rowShipper["GROSSWEIGHT"].ToString()),
                            InvoiceList = rowShipper["InvoiceList"].ToString(),
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                        });
                    }
                    var TBOVERSEAFORWARDER = new List<OverForwarders>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "TBOVERSEAFORWARDER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        TBOVERSEAFORWARDER.Add(new OverForwarders
                        {
                            OVERSEAFORWARDERCODE = rowShipper["CARDCODE"].ToString(),
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                        });
                    }
                    var TBTRUCKTYPEROW = new List<TruckType>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "TBTRUCKTYPEROWUpdateBookingSheet",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        TBTRUCKTYPEROW.Add(new TruckType
                        {
                            QTY = rowShipper["QTY"].ToString()!,
                            TRUCKTYPE = rowShipper["TRUCKTYPECODE"].ToString()!,
                            GROSSWEIGHT = Convert.ToDouble(rowShipper["GROSSWEIGHT"].ToString()),
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                            InvoiceList = rowShipper["InvoiceList"].ToString()
                        });
                    }
                    var THAIBORDER = new List<ThaiBorders>();
                    foreach (DataRow rowShipper in new GetRecordByDataTable(
                             "GetDetailBookingByDocEntry",
                             "THAIBORDER",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        THAIBORDER.Add(new ThaiBorders
                        {
                            ThaiBorder = rowShipper["CODE"].ToString()!,
                            LineId = Convert.ToInt32(rowShipper["LINEID"].ToString()),
                        });
                    }
                    getDetailBookingSheetByDocEntryList = new PostBookingSheetRequest
                    {
                        DocEntry = Convert.ToInt32(dataRow["DOCENTRY"].ToString()),
                        BookingID = Convert.ToInt32(dataRow["DOCNUM"].ToString()),
                        EWSereis = dataRow["SERIES"].ToString(),
                        JobNumber = dataRow["JOBNUMBER"].ToString(),
                        Origin = Convert.ToInt32(dataRow["ORIGINCODE"].ToString()),
                        Destination = Convert.ToInt32(dataRow["DESTINATIONCODE"].ToString()),
                        SaleEmployee = Convert.ToInt32(dataRow["SlpCode"].ToString()),
                        ImportType = dataRow["IMPORTTYPE"].ToString(),
                        ServiceType = dataRow["SERVICETYPECODE"].ToString(),
                        Shipper = shipper,
                        Consignee = consignee,
                        SaleQuotation = salesQuotation,
                        Commodities = COMMODITY,
                        GoodDescription = dataRow["GOODSDESCRIPTION"].ToString(),
                        TotalPackage = dataRow["TOTALPACKAGE"].ToString(),
                        NetWeight = Convert.ToDouble(dataRow["NETWEIGHT"].ToString()),
                        GrossWeight = Convert.ToDouble(dataRow["GROSSWEIGHT"].ToString()),
                        LoadingDate = dataRow["LOADINGDATE"].ToString(),
                        CorssBorderDate = dataRow["CROSSBORDERDATE"].ToString(),
                        ETARequirement = dataRow["ETAREQUIREMENT"].ToString(),
                        OverseaTrucker = OVERSEATRUCKER,
                        PlaceOfLoadings = PLACEOFLOADING,
                        PlaceOfDeliveries = PLACEOFDELIVERY,
                        LOLOYARDRemark = dataRow["LOLOYARDREMARK"].ToString(),
                        LoloYardOrUnloading = Convert.ToInt32(dataRow["LOLOYARD"].ToString()),
                        ThaiForwarder = THAIFORWARDER,
                        Volumes = VOLUME,
                        OverseaForwarder = TBOVERSEAFORWARDER,
                        TruckType = TBTRUCKTYPEROW,
                        ThaiBorders = THAIBORDER,
                        LCLOrFCL = Convert.ToInt32(dataRow["LCLFCL"].ToString()),
                        CYOrCFS = Convert.ToInt32(dataRow["CYCFS"].ToString()),
                        Remarks = dataRow["REMARK"].ToString(),
                        SpeacialRequest = dataRow["SPECIALREQUEST"].ToString(),
                        BookingDate = dataRow["BOOKINGDATE"].ToString(),
                        DG = dataRow["DG"].ToString(),
                        OtherRemark = dataRow["OtherRemark"].ToString(),
                    };
                }
                return Task.FromResult(new ResponseData<PostBookingSheetRequest>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDetailBookingSheetByDocEntryList,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<PostBookingSheetRequest>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetDistrict>>> GetDistrictsResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetDistrict>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetDistrict>(new GetRecordByDataTable(
                             "GETDISTRICTS",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetDistrict>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region POST
        public Task<PostResponse> PostBookingSheet(PostBookingSheetRequest postBookingSheetRequest)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = BookingSheets._Add(postBookingSheetRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        #endregion
        #region Function
        public Task<ResponseData<List<GetListSaleQuotation>>> GetListSaleQuotationResponseAsync()
        {
            try
            {
                var listGetListSaleQuotation = new List<GetListSaleQuotation>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETLISTSALEQUOTATION",
                             "ADDBOOKINGSHEET",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetListSaleQuotation.Add(new GetListSaleQuotation
                    {
                        CustomerName = dataRow["CUSTOMERNAME"].ToString()!,
                        QuotationDate = dataRow["QUOTATIONDATE"].ToString()!,
                        Tel = dataRow["TEL"].ToString()!,
                        CustomerRefNo = dataRow["CustomerRefNo"].ToString()!,
                        Email = dataRow["EMAIL"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        DocTotal = Convert.ToDecimal(dataRow["TOTAL"].ToString()),
                        DocStatus = dataRow["DOCSTATUS"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                    });
                }
                return Task.FromResult(new ResponseData<List<GetListSaleQuotation>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetListSaleQuotation
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
        public Task<ResponseData<GetDetailViewBookingSheet>> GetDetailViewBookingSheetResponseAsync(string docEntry)
        {
            try
            {
                var getDetailViewBookingSheet = new GetDetailViewBookingSheet();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETLISTSALEQUOTATION",
                             "ADDBOOKINGSHEET",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    //GetInvoice
                    var commodities = new List<Commodity>();
                    foreach (DataRow dataRowCommodities in new GetRecordByDataTable(
                            "GetInvoice",
                            dataRow["BOOKINGENTRY"].ToString()!,
                            "",
                            "",
                            "",
                            "").GetResultDataTable().Rows)
                    {
                        commodities.Add(new Commodity
                        {
                            INVOICE = dataRow["INVOICE"].ToString()!,
                        });
                    }
                    //PLACEOFLOADINGBYDOCENTRY
                    var placeOfLoading = new List<PlaceOfLoading>();
                    foreach (DataRow dataRowCommodities in new GetRecordByDataTable(
                            "PLACEOFLOADINGBYDOCENTRY",
                            dataRow["BOOKINGENTRY"].ToString()!,
                            "",
                            "",
                            "",
                            "").GetResultDataTable().Rows)
                    {
                        placeOfLoading.Add(new PlaceOfLoading
                        {
                            PLACELOADING = dataRow["NAME"].ToString()!,
                        });
                    }
                    //PLACEOFDELIVERYBYDOCENTRY
                    var placeOfDelivery = new List<PlaceOfDelivery>();
                    foreach (DataRow dataRowCommodities in new GetRecordByDataTable(
                            "PLACEOFDELIVERYBYDOCENTRY",
                            dataRow["BOOKINGENTRY"].ToString()!,
                            "",
                            "",
                            "",
                            "").GetResultDataTable().Rows)
                    {
                        placeOfDelivery.Add(new PlaceOfDelivery
                        {
                            PLACEDELIVERY = dataRow["NAME"].ToString()!,
                        });
                    }
                    //VOLUMEBYDOCENTRY
                    var volume = new List<Volume>();
                    foreach (DataRow dataRowCommodities in new GetRecordByDataTable(
                            "VOLUMEBYDOCENTRY",
                            dataRow["BOOKINGENTRY"].ToString()!,
                            "",
                            "",
                            "",
                            "").GetResultDataTable().Rows)
                    {
                        volume.Add(new Volume
                        {
                            VOLUMECODE = dataRow["NAME"].ToString()!,
                        });
                    }
                    //THAIBORDERBYDOCENTRY
                    var thaiBorder = new List<ThaiBorders>();
                    foreach (DataRow dataRowCommodities in new GetRecordByDataTable(
                            "THAIBORDERBYDOCENTRY",
                            dataRow["BOOKINGENTRY"].ToString()!,
                            "",
                            "",
                            "",
                            "").GetResultDataTable().Rows)
                    {
                        thaiBorder.Add(new ThaiBorders
                        {
                            ThaiBorder = dataRow["NAME"].ToString()!,
                        });
                    }
                    //OverseaTruckersByDocEntry
                    var OverseaTruckers = new List<string>();
                    foreach (DataRow dataRowCommodities in new GetRecordByDataTable(
                            "OverseaTruckersByDocEntry",
                            dataRow["BOOKINGENTRY"].ToString()!,
                            "",
                            "",
                            "",
                            "").GetResultDataTable().Rows)
                    {
                        thaiBorder.Add(new ThaiBorders
                        {
                            ThaiBorder = dataRow["NAME"].ToString()!,
                        });
                    }
                    getDetailViewBookingSheet = new GetDetailViewBookingSheet
                    {
                        DocEntry = dataRow["BOOKINGENTRY"].ToString()!,
                        BookingId = dataRow["BOOKINGID"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        BookingDate = dataRow["BOOKINGDATE"].ToString()!,
                        RouteFrom = dataRow["ROUTEFROM"].ToString()!,
                        RouteTo = dataRow["ROUTETO"].ToString()!,
                        SaleEmployee = dataRow["SALEEMPLOYEENAME"].ToString()!,
                        JobType = dataRow["JOBTYPE"].ToString()!,
                        ServiceType = dataRow["SERVICETYPE"].ToString()!,
                        Shipper = dataRow["SHIPPER"].ToString()!,
                        Consignee = dataRow["CONSIGNEE"].ToString()!,
                        Description = dataRow["DESCRIPTION"].ToString()!,
                        TotalPackage = dataRow["TOTALPACKAGE"].ToString()!,
                        NetWeight = dataRow["NETWEIGHT"].ToString()!,
                        GrossWeight = dataRow["GROSSWEIGHT"].ToString()!,
                        LoadingDate = dataRow["LOADINGDATE"].ToString()!,
                        CrossBorderDate = dataRow["CROSSBORDERDATE"].ToString()!,
                        ETARequirementDate = dataRow["ETAREQUIREMENT"].ToString()!,
                        ThaiForwarder = dataRow["THAIFORWARDER"].ToString()!,
                        TruckType = dataRow["TRUCKTYPE"].ToString()!,
                        Remark = dataRow["REMARK"].ToString()!,
                        SpecialRequest = dataRow["SPECIALREQUEST"].ToString()!,
                    };
                }
                return Task.FromResult(new ResponseData<GetDetailViewBookingSheet>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDetailViewBookingSheet
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetDetailViewBookingSheet>
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
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.BookingSheetLayoutPrinter!,
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
        #region Delete
        public Task<DeleteResponse> CancelBookingSheetAsync(string docNum)
        {
            try
            {
                if (GetQuery.GetValueByID("GETSTATUSCANCELBOOKINGSHEET", "ValueValid", docNum) == "0")
                {
                    //SapConnection login = new();
                    //if (login.LErrCode == 0)
                    //{
                    var a = BookingSheets._Cancel(docNum, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        #endregion
        #region Update
        public Task<PostResponse> PutBookingSheet(PostBookingSheetRequest postBookingSheetRequest)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = BookingSheets._Update(postBookingSheetRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        public Task<PostResponse> PutCommodityInBookingSheet(string CreateUser, int docEntry, List<Commodity> commodity, HeaderCommodityUpdate headerCommodityUpdate, List<OverTruckers> overseaTrucker
                                                                                , List<ThaiForwarders> thaiForwarders, List<OverForwarders> overForwarders
                                                                                , List<ThaiBorders> thaiBorders, PlaceOfLoading placeOfLoadings, PlaceOfLoading placeOfDeliveries)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = BookingSheets._UpdateCommodities(CreateUser, docEntry, commodity, headerCommodityUpdate
                                                    , overseaTrucker, thaiForwarders, overForwarders, thaiBorders, placeOfLoadings, placeOfDeliveries, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        #endregion

    }
}
