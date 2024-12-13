using Client.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.AdvancePayment
{
    public class ListAdvancePaymentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<ListPurchaseRequestConfirm> listAdvancePaymentConfirms)
        {
            return View("Views/Shared/Components/AdvancePayment/ListAdvancePayment.cshtml", listAdvancePaymentConfirms.OrderBy(x => x.ID).ToList());
        }
    }
}
