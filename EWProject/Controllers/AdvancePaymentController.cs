using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class AdvancePaymentController : Controller
    {
        private readonly IPettyCash pettyCash;
        private readonly IAdvancePayment advancePayment;
        public AdvancePaymentController(IPettyCash pettyCash, IAdvancePayment advancePayment)
        {
            this.pettyCash = pettyCash;
            this.advancePayment = advancePayment;
        }
        public async Task<IActionResult> Index()
        {
            return View(new ListMasterPettyCash
            {
                GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesJournalEntryNumber")).Data,
                GetBPCodeJournalVouchers = (await pettyCash.GetBPCodeJournalVoucherResponseAsync()).Data,
                GetAccountCodeJournalVouchers = (await pettyCash.GetAccountCodeJournalVoucherResponseAsync()).Data,
                GetVatGroupJournalVouchers = (await pettyCash.GetVatGroupJournalVoucherResponseAsync()).Data,
                getProjectManagmentList = (await advancePayment.GetProjectManagmentListResponseAsync()).Data,
            });
        }
        public async Task<IActionResult> ListAdvancePayment()
        {
            return View((await advancePayment.GetAdvancePaymentByUserResponseAsync()));
        }
        public async Task<IActionResult> ViewDetailAdvancePayment(string docEntry)
        {
            return View((await advancePayment.GetDetailAdvancePaymentByDocEntryResponse(docEntry)));
        }
        public async Task<IActionResult> ListApprovalAdvancePaymentAsync()
        {
            return View((await advancePayment.GetAdvancePaymentApprovalResponseAsync()));
        }
        public async Task<IActionResult> ListEmployeeClearingAdvancePayment()
        {
            return View((await advancePayment.GetAdvancePaymentClearingResponseAsync()));
        }
        public async Task<IActionResult> AddEmployeeClearingAdvance(string transId)
        {
            return View(new ListMasterClearingAdvancePayment
            {
                getAdvancePaymentClearings = (await advancePayment.GetAdvancePaymentClearingResponseByTransIdAsync(transId)).Data,
                getClearingAdvances = (await advancePayment.GetClearingAdvanceTypeReponseAsync()).Data,
                getSeries = (await advancePayment.GetSeriesResponseAsync()).Data,
            });
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitAdvancePaymentAsync(PostAdvancePaymentRequest postAdvancePaymentRequest)
        {
            var a = await advancePayment.PostAdvancePayment(postAdvancePaymentRequest);
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
        public async Task<IActionResult> OnApproveAdvancePaymentAsync(string docEntry, string userID)
        {
            var z = (await advancePayment.GetDetailAdvancePaymentByDocEntryResponse(docEntry));
            if (z.ErrorCode == 0)
            {
                z.Data.UserID = userID;
                var a = await advancePayment.PostJE(z.Data);
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
            else
            {
                return BadRequest(z);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostEmployeeClearingAdvancePaymentAsync(EmployeeClearingAdvanceRequest employeeClearingAdvanceRequest)
        {
            var a = (await advancePayment.PostEmployeeClearingAdvance(employeeClearingAdvanceRequest));
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
        public async Task<IActionResult> OnCancelAdvancePaymentAsync(string docNum)
        {
            var a = await advancePayment.CancelAdvancePaymentAsync(docNum);
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
        [HttpDelete]
        public async Task<IActionResult> OnRejectAdvancePaymentAsync(string docNum)
        {
            var a = await advancePayment.RejectAdvancePayment(docNum);
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
    }
}
