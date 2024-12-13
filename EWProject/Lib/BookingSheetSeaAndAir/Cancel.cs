using SAPbobsCOM;

namespace Client.Lib.BookingSheetSeaAndAir
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
            GeneralService oGeneralService;
            GeneralDataParams oGeneralParams;
            CompanyService companyService;
            companyService = oCompany.GetCompanyService();
            oGeneralService = companyService.GetGeneralService("BOOKING_SEA_AIR");
            oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
            oGeneralParams.SetProperty("DocEntry", docNum);
            oGeneralService.Cancel(oGeneralParams);
            oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
            _DocEntry = Convert.ToInt32(docNum);
        }
    }
}
