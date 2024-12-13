using SAPbobsCOM;

namespace Client.Lib.PettyCash
{
    public class CancelDraft
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public CancelDraft(string docNum, Company _company)
        {
            _CancelDraft(docNum, _company);
        }
        private void _CancelDraft(string docNum, Company oCompany)
        {
            try
            {
                GeneralService oGeneralService;
                GeneralDataParams oGeneralParams;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("TB_DRF_JE");
                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", docNum);
                oGeneralService.Cancel(oGeneralParams);
                oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                _DocEntry = Convert.ToInt32(docNum);
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
