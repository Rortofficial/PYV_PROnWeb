using Client.Lib.OtherFunction;
using SAPbobsCOM;

namespace Client.Lib.SalesCommission
{
    public class Reject
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Reject(string docNum, string remark, Company _company)
        {
            _Reject(docNum, remark, _company);
        }
        private void _Reject(string docNum, string remark, Company _company)
        {
            try
            {
                GeneralService oGeneralServiceUpdate;
                GeneralData oGeneralDataUpdate;
                GeneralDataParams oGeneralParamsUpdate;
                CompanyService companyService;
                _company.StartTransaction();
                companyService = _company.GetCompanyService();
                oGeneralServiceUpdate = companyService.GetGeneralService("TBCOMMISSION");
                oGeneralParamsUpdate = (GeneralDataParams)oGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParamsUpdate.SetProperty("DocEntry", docNum);
                oGeneralDataUpdate = oGeneralServiceUpdate.GetByParams(oGeneralParamsUpdate);
                oGeneralDataUpdate.SetProperty("U_ApproveStatus", "R");
                oGeneralDataUpdate.SetProperty("Remark", ClearEmptyString.clearEmptyString(remark));
                oGeneralServiceUpdate.Update(oGeneralDataUpdate);
                if (_company.InTransaction)
                {
                    _company.EndTransaction(BoWfTransOpt.wf_Commit);
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = Convert.ToInt32(docNum);
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
