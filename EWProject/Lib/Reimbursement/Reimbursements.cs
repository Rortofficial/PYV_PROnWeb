using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Reimbursement
{
    public class Reimbursements
    {
        public static ApproveReimbursement _Approve(ReimbursementRequest postReimbursementRequest,string CostCenter, Company _company) => new ApproveReimbursement(postReimbursementRequest, CostCenter, _company);
    }
}
