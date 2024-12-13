using Client.Lib.OtherFunction;
using Client.Lib.Settlement;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client.Controllers
{
    [Authorize]
    public class SettlementController : Controller
    {
        private readonly IAdvancePayment advancePayment;
        private readonly IApproveDocument approveDocument;
        private readonly IPurchaseRequest purchaseRequest;
        private readonly ISettlement settlement;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SettlementController(IAdvancePayment advancePayment, IApproveDocument approveDocument, IPurchaseRequest purchaseRequest, ISettlement settlement, IWebHostEnvironment webHostEnvironment)
        {
            this.advancePayment = advancePayment;
            this.approveDocument = approveDocument;
            this.purchaseRequest = purchaseRequest;
            this.settlement = settlement;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(new ListMasterSettlement
            {
                ListItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("ITEM-ALL", HttpContext.Request.Cookies["Department"].ToString())).Data,
                //ListCustomerJobSheetTrucking= (await settlement.getListCustomerJobSheetTruckingResponse()).Data,
            });
        }
        public async Task<IActionResult> ViewDetailPurchaseOrder(string docEntry, string type)
        {
            ViewBag.type = type;
            if (type == "draft")
            {
                return View("ViewDetailPurchaseOrderDraft", await settlement.GetDetailSettlementByDocEntryResponseAsync(docEntry));
            }
            else
            {
                return View(await settlement.GetDetailPurchaseOrderResponseAsync(docEntry));
            }
        }
        public async Task<IActionResult> ListSettlement()
        {
            return View(await settlement.GetPurchaseOrderListResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        [HttpGet]
        public async Task<IActionResult> GetListSettlement(string dateFrom, string dateTo, string type)
        {
            var a = await settlement.GetPurchaseOrderListResponseAsync(dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfSettlementAsync(string getBookingSheetExports)
        {
            List<GetAdvancePaymentClearing> obj = new List<GetAdvancePaymentClearing>();
            obj = JsonConvert.DeserializeObject<List<GetAdvancePaymentClearing>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "GetListSettlement.rdl", ExportDataFromList.AppicationType.Excel, "GetListSettlement" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        public async Task<IActionResult> EditSettlementDraft(string docEntry)
        {
            return View(new ListMasterEditSettlement
            {
                GetDetailSettlementDraft = await settlement.GetDetailSettlementByDocEntryResponseAsync(docEntry),
                ListItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("ITEM-ALL", HttpContext.Request.Cookies["Department"].ToString())).Data,
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetListAdvancePaymentClearingAsync()
        {
            var a = await advancePayment.GetAdvancePaymentClearingResponseAsync();
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetListAdvancePaymentClearingByDocEntryAsync(string docEntry)
        {
            var a = await approveDocument.GetDetailInformationDocumentApproveResponseAsync(docEntry);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetProjectManagementListAsync()
        {
            var a = await settlement.GetProjectManagmentListResponseAsync();
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetListRefInvByJobSheetResponseAsync(string cardCode, string JobNumber)
        {
            var a = await settlement.getListRefInvByJobSheetResponse(cardCode, JobNumber);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetListCustomerJobSheetTruckingResponseAsync(string JobNumber)
        {
            var a = await settlement.getListCustomerJobSheetTruckingResponse(JobNumber);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> PutPurchaseRequestAsync(UpdatePurchaseRequest updatePurchaseRequest)
        {
            var a = await settlement.PutPurchaseRequestAsync(updatePurchaseRequest, HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> PostSettlementDraftDocument(AddSettlementDraftRequest addSettlementDraftRequest)
        {
            addSettlementDraftRequest.UserID = HttpContext.Request.Cookies["ID"];
            var a = await settlement.AddSettlementDraftAsync(addSettlementDraftRequest);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPut]
        public async Task<IActionResult> PutSettlementDraftDocument(AddSettlementDraftRequest addSettlementDraftRequest)
        {
            addSettlementDraftRequest.UserID = HttpContext.Request.Cookies["ID"];
            var Obj = Settlements._ClearEmptyStringLine(addSettlementDraftRequest.Lines);
            addSettlementDraftRequest.Lines = Obj.addSettlementLines;
            var a = await settlement.UpdateSettlementDraftAsync(addSettlementDraftRequest);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSettlementDraftDocument(string docNum)
        {
            var a = await settlement.CancelSettlementDraftAsync(docNum);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }

    }
}
