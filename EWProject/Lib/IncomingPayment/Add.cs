using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.IncomingPayment
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Add(EmployeeClearingAdvanceRequest postAdvancePaymentRequest, Company _company)
        {
            _Add(postAdvancePaymentRequest, _company);
        }
        private void _Add(EmployeeClearingAdvanceRequest postAdvancePaymentRequest, Company oCompany)
        {
            oCompany.StartTransaction();
            try
            {
                #region Create Incoming
                SAPbobsCOM.Payments Incoming = null;
                Incoming = (Payments)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                Incoming.CardCode = postAdvancePaymentRequest.CardCode;
                Incoming.DocDate = postAdvancePaymentRequest.DocDate;
                Incoming.TaxDate = postAdvancePaymentRequest.DocDate;
                Incoming.UserFields.Fields.Item("U_WebID").Value = postAdvancePaymentRequest.WebId;
                //Incoming.DocCurrency = pay.Currencyid;
                //Incoming.Remarks = postAdvancePaymentRequest.;
                // Line 
                Incoming.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_JournalEntry;
                Incoming.Invoices.DocEntry = Convert.ToInt32(postAdvancePaymentRequest.TransId);
                Incoming.Invoices.DocLine = postAdvancePaymentRequest.DocLine;
                Incoming.Invoices.SumApplied = postAdvancePaymentRequest.Total;
                Incoming.Invoices.Add();
                //Credit Card
                foreach (var z in postAdvancePaymentRequest.Lines)
                {
                    Incoming.CreditCards.CreditCard = Convert.ToInt32(z.CreditMethodCode);
                    Incoming.CreditCards.CreditCardNumber = "123";
                    Incoming.CreditCards.CardValidUntil = DateTime.Now;
                    Incoming.CreditCards.OwnerIdNum = "123456789";
                    Incoming.CreditCards.CreditSum = z.AmountDue;
                    Incoming.CreditCards.VoucherNum = z.VoucherNo;
                    Incoming.CreditCards.PaymentMethodCode = 1;
                    Incoming.CreditCards.CreditType = BoRcptCredTypes.cr_Regular;
                    Incoming.CreditCards.Add();
                }
                int RetVal = Incoming.Add();
                var docEntry = oCompany.GetNewObjectKey();
                #endregion
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = oCompany.GetNewObjectKey();
                }
                else
                {
                    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                }
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
