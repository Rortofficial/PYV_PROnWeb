using SAPbobsCOM;
using System.Data;
using System.Data.Odbc;

namespace Client.Connection
{
    public class LoginOnlyDatabase
    {
        public LoginOnlyDatabase(Type type)
        {
            Login(type);
        }

        public OdbcDataAdapter AD { get; set; }

        public OdbcConnection CN { get; set; }

        public OdbcCommand CMD { get; set; }

        public string sErrMsg { get; private set; }

        public int lErrCode { get; private set; }

        public Company Company { get; internal set; }
        public enum Type
        {
            SapHana, SqlHana
        }

        private void Login(Type type)
        {
            //string Server = "";
            //string DbUserName = "";
            //string DbPassword = "";
            //string CompanyDB = "";
            //B1CRHPROXY //HDBODBC32
            //DSN=KodyazHANADb; //Driver={{B1CRHPROXY}};
            try
            {
                string connectionstr = "";

                if (type == Type.SapHana)
                    connectionstr = ConnectionString.SqlConnectionSap!;
                //$"Driver={{HDBODBC}};UID={ConnectionString.DbUserName};" +
                //            $"PWD={ConnectionString.DbPassword};SERVERNODE={ConnectionString.Server};[DATABASE={ConnectionString.CompanyDB}];";
                else if (type == Type.SqlHana)
                {
                    connectionstr = ConnectionString.SqlConnectionString!;
                    //$"Driver={{HDBODBC}};UID={ConnectionString.DbUserName};" +
                    //            $"PWD={ConnectionString.DbPassword};SERVERNODE={ConnectionString.Server};[DATABASE={ConnectionString.DBAddOn}];";
                }

                CN = new OdbcConnection(connectionstr);

                if (CN.State == ConnectionState.Closed) CN.Open();
                lErrCode = CN.State == ConnectionState.Open ? 0 : 9999;
            }
            catch (Exception ex)
            {
                lErrCode = ex.GetHashCode();
                sErrMsg = ex.Message;
            }
        }
    }
}
