using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class ListMasterReimbursementRequest
    {
        public List<GetAccountByEmployeeBudget> GetEmployeeBudgets { get; set; } = null!;
        public ReimbursementRequest GetReimbursementRequestsByDocEntry { get; set; }
    }
}
