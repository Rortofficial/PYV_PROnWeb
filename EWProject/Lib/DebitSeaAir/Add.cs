using Client.Connection;
using Client.Models.Gets;
using Client.Models.Request;
using SAPbobsCOM;
using System.Data;

namespace Client.Lib.DebitNoteSeaAir
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PurchaseRequest postPurchaseRequestRequest, string CostCenter, Company _company)
        {
            _Add(postPurchaseRequestRequest, CostCenter, _company);
        }
        private void _Add(PurchaseRequest postPurchaseRequestRequest, string CostCenter, Company oCompany)
        {
            try
            {
                #region Create Purchase Request
                JournalVouchers JVEntry = (JournalVouchers)oCompany.GetBusinessObject(BoObjectTypes.oJournalVouchers);
                JVEntry.JournalEntries.Series = Convert.ToInt32(postPurchaseRequestRequest.Series);
                JVEntry.JournalEntries.DueDate = Convert.ToDateTime(postPurchaseRequestRequest.IssueDate);
                JVEntry.JournalEntries.ReferenceDate = Convert.ToDateTime(postPurchaseRequestRequest.IssueDate);
                JVEntry.JournalEntries.Memo = postPurchaseRequestRequest.Remarks;
                JVEntry.JournalEntries.TransactionCode = "R004";
                JVEntry.JournalEntries.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString()
                                                                                + DateTime.Now.Month.ToString()
                                                                                + DateTime.Now.Year.ToString()
                                                                                + DateTime.Now.DayOfYear.ToString()
                                                                                + DateTime.Now.Hour.ToString()
                                                                                + DateTime.Now.Minute.ToString()
                                                                                + DateTime.Now.Second.ToString()
                                                                                + DateTime.Now.Millisecond.ToString()).ToString();
                JVEntry.JournalEntries.UserFields.Fields.Item("U_USERID").Value = postPurchaseRequestRequest.IssueByName;
                JVEntry.JournalEntries.UserFields.Fields.Item("U_REFINV").Value = postPurchaseRequestRequest.ArInvoice;
                JVEntry.JournalEntries.Lines.ShortName = postPurchaseRequestRequest.VendorCode;
                JVEntry.JournalEntries.Lines.Debit = Convert.ToDouble(postPurchaseRequestRequest.Lines.Sum(x => x.Amount));
                JVEntry.JournalEntries.Lines.ProjectCode = postPurchaseRequestRequest.JobNo;
                if (string.IsNullOrEmpty(postPurchaseRequestRequest.Lines.FirstOrDefault().DistributionRule)) JVEntry.JournalEntries.Lines.CostingCode = postPurchaseRequestRequest.Lines.FirstOrDefault().DistributionRule; else JVEntry.JournalEntries.Lines.CostingCode = CostCenter;
                JVEntry.JournalEntries.Lines.Add();
                foreach (var a in postPurchaseRequestRequest.Lines)
                {
                    JVEntry.JournalEntries.Lines.AccountCode = a.ServiceType;
                    JVEntry.JournalEntries.Lines.UserFields.Fields.Item("U_CardCode").Value = a.ItemCode;
                    //JEEntry.Lines.UserFields.Fields.Item("U_CardCode").Value = a.RefInv;
                    JVEntry.JournalEntries.Lines.Credit = Convert.ToDouble(a.Amount);
                    JVEntry.JournalEntries.Lines.LineMemo = a.remark;
                    if(string.IsNullOrEmpty(a.DistributionRule)) JVEntry.JournalEntries.Lines.CostingCode = a.DistributionRule; else JVEntry.JournalEntries.Lines.CostingCode = CostCenter;
                    JVEntry.JournalEntries.Lines.TaxGroup = a.VatCode;
                    JVEntry.JournalEntries.Lines.ProjectCode = postPurchaseRequestRequest.JobNo;
                    JVEntry.JournalEntries.Lines.Add();
                }
                int RetVal = JVEntry.Add();
                if (RetVal != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                }
                _DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey().ToString().Replace("\t1", "").Trim());
                #endregion
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
        private string GetValueByID(string type, string field, string id)
        {
            try
            {
                var getBookingSheetByUsersList = new List<GetBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             type,
                             id,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    return dataRow[field].ToString()!;
                }
                return "-1";
            }
            catch
            {
                return "-1";
            }
        }
    }
}
