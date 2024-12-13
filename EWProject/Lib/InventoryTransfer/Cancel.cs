using Client.Lib.OtherFunction;
using Client.Models.Response;
using SAPbobsCOM;

namespace Client.Lib.InventoryTransfer
{
    public class Cancel
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Cancel(int docEntry, Company oCompany)
        {
            _Cancel(docEntry, oCompany);
        }
        private void _Cancel(int docEntry, Company oCompany)
        {
            try
            {
                StockTransfer stockTransfer = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                if (stockTransfer.GetByKey(docEntry))
                {
                    var retval = stockTransfer.Cancel();
                    if (retval == 0)
                    {
                        QueryData.ConvertDataTable<PostResponse>(new Connection.GetRecordByDataTable(
                            Connection.GetRecordByDataTable.StoreType.Add,
                            "UpdateCancelStatusContainer",
                            docEntry.ToString(),
                            "",
                            "",
                            "",
                            "").GetResultDataTable());
                        _ErroreCode = 0;
                        _MessageErrore = "";
                        _DocEntry = docEntry.ToString();
                    }
                    else
                    {
                        oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                    }
                }
                else
                {
                    _ErroreCode = -1;
                    _MessageErrore = "Invalid DocEntry";
                }

            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
