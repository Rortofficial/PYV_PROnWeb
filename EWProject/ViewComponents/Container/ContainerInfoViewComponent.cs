using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.Container
{
    public class ContainerInfoViewComponent : ViewComponent
    {
        private readonly IConfirmBookingSheet confirmBookingSheet;

        public ContainerInfoViewComponent(IConfirmBookingSheet confirmBookingSheet)
        {
            this.confirmBookingSheet = confirmBookingSheet;
        }

        public async Task<IViewComponentResult> InvokeAsync(string BookingID)
        {
            return View("ContainerAdd", new ListMasterConfirmBookingSheet
            {
                getVendors = (await confirmBookingSheet.GetVendorResponseAsync()).Data,
                getEmployees = (await confirmBookingSheet.GetEmployeeResponseAsync()).Data,
                getTruckProvinces = (await confirmBookingSheet.GetTruckProvinceResponseAsync()).Data,
                getTruckNos = (await confirmBookingSheet.GetTruckNoResponseAsync("")).Data,
                getTrailerNos = (await confirmBookingSheet.GetTrailerNoResponseAsync()).Data,
                getContainers = (await confirmBookingSheet.GetContainerResponseAsync(BookingID)).Data,
                getDetailBookingSheetByUsers = (await confirmBookingSheet.GetDetailBookingSheetByUserResponseAsync(Convert.ToInt32(BookingID))).Data,
            });
        }
    }
}
