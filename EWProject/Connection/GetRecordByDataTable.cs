using Client.Lib.OtherFunction;
using System.Data;
using System.Data.Odbc;

namespace Client.Connection
{
    public class GetRecordByDataTable
    {
        private int getErrorCode;
        private string getErrorMessage = null!;
        private DataTable getResulDataTable = new DataTable();
        public int GetErrorCode()
        {
            return getErrorCode;
        }

        public string GetErrorMessage()
        {
            return getErrorMessage;
        }

        public DataTable GetResultDataTable()
        {
            return getResulDataTable;
        }
        public enum StoreType
        {
            Transaction,
            TransactionSeaAir,
            Layout,
            Config,
            Add,
            Custom,
        }
        public GetRecordByDataTable(string Type, string par1, string par2, string par3, string par4, string par5)
        {
            callStore(StoreType.Transaction, Type, par1, par2, par3, par4, par5);
        }
        public GetRecordByDataTable(StoreType storeType = StoreType.Transaction, string Type = "", string par1 = "", string par2 = "", string par3 = "", string par4 = "", string par5 = "")
        {
            callStore(storeType, Type, par1, par2, par3, par4, par5);
        }
        private void callStore(StoreType storeType = StoreType.Transaction, string Type = "", string par1 = "", string par2 = "", string par3 = "", string par4 = "", string par5 = "")
        {
            //OdbcCommand sqlCommand = new OdbcCommand();
            OdbcDataAdapter sqlDataAdapter;
            //sqlCommand.Connection = connectionSql.CN;
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.CommandText ="_USP_CALLTRANS_EWTRANSACTION";
            //$"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_EWTRANSACTION ('{Type}','{par1}','{par2}','{par3}','{par4}','{par5}')";
            //"_USP_CALLTRANS_EWTRANSACTION";
            //$"CALL \"{ConnectionString.CompanyDB}\"._USP_CALLTRANS_EWTRANSACTION ('{Type}','{par1}','{par2}','{par3}','{par4}','{par5}')", connectionSql.CN
            //@TYPE AS NVARCHAR(MAX),
            //@par1 AS NVARCHAR(MAX),
            //@par2 AS NVARCHAR(MAX),
            //@par3 AS NVARCHAR(MAX),
            //@par4 AS NVARCHAR(MAX),
            //@par5 AS NVARCHAR(MAX)
            //sqlCommand.Parameters.AddWithValue("DTYPE", Type);
            //sqlCommand.Parameters.AddWithValue("par1", par1);
            //sqlCommand.Parameters.AddWithValue("par2", par2);
            //sqlCommand.Parameters.AddWithValue("par3", par3);
            //sqlCommand.Parameters.AddWithValue("par4", par4);
            //sqlCommand.Parameters.AddWithValue("par5", par5);
            var storeName = "";
            if (storeType == StoreType.Transaction)
            {
                storeName = Configure.Transaction;
            }
            else if (storeType == StoreType.Layout)
            {
                storeName = Type;
            }
            else if (storeType == StoreType.Config)
            {
                storeName = Configure.Config;
            }
            else if (storeType == StoreType.Add)
            {
                storeName = Configure.AddTransaction;
            }
            else if (storeType == StoreType.Custom)
            {
                sqlDataAdapter = new OdbcDataAdapter(Type, SAP_Driver_oCompany._CheckingStatusConnectionHana());
                sqlDataAdapter.Fill(getResulDataTable);
                return;
            }
            else if (storeType == StoreType.TransactionSeaAir)
            {
                storeName = Configure.TransactionSeaAir;
            }
            var test = $"CALL \"{ConnectionString.CompanyDB}\".{storeName} ('{Type}','{par1}','{par2}','{par3}','{par4}','{par5}')";
            sqlDataAdapter = new OdbcDataAdapter($"CALL \"{ConnectionString.CompanyDB}\".{storeName} ('{Type}','{par1}','{par2}','{par3}','{par4}','{par5}')", SAP_Driver_oCompany._CheckingStatusConnectionHana());
            sqlDataAdapter.Fill(getResulDataTable);
        }
    }
}
