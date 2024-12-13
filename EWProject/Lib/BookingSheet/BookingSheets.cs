using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheet
{
    public class BookingSheets
    {
        public static Update _Update(PostBookingSheetRequest postBookingSheetRequest, Company _company) => new Update(postBookingSheetRequest, _company);
        public static UpdateCommodities _UpdateCommodities(string CreateUser, int docEntry, List<Commodity> commodityRequest, HeaderCommodityUpdate headerCommodityUpdate, List<OverTruckers> overseaTrucker
                                                                                , List<ThaiForwarders> thaiForwarders, List<OverForwarders> overForwarders
                                                                                , List<ThaiBorders> thaiBorders, PlaceOfLoading placeOfLoadings, PlaceOfLoading placeOfDeliveries, Company _company) => new UpdateCommodities(CreateUser, docEntry, commodityRequest, headerCommodityUpdate, overseaTrucker
                                                                                    , thaiForwarders, overForwarders, thaiBorders, placeOfLoadings, placeOfDeliveries, _company);
        public static Add _Add(PostBookingSheetRequest postBookingSheetRequest, Company _company) => new Add(postBookingSheetRequest, _company);
        public static Cancel _Cancel(string docNum, Company _company) => new Cancel(docNum, _company);
        public static Remove _Remove(string docNum, Company _company) => new Remove(docNum, _company);
    }
}
