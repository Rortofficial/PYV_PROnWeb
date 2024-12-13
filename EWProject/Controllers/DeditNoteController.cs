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
    public class DeditNoteController : Controller
    {
        private readonly ICreditNote creditNote;
        private readonly IDeditNote deditNote;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPurchaseRequest purchaseRequest;

        public DeditNoteController(ICreditNote creditNote, IDeditNote deditNote, IWebHostEnvironment webHostEnvironment, IPurchaseRequest purchaseRequest)
        {
            this.creditNote = creditNote;
            this.deditNote = deditNote;
            _webHostEnvironment = webHostEnvironment;
            this.purchaseRequest = purchaseRequest;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await purchaseRequest.GetTaxCodes("GetTaxCodeSales"));
        }
        public async Task<IActionResult> ListDeditNote()
        {
            return View(await deditNote.GetCreditNoteListResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        public async Task<IActionResult> ViewDetailARDeditMemo(string docEntry)
        {
            return View(await creditNote.GetDetailCreditMemoResponseAsync(docEntry, Configure.DebitNoteCBT));
        }
        [HttpGet]
        public async Task<IActionResult> GetListARInvoiceAsync(string department)
        {
            var a = await deditNote.GetARInvoiceResponseAsync(department);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetListARInvoiceWithJobNumberByDocEntryAsync(string docEntry)
        {
            var a = await deditNote.GetARInvoiceWithJobNumberByDocEntryResponseAsync(docEntry, HttpContext.Request.Cookies["Department"].ToString());
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetListDeditNote(string dateFrom, string dateTo, string type)
        {
            var a = await deditNote.GetCreditNoteListResponseAsync(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfDeditNoteAsync(string getBookingSheetExports)
        {
            List<GetAdvancePaymentClearing> obj = new List<GetAdvancePaymentClearing>();
            obj = JsonConvert.DeserializeObject<List<GetAdvancePaymentClearing>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListCreditNote.rdl", ExportDataFromList.AppicationType.Excel, "ListCreditNote" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> PostDeditNoteRequestAsync(PostARCreditMemoRequest postARCreditMemoRequest)
        {
            postARCreditMemoRequest.CreateUser = HttpContext.Request.Cookies["ID"];
            postARCreditMemoRequest.CostCenter = HttpContext.Request.Cookies["CostCenter"];
            var a = await deditNote.postARDeditMemoResponse(postARCreditMemoRequest);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
    }
}
