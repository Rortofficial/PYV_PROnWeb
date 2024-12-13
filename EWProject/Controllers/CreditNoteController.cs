using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize]
    public class CreditNoteController : Controller
    {
        private readonly ICreditNote creditNote;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPurchaseRequest purchaseRequest;
        public CreditNoteController(ICreditNote creditNote, IWebHostEnvironment webHostEnvironment, IPurchaseRequest purchaseRequest)
        {
            this.creditNote = creditNote;
            _webHostEnvironment = webHostEnvironment;
            this.purchaseRequest = purchaseRequest;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await purchaseRequest.GetTaxCodes("GetTaxCodeSales"));
        }
        public async Task<IActionResult> ListCreditNote()
        {
            return View(await creditNote.GetCreditNoteListResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        public async Task<IActionResult> ViewDetailARCreditMemo(string docEntry)
        {
            return View(await creditNote.GetDetailCreditMemoResponseAsync(docEntry, Configure.CreditNoteCBT));
        }
        [HttpGet]
        public async Task<IActionResult> GetListARInvoiceAsync(string department)
        {
            var a = await creditNote.GetARInvoiceResponseAsync(department);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetListArInvoiceWithJobNumberByDocEntryAsync(string docEntry)
        {
            var a = await creditNote.GetARInvoiceWithJobNumberByDocEntryResponseAsync(docEntry);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetListCreditNote(string dateFrom, string dateTo, string type)
        {
            var a = await creditNote.GetCreditNoteListResponseAsync(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfCreditNoteAsync(string getBookingSheetExports)
        {
            List<GetAdvancePaymentClearing> obj = new List<GetAdvancePaymentClearing>();
            obj = JsonConvert.DeserializeObject<List<GetAdvancePaymentClearing>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListCreditNote.rdl", ExportDataFromList.AppicationType.Excel, "ListCreditNote" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> PostCreditNoteRequestAsync(PostARCreditMemoRequest postARCreditMemoRequest)
        {
            postARCreditMemoRequest.CreateUser = HttpContext.Request.Cookies["ID"];
            postARCreditMemoRequest.CostCenter = HttpContext.Request.Cookies["CostCenter"];
            var a = await creditNote.postARCreditMemoResponse(postARCreditMemoRequest);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }

    }
}
