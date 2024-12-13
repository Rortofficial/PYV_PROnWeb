using Client.Connection;
using Client.Lib.ConfirmBookingSheetSeaAndAir;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class ConfirmBookingSeaAndAir : IConfirmBookingSeaAndAir
    {

        #region Get
        public Task<ResponseData<List<GetBookingSheetByUser>>> GetBookingSheetByUserResponseAsync()
        {
            try
            {
                var getBookingSheetByUsersList = new List<GetBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "ListBookingSheet",
                             "ConfirmBookingSheetSeaAndAir",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    getBookingSheetByUsersList.Add(new GetBookingSheetByUser
                    {
                        BookingID = dataRow["BOOKINGID"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        BookingDate = dataRow["BOOKINGDATE"].ToString()!,
                        LoadingDate = dataRow["LOADINGDATE"].ToString()!,
                        CrossBorderDate = dataRow["CrossBorderDate"].ToString()!,
                        ETARequirementDate = dataRow["ETARequirementDate"].ToString()!,
                        ImportType = dataRow["IMPORTYPE"].ToString()!,
                        Shipper = dataRow["Shipper"].ToString()!,
                        Consignee = dataRow["Consignee"].ToString()!,
                        CO = dataRow["CO"].ToString()!,
                        COCode = dataRow["COCODE"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        CreateBy = dataRow["CREATEBY"].ToString()!,
                        SaleEmployee = dataRow["SALENAME"].ToString()!,
                        SlpCode = Convert.ToInt32(dataRow["SlpCode"].ToString()),
                        Destination = dataRow["DESTINATION"].ToString(),
                        Origin = dataRow["ORIGIN"].ToString(),
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
        public Task<ResponseData<List<GetDetailBookingSheetByUser>>> GetDetailBookingSheetByUserResponseAsync(int BookingID)
        {
            try
            {
                var getDetailBookingSheetByUserList = new List<GetDetailBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailBookingSheetSeaAndAir",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var placeOfLoading = new List<string>();
                    var placeOfDelivery = new List<string>();
                    var volume = new List<BookingVolume>();
                    var invoice = new List<string>();
                    var shippers = new List<string>();
                    var consignees = new List<string>();
                    var thaiBorders = new List<string>();
                    foreach (DataRow dataRowPlaceOfLoading in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntry",
                             "GetPlaceOfLoadingBookingSheet",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        placeOfLoading.Add(dataRowPlaceOfLoading["PLACEOFLOADING"].ToString()!);
                    }
                    foreach (DataRow dataRowPlaceOfDelivery in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntry",
                             "GetPlaceOfDeliveryBookingSheet",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        placeOfDelivery.Add(dataRowPlaceOfDelivery["PLACEOFDELIVERY"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetVolumeBookingSheetSeaAndAir",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        volume.Add(new BookingVolume
                        {
                            Qty = Convert.ToInt32(dataRowVolume["QTY"].ToString()),
                            VoulumeCode = dataRowVolume["VOLUMECODE"].ToString()!,
                            VolumeDescription = dataRowVolume["VOLUME"].ToString()!,
                            ContainerNumber = Convert.ToInt32(dataRowVolume["CONTAINERNUMBER"].ToString()),
                            DocEntry = Convert.ToInt32(dataRowVolume["DOCENTRY"].ToString()),
                            GrossWeight = Convert.ToDouble(dataRowVolume["GROSSWEIGHT"].ToString()),
                            Type = dataRowVolume["TYPEOFCONTAINER"].ToString(),
                            VolumeInvoiceList = dataRowVolume["INVOICELIST"].ToString(),
                            LineId = Convert.ToInt32(dataRowVolume["LineId"]),
                        });
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(
                             "GetInvoice",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        invoice.Add(dataRowVolume["INVOICE"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntry",
                             "SHIPPER",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        shippers.Add(dataRowVolume["SHIPPER"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntry",
                             "CONSIGNEE",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        consignees.Add(dataRowVolume["CONSIGNEE"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntry",
                             "THAIBORDER",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        thaiBorders.Add(dataRowVolume["THAIBORDER"].ToString()!);
                    }
                    getDetailBookingSheetByUserList.Add(new GetDetailBookingSheetByUser
                    {
                        BookingID = Convert.ToInt32(dataRow["BOOKINGID"].ToString()),
                        SaleEmployee = dataRow["SALEEMPLOYEE"].ToString()!,
                        SlpCode = Convert.ToInt32(dataRow["SlpCode"].ToString()),
                        Route = dataRow["ROUTE"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        Origin = dataRow["ORIGIN"].ToString()!,
                        Destination = dataRow["DESTINATION"].ToString()!,
                        Shipper = shippers,
                        Consignee = consignees,
                        CO = dataRow["CO"].ToString()!,
                        ImportType = dataRow["IMPORTYPE"].ToString()!,
                        LOLOYARDRemark = dataRow["LOLOYARDREMARK"].ToString()!,
                        LOLOUNLOADING = dataRow["LOLOUNLOADING"].ToString()!,
                        FCLORLCL = dataRow["LCLORFCL"].ToString()!,
                        PlaceOfLoading = placeOfLoading,
                        PlaceOfDelivery = placeOfDelivery,
                        Volume = volume,
                        Invoices = invoice,
                        ThaiBorder = thaiBorders,
                    });

                }
                return Task.FromResult(new ResponseData<List<GetDetailBookingSheetByUser>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDetailBookingSheetByUserList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetDetailBookingSheetByUser>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetListConfirmBookingSheet>>> GetListConfirmBookingSheetResponseAsync(string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                var getListConfirmBookingSheetList = new List<GetListConfirmBookingSheet>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetListConfirmBookingSheetSeaAndAir",
                             dateFrom,
                             dateTo,
                             type,
                             userID,
                             "").GetResultDataTable().Rows)
                {
                    getListConfirmBookingSheetList.Add(new GetListConfirmBookingSheet
                    {
                        ConfirmBookingID = Convert.ToInt32(dataRow["CONFIRMBOOKINGID"].ToString()),
                        JobNo = dataRow["JOBNO"].ToString()!,
                        BookingDate = dataRow["BOOKINGDATE"].ToString()!,
                        LoadingDate = dataRow["LOADINGDATE"].ToString()!,
                        CrossBorderDate = dataRow["CrossBorderDate"].ToString()!,
                        ETARequirementDate = dataRow["ETARequirementDate"].ToString()!,
                        ImportType = dataRow["IMPORTYPE"].ToString()!,
                        Shipper = dataRow["Shipper"].ToString()!,
                        Consignee = dataRow["Consignee"].ToString()!,
                        //CO = dataRow["CO"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        CreateBy = dataRow["CREATEBY"].ToString()!,
                        DocEntry = Convert.ToInt32(dataRow["DOCENTRY"].ToString()),
                        BookingDocEntry = Convert.ToInt32(dataRow["BOOKINGDOCENTRY"].ToString()),
                        ProjectDocEntry = Convert.ToInt32(dataRow["PROJECTDOCENTRY"].ToString()),
                        DocStatus = dataRow["DOCSTATUSCANCEL"].ToString(),
                        DocStatusUpdate = dataRow["DocStatusUpdate"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetListConfirmBookingSheet>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getListConfirmBookingSheetList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListConfirmBookingSheet>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<GetDetailConfirmBookingSheetByDocEntryResponse> GetDetailConfirmBookingSheetByDocEntryResponseAsync(int DocEntry)
        {
            try
            {
                var getDetailConfirmBookingSheets = new GetDetailConfirmBookingSheetByDocEntry();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailConfirmBookingSeaAndAirByDocEntry",
                             "BookingSheetInformation",
                             DocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var BORDER = new List<GetGeneralListCode>();
                    foreach (DataRow row in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailConfirmBookingSeaAndAirByDocEntry",
                             "GetBorderBookingSheet",
                             DocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        BORDER.Add(new GetGeneralListCode
                        {
                            Code = Convert.ToInt32(row["CODE"].ToString()),
                            Name = row["NAME"].ToString()!,
                        });
                    }
                    var PlaceOfLoading = new List<GetGeneralListCode>();
                    foreach (DataRow row in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailConfirmBookingSeaAndAirByDocEntry",
                             "PLACEOFLOADING",
                             DocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        PlaceOfLoading.Add(new GetGeneralListCode
                        {
                            Name = row["PLACELOADING"].ToString()!,
                        });
                    }
                    var shipper = new List<GetGeneralListCode>();
                    foreach (DataRow row in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailConfirmBookingSeaAndAirByDocEntry",
                             "SHIPPER",
                             DocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        shipper.Add(new GetGeneralListCode
                        {
                            Name = row["SHIPPER"].ToString()!,
                        });
                    }
                    var placeOfDelivery = new List<GetGeneralListCode>();
                    foreach (DataRow row in new GetRecordByDataTable(GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailConfirmBookingSeaAndAirByDocEntry",
                             "PLACEOFDELIVERY",
                             DocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        placeOfDelivery.Add(new GetGeneralListCode
                        {
                            Name = row["PLACEOFDELIVERY"].ToString()!,
                        });
                    }
                    var consignee = new List<GetGeneralListCode>();
                    foreach (DataRow row in new GetRecordByDataTable(GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailConfirmBookingSeaAndAirByDocEntry",
                             "CONSIGNEE",
                             DocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        consignee.Add(new GetGeneralListCode
                        {
                            Name = row["CONSIGNEE"].ToString()!,
                        });
                    }
                    var listOfContainerInformations = new List<ListOfContainerInformation>();
                    foreach (DataRow row in new GetRecordByDataTable(
                             StoreType.TransactionSeaAir,
                             "GetDetailConfirmBookingSeaAndAirByDocEntry",
                             "GetListOfContainerInformation",
                             DocEntry.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        var listOfPurchaseRequestsAD = new List<Models.Request.PurchaseRequest>();
                        foreach (DataRow rowHeaderAdvance in new GetRecordByDataTable(
                                 StoreType.TransactionSeaAir,
                                 "GetDetailConfirmBookingSeaAndAirByDocEntry",
                                 "ListOfPurchaseRequest",
                                 DocEntry.ToString(),
                                 "PRAD",
                                 row["LINEID"].ToString(),
                                 "").GetResultDataTable().Rows)
                        {
                            var line = new List<Client.Models.Request.PurchaseRequestLine>();
                            foreach (DataRow rowLine in new GetRecordByDataTable(GetRecordByDataTable.StoreType.TransactionSeaAir,
                                     "GetDetailConfirmBookingSeaAndAirByDocEntry",
                                     "ListOfPurchaseRequestLine",
                                     rowHeaderAdvance["DOCENTRY"].ToString(),
                                     "",
                                     "",
                                     "").GetResultDataTable().Rows)
                            {
                                line.Add(new PurchaseRequestLine
                                {
                                    ItemCode = rowLine["ITEMCODE"].ToString()!,
                                    ItemName = rowLine["ITEMNAME"].ToString()!,
                                    Amount = Convert.ToDecimal(rowLine["PRICE"].ToString()),
                                    Origin = rowLine["ORIGIN"].ToString()!,
                                    Destination = rowLine["DESTINATION"].ToString()!,
                                    remark = rowLine["REMARKS"].ToString()!,
                                    ServiceType = rowLine["SERVICETYPE"].ToString()!,
                                });
                            }
                            listOfPurchaseRequestsAD.Add(new Models.Request.PurchaseRequest
                            {
                                DocEntry = rowHeaderAdvance["DOCENTRY"].ToString()!,
                                RefNo = rowHeaderAdvance["DOCNUM"].ToString()!,
                                JobNo = rowHeaderAdvance["PROJECTNUMBER"].ToString()!,
                                IssueDate = rowHeaderAdvance["ISSUEDATE"].ToString(),
                                DueDate = rowHeaderAdvance["DUEDATE"].ToString(),
                                VendorCode = rowHeaderAdvance["VENDORCODE"].ToString()!,
                                VendorName = rowHeaderAdvance["VENDORNAME"].ToString()!,
                                AmountCurrency = rowHeaderAdvance["CURRENCY"].ToString()!,
                                IssueBy = Convert.ToInt32(rowHeaderAdvance["EMPLOYEEID"].ToString()),
                                IssueByName = rowHeaderAdvance["EMPLOYEENAME"].ToString(),
                                GrandTotal = Convert.ToDecimal(rowHeaderAdvance["AMOUNT"].ToString()),
                                AmountTHB = Convert.ToDecimal(rowHeaderAdvance["AMOUNTTHB"].ToString()),
                                BankAccount = rowHeaderAdvance["BANKACCOUNT"].ToString()!,
                                BranchName = rowHeaderAdvance["BRANCH"].ToString()!,
                                BankName = rowHeaderAdvance["BANKCOUNTRY"].ToString()!,
                                Country = rowHeaderAdvance["BANKNAME"].ToString()!,
                                SwiftCode = rowHeaderAdvance["SWIFTCODE"].ToString()!,
                                Remarks = rowHeaderAdvance["REMARKS"].ToString()!,
                                Lines = line
                            });
                        }
                        var listOfPurchaseRequests = new List<Models.Request.PurchaseRequest>();
                        foreach (DataRow rowHeaderAdvance in new GetRecordByDataTable(
                                 "GetDetailConfirmBookingSeaAndAirByDocEntry",
                                 "ListOfPurchaseRequest",
                                 DocEntry.ToString(),
                                 "PRCOS",
                                 row["LINEID"].ToString(),
                                 "").GetResultDataTable().Rows)
                        {
                            var line = new List<Client.Models.Request.PurchaseRequestLine>();
                            foreach (DataRow rowLine in new GetRecordByDataTable(GetRecordByDataTable.StoreType.TransactionSeaAir,
                                     "GetDetailConfirmBookingSeaAndAirByDocEntry",
                                     "ListOfPurchaseRequestLine",
                                     rowHeaderAdvance["DOCENTRY"].ToString(),
                                     "",
                                     "",
                                     "").GetResultDataTable().Rows)
                            {
                                line.Add(new PurchaseRequestLine
                                {
                                    ItemCode = rowLine["ITEMCODE"].ToString()!,
                                    ItemName = rowLine["ITEMNAME"].ToString()!,
                                    Amount = Convert.ToDecimal(rowLine["PRICE"].ToString()),
                                    Origin = rowLine["ORIGIN"].ToString()!,
                                    Destination = rowLine["DESTINATION"].ToString()!,
                                    remark = rowLine["REMARKS"].ToString()!,
                                    ServiceType = rowLine["SERVICETYPE"].ToString()!,
                                });
                            }
                            listOfPurchaseRequests.Add(new Models.Request.PurchaseRequest
                            {
                                DocEntry = rowHeaderAdvance["DOCENTRY"].ToString()!,
                                RefNo = rowHeaderAdvance["DOCNUM"].ToString()!,
                                JobNo = rowHeaderAdvance["PROJECTNUMBER"].ToString()!,
                                IssueDate = rowHeaderAdvance["ISSUEDATE"].ToString(),
                                DueDate = rowHeaderAdvance["DUEDATE"].ToString(),
                                VendorCode = rowHeaderAdvance["VENDORCODE"].ToString()!,
                                VendorName = rowHeaderAdvance["VENDORNAME"].ToString()!,
                                AmountCurrency = rowHeaderAdvance["CURRENCY"].ToString()!,
                                IssueBy = Convert.ToInt32(rowHeaderAdvance["EMPLOYEEID"].ToString()),
                                IssueByName = rowHeaderAdvance["EMPLOYEENAME"].ToString(),
                                GrandTotal = Convert.ToDecimal(rowHeaderAdvance["AMOUNT"].ToString()),
                                AmountTHB = Convert.ToDecimal(rowHeaderAdvance["AMOUNTTHB"].ToString()),
                                BankAccount = rowHeaderAdvance["BANKACCOUNT"].ToString()!,
                                BranchName = rowHeaderAdvance["BRANCH"].ToString()!,
                                BankName = rowHeaderAdvance["BANKCOUNTRY"].ToString()!,
                                Country = rowHeaderAdvance["BANKNAME"].ToString()!,
                                SwiftCode = rowHeaderAdvance["SWIFTCODE"].ToString()!,
                                Remarks = rowHeaderAdvance["REMARKS"].ToString()!,
                                Lines = line
                            });
                        }
                        listOfContainerInformations.Add(new ListOfContainerInformation
                        {
                            Type = row["TYPE"].ToString()!,
                            ContainerOptionType = row["ContainerOptionType"].ToString()!,
                            ContainerType = row["CONTAINERTYPE"].ToString()!,
                            ContainerNo = row["CONTAINERNO"].ToString()!,
                            Owner = row["OWNER"].ToString()!,
                            GrossWeight = Convert.ToDouble(row["GROSSWEIGHT"]),
                            Yard = row["YARD"].ToString()!,
                            FCL = row["FCL_LCL"].ToString()!,
                            LOLO = row["LOLO_UNLOADING"].ToString()!,
                            TruckProvince = row["TRUCKPROVINCE"].ToString()!,
                            TruckPlateNo = row["TRUCKPLATENO"].ToString()!,
                            TruckType = row["TRUCKTYPE"].ToString()!,
                            Brand = row["BRAND"].ToString()!,
                            Colors = row["COLOR"].ToString()!,
                            TrailerProvince = row["TRAILERPROVINCE"].ToString()!,
                            TrailerPlateNo = row["TRAILERPLATE"].ToString()!,
                            TrailerType = row["TRAILERTYPE"].ToString()!,
                            DriverName1 = row["DriverName1"].ToString()!,
                            TPNo1 = row["TPNO1"].ToString()!,
                            IDCard1 = row["IDCARD1"].ToString()!,
                            DriverLicense1 = row["DRIVERLICENSE1"].ToString()!,
                            DriverName2 = row["DriverName2"].ToString()!,
                            TPNo2 = row["TPNO2"].ToString()!,
                            IDCard2 = row["IDCARD2"].ToString()!,
                            DriverLicense2 = row["DRIVERLICENSE2"].ToString()!,
                            VendorNo = row["VENDOR"].ToString()!,
                            TruckCostTHB = Convert.ToDouble(row["TRUCKCOSTTHB"]),
                            SealNo1 = row["SEALNO1"].ToString(),
                            SealNo2 = row["SEALNO2"].ToString(),
                            ListInvoice = row["ListInvoice"].ToString(),
                            listOfAdvancePayments = listOfPurchaseRequestsAD,
                            listOfPurchaseRequests = listOfPurchaseRequests,
                        });
                    }
                    getDetailConfirmBookingSheets = new GetDetailConfirmBookingSheetByDocEntry
                    {
                        JobType = dataRow["JOBTYPE"].ToString()!,
                        Route = dataRow["ROUTE"].ToString()!,
                        SaleName = dataRow["SALENAME"].ToString()!,
                        Border = BORDER,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        PlaceOfLoading = PlaceOfLoading,
                        Shipper = shipper,
                        PlaceOfDelivery = placeOfDelivery,
                        Consignee = consignee,
                        PriceList = dataRow["PRICELIST"].ToString()!,
                        Remarks = dataRow["REMARK"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        LoadingDate = dataRow["LoadingDate"].ToString()!,
                        CrossBorderDate = dataRow["CrossBorderDate"].ToString()!,
                        ETARequirementDate = dataRow["ETARequirementDate"].ToString()!,
                        CSName = dataRow["CSName"].ToString()!,
                        ListOfContainerInformations = listOfContainerInformations
                    };
                }
                return Task.FromResult(new GetDetailConfirmBookingSheetByDocEntryResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDetailConfirmBookingSheets,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync().Result.Data //ConfirmBookingSheetSeaAndAirLayoutPrinter
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetDetailConfirmBookingSheetByDocEntryResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetEditConfirmBookingSheet>>> GetEditConfirmBookingSheetByDocEntryResponseAsync(int DocEntry)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetEditConfirmBookingSheet>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetEditConfirmBookingSheet>(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetEditConfirmBookingSheetSeaAndAir",
                             DocEntry.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetEditConfirmBookingSheet>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetDetailBookingSheetByUser>>> GetEditDetailBookingSheetByUserResponseAsync(int BookingID)
        {
            try
            {
                var getDetailBookingSheetByUserList = new List<GetDetailBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailBookingSheetSeaAndAir",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var placeOfLoading = new List<string>();
                    var placeOfDelivery = new List<string>();
                    var volume = new List<BookingVolume>();
                    var invoice = new List<string>();
                    var shippers = new List<string>();
                    var consignees = new List<string>();
                    var thaiBorders = new List<string>();
                    foreach (DataRow dataRowPlaceOfLoading in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetPlaceOfLoadingBookingSheetSeaAndAir",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        placeOfLoading.Add(dataRowPlaceOfLoading["PLACEOFLOADING"].ToString()!);
                    }
                    foreach (DataRow dataRowPlaceOfDelivery in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetPlaceOfDeliveryBookingSheetSeaAndAir",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        placeOfDelivery.Add(dataRowPlaceOfDelivery["PLACEOFDELIVERY"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetVolumeBookingSheetConfirmBookingEditSeaAndAir",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        volume.Add(new BookingVolume
                        {
                            Qty = Convert.ToDouble(dataRowVolume["QTY"].ToString()),
                            VoulumeCode = dataRowVolume["VOLUMECODE"].ToString()!,
                            VolumeDescription = dataRowVolume["VOLUME"].ToString()!,
                            ContainerNumber = Convert.ToInt32(dataRowVolume["CONTAINERNUMBER"].ToString()),
                            DocEntry = Convert.ToInt32(dataRowVolume["DOCENTRY"].ToString()),
                            GrossWeight = Convert.ToDouble(dataRowVolume["GROSSWEIGHT"].ToString()),
                            Type = dataRowVolume["TYPEOFCONTAINER"].ToString(),
                            VolumeInvoiceList = dataRowVolume["INVOICELIST"].ToString(),
                            LineId = Convert.ToInt32(dataRowVolume["LineId"]),
                            ConfirmBookingID = Convert.ToInt32(dataRowVolume["ConfirmBookingID"]),
                        });
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetInvoiceSeaAndAir",
                             BookingID.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        invoice.Add(dataRowVolume["INVOICE"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntrySeaAndAir",
                             "SHIPPER",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        shippers.Add(dataRowVolume["SHIPPER"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntrySeaAndAir",
                             "CONSIGNEE",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        consignees.Add(dataRowVolume["CONSIGNEE"].ToString()!);
                    }
                    foreach (DataRow dataRowVolume in new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.TransactionSeaAir,
                             "GetDetailBookingByDocEntrySeaAndAir",
                             "THAIBORDER",
                             BookingID.ToString(),
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        thaiBorders.Add(dataRowVolume["THAIBORDER"].ToString()!);
                    }
                    getDetailBookingSheetByUserList.Add(new GetDetailBookingSheetByUser
                    {
                        BookingID = Convert.ToInt32(dataRow["BOOKINGID"].ToString()),
                        SaleEmployee = dataRow["SALEEMPLOYEE"].ToString()!,
                        SlpCode = Convert.ToInt32(dataRow["SlpCode"].ToString()),
                        Route = dataRow["ROUTE"].ToString()!,
                        JobNo = dataRow["JOBNO"].ToString()!,
                        Origin = dataRow["ORIGIN"].ToString()!,
                        Destination = dataRow["DESTINATION"].ToString()!,
                        Shipper = shippers,
                        Consignee = consignees,
                        CO = dataRow["CO"].ToString()!,
                        ImportType = dataRow["IMPORTYPE"].ToString()!,
                        LOLOYARDRemark = dataRow["LOLOYARDREMARK"].ToString()!,
                        LOLOUNLOADING = dataRow["LOLOUNLOADING"].ToString()!,
                        FCLORLCL = dataRow["LCLORFCL"].ToString()!,
                        PlaceOfLoading = placeOfLoading,
                        PlaceOfDelivery = placeOfDelivery,
                        Volume = volume,
                        Invoices = invoice,
                        ThaiBorder = thaiBorders,
                    });

                }
                return Task.FromResult(new ResponseData<List<GetDetailBookingSheetByUser>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = getDetailBookingSheetByUserList
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetDetailBookingSheetByUser>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Cancel
        public Task<DeleteResponse> CancelConfirmBookingSheetAsync(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries)
        {
            var a = ConfirmBookingSheetSeaAndAirs._Cancel(docNum, bookingDocEntry, projectManagementEntry, projectSeries, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new DeleteResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = (a.DocEntry != null) ? a.DocEntry.ToString() : ""
            });
        }
        #endregion
        #region Put
        public Task<PostResponse> PutConfirmBookingSheet(PutConfirmBookingSheetRequest putConfirmBookingSheetRequest)
        {
            #region  Update
            var a = ConfirmBookingSheetSeaAndAirs._Update(putConfirmBookingSheetRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
            #endregion
        }
        #endregion
        #region Post
        public Task<PostResponse> PostConfirmBookingSheet(PostConfirmBookingSheetRequest postConfirmBookingSheetRequest)
        {
            #region  Add
            var a = ConfirmBookingSheetSeaAndAirs._Add(postConfirmBookingSheetRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
            #endregion
        }
        #endregion
        private Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync()
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             Configure.ConfirmBookingSheetSeaAndAirLayoutPrinter!,
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
    }
}
