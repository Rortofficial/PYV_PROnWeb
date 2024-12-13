//using AspNetCore.Reporting;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
//using SAPbobsCOM;

namespace Client.Controllers
{
    [Authorize]
    public class SalesQuotationController : Controller
    {
        private readonly ISalesQuotation salesQuotation;
        private readonly IReportLayout reportLayout;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration configuration;

        public SalesQuotationController(ISalesQuotation salesQuotation, IReportLayout reportLayout, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.salesQuotation = salesQuotation;
            this.reportLayout = reportLayout;
            _webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Origin = configuration.GetSection("SeletedDefaultOrigin").Value;
            var a = (await salesQuotation.GetItemSalesQuotationResponses(HttpContext.Request.Cookies["Department"].ToString())).Data;
            return View(new ListMasterSalesQuotation
            {
                GetRefNos = (await salesQuotation.GetRefNoResponses(HttpContext.Request.Cookies["EmpID"].ToString())).Data,
                GetCreditTerms = (await salesQuotation.GetCreditTermResponses()).Data,
                GetServiceTypes = (await salesQuotation.GetServiceTypeResponses()).Data,
                GetOrigins = (await salesQuotation.GetOriginResponses()).Data,
                GetDestinations = (await salesQuotation.GetDestinationResponses()).Data,
                GetItemSaleQuotations = a,
                GetCOs = (await salesQuotation.GetCOResponseAsync()).Data,
                GetSaleEmployees = (await salesQuotation.GetSaleEmployeeResponseAsync()).Data,
                GetCurrencyByCustomers = (await salesQuotation.GetBPCurrencyResponses("")).Data,
            });
        }
        public async Task<IActionResult> ListSalesQuotation()
        {
            var a = await salesQuotation.GetListSaleQuotationResponse("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"].ToString());
            return View(a.Data);
        }
        public async Task<IActionResult> ViewDetailSalesQuotation(string docEntry)
        {
            return View(await salesQuotation.GetDetailViewSalesQuotationAsync(docEntry));
        }
        public async Task<IActionResult> GetAddressByCustomer(string cardCode)
        {
            return Ok(await salesQuotation.GetCustomerAddress(cardCode));
        }
        public async Task<IActionResult> GetBPCurrencyResponses(string cardCode)
        {
            return Ok(await salesQuotation.GetBPCurrencyResponses(cardCode));
        }
        public async Task<IActionResult> GetInformationBP(string cardCode)
        {
            return Ok(await salesQuotation.GetInformationBPResponses(cardCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetListSalesQuotation(string dateFrom, string dateTo, string type)
        {
            var a = await salesQuotation.GetListSaleQuotationResponse(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"].ToString());
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfSalesQuotationAsync(string getBookingSheetExports)
        {
            List<GetListSaleQuotation> obj = new List<GetListSaleQuotation>();
            obj = JsonConvert.DeserializeObject<List<GetListSaleQuotation>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "SALESQUOTATION", "ListSalesQuotation.rdl", ExportDataFromList.AppicationType.Excel, "ListOfSalesQuotation" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitSaleQuotation(PostSalesQuotationRequest postSalesQuotationRequest)
        {
            postSalesQuotationRequest.CostCenter = HttpContext.Request.Cookies["CostCenter"].ToString();
            var a = await salesQuotation.PostSalesQuotations(postSalesQuotationRequest);
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
        public async Task<IActionResult> GetContactPersonByCardCode(string cardCode)
        {
            var a = await salesQuotation.GetContactPersonByCardCode(cardCode);
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
        public async Task<IActionResult> OnDeleteSalesQuotation(int DocEntry)
        {
            var a = await salesQuotation.DeleteSalesQuotations(DocEntry);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> OnExportLayoutMultipleAsync(List<string> listDocEntry,string layoutCode)
        //{
        //    //listDocEntry.Add("6037");
        //    //listDocEntry.Add("6036");
        //    //layoutCode = "SQ-0023";
        //    //List<byte[][]> layoutResponse = new();
        //    //PrintViewLayoutResponse printViewLayoutResponse = new PrintViewLayoutResponse();
        //    //foreach (var docEntry in listDocEntry)
        //    //{
        //    //    printViewLayoutResponse = await reportLayout.CallViewLayout(layoutCode, docEntry, _webHostEnvironment);
        //    //    layoutResponse.ToList()(printViewLayoutResponse.Data);
        //    //}
        //    //printViewLayoutResponse.Data=PDFMerge.MergePdfBytes(,);
        //    //return File(printViewLayoutResponse.Data, printViewLayoutResponse.ApplicationType, printViewLayoutResponse.FileName);
        //}
    }
}
