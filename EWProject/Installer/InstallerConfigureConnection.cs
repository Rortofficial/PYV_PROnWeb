using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Permission;

namespace Client.Installer
{
    public class InstallerConfigureConnection : IInstaller
    {
        public void Installer(IServiceCollection servieCollection, IConfiguration configuration)
        {
            #region Connection String
            ConnectionString.DbServerType = configuration.GetSection("DbServerType").Value;
            ConnectionString.Server = configuration.GetSection("Server").Value;
            ConnectionString.LicenseServer = configuration.GetSection("LicenseServer").Value;
            ConnectionString.SLDServer = configuration.GetSection("SLDServer").Value;
            ConnectionString.DbUserName = configuration.GetSection("DbUserName").Value;
            ConnectionString.DbPassword = configuration.GetSection("DbPassword").Value;
            ConnectionString.CompanyDB = configuration.GetSection("CompanyDB").Value;
            ConnectionString.UserName = configuration.GetSection("UserNameSAP").Value;
            ConnectionString.Password = configuration.GetSection("Password").Value;
            ConnectionString.SqlConnectionString = configuration.GetSection("ConnectionString").Value.ToString();
            ConnectionString.SqlConnectionSap = configuration.GetSection("ConnectionStringSAP").Value.ToString();
            ConnectionString.DBAddOn = configuration.GetSection("TmpDatabase").Value.ToString();
            Configure.Transaction = configuration.GetSection("StoreTransaction").Value.ToString();
            Configure.TransactionSeaAir = configuration.GetSection("StoreTransactionSeaAir").Value.ToString();
            Configure.AddTransaction = configuration.GetSection("StoreAddTransaction").Value.ToString();
            Configure.Layout = configuration.GetSection("StoreLayout").Value.ToString();
            Configure.Config = configuration.GetSection("StoreConfig").Value.ToString();
            Configure.LayoutPrintType = configuration.GetSection("ConfigureStoreType").Value.ToString();
            Configure.SalesQuotationLayoutPrinter = configuration.GetSection("SaleQuotationLayoutPrint").Value.ToString();
            Configure.BookingSheetLayoutPrinter = configuration.GetSection("BookingSheetLayoutPrint").Value.ToString();
            Configure.BookingSheetSeaAirLayoutPrinter = configuration.GetSection("BookingSheetSeaAirLayoutPrint").Value.ToString();
            Configure.ConfirmBookingSheetLayoutPrinter = configuration.GetSection("ConfirmBookingSheetLayoutPrint").Value.ToString();
            Configure.ConfirmBookingSheetSeaAndAirLayoutPrinter = configuration.GetSection("ConfirmBookingSheetSeaAndAirLayoutPrint").Value.ToString();
            Configure.JobSheetTruckingLayoutPrinter = configuration.GetSection("JobSheetTruckingLayoutPrint").Value.ToString();
            Configure.JobSheetTruckingLayoutSeaAirPrinter = configuration.GetSection("JobSheetTruckingSeaAirLayoutPrint").Value.ToString();
            Configure.TruckWayBillLayoutPrinter = configuration.GetSection("TruckWayBillLayoutPrint").Value.ToString();
            Configure.AdvancePaymentLayoutPrinter = configuration.GetSection("AdvancePaymentLayoutPrint").Value.ToString();
            Configure.PurchaseRequestLayoutPrinter = configuration.GetSection("PurchaseRequestLayoutPrint").Value.ToString();
            Configure.PurchaseRequestCommissionLayoutPrint = configuration.GetSection("PurchaseRequestCommissionLayoutPrint").Value.ToString();
            Configure.PettyCashLayoutPrint = configuration.GetSection("PettyCashLayoutPrint").Value.ToString();
            Configure.CreditNoteCBT = configuration.GetSection("CreditNoteCBTLayoutPrint").Value.ToString();
            Configure.DebitNoteCBT = configuration.GetSection("DebitNoteCBTLayoutPrint").Value.ToString();
            Configure.CreditNoteSA = configuration.GetSection("CreditNoteSALayoutPrint").Value.ToString();
            Configure.DebitNoteSA = configuration.GetSection("DebitNoteSALayoutPrint").Value.ToString();
            Configure.PRSettlement = configuration.GetSection("PRSettlementLayoutPrint").Value.ToString();
            Configure.PrefixLead = Convert.ToInt32(configuration.GetSection("PrefixLead").Value);
            Configure.CustomerGroup = Convert.ToInt32(configuration.GetSection("CustomerGroup").Value);
            Configure.DutyTaxCode = configuration.GetSection("DutyTaxCode").Value;
            Configure.ItemGroup = Convert.ToInt32(configuration.GetSection("ItemGroup").Value);
            Configure.GetObjectTypes = configuration.GetSection("ObjectType").Get<List<GetObjectType>>();
            Configure.ListOfPermissions = configuration.GetSection("Permission").Get<List<Permission>>();
            Configure.FileUploadPath = configuration.GetSection("FileUploadPath").Value;
            SAP_Driver_oCompany.Init_oCompany();
            Configure.ListOfPermissionsByUser = new List<PropertiesPermission>();
            //Configure.GetAllPermission("-99");
            #endregion
        }
    }
}
