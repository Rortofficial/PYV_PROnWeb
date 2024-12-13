using Client.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.PurchaseRequest
{
    public class ViewPurchaseRequestViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ListPurchaseRequestConfirm listPurchaseRequestConfirms)
        {
            return View("Views/Shared/Components/PurchaseRequest/ViewPurchaseRequest.cshtml", listPurchaseRequestConfirms);
        }
    }
}
