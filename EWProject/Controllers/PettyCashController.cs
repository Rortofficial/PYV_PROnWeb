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
    public class PettyCashController : Controller
    {
        private readonly IPettyCash pettyCash;
        private readonly IPurchaseRequest purchaseRequest;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PettyCashController(IPettyCash pettyCash, IPurchaseRequest purchaseRequest, IWebHostEnvironment webHostEnvironment)
        {
            this.pettyCash = pettyCash;
            this.purchaseRequest = purchaseRequest;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string empID)
        {
            return View(new ListMasterPettyCash
            {
                GetMaxJournalVoucherNumber = (await pettyCash.GetMaxJournalVoucherNumberResponseAsync()).Data,
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesJournalVoucherNumber")).Data,
                GetBPCodeJournalVouchers = (await pettyCash.GetBPCodeJournalVoucherResponseAsync()).Data,
                GetAccountCodeJournalVouchers = (await pettyCash.GetAccountCodeJournalVoucherResponseAsync()).Data,
                GetEmployeeBudgets = await pettyCash.GetEmployeeBudgetByIDAsync(empID)
                //GetVatGroupJournalVouchers = (await pettyCash.GetVatGroupJournalVoucherResponseAsync()).Data,
                //getProjectManagmentList = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                //GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
            });
        }
        public async Task<IActionResult> EditDraftPettyCash(string docEntry, string empID)
        {
            ViewBag.docEntry = docEntry;
            return View(new ListMasterPettyCash
            {
                GetMaxJournalVoucherNumber = (await pettyCash.GetMaxJournalVoucherNumberResponseAsync()).Data,
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesJournalVoucherNumber")).Data,
                GetBPCodeJournalVouchers = (await pettyCash.GetBPCodeJournalVoucherResponseAsync()).Data,
                GetAccountCodeJournalVouchers = (await pettyCash.GetAccountCodeJournalVoucherResponseAsync()).Data,
                GetEmployeeBudgets = await pettyCash.GetEmployeeBudgetByIDAsync(empID),
                GetPettyCashViewDetail = await pettyCash.GetViewDetailPettyCashResponseByDocEntryAsync(docEntry, "DraftUpdate"),
                //GetVatGroupJournalVouchers = (await pettyCash.GetVatGroupJournalVoucherResponseAsync()).Data,
                //getProjectManagmentList = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                //GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
            });
        }
        public async Task<IActionResult> ListPettyCashAsync()
        {
            return View((await pettyCash.GetListJournalVoucherResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        public async Task<IActionResult> ViewDetailPettyCashByDocEntryAsync(string docEntry, string type)
        {
            ViewBag.docEntry = docEntry;
            ViewBag.type = type;
            return View(await pettyCash.GetViewDetailPettyCashResponseByDocEntryAsync(docEntry, type));
        }
        public IActionResult ListAdvancePayment(string data)
        {
            List<ListPurchaseRequestConfirm> obj = new List<ListPurchaseRequestConfirm>();
            if (data != null)
            {
                obj = JsonConvert.DeserializeObject<List<ListPurchaseRequestConfirm>>(data);
            }
            return ViewComponent("ListAdvancePayment", new { listAdvancePaymentConfirms = obj });
        }
        public IActionResult ViewAdvancePayment(string data)
        {
            ListPurchaseRequestConfirm obj = new ListPurchaseRequestConfirm();
            obj = JsonConvert.DeserializeObject<ListPurchaseRequestConfirm>(data);
            return ViewComponent("ViewAdvancePayment", new { listAdvancePaymentConfirms = obj });
        }
        public IActionResult AdvancePayment(string data
                                             , string listPRID
                                             , string listAdvanceID
                                             , string listIDName
                                             , string listPRTemplate
                                             , string listADtemplate
                                             , string prAction
                                             , string listObjectAdvance)
        {
            return ViewComponent("AdvancePayment", new
            {
                lsParam = data,
                listPRID = listPRID
                                                        ,
                listAdvanceID = listAdvanceID,
                AdvanceID = listIDName
                                                        ,
                listPRTemplate = listPRTemplate,
                listADtemplate = listADtemplate
                                                        ,
                actionType = prAction
                                                        ,
                listObjectAdvancePayment = listObjectAdvance
            });
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitPettyCashDraftAsync(PostPettyCashRequest postPettyCashRequest)
        {
            postPettyCashRequest.Header.WebID = (DateTime.Now.Day.ToString()
                                                                                + DateTime.Now.Month.ToString()
                                                                                + DateTime.Now.Year.ToString()
                                                                                + DateTime.Now.DayOfYear.ToString()
                                                                                + DateTime.Now.Hour.ToString()
                                                                                + DateTime.Now.Minute.ToString()
                                                                                + DateTime.Now.Second.ToString()
                                                                                + DateTime.Now.Millisecond.ToString()).ToString();
            var a = await pettyCash.PostPettyCashDraft(postPettyCashRequest);
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
        public async Task<IActionResult> OnSubmitPettyJECashAsync(PostPettyCashRequest postPettyCashRequest)
        {
            postPettyCashRequest.Header.UpdateBy = HttpContext.Request.Cookies["ID"];
            var a = await pettyCash.PostPettyCash(postPettyCashRequest, "", HttpContext.Request.Cookies["CostCenter"]);
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
        public async Task<IActionResult> OnCancelDraftAsync(string docEntry)
        {
            var a = await pettyCash.CancelPettyCashDraft(docEntry);
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
        public async Task<IActionResult> OnUpdateDraftAsync(string docEntry, PostPettyCashRequest postPettyCashRequest)
        {
            postPettyCashRequest.Header.UpdateBy = HttpContext.Request.Cookies["ID"];
            var a = await pettyCash.PutPettyCash(docEntry, "", postPettyCashRequest);
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
        public async Task<IActionResult> OnSubmitPettyJECashOnUpdateAsync(string docEntry, PostPettyCashRequest postPettyCashRequest)
        {
            postPettyCashRequest.Header.UpdateBy = HttpContext.Request.Cookies["ID"];
            var docEntryJE = await pettyCash.PostPettyCash(postPettyCashRequest, docEntry, HttpContext.Request.Cookies["CostCenter"]);
            var a = await pettyCash.PutPettyCash(docEntry, docEntryJE.DocEntry, postPettyCashRequest);
            a = await pettyCash.ClosePettyCashDraft(docEntry);
            a.DocEntry = docEntryJE.DocEntry;
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
        public async Task<IActionResult> GetListPettyCash(string dateFrom, string dateTo, string type)
        {
            var a = await pettyCash.GetListJournalVoucherResponseAsync(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfPettyCashAsync(string getBookingSheetExports)
        {
            List<GetListJournalVoucher> obj = new List<GetListJournalVoucher>();
            obj = JsonConvert.DeserializeObject<List<GetListJournalVoucher>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListPettyCash.rdl", ExportDataFromList.AppicationType.Excel, "ListPettyCash" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
    }
}
