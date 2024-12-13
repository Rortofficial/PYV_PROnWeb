using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.PettyCash
{
    public class AddDraft
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public AddDraft(PostPettyCashRequest postPettyCashRequest, Company _company)
        {
            _AddDraft(postPettyCashRequest, _company);
        }
        private void _AddDraft(PostPettyCashRequest postPettyCashRequest, Company oCompany)
        {
            //oCompany.StartTransaction();
            try
            {
                #region Code
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("TB_DRF_JE");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                AddHeader.AddHeaderObject(postPettyCashRequest.Header, oGeneralData);
                AddChild.AddChildRow(postPettyCashRequest.Lines, oGeneralData, "TB_DRF_JE_ROW", "LineId");
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                int docEntry = (int)responseoGeneralService.GetProperty("DocEntry");
                #endregion
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = docEntry;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
