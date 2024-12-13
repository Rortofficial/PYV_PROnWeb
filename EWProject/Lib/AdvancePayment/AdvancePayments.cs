using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.AdvancePayment
{
    public class AdvancePayments
    {
        public static Add _Add(PostAdvancePaymentRequest postAdvancePaymentRequest, Company _company) => new Add(postAdvancePaymentRequest, _company);
        public static Cancel _Cancel(string docNum, Company _company) => new Cancel(docNum, _company);
        public static Reject _Reject(string docNum, string RemarksReson = "",string rejectBy="", Company _company = null) => new Reject(docNum, RemarksReson, rejectBy, _company);
        public static Approve _Approve(string docNum, string RemarksReson,string approveBy, string CostCenter, Company _company = null) => new Approve(docNum, RemarksReson, approveBy, CostCenter, _company);
    }
}
