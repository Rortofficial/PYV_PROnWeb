using Client.Connection;
using SAPbobsCOM;
using System.Data.Odbc;

namespace Client.Lib.OtherFunction
{
    public static class SAP_Driver_oCompany
    {
        public static void Init_oCompany()
        {
            ConnectionString.oCompany = new SapConnection().Company;
            ConnectionString.OdbcHanaConnection = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana).CN;
        }
        public static Company _CheckingStatusOCompany() => (ConnectionString.oCompany.Connected) ? ConnectionString.oCompany : ConnectionString.oCompany = new SapConnection().Company;
        public static OdbcConnection _CheckingStatusConnectionHana() => (ConnectionString.OdbcHanaConnection.State == System.Data.ConnectionState.Open) ? ConnectionString.OdbcHanaConnection : ConnectionString.OdbcHanaConnection = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana).CN;
    }
}
