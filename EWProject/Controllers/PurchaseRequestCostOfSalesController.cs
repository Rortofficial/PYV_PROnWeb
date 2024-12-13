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
    public class PurchaseRequestCostOfSalesController : Controller
    {
        private readonly IPurchaseRequest purchaseRequest;
        private readonly IPettyCash pettyCash;
        private readonly IApproveDocument approveDocument;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PurchaseRequestCostOfSalesController(IPurchaseRequest purchaseRequest, IPettyCash pettyCash, IWebHostEnvironment webHostEnvironment, IApproveDocument approveDocument)
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
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesDocNumPurchaseRequestCOGS")).Data,
                DocNum = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).DocNum,
                GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("PRCOSGConfirmBooking", HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("CONFIRMBOOKINGSHEET")).Data,
                GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
                GetTaxCodes = (await purchaseRequest.GetTaxCodes()).Data,
                GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data,
                GetTruckNoAll = (await purchaseRequest.GetTruckNoByJobNoResponseAsync("-1")).Data,
            });
        }
        public async Task<IActionResult> EditPurchaseRequestCostOfSale(string docEntry)
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
                GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data,
                GetTruckNoAll = (await purchaseRequest.GetTruckNoByJobNoResponseAsync("-1")).Data,
                getDetailPurchaseRequest = (await approveDocument.GetDetailInformationDocumentApproveResponseAsync(docEntry)).Data,
            });
        }
        public async Task<IActionResult> ListPurchaseRequestCostOfSale()
        {
            return View((await purchaseRequest.GetPurchaseRequestResponseAsync("PRCOS", "1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetListPurchaseRequestCostOfSale(string dateFrom, string dateTo, string type)
        {
            var a = await purchaseRequest.GetPurchaseRequestResponseAsync("PRCOS", dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfPurchaseRequestCostOfSaleAsync(string getBookingSheetExports)
        {
            List<GetPurchaseRequest> obj = new List<GetPurchaseRequest>();
            obj = JsonConvert.DeserializeObject<List<GetPurchaseRequest>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListPurchaseRequest.rdl", ExportDataFromList.AppicationType.Excel, "ListOfPurchaseCostofSales" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
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
        [HttpPut]
        public async Task<IActionResult> OnUpdatePurchaseRequest(string docEntry, EditPurchaseRequest purchaseRequests, int projectNum, string remarks)
        {
            purchaseRequests.Header.AmountTHB = (purchaseRequests.Header.AmountTHB.ToString() == "NaN") ? 0 : purchaseRequests.Header.AmountTHB;
            foreach (var z in purchaseRequests.EditPurchaseRows)
            {
                if (z.JobNumber != "0" && z.JobNumber != null)
                {
                    var jobNo = z.JobNumber;
                    z.JobNumber = GetQuery.GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", jobNo, "", "", "", "");
                }
                else
                {
                    z.JobNumber = "";
                }
            }
            var a = await purchaseRequest.EditPurchaseRequestAsync(docEntry, purchaseRequests, projectNum, remarks);
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
        public async Task<IActionResult> OnCancelPurchaseRequestAsync(string docNum)
        {
            var a = await purchaseRequest.CancelPurchaseRequestAsync(docNum);
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
        public async Task<IActionResult> GetRetriveData(string confirmBooking)
        {
            var a = await purchaseRequest.GetVendorByJobNoResponseAsync(confirmBooking, "ADD");
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
        public async Task<IActionResult> GetRetriveDataUpdate(string confirmBooking)
        {
            var a = await purchaseRequest.GetVendorByJobNoResponseAsync(confirmBooking, "UPDATE");
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
        public async Task<IActionResult> GetRefInvChecking(string RefInv)
        {
            var a = await purchaseRequest.GetCheckingRefInvPr(RefInv);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
    }
}
