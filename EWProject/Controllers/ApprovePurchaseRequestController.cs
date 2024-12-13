using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class ApprovePurchaseRequestController : Controller
    {
        private readonly IApproveDocument approveDocument;

        public ApprovePurchaseRequestController(IApproveDocument approveDocument)
        {
            this.approveDocument = approveDocument;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetApproveListByDocumentType(string dateFrom, string dateTo, string documentType)
        {
            if (String.IsNullOrEmpty(dateFrom))
            {
                dateFrom = "1999-01-01";
            }
            if (String.IsNullOrEmpty(dateTo))
            {
                dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var a = await approveDocument.GetListDocumentApproveByTypeResponseAsync(dateFrom, dateTo, documentType);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailInformationDocumentApproveByDocNum(string docNum)
        {
            var a = await approveDocument.GetDetailInformationDocumentApproveResponseAsync(docNum);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnApproveListByDocumentType(string docNum, string RemarksReson)
        {
            var a = await approveDocument.OnApprovePurchaseRequestResponseAsync(docNum, RemarksReson, HttpContext.Request.Cookies["ID"], HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnRejectListByDocumentType(string docNum, string RemarksReson)
        {
            var a = await approveDocument.OnRejectPurchaseRequestResponseAsync(docNum, RemarksReson, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
    }
}
