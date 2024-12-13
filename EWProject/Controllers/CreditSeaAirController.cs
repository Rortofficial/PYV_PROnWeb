using Client.Lib.BookingSheetSeaAndAir;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class CreditSeaAirController : Controller
    {
        private readonly IPettyCash _pettyCash;
        private readonly ICreditSeaAir _creditSeaAir;
        private readonly IPurchaseRequest _purchaseRequest;
        private readonly IBookingSheetSeaAndAir bookingSheetSeaAndAir;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreditSeaAirController(IPettyCash pettyCash, ICreditSeaAir creditSeaAir, IPurchaseRequest purchaseRequest, IBookingSheetSeaAndAir bookingSheetSeaAndAir, IWebHostEnvironment webHostEnvironment)
        {
            _pettyCash = pettyCash;
            _creditSeaAir = creditSeaAir;
            _webHostEnvironment = webHostEnvironment;
            _purchaseRequest = purchaseRequest;
            this.bookingSheetSeaAndAir = bookingSheetSeaAndAir;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _purchaseRequest.GetTaxCodes("GetTaxCodeSales"));
        }
        public async Task<IActionResult> ListCreditAsync()
        {
            return View(await _creditSeaAir.GetListOfCreditNote("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        [HttpPost]
        public async Task<IActionResult> PostCreditSeaAir(Models.Request.PurchaseRequest postPurchaseRequestRequest)
        {
            var a = await _creditSeaAir.PostCreditSeaAir(postPurchaseRequestRequest, HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        public async Task<IActionResult> ViewDetailCreditNote(string docEntry)
        {
            return View(await _creditSeaAir.GetCreditSeaAirDeatailByDocEntry(docEntry, "HeaderCredit"));
        }
        [HttpGet]
        public async Task<IActionResult> GetListCreditNoteAsync(string dateFrom, string dateTo, string field)
        {
            var a = await _creditSeaAir.GetListOfCreditNote(dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfListCreditNoteAsync(string getBookingSheetExports)
        {
            List<GetListCreditNoteSeaAirExport> obj = new List<GetListCreditNoteSeaAirExport>();
            obj = JsonConvert.DeserializeObject<List<GetListCreditNoteSeaAirExport>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "BOOKINGSHEET", "ListCreditNoteSeaAir.rdl", ExportDataFromList.AppicationType.Excel, "ListOfCreditNoteSeaAir" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        [HttpDelete]
        public async Task<IActionResult> OnCancelCreditNoteAsync(string docNum)
        {
            var a = await _creditSeaAir.RemoveCreditSeaAir(Convert.ToInt32(docNum));
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
    }
}
