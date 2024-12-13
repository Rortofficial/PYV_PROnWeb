using Client.Models.Gets;
using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.Container
{
    public class ContainerEditSeaAndAirViewComponent : ViewComponent
    {
        private readonly IConfirmBookingSheet confirmBookingSheet;
        private readonly IConfirmBookingSeaAndAir confirmBookingSeaAndAir;

        public ContainerEditSeaAndAirViewComponent(IConfirmBookingSheet confirmBookingSheet, IConfirmBookingSeaAndAir confirmBookingSeaAndAir)
        {
            this.confirmBookingSheet = confirmBookingSheet;
            this.confirmBookingSeaAndAir = confirmBookingSeaAndAir;
        }
        public async Task<IViewComponentResult> InvokeAsync(string BookingID, List<GetEditConfirmBookingSheet> GetEditConfirmBookingSheets)
        {
            return View("Views/Shared/Components/ContainerInfo/ContainerSeaAndAirEdit.cshtml", new ListMasterConfirmBookingSheet //../../Views/Shared/Components/ContainerInfo/ContainerEdit.cshtml
            {
                getVendors = (await confirmBookingSheet.GetVendorResponseAsync()).Data,
                getEmployees = (await confirmBookingSheet.GetEmployeeResponseAsync()).Data,
                getTruckProvinces = (await confirmBookingSheet.GetTruckProvinceResponseAsync()).Data,
                getTruckNos = (await confirmBookingSheet.GetTruckNoResponseAsync("")).Data,
                getTrailerNos = (await confirmBookingSheet.GetTrailerNoResponseAsync()).Data,
                getContainers = (await confirmBookingSheet.GetContainerResponseAsync(BookingID)).Data,
                getDetailBookingSheetByUsers = (await confirmBookingSeaAndAir.GetEditDetailBookingSheetByUserResponseAsync(Convert.ToInt32(BookingID))).Data,
                GetEditConfirmBookingSheets = GetEditConfirmBookingSheets,
            });
        }
    }
}
