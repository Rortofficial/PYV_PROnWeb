using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.ConfirmBookingSheetSeaAndAir
{
    public class ConfirmBookingSheetSeaAndAirs
    {
        public static Add _Add(PostConfirmBookingSheetRequest postConfirmBookingSheetRequest, Company oCompany) => new Add(postConfirmBookingSheetRequest, oCompany);
        public static Update _Update(PutConfirmBookingSheetRequest putConfirmBookingSheetRequest, Company oCompany) => new Update(putConfirmBookingSheetRequest, oCompany);
        public static Cancel _Cancel(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries, Company oCompany) => new Cancel(docNum, bookingDocEntry, projectManagementEntry, projectSeries, oCompany);
    }
}
