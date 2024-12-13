using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.InventoryTransfer
{
    public class InventoryTransfers
    {
        public static Add _Add(PostInventoryTransfer postInventoryTransfer, Company oCompany) => new Add(postInventoryTransfer, oCompany);
        public static Cancel _Cancel(int docEntry, Company oCompany) => new Cancel(docEntry, oCompany);
    }
}
