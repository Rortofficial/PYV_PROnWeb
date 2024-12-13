using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class ReimbursementController : Controller
    {
        private readonly IReimbursement reimbursement;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReimbursementController(IReimbursement reimbursement, IWebHostEnvironment webHostEnvironment)
        {
            this.reimbursement = reimbursement;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(new ListMasterReimbursementRequest
            {
                GetEmployeeBudgets = (await reimbursement.GetEmployeeBudget()).Data
            });
        }
        public async Task<IActionResult> UpdateReimbursementRequestAsync(string docEntry)
        {
            return View(new ListMasterReimbursementRequest
            {
                GetEmployeeBudgets = (await reimbursement.GetEmployeeBudget()).Data,
                GetReimbursementRequestsByDocEntry = (await reimbursement.ReimbursementGet(docEntry, "1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data.FirstOrDefault(),
            });
        }
        public async Task<IActionResult> ListReimbursement()
        {
            return View(await reimbursement.ReimbursementGet("", "1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        [HttpGet]
        public async Task<IActionResult> GetListReimbursement(string dateFrom, string dateTo, string type)
        {
            var a = await reimbursement.ReimbursementGet("", dateFrom, dateTo, "ALL", HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfReimbursementAsync(string getBookingSheetExports)
        {
            List<ReimbursementRequest> obj = new List<ReimbursementRequest>();
            obj = JsonConvert.DeserializeObject<List<ReimbursementRequest>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListOfReimbursement.rdl", ExportDataFromList.AppicationType.Excel, "ListOfReimbursement" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        public async Task<IActionResult> ViewDetailReimbursementByDocEntryAsync(string docEntry)
        {
            return View((await reimbursement.ReimbursementGet(docEntry, "1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data.FirstOrDefault());
        }
        public IActionResult ListApproveReimbursementAsync()
        {
            //GetAccountCode
            return View(new ListApproveReimbursementMasterData
            {
                ReimbursementRequests = reimbursement.ListReimbursementApprove().Result.Data,
                GetAccountReimbursments = reimbursement.GetAccountCode().Result.Data,
            });
        }
        public IActionResult ViewReimbursement()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostReimbursementReques(ReimbursementRequest reimbursementrequest)
        {
            var a = await reimbursement.ReimbursementPost(reimbursementrequest);
            if (a.ErrorCode is 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> PostApproveDocumentActionAsync(List<ListApproveReimbursement> listApproveReimbursements)
        {
            var a = await reimbursement.ApproveReimbursementPost(listApproveReimbursements, HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode is 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPut]
        public async Task<IActionResult> PutReimbursementReques(ReimbursementRequest reimbursementrequest)
        {
            var a = await reimbursement.ReimbursementPut(reimbursementrequest);
            if (a.ErrorCode is 0) return Ok(a); else return BadRequest(a);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReimbursementReques(string docEntry)
        {
            var a = await reimbursement.ReimbursementDelete(docEntry);
            if (a.ErrorCode is 0) return Ok(a); else return BadRequest(a);
        }
    }
}
