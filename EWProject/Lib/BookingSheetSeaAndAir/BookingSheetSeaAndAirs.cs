using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheetSeaAndAir
{
    public class BookingSheetSeaAndAirs
    {
        public static Add _Add(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir, Company _company) => new Add(addBookingSheetSeaAndAir, _company);
        public static Update _Update(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir, Company _company) => new Update(addBookingSheetSeaAndAir, _company);
        public static UpdateCommodities _UpdateCommodities(string CreateUser, int docEntry, List<CommoditySeaAndAir> commodityRequest, HeaderCommodityUpdate headerCommodityUpdate, List<DestAgentUpdateCommodities> listDestAgent, List<ShipperUpdateCommodities> listShipper, List<ConsigneeUpdateCommodities> listConsignee, List<CustomerUpdateCommodities> listCustomer, Company _company) => new UpdateCommodities(CreateUser, docEntry, commodityRequest, headerCommodityUpdate,listDestAgent,listShipper,listConsignee,listCustomer, _company);
        public static Cancel _Cancel(string docNum, Company _company) => new Cancel(docNum, _company);
        public static Remove _Remove(string docNum, Company _company) => new Remove(docNum, _company);
    }
}
