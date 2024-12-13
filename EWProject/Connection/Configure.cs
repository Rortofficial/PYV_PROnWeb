using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Permission;

namespace Client.Connection
{
    public class Configure
    {
        public static string Transaction { get; set; }
        public static string TransactionSeaAir { get; set; }
        public static string AddTransaction { get; set; }
        public static string Layout { get; set; }
        public static string Config { get; set; }
        public static string LayoutPrintType { get; set; }
        public static string SalesQuotationLayoutPrinter { get; set; }
        public static string BookingSheetLayoutPrinter { get; set; }
        public static string BookingSheetSeaAirLayoutPrinter { get; set; }
        public static string ConfirmBookingSheetLayoutPrinter { get; set; }
        public static string ConfirmBookingSheetSeaAndAirLayoutPrinter { get; set; }
        public static string JobSheetTruckingLayoutPrinter { get; set; }
        public static string JobSheetTruckingLayoutSeaAirPrinter { get; set; }
        public static string TruckWayBillLayoutPrinter { get; set; }
        public static string AdvancePaymentLayoutPrinter { get; set; }
        public static string PurchaseRequestLayoutPrinter { get; set; }
        public static string PurchaseRequestCommissionLayoutPrint { get; set; }
        public static string PRSettlement { get; set; }
        public static string CreditNoteCBT { get; set; }
        public static string DebitNoteCBT { get; set; }
        public static string CreditNoteSA { get; set; }
        public static string DebitNoteSA { get; set; }
        public static string PettyCashLayoutPrint { get; set; }
        public static int PrefixLead { get; set; }
        public static int CustomerGroup { get; set; }
        public static string DutyTaxCode { get; set; }
        public static int ItemGroup { get; set; }
        public static List<GetObjectType> GetObjectTypes { get; set; }
        public static string FileUploadPath { get; set; }
        public static List<Permission> ListOfPermissions { get; set; }
        public static List<PropertiesPermission> ListOfPermissionsByUser;
        public static List<PropertiesPermission> ListOfPermissionsAll;
        public static Task GetAllPermission(string userID)
        {
            try
            {
                //ListOfPermissionsAll = new List<PropertiesPermission>();
                ListOfPermissionsByUser = new List<PropertiesPermission>();
                ListOfPermissionsByUser = QueryData.ConvertDataTable<PropertiesPermission>(new GetRecordByDataTable(
                    GetRecordByDataTable.StoreType.Config,
                    "GETPERMISSIONBYUSER",
                    userID,
                    "",
                    "",
                    "",
                    "").GetResultDataTable());
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var errorCode = ex.GetHashCode;
                return Task.CompletedTask;
            }
        }
    }
}
