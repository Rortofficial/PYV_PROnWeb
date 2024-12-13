using SAPbobsCOM;

namespace Client.Lib.Settlement
{
    public class Cancel
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Cancel(string docNum, Company _company)
        {
            _Cancel(docNum, _company);
        }
        private void _Cancel(string docNum, Company _company)
        {
            try
            {
                GeneralService oGeneralService;
                //GeneralData oGeneralData;
                GeneralDataParams oGeneralParams;
                //GeneralDataParams oGeneralParams;
                CompanyService companyService;
                _company.StartTransaction();
                companyService = _company.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("SETTLEMENT");
                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", docNum);
                oGeneralService.Cancel(oGeneralParams);
                if (_company.InTransaction)
                {
                    _company.EndTransaction(BoWfTransOpt.wf_Commit);
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = docNum;
                }
                else
                {
                    _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                    _company.GetLastError(out _ErroreCode, out _MessageErrore);
                }
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
