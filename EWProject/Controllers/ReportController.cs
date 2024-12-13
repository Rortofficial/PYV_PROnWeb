using Client.Connection;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReport report;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ReportController(IReport report, IWebHostEnvironment webHostEnvironment)
        {
            this.report = report;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await report.GetReportByUserAsync(HttpContext.Request.Cookies["ID"]));
        }
        public async Task<IActionResult> ReportByID(string id)
        {
            var a = await report.GetReportByIDAsync(id);
            string attributeObject = "<div class=\"row\">";
            if (a.ErrorCode == 0)
            {
                attributeObject += "<div class=\\\"col-12\\\"><div style=\\\"float:right\\\"><h2>" + a.Data[0].RerpotName + "</h2></div></div>";
                foreach (var z in a.Data)
                {
                    foreach (var i in Configure.GetObjectTypes)
                    {
                        if (z.ObjectType == i.ObjectType)
                        {
                            var tage = "";
                            tage += "<div class=\"col-6 mt-2\"><div class=\"row\">";
                            tage += "<div class=\"col-4\" style=\"margin:auto;text-align:left;font-weight:bold;\">" + z.TitleName + "</div>";
                            tage += "<div class=\"col-8\">" + i.ObjectAttribute.Replace("@Type", z.ObjectType).Replace("@Name", z.ObjectID).Replace("@ObjectID", z.ObjectID) + "</div>";
                            tage += "</div></div>";
                            var script = "";
                            if (i.Script != "" && i.Script != null)
                            {
                                script = i.Script.Replace("@ObjectID", z.ObjectID).Replace("@Numberselect", z.NumberSelect);
                            }
                            var option = "";
                            if (i.Query == true)
                            {
                                var dt = await report.GetSubAttributeAsync(HttpContext.Request.Cookies["ID"], z.ObjectQuery);
                                foreach (var dr in dt.Data)
                                {
                                    option += i.SubAttribute.Replace("@Code", dr.Code).Replace("@Name", dr.Name);
                                }
                                tage = tage.Replace("@option", option);
                            }
                            attributeObject += (tage + script);
                        }
                    }
                }
            }
            attributeObject += "</div>";
            ViewBag.Tage = attributeObject;
            return View(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitReport()
        {
            var reportID = HttpContext.Request.Form["txtCode"].ToString();
            var extensionType = HttpContext.Request.Form["cboSelectSeries"].ToString();
            var viewDetailReportByID = await report.GetReportViewDetailByIDAsync(reportID);
            string cmd = $"CALL \"{ConnectionString.CompanyDB}\".{viewDetailReportByID.Data.StoreName}(";
            string ds = "Report";
            string paramater = "";
            var count = 1;
            var conditionCount = viewDetailReportByID.Data.Lines.Count();
            foreach (var a in viewDetailReportByID.Data.Lines)
            {
                if (count == conditionCount)
                {
                    paramater += $"'{HttpContext.Request.Form[$"{a.ObjectID}"].ToString()}'";
                }
                else
                {
                    paramater += $"'{HttpContext.Request.Form[$"{a.ObjectID}"].ToString()}',";
                }
                count++;
            }
            cmd += paramater + ");";
            var rs = await report.CallReport(viewDetailReportByID.Data.ReportFile, extensionType, cmd, ds, viewDetailReportByID.Data.Name, webHostEnvironment);
            return File(rs.Data, rs.ApplicationType, rs.FileName);
        }
    }
}
