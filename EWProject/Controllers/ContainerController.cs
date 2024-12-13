using Client.Models.Request;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class ContainerController : Controller
    {
        private readonly IContainer container;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ContainerController(IContainer container, IWebHostEnvironment webHostEnvironment)
        {
            this.container = container;
            this.webHostEnvironment = webHostEnvironment;
        }
        #region View
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListContainer()
        {
            return View(await container.GetListInventoryTransfer());
        }
        public async Task<IActionResult> ViewDetailContainer(string docEntry)
        {
            return View(await container.GetInventoryTransferDetailByDocEntry(docEntry));
        }
        #endregion
        #region Get
        [HttpGet]
        public async Task<IActionResult> GetContainerStatusUpdateReportAsync(string dateFrom, string dateTo)
        {
            var rs = await container.GetContainerStatusResponseAsync(webHostEnvironment, dateFrom, dateTo);
            return File(rs.Data, rs.ApplicationType, rs.FileName);
        }
        //[HttpGet]
        public async Task<IActionResult> GetPathLoadReport(List<IFormFile> MyUploader)
        {
            var a = await container.GetLoadExcelPathAsync(MyUploader);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> PostInventorytransfer(PostInventoryTransfer postInventoryTransfer)
        {
            var a = await container.InventoryTransfer(postInventoryTransfer);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
        #endregion
        #region Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteInventoryTransferAsync(string docNum)
        {
            var a = await container.DeleteInventoryTransfer(docNum);
            return (a.ErrorCode == 0) ? Ok(a) : BadRequest(a);
        }
        #endregion
    }
}
