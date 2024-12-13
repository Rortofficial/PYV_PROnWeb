using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize]
    public class BookingSheetSeaAndAirController : Controller
    {
        private readonly IBookingSheetSeaAndAir bookingSheetSeaAndAir;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBookingSheet bookingSheet;
        public BookingSheetSeaAndAirController(IBookingSheetSeaAndAir bookingSheetSeaAndAir, IBookingSheet bookingSheet, IWebHostEnvironment webHostEnvironment)
        {
            this.bookingSheetSeaAndAir = bookingSheetSeaAndAir;
            _webHostEnvironment = webHostEnvironment;
            this.bookingSheet = bookingSheet;
        }
        public async Task<IActionResult> Index()
        {
            return View(new ListMasterBookingSheetSeaAndAir
            {
                GetJobNumberSeaAndAirs = await bookingSheetSeaAndAir.GetJobNumberSeaAndAirResponseAsync(),
                SaleEmployees = await bookingSheet.GetSaleEmployeeResponseAsync(),
                GetPlaceOfLoadings = await bookingSheet.GetPlaceOfLoadingResponseAsync(),
                GetOverseaForwarders = await bookingSheet.GetOverseaForwarderResponseAsync(),
                GetPlaceOfDeliveries = await bookingSheet.GetPlaceOfDeliveryResponseAsync(),
                GetOverseaTruckers = await bookingSheet.GetOverseaTruckerResponseAsync(),
                GetOrigins = await bookingSheetSeaAndAir.GetOriginResponseAsync(),
                GetDestinations = await bookingSheetSeaAndAir.GetDestinationResponseAsync(),
                GetShippers = await bookingSheet.GetShipperResponseAsync(),
                GetCoLoaderSeaAndAirs = await bookingSheetSeaAndAir.GetCoLoaderSeaAndAirResponseAsync(),
                GetCustomerSeaAndAirs = await bookingSheetSeaAndAir.GetCustomerSeaAndAirResponseAsync(),
                GetConsignees = await bookingSheet.GetConsigneeResponseAsync(),
                GetShippingLineSeaAndAirs = await bookingSheetSeaAndAir.GetShippingLineSeaAndAirResponseAsync(),
                Get_DEST_AGENTSeaAndAirs = await bookingSheetSeaAndAir.Get_DEST_AGENTSeaAndAirResponseAsync(),
                GetPortOfReceiptSeaAndAirs = await bookingSheetSeaAndAir.GetPortOfReceiptSeaAndAirResponseAsync(),
                GetPortOfDischargeSeaAndAirs = await bookingSheetSeaAndAir.GetPortOfDischargeSeaAndAirResponseAsync(),
                GetThaiForwarders = await bookingSheet.GetThaiForwarderResponseAsync(),
                GetContainerTypes = await bookingSheet.GetContainerTypeResponseAsync(),
                GetThaiBorders = await bookingSheet.GetThaiBorderResponseAsync(),
                //GetCYAtOrContactPersonSeaAndAirs = await bookingSheetSeaAndAir.GetCYAtOrContactPersonSeaAndAirResponseAsync(),
                //GetReturnAtOrContactPersonSeaAndAirs = await bookingSheetSeaAndAir.GetReturnAtOrContactPersonSeaAndAirResponseAsync(),
                GetLoadingAtSeaAndAirs = await bookingSheetSeaAndAir.GetLoadingAtSeaAndAirResponseAsync(),
                GetTruckTypes = await bookingSheet.GetTruckTypeResponseAsync(),
            });
        }
        public async Task<IActionResult> ListBookingSheetAsync()
        {
            return View((await bookingSheetSeaAndAir.GetBookingSheetByUserSeaAndAirResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetListBookingSheet(string dateFrom, string dateTo, string field)
        {
            var a = await bookingSheetSeaAndAir.GetBookingSheetByUserSeaAndAirResponseAsync(dateFrom, dateTo, field, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        public async Task<IActionResult> EditBookingSheetAsync(string docEntry)
        {
            ViewBag.docEntry = docEntry;
            return View(new ListMasterBookingSheetSeaAndAir
            {
                GetJobNumberSeaAndAirs = await bookingSheetSeaAndAir.GetJobNumberSeaAndAirResponseAsync(),
                SaleEmployees = await bookingSheet.GetSaleEmployeeResponseAsync(),
                GetPlaceOfLoadings = await bookingSheet.GetPlaceOfLoadingResponseAsync(),
                GetOverseaForwarders = await bookingSheet.GetOverseaForwarderResponseAsync(),
                GetPlaceOfDeliveries = await bookingSheet.GetPlaceOfDeliveryResponseAsync(),
                GetOverseaTruckers = await bookingSheet.GetOverseaTruckerResponseAsync(),
                GetOrigins = await bookingSheetSeaAndAir.GetOriginResponseAsync(),
                GetDestinations = await bookingSheetSeaAndAir.GetDestinationResponseAsync(),
                GetShippers = await bookingSheet.GetShipperResponseAsync(),
                GetCoLoaderSeaAndAirs = await bookingSheetSeaAndAir.GetCoLoaderSeaAndAirResponseAsync(),
                GetCustomerSeaAndAirs = await bookingSheetSeaAndAir.GetCustomerSeaAndAirResponseAsync(),
                GetConsignees = await bookingSheet.GetConsigneeResponseAsync(),
                GetShippingLineSeaAndAirs = await bookingSheetSeaAndAir.GetShippingLineSeaAndAirResponseAsync(),
                Get_DEST_AGENTSeaAndAirs = await bookingSheetSeaAndAir.Get_DEST_AGENTSeaAndAirResponseAsync(),
                GetPortOfReceiptSeaAndAirs = await bookingSheetSeaAndAir.GetPortOfReceiptSeaAndAirResponseAsync(),
                GetPortOfDischargeSeaAndAirs = await bookingSheetSeaAndAir.GetPortOfDischargeSeaAndAirResponseAsync(),
                GetThaiForwarders = await bookingSheet.GetThaiForwarderResponseAsync(),
                GetContainerTypes = await bookingSheet.GetContainerTypeResponseAsync(),
                GetThaiBorders = await bookingSheet.GetThaiBorderResponseAsync(),
                //GetCYAtOrContactPersonSeaAndAirs = await bookingSheetSeaAndAir.GetCYAtOrContactPersonSeaAndAirResponseAsync(),
                //GetReturnAtOrContactPersonSeaAndAirs = await bookingSheetSeaAndAir.GetReturnAtOrContactPersonSeaAndAirResponseAsync(),
                GetLoadingAtSeaAndAirs = await bookingSheetSeaAndAir.GetLoadingAtSeaAndAirResponseAsync(),
                GetTruckTypes = await bookingSheet.GetTruckTypeResponseAsync(),
                getDetailBookingSheetSeaAndAir = await bookingSheetSeaAndAir.GetBookingSheetByDocEntrySeaAndAirResponseAsync(docEntry),
            });
        }
        public async Task<IActionResult> EditBookingSheetCommoditiesAsync(string docEntry)
        {
            ViewBag.BookingID = docEntry;
            ResponseData<ListMasterBookingSheetSeaAndAir> response = new ResponseData<ListMasterBookingSheetSeaAndAir>();
            response.Data = new ListMasterBookingSheetSeaAndAir
            {
                Get_DEST_AGENTSeaAndAirs = await bookingSheetSeaAndAir.Get_DEST_AGENTSeaAndAirResponseAsync(),
                GetConsignees = await bookingSheet.GetConsigneeResponseAsync(),
                GetShippers= await bookingSheet.GetShipperResponseAsync(),
                GetCustomerSeaAndAirs = await bookingSheetSeaAndAir.GetCustomerSeaAndAirResponseAsync(),
                getDetailBookingSheetSeaAndAir = await bookingSheetSeaAndAir.GetBookingSheetByDocEntrySeaAndAirResponseAsync(docEntry)
            };
            return View(response);
        }
        public async Task<IActionResult> ViewDetailBookingSheet(string docEntry)
        {
            return View(await bookingSheetSeaAndAir.GetBookingSheetByDocEntrySeaAndAirResponseAsync(docEntry));
        }
        [HttpPost]
        public async Task<IActionResult> SubmitBookingSheetSeaAndAirAsync(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir)
        {
            //addBookingSheetSeaAndAir.HeaderObj.BOOKINGDATE = DateTime.Now.Date;
            addBookingSheetSeaAndAir.HeaderObj.CuffOfTimeAir = addBookingSheetSeaAndAir.HeaderObj.CuffOfDateAir;
            var a = await bookingSheetSeaAndAir.PostBookingSheetSeaAndAir(addBookingSheetSeaAndAir);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBookingSheetSeaAndAirAsync(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir, string docEntry)
        {
            //addBookingSheetSeaAndAir.HeaderObj.BOOKINGDATE = DateTime.Now.Date;
            addBookingSheetSeaAndAir.HeaderObj.CuffOfTimeAir = addBookingSheetSeaAndAir.HeaderObj.CuffOfDateAir;
            addBookingSheetSeaAndAir.HeaderObj.BOOKINGNO = docEntry;
            var a = await bookingSheetSeaAndAir.PutBookingSheetSeaAndAir(addBookingSheetSeaAndAir);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPut]
        public async Task<IActionResult> OnUpdateCommoditiesBookingSheetSeaAndAirAsync(string CreateUser, int docEntry, List<CommoditySeaAndAir> commodities
                                                                                        , HeaderCommodityUpdate headerCommodityUpdate
                                                                                        ,List<DestAgentUpdateCommodities> listDestAgent
                                                                                        , List<ShipperUpdateCommodities> listShipper
                                                                                        , List<ConsigneeUpdateCommodities> listConsignee
                                                                                        , List<CustomerUpdateCommodities> listCustomer)
        {
            var a = await bookingSheetSeaAndAir.PutCommodityInBookingSheet(CreateUser, docEntry, commodities, headerCommodityUpdate, listDestAgent,listShipper,listConsignee, listCustomer);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }

        [HttpDelete]
        public async Task<IActionResult> OnCancelBookingSheetAsync(string docNum)
        {
            var a = await bookingSheetSeaAndAir.CancelBookingSheetSeaAndAirAsync(docNum);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfListBookingSheetAsync(string getBookingSheetExports)
        {
            List<GetBookingSheetExport> obj = new List<GetBookingSheetExport>();
            obj = JsonConvert.DeserializeObject<List<GetBookingSheetExport>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "BOOKINGSHEET", "ListBookingSheet.rdl", ExportDataFromList.AppicationType.Excel, "ListOfBookingSheet" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
    }
}
