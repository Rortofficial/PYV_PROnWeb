using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

namespace Client.Controllers
{
    [Authorize]
    public class PurchaseRequestSalesCommissionController : Controller
    {
        private readonly IBookingSheet bookingSheet;
        private readonly IPurchaseRequestForCommission purchaseRequestForCommission;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PurchaseRequestSalesCommissionController(IBookingSheet bookingSheet, IPurchaseRequestForCommission purchaseRequestForCommission, IWebHostEnvironment webHostEnvironment)
        {
            this.bookingSheet = bookingSheet;
            this.purchaseRequestForCommission = purchaseRequestForCommission;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await bookingSheet.GetSaleEmployeeResponseAsync());
        }
        public async Task<IActionResult> ListPurchaseRequestSalesCommission()
        {
            return View(await purchaseRequestForCommission.GetListSalesCommissions("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        [HttpGet]
        public async Task<IActionResult> GetListPurchaseRequestSalesCommission(string dateFrom, string dateTo, string type)
        {
            var a = await purchaseRequestForCommission.GetListSalesCommissions(dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfPurchaseRequestSalesCommissionAsync(string getBookingSheetExports)
        {
            List<GetListSalesCommission> obj = new List<GetListSalesCommission>();
            obj = JsonConvert.DeserializeObject<List<GetListSalesCommission>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ListPurchaseRequestSalesCommission", "ListPurchaseRequestSalesCommission.rdl", ExportDataFromList.AppicationType.Excel, "ListPurchaseRequestSalesCommission" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfPurchaseRequestSalesCommissionBySaleEmployeeAsync(string getCommissionBySaleEmployee)
        {
            List<GetCommissionBySaleEmployee> obj = new List<GetCommissionBySaleEmployee>();
            obj = JsonConvert.DeserializeObject<List<GetCommissionBySaleEmployee>>(getCommissionBySaleEmployee);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ListPRSaleCommissionBySaleEmployee", "ListPRSaleCommissionBySaleEmployee.rdl", ExportDataFromList.AppicationType.Excel, "ListPRSaleCommissionBySalesEmployee" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        public async Task<IActionResult> ViewDetailPurchaseRequestSalesCommissionAsync(string docEntry)
        {
            return View(await purchaseRequestForCommission.getSalesCommissionDetailByDocEntry(docEntry));
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesComissionDetailByDocEntryAsync(string docNum)
        {
            var a = await purchaseRequestForCommission.getSalesCommissionDetailByDocEntry(docNum);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetSaleCommissionBySaleEmployee(string slpCode)
        {
            var a = await purchaseRequestForCommission.getCommissionBySaleEmployee(slpCode);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }

        [HttpPost]
        public async Task<IActionResult> PostSalesCommissionDraftBySaleEmployee(List<PostInvoiceListSalesCommission> invoiceList, PostSalesCommissions postSalesCommissions)
        {
            postSalesCommissions.Lines = new List<PostSalesCommissionsRow>();
            foreach (var a1 in invoiceList)
            {
                var con = await purchaseRequestForCommission.CheckCommissionDetailByJobNumber(a1);
                if (con.ErrorCode == 0)
                {
                    if (con.Data.ARBalance == 0)
                    {
                        postSalesCommissions.Lines.Add(con.Data);
                    }
                    else
                    {
                        return BadRequest("Document that has gennerate Commission has AR balance available");
                    }
                }
                else
                {
                    return BadRequest(con);
                }
            }
            var a = await purchaseRequestForCommission.PostCommissionBySaleEmployee(postSalesCommissions);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
        [HttpDelete]
        public async Task<IActionResult> CancelSalesCommissionDraftBySaleEmployee(string docNum)
        {
            var a = await purchaseRequestForCommission.CancelCommissionBySaleEmployee(docNum);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> RejectSalesCommissionDraftBySaleEmployee(string docNum, string RemarksReson)
        {
            var a = await purchaseRequestForCommission.RejectCommissionBySaleEmployee(docNum, RemarksReson);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveSalesCommissionDraftBySaleEmployee(string docEntry, string RemarksReson)
        {
            var postSalesCommissions = await purchaseRequestForCommission.getSalesCommissionDetailByDocEntry(docEntry);
            postSalesCommissions.Data.Reason = RemarksReson;
            var a = await purchaseRequestForCommission.ApproveCommissionBySaleEmployee(postSalesCommissions.Data, HttpContext.Request.Cookies["CostCenter"]);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
    }
}
