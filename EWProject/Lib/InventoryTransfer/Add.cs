using Client.Lib.OtherFunction;
using Client.Models.Request;
using Client.Models.Response;
using SAPbobsCOM;

namespace Client.Lib.InventoryTransfer
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Add(PostInventoryTransfer postInventoryTransfer, Company oCompany)
        {
            _Add(postInventoryTransfer, oCompany);
        }
        private void _Add(PostInventoryTransfer postInventoryTransfer, Company oCompany)
        {
            try
            {
                postInventoryTransfer.Lines = postInventoryTransfer.Lines.DistinctBy(x => x.ItemCode).ToList();
                StockTransfer stockTransfer = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                stockTransfer.DocDate = DateTime.Today;
                stockTransfer.Comments = postInventoryTransfer.Comment;
                stockTransfer.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString()
                                                                    + DateTime.Now.Month.ToString()
                                                                    + DateTime.Now.Year.ToString()
                                                                    + DateTime.Now.DayOfYear.ToString()
                                                                    + DateTime.Now.Hour.ToString()
                                                                    + DateTime.Now.Minute.ToString()
                                                                    + DateTime.Now.Second.ToString()
                                                                    + DateTime.Now.Millisecond.ToString());
                foreach (var line in postInventoryTransfer.Lines)
                {
                    stockTransfer.FromWarehouse = line.WarehouseFrom;
                    stockTransfer.ToWarehouse = line.WarehouseTo;
                    if (line.Type == "EXCHANGE")
                    {
                        stockTransfer.Lines.ItemCode = line.ItemCode;
                        stockTransfer.Lines.Quantity = 1;
                        stockTransfer.Lines.WarehouseCode = line.WarehouseTo;
                        stockTransfer.Lines.FromWarehouseCode = line.WarehouseFrom;
                        stockTransfer.Lines.Add();
                        stockTransfer.Lines.ItemCode = line.ItemCodeExchange;
                        stockTransfer.Lines.Quantity = 1;
                        stockTransfer.Lines.WarehouseCode = line.WarehouseFrom;
                        stockTransfer.Lines.FromWarehouseCode = line.WarehouseTo;
                        stockTransfer.Lines.Add();
                    }
                    else
                    {
                        stockTransfer.Lines.ItemCode = line.ItemCode;
                        stockTransfer.Lines.Quantity = 1;
                        stockTransfer.Lines.WarehouseCode = line.WarehouseTo;
                        stockTransfer.Lines.FromWarehouseCode = line.WarehouseFrom;
                        stockTransfer.Lines.Add();
                    }
                }
                var Result = stockTransfer.Add();
                if (Result == 0)
                {
                    foreach (var line in postInventoryTransfer.Lines)
                    {
                        QueryData.ConvertDataTable<PostResponse>(new Connection.GetRecordByDataTable(
                           Connection.GetRecordByDataTable.StoreType.Add,
                           "UpdateStatusContainer",
                           line.ItemCode,
                           "",
                           "",
                           "",
                           "").GetResultDataTable());
                    }
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = oCompany.GetNewObjectKey();
                }
                else
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
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
