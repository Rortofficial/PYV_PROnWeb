using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.PettyCash
{
    public class UpdateDraft
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public UpdateDraft(string docEntry, string docEntryJE, PostPettyCashRequest postPettyCashRequest, Company _company)
        {
            _UpdateDraft(docEntry, docEntryJE, postPettyCashRequest, _company);
        }
        private void _UpdateDraft(string docEntry, string docEntryJE, PostPettyCashRequest postPettyCashRequest, Company _company)
        {
            try
            {
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                //_company.StartTransaction();
                companyService = _company.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("TB_DRF_JE");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", docEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                UpdateHeader.UpdateHeaderObject(postPettyCashRequest.Header, oConfirmBookingGeneralDataUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_JEDocEntry", ClearEmptyString.clearEmptyString(docEntryJE));
                UpdateChild.UpdateChildObject(postPettyCashRequest.Lines, oConfirmBookingGeneralDataUpdate, "TB_DRF_JE_ROW", "LineId", docEntry);
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = Convert.ToInt32(docEntry);
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
