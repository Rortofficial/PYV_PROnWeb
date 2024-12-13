using SAPbobsCOM;

namespace Client.Lib.CreditNoteSeaAir
{
    public class Remove
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Remove(int docEntry, Company _company)
        {
            _Cancel(docEntry, _company);
        }
        private void _Cancel(int docEntry, Company oCompany)
        {
            try
            {
                JournalVouchers JVEntry;
                JVEntry = (JournalVouchers)oCompany.GetBusinessObject(BoObjectTypes.oJournalVouchers);
                JVEntry.JournalEntries.GetByKey(docEntry);
                int RetVal = JVEntry.JournalEntries.Remove();
                if (RetVal != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                }
                _DocEntry = docEntry;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
