using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterClearingAdvancePayment
    {
        public List<GetAdvancePaymentClearing> getAdvancePaymentClearings { get; set; }
        public List<GetSeries> getSeries { get; set; }
        public List<GetClearingAdvance> getClearingAdvances { get; set; }
    }
}
