using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize]
    public class JobSheetTruckingController : Controller
    {
        private readonly IJobSheetTrucking jobSheetTrucking;
        private readonly IWebHostEnvironment webHostEnvironment;

        public JobSheetTruckingController(IJobSheetTrucking jobSheetTrucking, IWebHostEnvironment webHostEnvironment)
        {
            this.jobSheetTrucking = jobSheetTrucking;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int ConfirmBooking, int UserID)
        {
            ViewBag.confirmBookingID = ConfirmBooking;
            return View(new ListMasterJobSheetTrucking
            {
                GetDetailJobSheetTruckings = (await jobSheetTrucking.GetConfirmBookingSheetDetailByUserResponseAsync(ConfirmBooking)).Data,
                GetItemDetailByItemTypes = (await jobSheetTrucking.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetSaleQuotationJobSheets = (await jobSheetTrucking.GetSaleQuotationJobSheetResponse(ConfirmBooking, UserID)).Data
            });
        }
        public async Task<IActionResult> EditJobSheetTruckingDraft(int ConfirmBooking, int jobSheetDocEntry, int UserID)
        {
            ViewBag.JobSheetDocEntry = jobSheetDocEntry;
            ViewBag.confirmBookingID = ConfirmBooking;
            return View(new ListMasterJobSheetTrucking
            {
                GetDetailJobSheetTruckings = (await jobSheetTrucking.GetConfirmBookingSheetDetailByUserResponseAsync(ConfirmBooking)).Data,
                GetItemDetailByItemTypes = (await jobSheetTrucking.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
                GetSaleQuotationJobSheets = (await jobSheetTrucking.GetSaleQuotationJobSheetResponse(ConfirmBooking, UserID)).Data,
                GetJobSheetTruckingEditDrafts = (await jobSheetTrucking.GetJobSheetTruckingEditDraftByDocEntry(jobSheetDocEntry)).Data,
                GetUomGroups = (await jobSheetTrucking.GetUomGroups("")).Data,
            });
        }
        public async Task<IActionResult> ListConfirmBookingSheet()
        {
            return View(await jobSheetTrucking.GetConfirmBookingSheetByUserResponse());
        }
        public async Task<IActionResult> ListJobSheetTrucking()
        {
            return View(await jobSheetTrucking.GetListJobSheetTruckingResponses("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "DEFAULT", HttpContext.Request.Cookies["ID"]));
        }
        public async Task<IActionResult> EditJobSheetTrucking(int ConfirmBooking, int jobSheetDocEntry, int UserID)
        {
            return View(new ListMasterJobSheetTrucking
            {
                GetJobSheetTruckingEditResponses = await jobSheetTrucking.GetJobSheetTruckingEditResponsesAsync(ConfirmBooking, jobSheetDocEntry),
                GetDetailJobSheetTruckings = (await jobSheetTrucking.GetConfirmBookingSheetDetailByUserResponseAsync(ConfirmBooking)).Data,
                GetItemDetailByItemTypes = (await jobSheetTrucking.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
            });
        }
        public async Task<IActionResult> EditAttachmentJobSheetTrucking(int ConfirmBooking, int jobSheetDocEntry, int UserID)
        {
            return View(new ListMasterJobSheetTrucking
            {
                GetJobSheetTruckingEditResponses = await jobSheetTrucking.GetJobSheetTruckingEditResponsesAsync(ConfirmBooking, jobSheetDocEntry),
                GetDetailJobSheetTruckings = (await jobSheetTrucking.GetConfirmBookingSheetDetailByUserResponseAsync(ConfirmBooking)).Data,
                GetItemDetailByItemTypes = (await jobSheetTrucking.GetItemDetailByItemTypeResponseAsync(UserID.ToString(), HttpContext.Request.Cookies["Department"].ToString())).Data,
            });
        }
        public async Task<IActionResult> ViewDetailJobSheetTrucking(int docEntry)
        {
            //await confirmBookingSheet.GetDetailConfirmBookingSheetByDocEntryResponseAsync(docEntry)
            var data = await jobSheetTrucking.GetJobSheetTruckingDetailByDocEntry(docEntry);
            return View(new ListMasterJobSheetTrucking
            {
                GetDetailJobSheetTruckings = (await jobSheetTrucking.GetConfirmBookingSheetDetailByUserResponseAsync(data.Data[0].ConfirmBookingID)).Data,
                GetSaleQuotationJobSheets = (await jobSheetTrucking.GetSaleQuotationJobSheetResponse(Convert.ToInt32(data.Data[0].SaleQuotationDocNum), 1)).Data,
                GetJobSheetTruckings = data.Data,
                ListLayoutPrint = data.ListLayoutPrint,
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetListJobSheetTrucking(string dateFrom, string dateTo, string type)
        {
            var a = await jobSheetTrucking.GetListJobSheetTruckingResponses(dateFrom, dateTo, type, HttpContext.Request.Cookies["ID"]);
            if (a.ErrorCode == 0) return Ok(a); else return BadRequest(a);
        }
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
        public async Task<IActionResult> OnSubmitJobSheetTrucking(PostJobSheetTruckingRequest postJobSheetTruckingRequest, string draftdocument)
        {
            try
            {
                postJobSheetTruckingRequest.CostCenter = HttpContext.Request.Cookies["CostCenter"];
                var a = await jobSheetTrucking.PostJobSheetTrucking(postJobSheetTruckingRequest, draftdocument);
                if (a.ErrorCode == 0)
                {
                    return Ok(a);
                }
                else
                {
                    return BadRequest(a);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> OnUpdateJobSheetTrucking(GetJobSheetTruckingEdit updateJobSheetTruckingEdit)
        {
            var a = await jobSheetTrucking.PutJobSheetTrucking(updateJobSheetTruckingEdit);
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
        public async Task<IActionResult> OnUpdateAttachmentJobSheetTrucking(GetJobSheetTruckingEdit updateJobSheetTruckingEdit)
        {
            var a = await jobSheetTrucking.PutAttachmentJobSheetTrucking(updateJobSheetTruckingEdit);
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
        public async Task<IActionResult> OnCancelJobSheetTrucking(string docEntry, string docEntrySO)
        {
            var a = await jobSheetTrucking.CancelJobSheetTrucking(docEntry, docEntrySO, HttpContext.Request.Cookies["ID"]);
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
        public async Task<IActionResult> GetCurrencyByCardCode(string cardCode, string confirmBookingID)
        {
            var a = await jobSheetTrucking.GetCurrecnyByCardCode(cardCode, confirmBookingID);
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
        [HttpPut]
        public async Task<IActionResult> OnUpdateDraftJobSheetTrucking(UpdateDraftJobSheetTrucking updateJobSheetTruckingEdit, string draftdocument)
        {
            updateJobSheetTruckingEdit.CostCenter= HttpContext.Request.Cookies["CostCenter"];
            var a = await jobSheetTrucking.PutJobSheetTruckingDraft(updateJobSheetTruckingEdit, draftdocument);
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
