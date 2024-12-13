using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Reimbursement
{
    public class ApproveReimbursement
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry="";
        public ApproveReimbursement(ReimbursementRequest postReimbursementRequest,string CostCenter, Company _company)
        {
            _ApproveReimbursement(postReimbursementRequest, CostCenter, _company);
        }
        private void _ApproveReimbursement(ReimbursementRequest postReimbursementRequest,string CostCenter, Company oCompany)
        {
            try
            {
                JournalEntries JVEntry = (JournalEntries)oCompany.GetBusinessObject(BoObjectTypes.oJournalEntries);
                //JVEntry.Series = Convert.ToInt32(postAdvancePaymentRequest.Series);
                JVEntry.TaxDate = DateTime.Now;
                //JVEntry.Memo = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Remark);
                JVEntry.Reference = ClearEmptyString.clearEmptyString(postReimbursementRequest.Remark);
                JVEntry.TransactionCode = "R001";
                //JVEntry.Reference2 = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref2);
                //JVEntry.Reference3 = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref3);
                JVEntry.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString()
                                        + DateTime.Now.Month.ToString()
                                        + DateTime.Now.Year.ToString()
                                        + DateTime.Now.DayOfYear.ToString()
                                        + DateTime.Now.Hour.ToString()
                                        + DateTime.Now.Minute.ToString()
                                        + DateTime.Now.Second.ToString()
                                        + DateTime.Now.Millisecond.ToString()).ToString();
                JVEntry.UserFields.Fields.Item("U_ReimbursementDocEntry").Value = ClearEmptyString.clearEmptyString(postReimbursementRequest.DocEntry.ToString());
                JVEntry.UserFields.Fields.Item("U_USERID").Value = ClearEmptyString.clearEmptyString(postReimbursementRequest.UserID);
                //JVEntry.ProjectCode = postAdvancePaymentRequest.ProjectManagement;
                JVEntry.Lines.ShortName = postReimbursementRequest.CardCode;
                JVEntry.Lines.Debit = postReimbursementRequest.Amount;
                JVEntry.Lines.CostingCode = CostCenter;
                //JVEntry.JournalEntries.Lines.ProjectCode = postAdvancePaymentRequest.ProjectManagement;
                JVEntry.Lines.Add();
                JVEntry.Lines.AccountCode = postReimbursementRequest.AccountCode;
                //JVEntry.JournalEntries.Lines.TaxGroup = a.TaxGroup;
                JVEntry.Lines.Credit = postReimbursementRequest.Amount;
                JVEntry.Lines.CostingCode = CostCenter;
                JVEntry.Lines.Add();
                //credit = credit + a.Credit;
                //debit = debit + a.Debit;
                int RetVal = JVEntry.Add();
                var docEntry = oCompany.GetNewObjectKey();
                if (RetVal != 0)
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
