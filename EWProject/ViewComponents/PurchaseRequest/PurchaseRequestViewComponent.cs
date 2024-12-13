using Client.Models.Request;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.ViewComponents.PurchaseRequest
{
    public class PurchaseRequestViewComponent : ViewComponent
    {
        //ViewPurchaseRequestViewComponent
        private readonly IPurchaseRequest purchaseRequest;
        private readonly IConfiguration configuration;

        public PurchaseRequestViewComponent(IPurchaseRequest purchaseRequest, IConfiguration configuration)
        {
            this.purchaseRequest = purchaseRequest;
            this.configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string lsParam, string listPRID
                                                            , string listAdvanceID, string PRID
                                                            , string listPRTemplate, string listADtemplate
                                                            , string listidvendor, string itemCosting
                                                            , string actionType
                                                            , string listObjectPurchaseRequest)
        {
            if (actionType == "Add")
            {
                ViewBag.listPR = lsParam;
                ViewBag.listPRID = listPRID;
                ViewBag.listAdvanceID = listAdvanceID;
                ViewBag.PRID = PRID;
                ViewBag.listPRTemplate = listPRTemplate;
                ViewBag.listADtemplate = listADtemplate;
                ViewBag.listIDVendorCode = listidvendor;
                ViewBag.itemCode = configuration.GetSection("ItemCodeTransport").Value;
                ViewBag.itemCosting = itemCosting;
                return View(new ListMasterPurchaseRequest
                {
                    GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                    ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                    ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("PRCOSGConfirmBooking", HttpContext.Request.Cookies["Department"].ToString())).Data,
                    //GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                    GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
                    GetTaxCodes = (await purchaseRequest.GetTaxCodes()).Data,
                    GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data,
                    GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("CONFIRMBOOKINGSHEET")).Data,
                });
            }
            else
            {
                ListPurchaseRequestConfirm obj = new ListPurchaseRequestConfirm();
                obj = JsonConvert.DeserializeObject<ListPurchaseRequestConfirm>(listObjectPurchaseRequest);
                ViewBag.listPR = obj.ListPRParam.ListPR;
                ViewBag.listPRID = obj.ListPRParam.ListPRID;
                ViewBag.listAdvanceID = obj.ListPRParam.ListAdvanceID;
                ViewBag.PRID = obj.ListPRParam.PRID;
                ViewBag.listPRTemplate = obj.ListPRParam.ListPRTemplate;
                ViewBag.listADtemplate = obj.ListPRParam.ListADTemplate;
                ViewBag.ID = obj.ID;
                return View("EditPurchaseRequest", new ListMasterPurchaseRequest
                {
                    GetRefNoPurchaseRequest = (await purchaseRequest.GetRefNoPurchaseRequestResponsesAsync()).Data,
                    ListGetAccountCodePurchaseRequest = (await purchaseRequest.GetAccountCodePurchaseRequestResponsesAsync()).Data,
                    ListGetItemCodePurchaseRequest = (await purchaseRequest.GetItemCodePurchaseRequestResponsesAsync("PRCOSGConfirmBooking", HttpContext.Request.Cookies["Department"].ToString())).Data,
                    //GetProjectManagmentLists = (await purchaseRequest.GetProjectManagmentListResponseAsync()).Data,
                    GetVendorLists = (await purchaseRequest.GetVendorResponseAsync("CONFIRMBOOKINGSHEET")).Data,
                    GetDistributionRuless = (await purchaseRequest.GetDistributionRulesResponseAsync()).Data,
                    GetTaxCodes = (await purchaseRequest.GetTaxCodes()).Data,
                    GetPaymentTerms = (await purchaseRequest.GetPaymentTerms()).Data,
                    ListPurchaseRequestConfirms = obj
                });
            }
        }

    }
}
