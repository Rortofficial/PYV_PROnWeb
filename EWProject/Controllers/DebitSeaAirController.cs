using Client.Lib.BookingSheetSeaAndAir;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class DebitSeaAirController : Controller
    {
        private readonly IPettyCash pettyCash;
        private readonly ICreditSeaAir creditSeaAir;
        private readonly IPurchaseRequest purchaseRequest;
        private readonly IBookingSheetSeaAndAir bookingSheetSeaAndAir;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DebitSeaAirController(IPettyCash pettyCash, ICreditSeaAir creditSeaAir, IPurchaseRequest purchaseRequest, IBookingSheetSeaAndAir bookingSheetSeaAndAir, IWebHostEnvironment webHostEnvironment)
        {
            this.pettyCash = pettyCash;
            this.creditSeaAir = creditSeaAir;
            _webHostEnvironment = webHostEnvironment;
            this.purchaseRequest = purchaseRequest;
            this.bookingSheetSeaAndAir = bookingSheetSeaAndAir;
        }
        public async Task<IActionResult> IndexAsync()
        {
            //return View(new ListMasterCreditSeaAir
            //{
            //    GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesDocNumJournalVoucher")).Data,
            //    ListGetItemCodePurchaseRequest = (await creditSeaAir.GetItemCodeCreditSeaAirResponsesAsync("GetItemJournalVoucher", HttpContext.Request.Cookies["Department"].ToString())).Data,
            //    GetProjectManagmentLists = (await creditSeaAir.GetProjectManagmentListResponseAsync()).Data,
            //    GetVendorSeaAndAirs = (await purchaseRequest.GetVendorResponseAsync("Normal")).Data,
            //    GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
            //    GetTaxCodes = (await creditSeaAir.GetTaxCodes("Sales")).Data,
            //    GetArCreditNoteRefInvSeaAndAirs = (await creditSeaAir.GetArCreditNoteRefInvSeaAndAir()).Data,
            //});
            return View(await purchaseRequest.GetTaxCodes("GetTaxCodeSales"));
        }
        public async Task<IActionResult> ListDebitAsync()
        {
            return View((await creditSeaAir.GetListOfDebitNote("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])));
        }
        [HttpPost]
        public async Task<IActionResult> PostCreditSeaAir(Models.Request.PurchaseRequest postPurchaseRequestRequest)
        {
            var a = await creditSeaAir.PostDebitSeaAir(postPurchaseRequestRequest, HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        public async Task<IActionResult> ViewDetailDebitNote(string docEntry)
        {
            return View(await creditSeaAir.GetCreditSeaAirDeatailByDocEntry(docEntry, "HeaderDebit"));
        }
        [HttpGet]
        public async Task<IActionResult> GetListCreditNoteAsync(string dateFrom, string dateTo, string field)
        {
            var a = await creditSeaAir.GetListOfDebitNote(dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
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
            var a = await creditSeaAir.RemoveCreditSeaAir(Convert.ToInt32(docNum));
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
    }
}
