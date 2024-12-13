using Client.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.PurchaseRequest
{
    public class ListPurchaseRequestViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<ListPurchaseRequestConfirm> listPurchaseRequestConfirms)
        {
            return View("Views/Shared/Components/PurchaseRequest/ListPurchaseRequest.cshtml", listPurchaseRequestConfirms.OrderBy(x => x.ID).ToList());
        }
    }
}
