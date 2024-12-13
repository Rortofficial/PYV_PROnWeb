using Client.Repository;
using Client.SignalR;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Client.Controllers
{
    [Authorize]
    public class MessageAlertController : Controller
    {
        private readonly IAlert alert;
        private IHubContext<ChatHub> hubContext { get; set; }

        public MessageAlertController(IAlert alert, IHubContext<ChatHub> hubContext)
        {
            this.alert = alert;
            this.hubContext = hubContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await alert.GetCSNameResponseAsync());
        }
        public async Task<IActionResult> ListMessage(string userID)
        {
            return View(await alert.GetMessagesByUser(userID));
        }
        public async Task<IActionResult> ViewAlert(string userID, string alertID)
        {
            await alert.PutMessage(userID, alertID);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", userID);
            return View(await alert.GetMessagesByAlertID(alertID));
        }
        [HttpGet]
        public async Task<IActionResult> GetMessage(string userID)
        {
            var a = await alert.GetMessagesTop5AlertByUserID(userID);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
    }
}
