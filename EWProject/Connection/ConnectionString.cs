using SAPbobsCOM;
using System.Data.Odbc;

namespace Client.Connection
{
    public class ConnectionString
    {
        public static string DbServerType { get; set; } = null!;
        public static string Server { get; set; } = null!;
        public static string LicenseServer { get; set; } = null!;
        public static string SLDServer { get; set; } = null!;
        public static string DbUserName { get; set; } = null!;
        public static string DbPassword { get; set; } = null!;
        public static string CompanyDB { get; set; } = null!;
        public static string UserName { get; set; } = null!;
        public static string Password { get; set; } = null!;
        public static string SqlConnectionString { get; set; }
        public static OdbcConnection OdbcHanaConnection { get; set; }
        public static string SqlConnectionSap { get; set; }
        public static string DBAddOn { get; set; }
        public static Company oCompany { get; set; }
    }
}
