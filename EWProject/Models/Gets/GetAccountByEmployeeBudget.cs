namespace Client.Models.Gets
{
    public class GetAccountByEmployeeBudget
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public double BudgetOnHand { get; set; }
        public double BudgetLimit { get; set; }
    }
}
