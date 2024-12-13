using Client.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.AdvancePayment
{
    //[ViewComponent(Name = "ViewAdvancePayment")]
    public class ViewAdvancePaymentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ListPurchaseRequestConfirm listAdvancePaymentConfirms)
        {
            return View("Views/Shared/Components/AdvancePayment/ViewAdvancePayment.cshtml", listAdvancePaymentConfirms);
        }
    }
}
