using Client.Models.Response;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Client.ViewComponents.Container
{
    public class ContainerEditViewComponent : ViewComponent
    {
        private readonly IConfirmBookingSheet confirmBookingSheet;

        public ContainerEditViewComponent(IConfirmBookingSheet confirmBookingSheet)
        {
            this.confirmBookingSheet = confirmBookingSheet;
        }
        public async Task<IViewComponentResult> InvokeAsync(string BookingID, int docEntry /* List<GetEditConfirmBookingSheet> GetEditConfirmBookingSheets*/)
        {
            return View("Views/Shared/Components/ContainerInfo/ContainerEdit.cshtml", new ListMasterConfirmBookingSheet //../../Views/Shared/Components/ContainerInfo/ContainerEdit.cshtml
            {
                getVendors = (await confirmBookingSheet.GetVendorResponseAsync()).Data,
                getEmployees = (await confirmBookingSheet.GetEmployeeResponseAsync()).Data,
                getTruckProvinces = (await confirmBookingSheet.GetTruckProvinceResponseAsync()).Data,
                getTruckNos = (await confirmBookingSheet.GetTruckNoResponseAsync("")).Data,
                getTrailerNos = (await confirmBookingSheet.GetTrailerNoResponseAsync()).Data,
                getContainers = (await confirmBookingSheet.GetContainerResponseAsync(BookingID)).Data,
                getDetailBookingSheetByUsers = (await confirmBookingSheet.GetEditDetailBookingSheetByUserResponseAsync(Convert.ToInt32(BookingID))).Data,
                GetEditConfirmBookingSheets = (await confirmBookingSheet.GetEditConfirmBookingSheetByDocEntryResponseAsync(docEntry)).Data,//GetEditConfirmBookingSheets,
            });
        }
    }
}
