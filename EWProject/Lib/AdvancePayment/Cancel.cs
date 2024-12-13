using SAPbobsCOM;

namespace Client.Lib.AdvancePayment
{
    public class Cancel
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Cancel(string docNum, Company _company)
        {
            _Cancel(docNum, _company);
        }
        private void _Cancel(string docNum, Company oCompany)
        {
            try
            {
                #region Code
                GeneralService oGeneralService;
                GeneralDataParams oGeneralParams;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("ADVANCEPAYMENT");
                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", docNum);
                oGeneralService.Cancel(oGeneralParams);
                #endregion
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
