using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.Container
{
    public class ContainerInfoSeaAndAirViewComponent : ViewComponent
    {
        private readonly IConfirmBookingSheet confirmBookingSheet;
        private readonly IConfirmBookingSeaAndAir confirmBookingSeaAndAir;

        public ContainerInfoSeaAndAirViewComponent(IConfirmBookingSheet confirmBookingSheet, IConfirmBookingSeaAndAir confirmBookingSeaAndAir)
        {
            this.confirmBookingSheet = confirmBookingSheet;
            this.confirmBookingSeaAndAir = confirmBookingSeaAndAir;
        }
        public async Task<IViewComponentResult> InvokeAsync(string BookingID)
        {
            return View("Views/Shared/Components/ContainerInfo/ContainerSeaAndAirAdd.cshtml", new ListMasterConfirmBookingSheet
            {
                getVendors = (await confirmBookingSheet.GetVendorResponseAsync()).Data,
                getEmployees = (await confirmBookingSheet.GetEmployeeResponseAsync()).Data,
                getTruckProvinces = (await confirmBookingSheet.GetTruckProvinceResponseAsync()).Data,
                getTruckNos = (await confirmBookingSheet.GetTruckNoResponseAsync("")).Data,
                getTrailerNos = (await confirmBookingSheet.GetTrailerNoResponseAsync()).Data,
                getContainers = (await confirmBookingSheet.GetContainerResponseAsync(BookingID)).Data,
                getDetailBookingSheetByUsers = (await confirmBookingSeaAndAir.GetDetailBookingSheetByUserResponseAsync(Convert.ToInt32(BookingID))).Data,
            });
        }
    }
}
