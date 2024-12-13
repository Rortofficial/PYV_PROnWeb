using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.SalesCommission
{
    public static class SalesCommissions
    {
        public static Add _Add(PostSalesCommissions postSalesCommissions, Company _company) => new Add(postSalesCommissions, _company);
        public static Reject _Reject(string docNum, string remark, Company _company) => new Reject(docNum, remark, _company);
        public static Cancel _Cancel(string docNum, Company _company) => new Cancel(docNum, _company);
        public static Approve _Approve(PostSalesCommissions postSalesCommissions, string CostCenter, Company _company) => new Approve(postSalesCommissions,CostCenter, _company);
    }
}
