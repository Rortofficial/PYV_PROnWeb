using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.PettyCash
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PostPettyCashRequest postPettyCashRequest, string draftJE,string CostCenter, Company _company)
        {
            _Add(postPettyCashRequest, draftJE,CostCenter, _company);
        }
        private void _Add(PostPettyCashRequest postPettyCashRequest, string draftJE, string CostCenter, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Create Purchase Request
                JournalEntries JEEntry = (JournalEntries)oCompany.GetBusinessObject(BoObjectTypes.oJournalEntries);
                JEEntry.Series = Convert.ToInt32(postPettyCashRequest.Header.Series);
                JEEntry.DueDate = Convert.ToDateTime(postPettyCashRequest.Header.PostingDate);
                JEEntry.ReferenceDate = Convert.ToDateTime(postPettyCashRequest.Header.PostingDate);
                JEEntry.Memo = postPettyCashRequest.Header.Remarks;
                JEEntry.Reference = postPettyCashRequest.Header.Ref1;
                JEEntry.Reference2 = postPettyCashRequest.Header.Ref2;
                JEEntry.Reference3 = postPettyCashRequest.Header.Ref3;
                JEEntry.TransactionCode = "R002";
                JEEntry.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString()
                                                                                + DateTime.Now.Month.ToString()
                                                                                + DateTime.Now.Year.ToString()
                                                                                + DateTime.Now.DayOfYear.ToString()
                                                                                + DateTime.Now.Hour.ToString()
                                                                                + DateTime.Now.Minute.ToString()
                                                                                + DateTime.Now.Second.ToString()
                                                                                + DateTime.Now.Millisecond.ToString()).ToString();
                JEEntry.UserFields.Fields.Item("U_USERID").Value = ClearEmptyString.clearEmptyString(postPettyCashRequest.Header.UserID);
                JEEntry.UserFields.Fields.Item("U_UpdateBy").Value = ClearEmptyString.clearEmptyString(postPettyCashRequest.Header.UpdateBy);
                JEEntry.UserFields.Fields.Item("U_JEDraftDocEntry").Value = ClearEmptyString.clearEmptyString(draftJE);
                foreach (var a in postPettyCashRequest.Lines)
                {
                    if (a.AccountTypeCode == "-1") //AccountCode
                    {
                        JEEntry.Lines.AccountCode = a.AccountCodeOrBpCode;
                        JEEntry.Lines.CostingCode = CostCenter;
                        JEEntry.Lines.UserFields.Fields.Item("U_CardCode").Value = ClearEmptyString.clearEmptyString(a.CardCode);
                        JEEntry.Lines.UserFields.Fields.Item("U_TaxDate").Value = ClearEmptyString.clearEmptyString(a.DateLine);
                        JEEntry.Lines.LineMemo = a.Remarks;
                        JEEntry.Lines.Debit = a.Debit;
                    }
                    else if (a.AccountTypeCode == "1")// BP Code
                    {
                        JEEntry.Lines.ShortName = a.AccountCodeOrBpCode;
                        JEEntry.Lines.CostingCode = CostCenter;
                        JEEntry.Lines.Credit = a.Credit;
                    }
                    JEEntry.Lines.Add();
                }
                int RetVal = JEEntry.Add();
                if (RetVal != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                }
                _DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey());
                #endregion
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
