using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.IncomingPayment
{
    public class IncomingPayments
    {
        public static Add _Add(EmployeeClearingAdvanceRequest postAdvancePaymentRequest, Company _company) => new Add(postAdvancePaymentRequest, _company);
    }
}
