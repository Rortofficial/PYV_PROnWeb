using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class JobSheetTruckingSeaAndAirController : Controller
    {
        #region Data Member
        private readonly IJobSheetTrucking jobSheetTrucking;
        private readonly IJobSheetTruckingSeaAndAir jobSheetTruckingSeaAndAir;
        private readonly IWebHostEnvironment webHostEnvironment;
        #endregion
        #region Constructor
        public JobSheetTruckingSeaAndAirController(IJobSheetTrucking jobSheetTrucking, IJobSheetTruckingSeaAndAir jobSheetTruckingSeaAndAir, IWebHostEnvironment webHostEnvironment)
        {
            this.jobSheetTrucking = jobSheetTrucking;
            this.jobSheetTruckingSeaAndAir = jobSheetTruckingSeaAndAir;
            this.webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region View
        public async Task<IActionResult> Index(int ConfirmBooking, int UserID)
        {
            return View(new ListMasterJobSheetTrucking
            {
                GetDetailJobSheetTruckings = (await jobSheetTruckingSeaAndAir.GetConfirmBookingSheetDetailByUserResponseAsync(ConfirmBooking)).Data,
                GetItemDetailByItemTypes = (await jobSheetTruckingSeaAndAir.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetVendorByConfirmJobSheetSeaAirs = (await jobSheetTruckingSeaAndAir.GetVendorByConfirmJobSheetSeaAirResponseAsync(ConfirmBooking)).Data,
                //GetSaleQuotationJobSheets = (await jobSheetTrucking.GetSaleQuotationJobSheetResponse(ConfirmBooking, UserID)).Data
            });
        }
        public async Task<IActionResult> ListConfirmBookingSheet()
        {
            return View(await jobSheetTruckingSeaAndAir.GetConfirmBookingSheetByUserResponse());
        }
        public async Task<IActionResult> ListJobSheetTrucking()
        {
            return View(await jobSheetTruckingSeaAndAir.GetListJobSheetTruckingResponses("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        public async Task<IActionResult> EditJobSheetTruckingDraftAsync(int ConfirmBooking, int jobSheetDocEntry, int UserID)
        {
            var data = await jobSheetTruckingSeaAndAir.GetJobSheetTruckingDetailByDocEntry(jobSheetDocEntry);
            ViewBag.DocEntry = jobSheetDocEntry;
            return View(new ListMasterJobSheetTruckingSeaAir
            {
                GetJobSheetTruckings = data.Data,
                GetDetailJobSheetTruckings = (await jobSheetTruckingSeaAndAir.GetConfirmBookingSheetDetailByUserResponseAsync(ConfirmBooking)).Data,
                GetItemDetailByItemTypes = (await jobSheetTruckingSeaAndAir.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetVendorByConfirmJobSheetSeaAirs = (await jobSheetTruckingSeaAndAir.GetVendorByConfirmJobSheetSeaAirResponseAsync(ConfirmBooking)).Data,
            });
        }
        public async Task<IActionResult> EditJobSheetTrucking(int ConfirmBooking, int jobSheetDocEntry, int UserID)
        {
            var data = await jobSheetTruckingSeaAndAir.GetJobSheetTruckingDetailByDocEntry(jobSheetDocEntry);
            ViewBag.DocEntry = jobSheetDocEntry;
            return View(new ListMasterJobSheetTruckingSeaAir
            {
                GetDetailJobSheetTruckings = (await jobSheetTruckingSeaAndAir.GetConfirmBookingSheetDetailByUserResponseAsync(Convert.ToInt32(data.Data.ObjectHeader.CONFIRMBOOKINGID))).Data,
                //GetSaleQuotationJobSheets = (await jobSheetTrucking.GetSaleQuotationJobSheetResponse(Convert.ToInt32(data.Data[0].SaleQuotationDocNum), 1)).Data,
                GetItemDetailByItemTypes = (await jobSheetTruckingSeaAndAir.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetJobSheetTruckings = data.Data,
                ListLayoutPrint = data.ListLayoutPrint,
                GetVendorByConfirmJobSheetSeaAirs = (await jobSheetTruckingSeaAndAir.GetVendorByConfirmJobSheetSeaAirResponseAsync(ConfirmBooking)).Data,
            });
        }
        public async Task<IActionResult> ViewDetailJobSheetTrucking(int docEntry)
        {
            //await confirmBookingSheet.GetDetailConfirmBookingSheetByDocEntryResponseAsync(docEntry)
            var data = await jobSheetTruckingSeaAndAir.GetJobSheetTruckingDetailByDocEntry(docEntry);
            ViewBag.DocEntry = docEntry;
            return View(new ListMasterJobSheetTruckingSeaAir
            {
                GetDetailJobSheetTruckings = (await jobSheetTruckingSeaAndAir.GetConfirmBookingSheetDetailByUserResponseAsync(Convert.ToInt32(data.Data.ObjectHeader.CONFIRMBOOKINGID))).Data,
                //GetSaleQuotationJobSheets = (await jobSheetTrucking.GetSaleQuotationJobSheetResponse(Convert.ToInt32(data.Data[0].SaleQuotationDocNum), 1)).Data,
                GetJobSheetTruckings = data.Data,
                ListLayoutPrint = data.ListLayoutPrint,
            });
        }
        #endregion
        #region Get
        [HttpGet]
        public async Task<IActionResult> GetListJobSheetTrucking(string dateFrom, string dateTo, string type)
        {
            var a = await jobSheetTruckingSeaAndAir.GetListJobSheetTruckingResponses(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrencyByCardCode(string cardCode)
        {
            var a = await jobSheetTruckingSeaAndAir.GetCurrecnyByCardCode(cardCode);
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
        public async Task<IActionResult> GetUomCode(string UomCode)
        {
            var a = await jobSheetTrucking.GetUomGroups(UomCode);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> OnExportDataOfJobSheetTruckingAsync(string getBookingSheetExports)
        {
            List<GetListJobSheetTrucking> obj = new List<GetListJobSheetTrucking>();
            obj = JsonConvert.DeserializeObject<List<GetListJobSheetTrucking>>(getBookingSheetExports);
            var a = await ExportDataFromList.ExportExcel(obj, webHostEnvironment, "JobSheetTrucking", "ListJobSheetTrucking.rdl", ExportDataFromList.AppicationType.Excel, "ListJobSheetTrucking" + DateTime.Now.ToString("dd-MM-yyyy_h:mm"));
            return Ok(a);
        }
        public IActionResult OnPostMyUploader(List<IFormFile> MyUploader)
        {
            try
            {
                if (MyUploader != null)
                {
                    var listName = new List<Models.Gets.Attachments>();
                    string uploadsFolderTmp = Path.Combine(webHostEnvironment.WebRootPath, "mediaUpload");
                    string uploadsFolder = Path.Combine(Configure.FileUploadPath);
                    string filePath;
                    string filePathTmp;
                    dynamic fileStream;
                    foreach (var MyUploaders in MyUploader)
                    {
                        filePath = Path.Combine(uploadsFolder, MyUploaders.FileName);
                        filePathTmp = Path.Combine(uploadsFolderTmp, MyUploaders.FileName);
                        listName.Add(new Models.Gets.Attachments
                        {
                            ATTACHMENTNAME = MyUploaders.FileName,
                        });
                        using (fileStream = new FileStream(filePath, FileMode.Create)) MyUploaders.CopyTo(fileStream);
                        using (fileStream = new FileStream(filePathTmp, FileMode.Create)) MyUploaders.CopyTo(fileStream);
                    }
                    return Ok(listName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(null);
        }
        [HttpPost]
        public async Task<IActionResult> OnSubmitJobSheetTrucking(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingRequest, string draftdocument)
        {
            var a = await jobSheetTruckingSeaAndAir.PostJobSheetTrucking(postJobSheetTruckingRequest,draftdocument, HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        #endregion
        #region Put
        [HttpPut]
        public async Task<IActionResult> OnUpdateJobSheetTrucking(PostJobSheetTruckingSeaAirRequest updateJobSheetTruckingEdit, string jobDocEntry)
        {
            var a = await jobSheetTruckingSeaAndAir.PutJobSheetTrucking(updateJobSheetTruckingEdit, jobDocEntry, HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPut]
        public async Task<IActionResult> OnUpdateDraftJobSheetTrucking(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingRequest, string draftdocument, string jobSheetDocEntry)
        {
            var a = await jobSheetTruckingSeaAndAir.PutDraftJobSheetTrucking(postJobSheetTruckingRequest, draftdocument, jobSheetDocEntry, HttpContext.Request.Cookies["CostCenter"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        #endregion
        #region Delete
        [HttpDelete]
        public async Task<IActionResult> OnCancelJobSheetTruckingAsync(string jobDocEntry,int docEntrySO)
        {
            var a = await jobSheetTruckingSeaAndAir.DeleteJobSheetTrucking(jobDocEntry,docEntrySO, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
        #endregion
        #region Partial View
        public async Task<IActionResult> PartialJobSheetTruckingSeaAirDetail(int ConfirmBooking, int jobSheetDocEntry, int UserID)
        {
            var data = await jobSheetTruckingSeaAndAir.GetJobSheetTruckingDetailByDocEntry(jobSheetDocEntry);
            return PartialView("_PartialJobSheetTruckingSeaAirDetail", new ListMasterJobSheetTruckingSeaAir
            {
                GetDetailJobSheetTruckings = (await jobSheetTruckingSeaAndAir.GetConfirmBookingSheetDetailByUserResponseAsync(ConfirmBooking)).Data,
                GetItemDetailByItemTypes = (await jobSheetTruckingSeaAndAir.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetVendorByConfirmJobSheetSeaAirs = (await jobSheetTruckingSeaAndAir.GetVendorByConfirmJobSheetSeaAirResponseAsync(ConfirmBooking)).Data,
                GetJobSheetTruckings = data.Data,
                ListLayoutPrint = data.ListLayoutPrint
            });
        }
        #endregion
    }
}
