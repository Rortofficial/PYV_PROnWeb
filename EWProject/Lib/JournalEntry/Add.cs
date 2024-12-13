using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.JournalEntry
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Add(PostAdvancePaymentRequest postAdvancePaymentRequest, Company _company)
        {
            _Add(postAdvancePaymentRequest, _company);
        }
        private void _Add(PostAdvancePaymentRequest postAdvancePaymentRequest, Company oCompany)
        {
            oCompany.StartTransaction();
            try
            {
                #region Create JE
                JournalEntries JVEntry = (JournalEntries)oCompany.GetBusinessObject(BoObjectTypes.oJournalEntries);
                JVEntry.Series = Convert.ToInt32(postAdvancePaymentRequest.Series);
                JVEntry.TaxDate = postAdvancePaymentRequest.DocDate;
                JVEntry.Memo = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Remark);
                JVEntry.Reference = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref1);
                JVEntry.Reference2 = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref2);
                JVEntry.Reference3 = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref3);
                JVEntry.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString()
                                        + DateTime.Now.Month.ToString()
                                        + DateTime.Now.Year.ToString()
                                        + DateTime.Now.DayOfYear.ToString()
                                        + DateTime.Now.Hour.ToString()
                                        + DateTime.Now.Minute.ToString()
                                        + DateTime.Now.Second.ToString()
                                        + DateTime.Now.Millisecond.ToString()).ToString();
                JVEntry.UserFields.Fields.Item("U_AdvanceDocNum").Value = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.DocEntry);
                JVEntry.UserFields.Fields.Item("U_USERID").Value = ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.UserID);
                JVEntry.ProjectCode = postAdvancePaymentRequest.ProjectManagement;
                foreach (var a in postAdvancePaymentRequest.Lines)
                {

                    JVEntry.Lines.ShortName = a.BpCode;
                    JVEntry.Lines.Debit = a.amount;
                    //JVEntry.JournalEntries.Lines.ProjectCode = postAdvancePaymentRequest.ProjectManagement;
                    JVEntry.Lines.Add();
                    JVEntry.Lines.AccountCode = a.AccountCode;
                    //JVEntry.JournalEntries.Lines.TaxGroup = a.TaxGroup;
                    JVEntry.Lines.Credit = a.amount;
                    JVEntry.Lines.Add();
                    //credit = credit + a.Credit;
                    //debit = debit + a.Debit;
                }
                int RetVal = JVEntry.Add();
                var docEntry = oCompany.GetNewObjectKey();
                if (RetVal != 0)
                {
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                }
                #region Update DocEntry Of ConfirmBookingSheet link with ProjectManagement
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                //GeneralData oBookingChildUpdate;
                //GeneralDataCollection oBookingChildrenUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService oConfirmBookingCmpSrvUpdate;
                oConfirmBookingCmpSrvUpdate = oCompany.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = oConfirmBookingCmpSrvUpdate.GetGeneralService("ADVANCEPAYMENT");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", postAdvancePaymentRequest.DocEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_Status", "A");
                oConfirmBookingGeneralDataUpdate.SetProperty("U_JEDocEntry", docEntry);
                //Add Childrence
                //oBookingChildrenUpdate = oBookingGeneralDataUpdate.Child("TRUCKINFORMATION");
                //oBookingChildUpdate = oBookingChildrenUpdate.Add();
                //oBookingChildUpdate.SetProperty("U_INOVICENO", "");
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                #endregion
                #endregion
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = oCompany.GetNewObjectKey();
                }
                else
                {
                    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                }
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
