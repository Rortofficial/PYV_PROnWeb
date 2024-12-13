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
    public class ConfirmBookingSheetSeaAndAirController : Controller
    {
        private readonly IConfirmBookingSheet confirmBookingSheet;
        private readonly IConfirmBookingSeaAndAir confirmBookingSeaAndAir;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ConfirmBookingSheetSeaAndAirController(IConfirmBookingSheet confirmBookingSheet, IConfirmBookingSeaAndAir confirmBookingSeaAndAir, IWebHostEnvironment webHostEnvironment)
        {
            this.confirmBookingSheet = confirmBookingSheet;
            _webHostEnvironment = webHostEnvironment;
            this.confirmBookingSeaAndAir = confirmBookingSeaAndAir;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(new ListMasterConfirmBookingSheet
            {
                getBookingSheetByUser = (await confirmBookingSeaAndAir.GetBookingSheetByUserResponseAsync()).Data,
                getPriceListConfirmBookings = (await confirmBookingSheet.GetPriceListConfirmBookingResponseAsync()).Data,
            });
        }
        public async Task<IActionResult> ListOfConfirmBookingSheetAsync()
        {
            return View((await confirmBookingSeaAndAir.GetListConfirmBookingSheetResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        public async Task<IActionResult> ViewDetailConfirmBookingSheet(int docEntry)
        {
            return View(await confirmBookingSeaAndAir.GetDetailConfirmBookingSheetByDocEntryResponseAsync(docEntry));
        }
        public async Task<IActionResult> GetDetailConfirmBooking(string BookingID)
        {
            var a = await confirmBookingSeaAndAir.GetDetailBookingSheetByUserResponseAsync(Convert.ToInt32(BookingID));
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        public async Task<IActionResult> EditConfirmBookingSheet(int docEntry)
        {
            var a = (await confirmBookingSeaAndAir.GetEditConfirmBookingSheetByDocEntryResponseAsync(docEntry)).Data;
            return View(new ListMasterConfirmBookingSheet
            {
                getPriceListConfirmBookings = (await confirmBookingSheet.GetPriceListConfirmBookingResponseAsync()).Data,
                GetEditConfirmBookingSheets = a,
                GetCSNames = (await confirmBookingSheet.GetCSNameResponseAsync()).Data,
            });
        }
        public IActionResult ContainerInformation(string BookingID)
        {
            return ViewComponent("ContainerInfoSeaAndAir", new { BookingID = BookingID });
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitConfirmBookingSheetAsync(PostConfirmBookingSheetRequest postBookingSheetRequest)
        {
            var a = await confirmBookingSeaAndAir.PostConfirmBookingSheet(postBookingSheetRequest);
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
        public async Task<IActionResult> OnUpdateConfirmBookingSheetAsync(PutConfirmBookingSheetRequest putBookingSheetRequest)
        {
            var a = await confirmBookingSeaAndAir.PutConfirmBookingSheet(putBookingSheetRequest);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> OnCancelBookingSheetAsync(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries)
        {
            var a = await confirmBookingSeaAndAir.CancelConfirmBookingSheetAsync(docNum, bookingDocEntry, projectManagementEntry, projectSeries);
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
        public IActionResult ContainerInformationEidit(string BookingID, string GetEditConfirmBookingSheets)
        {
            var obj = JsonConvert.DeserializeObject<List<GetEditConfirmBookingSheet>>(GetEditConfirmBookingSheets);
            return ViewComponent("ContainerEditSeaAndAir", new { BookingID = BookingID, GetEditConfirmBookingSheets = obj });
        }
        [HttpGet]
        public async Task<IActionResult> GetListConfirmBookingSheet(string dateFrom, string dateTo, string type)
        {
            var a = await confirmBookingSeaAndAir.GetListConfirmBookingSheetResponseAsync(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfConfirmBookingSheetAsync(string getBookingSheetExports)
        {
            List<GetListConfirmBookingSheet> obj = new List<GetListConfirmBookingSheet>();
            obj = JsonConvert.DeserializeObject<List<GetListConfirmBookingSheet>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListConfirmBookingSheet.rdl", ExportDataFromList.AppicationType.Excel, "ListConfirmBookingSheet" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
    }
}
