using Client.Lib.OtherFunction;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize]
    public class PurchaseRequestAdvancePaymentController : Controller
    {
        private readonly IPurchaseRequest purchaseRequest;
        private readonly IPettyCash pettyCash;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApproveDocument approveDocument;

        public PurchaseRequestAdvancePaymentController(IPurchaseRequest purchaseRequest, IPettyCash pettyCash, IWebHostEnvironment webHostEnvironment, IApproveDocument approveDocument)
        {
            this.purchaseRequest = purchaseRequest;
            this.pettyCash = pettyCash;
            _webHostEnvironment = webHostEnvironment;
            this.approveDocument = approveDocument;
        }
        public async Task<IActionResult> Index()
        {
            return View(new ListMasterPurchaseRequest
            {
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesDocNumPurchaseRequestAdvancePayment")).Data,
                GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                DocNum = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).DocNum,
                ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("AdvanceForCustomer", HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("Advance")).Data,
                GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
                GetTruckNoAll = (await purchaseRequest.GetTruckNoByJobNoResponseAsync("-1")).Data,
                GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data
            });
        }
        public async Task<IActionResult> EditPurchaseAdvancePayment(string docEntry)
        {
            return View(new ListMasterPurchaseRequest
            {
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesDocNumPurchaseRequestCOGS")).Data,
                DocNum = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).DocNum,
                GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("PRCOSGConfirmBooking", HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("CONFIRMBOOKINGSHEET")).Data,
                GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
                GetTaxCodes = (await purchaseRequest.GetTaxCodes()).Data,
                GetTruckNoAll = (await purchaseRequest.GetTruckNoByJobNoResponseAsync("-1")).Data,
                getDetailPurchaseRequest = (await approveDocument.GetDetailInformationDocumentApproveResponseAsync(docEntry)).Data,
                GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data
            });
        }
        public async Task<IActionResult> ListPurchaseRequestAdvancePayment()
        {
            return View((await purchaseRequest.GetPurchaseRequestResponseAsync("PRAD", "1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetListPurchaseRequestAdvancePayment(string dateFrom, string dateTo, string type)
        {
            var a = await purchaseRequest.GetPurchaseRequestResponseAsync("PRAD", dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfPurchaseRequestAdvancePaymentAsync(string getBookingSheetExports)
        {
            List<ReimbursementRequest> obj = new List<ReimbursementRequest>();
            obj = JsonConvert.DeserializeObject<List<ReimbursementRequest>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListOfReimbursement.rdl", ExportDataFromList.AppicationType.Excel, "ListOfPurchaseAdvancePayment" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitPurchaseRequest(Models.Request.PurchaseRequest postPurchaseRequestRequest, string typePurchaseRequest)
        {
            var a = await purchaseRequest.PostPurchaseRequestAsync(postPurchaseRequestRequest, typePurchaseRequest);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
    }
}
