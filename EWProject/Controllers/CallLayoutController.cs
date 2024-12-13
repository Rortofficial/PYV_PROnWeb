using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class CallLayoutController : Controller
    {
        private readonly IReportLayout reportLayout;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CallLayoutController(IReportLayout reportLayout, IWebHostEnvironment webHostEnvironment)
        {
            this.reportLayout = reportLayout;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> PrintLayout(string docEntry, string layoutCode)
        {
            var a = await reportLayout.CallViewLayout(layoutCode, docEntry, webHostEnvironment);
            return File(a.Data, a.ApplicationType, a.FileName);
        }
    }
}
