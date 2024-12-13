using Client.Lib.OtherFunction;
using Client.Models.Request;

namespace Client.Lib.Settlement
{
    public class ClearEmptyStringLine
    {
        public List<AddSettlementLine> addSettlementLines => _addSettlementLines;
        private List<AddSettlementLine> _addSettlementLines;
        public ClearEmptyStringLine(List<AddSettlementLine> Lines)
        {
            ClearUpdateLineEmpty(Lines);
        }
        private List<AddSettlementLine> ClearUpdateLineEmpty(List<AddSettlementLine> Lines)
        {
            _addSettlementLines = new List<AddSettlementLine>();
            foreach (var a in Lines)
            {
                _addSettlementLines.Add(new AddSettlementLine
                {
                    LineId = a.LineId,
                    Paid = a.Paid,
                    DeclarationNo = ClearEmptyString.clearEmptyString(a.DeclarationNo),
                    CustomerCode = a.CustomerCode,
                    InvNumber = ClearEmptyString.clearEmptyString(a.InvNumber),
                    JobNumber = ClearEmptyString.clearEmptyString(a.JobNumber),
                    ItemCode = ClearEmptyString.clearEmptyString(a.ItemCode),
                    RemarkOrRisk = ClearEmptyString.clearEmptyString(a.RemarkOrRisk),
                });
            }
            return _addSettlementLines;
        }
    }
}
