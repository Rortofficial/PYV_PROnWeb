using Client.Models.Request;

namespace Client.Models.Gets
{
    public class ListApproveReimbursementMasterData
    {
        public List<ReimbursementRequest> ReimbursementRequests { get; set; }
        public List<GetAccountReimbursment> GetAccountReimbursments { get; set; }
    }
}
