using SAPbobsCOM;

namespace Client.Lib.BookingSheet
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
            //oCompany.StartTransaction();
            try
            {
                GeneralService oGeneralService;
                GeneralDataParams oGeneralParams;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("BOOKINGSHEET");
                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", docNum);
                oGeneralService.Cancel(oGeneralParams);
                oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                _DocEntry = Convert.ToInt32(docNum);
                //if (oCompany.InTransaction)
                //{
                //    oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                //    _ErroreCode = 0;
                //    _MessageErrore = "";
                //    _DocEntry = Convert.ToInt32(docNum);
                //}
                //else
                //{
                //    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                //    oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                //}
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                //oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
