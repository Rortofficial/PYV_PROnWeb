using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Settlement
{
    public class Update
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Update(AddSettlementDraftRequest addSettlementDraftRequest, Company _company)
        {
            _Update(addSettlementDraftRequest, _company);
        }
        private void _Update(AddSettlementDraftRequest addSettlementDraftRequest, Company _company)
        {
            try
            {
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                //_company.StartTransaction();
                companyService = _company.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("SETTLEMENT");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", addSettlementDraftRequest.DocEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_TotalPaid", ClearEmptyString.clearEmptyString(addSettlementDraftRequest.TotalPaid.ToString()));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_GrandTotal", ClearEmptyString.clearEmptyString(addSettlementDraftRequest.GrandTotal.ToString()));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(addSettlementDraftRequest.UserID.ToString()));
                UpdateChild.UpdateChildObject(addSettlementDraftRequest.Lines, oConfirmBookingGeneralDataUpdate, "SETTLEMENTROW", "LineId", addSettlementDraftRequest.DocEntry.ToString());
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                //_company.EndTransaction(BoWfTransOpt.wf_Commit);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = addSettlementDraftRequest.DocEntry;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
