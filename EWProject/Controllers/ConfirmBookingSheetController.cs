using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Client.Controllers
{
    [Authorize]
    public class ConfirmBookingSheetController : Controller
    {
        private readonly IConfirmBookingSheet confirmBookingSheet;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ConfirmBookingSheetController(IConfirmBookingSheet confirmBookingSheet, IWebHostEnvironment webHostEnvironment)
        {
            this.confirmBookingSheet = confirmBookingSheet;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public async Task<IActionResult> Index()
        {
            return View(new ListMasterConfirmBookingSheet
            {
                //getBookingSheetByUsers=(await confirmBookingSheet.GetBookingSheetByUserResponseAsync()).Data,
                //getDetailBookingSheetByUsers = (await confirmBookingSheet.GetDetailBookingSheetByUserResponseAsync(Convert.ToInt32(BookingID))).Data,
                //getContainers=(await confirmBookingSheet.GetContainerResponseAsync()).Data,
                //getVendors=(await confirmBookingSheet.GetVendorResponseAsync()).Data,
                //getTruckNos = (await confirmBookingSheet.GetTruckNoResponseAsync()).Data,
                //getEmployees = (await confirmBookingSheet.GetEmployeeResponseAsync()).Data,
                getBookingSheetByUser = (await confirmBookingSheet.GetBookingSheetByUserResponseAsync()).Data,
                getPriceListConfirmBookings = (await confirmBookingSheet.GetPriceListConfirmBookingResponseAsync()).Data,
                //GetCSNames = (await confirmBookingSheet.GetCSNameResponseAsync()).Data,
            });
        }

        public async Task<IActionResult> EditConfirmBookingSheet(int docEntry)
        {
            ViewBag.docEntry = docEntry;
            return View(new ListMasterConfirmBookingSheet
            {
                getPriceListConfirmBookings = (await confirmBookingSheet.GetPriceListConfirmBookingResponseAsync()).Data,
                GetEditConfirmBookingSheets = (await confirmBookingSheet.GetEditConfirmBookingSheetByDocEntryResponseAsync(docEntry)).Data,
                GetCSNames = (await confirmBookingSheet.GetCSNameResponseAsync()).Data,
            });
        }

        public async Task<IActionResult> ViewDetailConfirmBookingSheet(int docEntry)
        {
            return View(await confirmBookingSheet.GetDetailConfirmBookingSheetByDocEntryResponseAsync(docEntry));
        }
        public async Task<IActionResult> GetTruckNoAsync(string province)
        {
            var a = await confirmBookingSheet.GetTruckNoResponseAsync(province);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        public async Task<IActionResult> GetDetailConfirmBooking(string BookingID)
        {
            var a = await confirmBookingSheet.GetDetailBookingSheetByUserResponseAsync(Convert.ToInt32(BookingID));
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }

        public IActionResult ContainerInformation(string BookingID)
        {
            return ViewComponent("ContainerInfo", new { BookingID = BookingID });
        }
        public IActionResult ContainerInformationEidit(string BookingID, int docEntry/*string GetEditConfirmBookingSheets*/)
        {
            //var obj = JsonConvert.DeserializeObject<List<GetEditConfirmBookingSheet>>(GetEditConfirmBookingSheets);
            return ViewComponent("ContainerEdit", new { BookingID = BookingID, docEntry = docEntry });
        }
        public async Task<IActionResult> ListBookingSheet()
        {
            return View((await confirmBookingSheet.GetBookingSheetByUserResponseAsync()).Data);
        }
        public async Task<IActionResult> ListConfirmBookingSheet()
        {
            return View((await confirmBookingSheet.GetListConfirmBookingSheetResponseAsync("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"])).Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetListConfirmBookingSheet(string dateFrom, string dateTo, string type)
        {
            var a = await confirmBookingSheet.GetListConfirmBookingSheetResponseAsync(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfConfirmBookingSheetAsync(string getBookingSheetExports)
        {
            List<GetListConfirmBookingSheet> obj = new List<GetListConfirmBookingSheet>();
            obj = JsonConvert.DeserializeObject<List<GetListConfirmBookingSheet>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, _webHostEnvironment, "ConfirmBookingSheet", "ListConfirmBookingSheet.rdl", ExportDataFromList.AppicationType.Excel, "ListConfirmBookingSheet" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitConfirmBookingSheetAsync(PostConfirmBookingSheetRequest postBookingSheetRequest)
        {
            var a = await confirmBookingSheet.PostConfirmBookingSheet(postBookingSheetRequest);
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
        //public async Task<IActionResult> OnSubmitUATConfirmBookingSheetAsync()
        //{
        //    try
        //    {
        //        var ConbookingData = QueryData.ConvertDataTable<PostConfirmBookingSheetRequest>(new GetRecordByDataTable(
        //                    GetRecordByDataTable.StoreType.Add,
        //                    "ADDCONFIRMBOOKING",
        //                    "",
        //                    "",
        //                    "",
        //                    "",
        //                    "").GetResultDataTable());
        //        foreach (var data in ConbookingData)
        //        {
        //            data.Lines = QueryData.ConvertDataTable<TruckInformationRequest>(new GetRecordByDataTable(
        //                        GetRecordByDataTable.StoreType.Add,
        //                        "TRUCKINFORMATION",
        //                        data.DocEntry.ToString(),
        //                        "",
        //                        "",
        //                        "",
        //                        "").GetResultDataTable());
        //            foreach (var a1 in data.Lines)
        //            {
        //                a1.ListPurchaseRequest = QueryData.ConvertDataTable<Models.Request.PurchaseRequest>(new GetRecordByDataTable(
        //                        GetRecordByDataTable.StoreType.Add,
        //                        "ADVANCEPAYMENT",
        //                        data.DocEntry.ToString(),
        //                        "",
        //                        "",
        //                        "",
        //                        "").GetResultDataTable());
        //                foreach (var addRow in a1.ListPurchaseRequest)
        //                {
        //                    addRow.Lines = QueryData.ConvertDataTable<PurchaseRequestLine>(new GetRecordByDataTable(
        //                        GetRecordByDataTable.StoreType.Add,
        //                        "ADVANCEPAYMENTROW",
        //                        addRow.DocEntry,
        //                        "",
        //                        "",
        //                        "",
        //                        "").GetResultDataTable());
        //                }
        //            }
        //        }
        //        var oCom = SAP_Driver_oCompany._CheckingStatusOCompany();
        //        oCom.StartTransaction();
        //        foreach (var data in ConbookingData)
        //        {
        //            var a = ConfirmBookingSheets._Add(data, SAP_Driver_oCompany._CheckingStatusOCompany());
        //            if (a.ErroreCode != 0)
        //            {
        //                oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
        //                break;
        //            }
        //        }
        //        oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
        //    }catch(Exception ex)
        //    {
        //        var z21 = ex.Message;
        //    }
        //    return Ok("Hello Okay");
        //}
        [HttpPut]
        public async Task<IActionResult> OnUpdateConfirmBookingSheetAsync(PutConfirmBookingSheetRequest putBookingSheetRequest)
        {
            var a = await confirmBookingSheet.PutConfirmBookingSheet(putBookingSheetRequest);
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
        public async Task<IActionResult> OnCancelBookingSheetAsync(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries)
        {
            var a = await confirmBookingSheet.CancelConfirmBookingSheetAsync(docNum, bookingDocEntry, projectManagementEntry, projectSeries);
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
