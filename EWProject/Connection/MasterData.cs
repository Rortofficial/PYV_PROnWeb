using Client.Models.Response;
using Client.Repository;

namespace Client.Connection
{
    public class MasterData
    {
        public static ListMasterBookingSheet listMasterBookingSheet = null;
        public static async Task CallGetData(IBookingSheet bookingSheet, ISalesQuotation salesQuotation)
        {
            MasterData.listMasterBookingSheet = new ListMasterBookingSheet()
            {
                SaleEmployees = (await bookingSheet.GetSaleEmployeeResponseAsync()).Data,
                GetRoutes = (await bookingSheet.GetRouteResponseAsync()).Data,
                GetImportTypes = (await bookingSheet.GetImportTypeResponseAsync()).Data,
                GetOrigins = (await bookingSheet.GetOriginResponseAsync()).Data,
                GetDestinations = (await bookingSheet.GetDestinationResponseAsync()).Data,
                GetShippers = (await bookingSheet.GetShipperResponseAsync()).Data,
                GetCOs = (await bookingSheet.GetCOResponseAsync()).Data,
                GetConsignees = (await bookingSheet.GetConsigneeResponseAsync()).Data,
                GetThaiTruckers = (await bookingSheet.GetThaiTruckerResponseAsync()).Data,
                GetOverseaTruckers = (await bookingSheet.GetOverseaTruckerResponseAsync()).Data,
                GetLOLOYARDORUNLOADINGs = (await bookingSheet.GetLOLOYARDORUNLOADINGResponseAsync()).Data,
                GetThaiForwarders = (await bookingSheet.GetThaiForwarderResponseAsync()).Data,
                GetOverseaForwarders = (await bookingSheet.GetOverseaForwarderResponseAsync()).Data,
                GetThaiBorders = (await bookingSheet.GetThaiBorderResponseAsync()).Data,
                GetLCLORFCLs = (await bookingSheet.GetLCLORFCLResponseAsync()).Data,
                GetCYORCFSs = (await bookingSheet.GetCYORCFSResponseAsync()).Data,
                GetContainerTypes = (await bookingSheet.GetContainerTypeResponseAsync()).Data,
                GetPlaceOfLoadings = (await bookingSheet.GetPlaceOfLoadingResponseAsync()).Data,
                GetPlaceOfDeliveries = (await bookingSheet.GetPlaceOfDeliveryResponseAsync()).Data,
                GetVolumes = (await bookingSheet.GetVolumeResponseAsync()).Data,
                GetDocNumBookingSheets = (await bookingSheet.GetDocNumBookingSheetResponseAsync()).Data,
                GetServiceTypes = (await bookingSheet.GetServiceTypeResponseAsync()).Data,
                GetTruckTypes = (await bookingSheet.GetTruckTypeResponseAsync()).Data,
                //GetListSaleQuotations = (await salesQuotation.GetListSaleQuotationResponse("1999-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "ALL")).Data,
                GetListSaleQuotations = (await salesQuotation.GetListSaleQuotationBookingResponse()).Data,
                GetDistricts = (await bookingSheet.GetDistrictsResponseAsync()).Data,
            };
        }
    }
}
