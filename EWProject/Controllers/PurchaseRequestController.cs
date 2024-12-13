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
    public class PurchaseRequestController : Controller
    {
        private readonly IPurchaseRequest purchaseRequest;
        private readonly IApproveDocument approveDocument;
        private readonly IPettyCash pettyCash;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PurchaseRequestController(IPurchaseRequest purchaseRequest, IApproveDocument approveDocument, IPettyCash pettyCash, IWebHostEnvironment webHostEnvironment)
        {
            this.purchaseRequest = purchaseRequest;
            this.approveDocument = approveDocument;
            this.pettyCash = pettyCash;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult LisPurchaseRequest(string data)
        {
            List<ListPurchaseRequestConfirm> obj = new List<ListPurchaseRequestConfirm>();
            if (data != null)
            {
                obj = JsonConvert.DeserializeObject<List<ListPurchaseRequestConfirm>>(data);
            }
            //ListConfirmPR
            return ViewComponent("ListPurchaseRequest", new { listPurchaseRequestConfirms = obj });
        }
        public IActionResult PurchaseRequestAdd(string data, string listPRID
                                             , string listAdvanceID
                                             , string listIDName
                                             , string listPRTemplate
                                             , string listADtemplate
                                             , string listidvendor
                                             , string itemCosting
                                             , string prAction
                                             , string listObjectPurchaseRequest)
        {

            return ViewComponent("PurchaseRequest", new
            {
                lsParam = data,
                listPRID = listPRID
                                                        ,
                listAdvanceID = listAdvanceID,
                PRID = listIDName
                                                        ,
                listPRTemplate = listPRTemplate,
                listADtemplate = listADtemplate
                                                        ,
                listidvendor = listidvendor
                                                        ,
                itemCosting = itemCosting
                                                        ,
                actionType = prAction
                                                        ,
                listObjectPurchaseRequest = listObjectPurchaseRequest
            });
        }
        public async Task<IActionResult> ListPurchaseRequest()
        {
            return View((await purchaseRequest.GetPurchaseRequestResponseAsync("PR", "1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetListPurchaseRequest(string dateFrom, string dateTo, string type)
        {
            var a = await purchaseRequest.GetPurchaseRequestResponseAsync("PR", dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfPurchaseRequestAsync(string getBookingSheetExports)
        {
            List<GetPurchaseRequest> obj = new List<GetPurchaseRequest>();
            obj = JsonConvert.DeserializeObject<List<GetPurchaseRequest>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListPurchaseRequest.rdl", ExportDataFromList.AppicationType.Excel, "ListPurchaseRequest" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        public async Task<IActionResult> Index()
        {
            //return View(new ListMasterPurchaseRequest
            //{
            //    ListGetItemCodePurchaseRequest= (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("Normal")).Data,
            //    GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
            //    ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
            //    GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
            //});
            return View(new ListMasterPurchaseRequest
            {
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesDocNumPurchaseRequest")).Data,
                GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                DocNum = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).DocNum,
                ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("Normal", HttpContext.Request.Cookies["Department"].ToString())).Data,
                //GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                GetTaxCodes = (await purchaseRequest.GetTaxCodes()).Data,
                GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data,
                GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("Normal")).Data,
                GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
            });
        }
        public async Task<IActionResult> EditPurchaseRequest(string docEntry)
        {
            return View(new ListMasterPurchaseRequest
            {
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesDocNumPurchaseRequestCOGS")).Data,
                DocNum = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).DocNum,
                GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("Normal", HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("CONFIRMBOOKINGSHEET")).Data,
                GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
                GetTaxCodes = (await purchaseRequest.GetTaxCodes()).Data,
                GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data,
                GetTruckNoAll = (await purchaseRequest.GetTruckNoByJobNoResponseAsync("-1")).Data,
                getDetailPurchaseRequest = (await approveDocument.GetDetailInformationDocumentApproveResponseAsync(docEntry)).Data,
            });
        }
        public async Task<IActionResult> GetPriceByItem(string ItemCode, string CardCode, string PriceList)
        {
            var a = await purchaseRequest.GetPriceListByItemCodeResponseAsync(ItemCode, CardCode, PriceList);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        public async Task<IActionResult> GetCurrencyByCustomerAsync(string cardCode)
        {
            var a = await purchaseRequest.GetGetCurrencyByCustomerAsync(cardCode);
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
        public async Task<IActionResult> GetTruckByJobNumber(string jobNo)
        {
            var a = await purchaseRequest.GetTruckNoByJobNoResponseAsync(jobNo);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        public IActionResult ViewPurchaseRequest(string data)
        {
            ListPurchaseRequestConfirm obj = null!;
            obj = JsonConvert.DeserializeObject<ListPurchaseRequestConfirm>(data);
            return ViewComponent("ViewPurchaseRequest", new { listPurchaseRequestConfirms = obj });
        }
        public async Task<IActionResult> ViewDetailPurchaseRequestAsync(string docEntry)
        {
            var a = await approveDocument.GetDetailInformationDocumentApproveResponseAsync(docEntry);
            var layout = await purchaseRequest.GetLayoutShowByTypeResponsenAsync();
            a.ListLayoutPrint = layout.Data;
            return View(a);
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
