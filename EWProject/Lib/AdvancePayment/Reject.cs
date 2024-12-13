using Client.Lib.OtherFunction;
using SAPbobsCOM;

namespace Client.Lib.AdvancePayment
{
    public class Reject
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Reject(string DocEntry, string RemarksReson = "",string rejectBy="", Company _company = null)
        {
            _Reject(DocEntry, RemarksReson,rejectBy, _company);
        }
        private void _Reject(string DocEntry, string RemarksReson = "", string rejectBy= "", Company oCompany = null)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Code
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService oConfirmBookingCmpSrvUpdate;
                oConfirmBookingCmpSrvUpdate = oCompany.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = oConfirmBookingCmpSrvUpdate.GetGeneralService("ADVANCEPAYMENT");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", DocEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_ApproveStatus", "R");
                oConfirmBookingGeneralDataUpdate.SetProperty("U_REASON", ClearEmptyString.clearEmptyString(RemarksReson));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_ApproveBy", ClearEmptyString.clearEmptyString(rejectBy));
                //Add Childrence
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                #endregion
                oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                _DocEntry = Convert.ToInt32(DocEntry);
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
