using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security;

[assembly: AllowPartiallyTrustedCallers]
namespace Client.Controllers
{
    [Authorize]
    public class BookingSheetController : Controller
    {
        private readonly IBookingSheet bookingSheet;
        private readonly ISalesQuotation salesQuotation;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration configuration;
        public IHttpContextAccessor context;

        public BookingSheetController(IBookingSheet bookingSheet, ISalesQuotation salesQuotation, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.bookingSheet = bookingSheet;
            this.salesQuotation = salesQuotation;
            _webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        }
        public async Task<IActionResult> Index()
        {
            //if (MasterData.listMasterBookingSheet == null)
            //{
            //    await MasterData.CallGetData(bookingSheet, salesQuotation);
            //}
            await MasterData.CallGetData(bookingSheet, salesQuotation);
            ViewBag.Origin = configuration.GetSection("SeletedDefaultOrigin").Value;
            return View(MasterData.listMasterBookingSheet);
        }
        public async Task<IActionResult> EditBookingSheet(string docEntry)
        {
            //if (MasterData.listMasterBookingSheet == null)
            //{
            //    await MasterData.CallGetData(bookingSheet, salesQuotation);
            //}
            await MasterData.CallGetData(bookingSheet, salesQuotation);
            ViewBag.BookingID = docEntry;
            return View(MasterData.listMasterBookingSheet);
        }
        public async Task<IActionResult> EditBookingSheetCommodities(string docEntry)
        {
            //if (MasterData.listMasterBookingSheet == null)
            //{
            //    await MasterData.CallGetData(bookingSheet, salesQuotation);
            //}
            await MasterData.CallGetData(bookingSheet, salesQuotation);
            MasterData.listMasterBookingSheet.GetDetailBookingSheetByID = await bookingSheet.GetBookingSheetByDocEntryResponseAsync(docEntry);
            ViewBag.BookingID = docEntry;
            return View(MasterData.listMasterBookingSheet);
        }
        public async Task<IActionResult> ListBookingSheet()
        {
            return View((await bookingSheet.GetBookingSheetByUserResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        public async Task<IActionResult> ViewDetailBookingSheet(string docEntry)
        {
            return View(await bookingSheet.GetBookingSheetByDocEntryResponseAsync(docEntry));
        }
        public async Task<IActionResult> GetSaleEmployee()
        {
            var a = await bookingSheet.GetSaleEmployeeResponseAsync();
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetListBookingSheet(string dateFrom, string dateTo, string value, string field)
        {
            var a = await bookingSheet.GetBookingSheetByUserResponseAsync(dateFrom, dateTo, field, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetBookingSheetDetailByIDAsync(string docEntry)
        {
            var a = await bookingSheet.GetBookingSheetUpdateByDocEntryResponseAsync(docEntry);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitBookingSheetAsync(PostBookingSheetRequest postBookingSheetRequest)
        {
            var a = await bookingSheet.PostBookingSheet(postBookingSheetRequest);
            //return Ok(Task.FromResult(Message));
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
        [HttpPut]
        public async Task<IActionResult> OnUpdateBookingSheetAsync(PostBookingSheetRequest postBookingSheetRequest)
        {
            var a = await bookingSheet.PutBookingSheet(postBookingSheetRequest);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPut]
        public async Task<IActionResult> OnUpdateCommoditiesBookingSheetAsync(string CreateUser, int docEntry, List<Commodity> commodities
                                                                                , HeaderCommodityUpdate headerCommodityUpdate, List<OverTruckers> overseaTrucker
                                                                                , List<ThaiForwarders> thaiForwarders, List<OverForwarders> overForwarders
                                                                                , List<ThaiBorders> thaiBorders, PlaceOfLoading placeOfLoadings, PlaceOfLoading placeOfDeliveries)
        {
            var a = await bookingSheet.PutCommodityInBookingSheet(CreateUser, docEntry, commodities, headerCommodityUpdate, overseaTrucker, thaiForwarders, overForwarders, thaiBorders, placeOfLoadings, placeOfDeliveries);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpDelete]
        public async Task<IActionResult> OnCancelBookingSheetAsync(string docNum)
        {
            var a = await bookingSheet.CancelBookingSheetAsync(docNum);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        #region Comment Upload Master Data
        //[HttpPost]
        //public async Task<IActionResult> OnSubmitUATBookingSheetAsync()
        //{
        //    var type1 = "";

        //    try
        //    {
        //        var bookingData = QueryData.ConvertDataTable<PostBookingSheetRequest>(new GetRecordByDataTable(
        //                     GetRecordByDataTable.StoreType.Add,
        //                     "ADDBOOKINGSHEET",
        //                     "",
        //                     "",
        //                     "",
        //                     "",
        //                     "").GetResultDataTable());
        //        foreach (var d in bookingData)
        //        {
        //            d.Commodities = QueryData.ConvertDataTable<Commodity>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "Commodities",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "Commodities";
        //            d.PlaceOfLoadings = QueryData.ConvertDataTable<PlaceOfLoading>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "PlaceOfLoadings",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "PlaceOfLoadings";
        //            d.PlaceOfDeliveries = QueryData.ConvertDataTable<PlaceOfDelivery>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "PlaceOfDeliveries",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "PlaceOfDeliveries";
        //            d.Volumes = QueryData.ConvertDataTable<Volume>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "Volumes",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "Volumes";
        //            d.ThaiBorders = QueryData.ConvertDataTable<ThaiBorders>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "ThaiBorders",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "ThaiBorders";
        //            d.OverseaTrucker = QueryData.ConvertDataTable<OverTruckers>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "OverseaTrucker",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "OverseaTrucker";
        //            d.OverseaForwarder = QueryData.ConvertDataTable<OverForwarders>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "OverseaForwarder",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "OverseaForwarder";
        //            d.SaleQuotation = QueryData.ConvertDataTable<SaleQuotations>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "SaleQuotation",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "SaleQuotation";
        //            d.ThaiForwarder = QueryData.ConvertDataTable<ThaiForwarders>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "ThaiForwarder",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "ThaiForwarder";
        //            d.Shipper = QueryData.ConvertDataTable<Shippers>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "Shipper",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "Shipper";
        //            d.Consignee = QueryData.ConvertDataTable<Consignees>(new GetRecordByDataTable(
        //                         GetRecordByDataTable.StoreType.Add,
        //                         "Consignee",
        //                         d.DocEntry.ToString(),
        //                         "",
        //                         "",
        //                         "",
        //                         "").GetResultDataTable());
        //            type1 = "Consignee";
        //            d.TruckType = QueryData.ConvertDataTable<TruckType>(new GetRecordByDataTable(
        //                        GetRecordByDataTable.StoreType.Add,
        //                        "TruckType",
        //                        d.DocEntry.ToString(),
        //                        "",
        //                        "",
        //                        "",
        //                        "").GetResultDataTable());
        //            type1 = "TruckType";
        //        }
        //        var oCom = SAP_Driver_oCompany._CheckingStatusOCompany();
        //        oCom.StartTransaction();
        //        foreach (var data in bookingData)
        //        {
        //            var a = BookingSheets._Add(data, oCom);
        //            if (a.ErroreCode != 0)
        //            {
        //                oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
        //                break;
        //            }
        //        }
        //        oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
        //        //return Ok(Task.FromResult(Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        var xc = type1;
        //        return Ok(ex.Message);
        //    }

        //    return Ok("Hello Word");
        //}
        #endregion

    }
}
