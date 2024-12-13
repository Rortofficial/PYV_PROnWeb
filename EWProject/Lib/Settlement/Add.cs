using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Settlement
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(AddSettlementDraftRequest addSettlementDraftRequest, Company _company)
        {
            _Add(addSettlementDraftRequest, _company);
        }
        private void _Add(AddSettlementDraftRequest addSettlementDraftRequest, Company _company)
        {
            _company.StartTransaction();
            try
            {
                CompanyService companyService;
                companyService = _company.GetCompanyService();
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                GeneralData oChild;
                GeneralDataCollection oChildren;
                oGeneralService = companyService.GetGeneralService("SETTLEMENT");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                oGeneralData.SetProperty("U_RefNo", addSettlementDraftRequest.RefNo);
                oGeneralData.SetProperty("U_PODocEntry", addSettlementDraftRequest.DocEntry);
                oGeneralData.SetProperty("U_TotalPaid", addSettlementDraftRequest.TotalPaid);
                oGeneralData.SetProperty("U_GrandTotal", addSettlementDraftRequest.GrandTotal);
                oGeneralData.SetProperty("U_CreateBy", ClearEmptyString.clearEmptyString(addSettlementDraftRequest.UserID));
                //Add Line
                foreach (var a in addSettlementDraftRequest.Lines)
                {
                    oChildren = oGeneralData.Child("SETTLEMENTROW");
                    oChild = oChildren.Add();
                    oChild.SetProperty("U_ItemCode", ClearEmptyString.clearEmptyString(a.ItemCode));
                    oChild.SetProperty("U_Paid", a.Paid);
                    oChild.SetProperty("U_JobNumber", ClearEmptyString.clearEmptyString(a.JobNumber));
                    oChild.SetProperty("U_CustomerCode", ClearEmptyString.clearEmptyString(a.CustomerCode));
                    oChild.SetProperty("U_InvNumber", ClearEmptyString.clearEmptyString(a.InvNumber));
                    oChild.SetProperty("U_DeclarationNo", ClearEmptyString.clearEmptyString(a.DeclarationNo));
                    oChild.SetProperty("U_RemarkOrRisk", ClearEmptyString.clearEmptyString(a.RemarkOrRisk));
                }
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                if (_company.InTransaction)
                {
                    _company.EndTransaction(BoWfTransOpt.wf_Commit);
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = (int)responseoGeneralService.GetProperty("DocEntry");
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
                _company.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
