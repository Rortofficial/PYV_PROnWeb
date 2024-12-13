using SAPbobsCOM;

namespace Client.Lib.BookingSheetSeaAndAir
{
    public class Remove
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Remove(string docNum, Company _company)
        {
            _Remove(docNum, _company);
        }
        private void _Remove(string docNum, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                GeneralService oGeneralService;
                GeneralDataParams oGeneralParams;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("BOOKING_SEA_AIR");
                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", docNum);
                oGeneralService.Delete(oGeneralParams);
                oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                _DocEntry = Convert.ToInt32(docNum);
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
