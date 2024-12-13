using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Response;
using SAPbobsCOM;

namespace Client.Lib.Purchase
{
    public class CancelPurchaseOrAdvancePayment
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public CancelPurchaseOrAdvancePayment(string docNum, Company _company)
        {
            _Cancel(docNum, _company);
        }
        private void _Cancel(string docNum, Company oCompany)
        {
            try
            {
                GeneralService oGeneralService;
                GeneralDataParams oGeneralParams;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("ADVANCEPAYMENT");
                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", docNum);
                oGeneralService.Cancel(oGeneralParams);
                QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.Add,
                             "CancelLineStatusInActivePRCOS",//define new Type for Script
                             docNum,
                             "",
                             "",
                             "",
                             "").GetResultDataTable()).FirstOrDefault();
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
