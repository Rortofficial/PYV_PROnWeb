using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.ViewComponents.AdvancePayment
{
    public class AdvancePaymentViewComponent : ViewComponent
    {
        //private readonly IPettyCash pettyCash;
        private readonly IPurchaseRequest purchaseRequest;
        public AdvancePaymentViewComponent(/*IPettyCash pettyCash,*/ IPurchaseRequest purchaseRequest)
        {
            //this.pettyCash = pettyCash;
            this.purchaseRequest = purchaseRequest;
        }
        public async Task<IViewComponentResult> InvokeAsync(string lsParam, string listPRID
                                                            , string listAdvanceID, string AdvanceID
                                                            , string listPRTemplate, string listADtemplate
                                                            , string actionType
                                                            , string listObjectAdvancePayment)
        {
            if (actionType == "Add")
            {
                ViewBag.listAdvance = lsParam;
                ViewBag.listPRID = listPRID;
                ViewBag.listAdvanceID = listAdvanceID;
                ViewBag.AdvanceID = AdvanceID;
                ViewBag.listPRTemplate = listPRTemplate;
                ViewBag.listADtemplate = listADtemplate;
                //return View(new ListMasterPettyCash
                //{
                //    GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesJournalEntryNumber")).Data,
                //    GetBPCodeJournalVouchers = (await pettyCash.GetBPCodeJournalVoucherResponseAsync()).Data,
                //    GetAccountCodeJournalVouchers = (await pettyCash.GetAccountCodeJournalVoucherResponseAsync()).Data,
                //    GetVatGroupJournalVouchers = (await pettyCash.GetVatGroupJournalVoucherResponseAsync()).Data,
                //});
                return View(new ListMasterPurchaseRequest
                {
                    GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                    ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                    ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("AdvanceForCustomer", HttpContext.Request.Cookies["Department"].ToString())).Data,
                    //GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                    GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("Advance")).Data,
                });
            }
            else
            {
                ListPurchaseRequestConfirm obj = new ListPurchaseRequestConfirm();
                obj = JsonConvert.DeserializeObject<ListPurchaseRequestConfirm>(listObjectAdvancePayment);
                ViewBag.listAdvance = obj.ListPRParam.ListAD;
                ViewBag.listPRID = obj.ListPRParam.ListPRID;
                ViewBag.listAdvanceID = obj.ListPRParam.ListAdvanceID;
                ViewBag.AdvanceID = obj.ListPRParam.ListAdvanceID;
                ViewBag.listPRTemplate = obj.ListPRParam.ListPRTemplate;
                ViewBag.listADtemplate = obj.ListPRParam.ListADTemplate;
                ViewBag.ID = obj.ID;
                //return View("EditAdvancePayment", new ListMasterPettyCash
                //{
                //    GetSeriesJournalVouchers = (await pettyCash.GetSeriesJournalVoucherResponseAsync("GetSeriesJournalEntryNumber")).Data,
                //    GetBPCodeJournalVouchers = (await pettyCash.GetBPCodeJournalVoucherResponseAsync()).Data,
                //    GetAccountCodeJournalVouchers = (await pettyCash.GetAccountCodeJournalVoucherResponseAsync()).Data,
                //    GetVatGroupJournalVouchers = (await pettyCash.GetVatGroupJournalVoucherResponseAsync()).Data,
                //    ListAdvancePaymentConfirms = obj,
                //});
                return View("EditAdvancePayment", new ListMasterPurchaseRequest
                {
                    GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                    ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                    ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("AdvanceForCustomer", HttpContext.Request.Cookies["Department"].ToString())).Data,
                    //GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                    GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("Advance")).Data,
                    ListPurchaseRequestConfirms = obj,
                });
            }
        }
    }
}
