using Client.Lib.OtherFunction;
using Client.Models.Gets;
using SAPbobsCOM;

namespace Client.Lib.JobSheetTrucking
{
    public class Update
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Update(GetJobSheetTruckingEdit updateJobSheetTruckingEdit, Company _company)
        {
            _Update(updateJobSheetTruckingEdit, _company);
        }
        private void _Update(GetJobSheetTruckingEdit updateJobSheetTruckingEdit, Company _company)
        {
            try
            {
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                _company.StartTransaction();
                companyService = _company.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("JOBSHEETRUCKING");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", updateJobSheetTruckingEdit.DocEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALCOSTING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.TotalCosting.ToString()));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_GRANDTOTALCOSTING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.GrandTotalCosting.ToString()));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_GRANDTOTALCOSTINGUSD", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.GrandTotalCostingUSD.ToString()));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALPROFIT", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.Profit.ToString()));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_REMARKSCOSTING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.RemarkForCosting));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_REMAKRSSELLING", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.RemarkForSelling));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_COSTINGCONFIRM", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.CostingConfirm));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.UpdateBy));
                UpdateChild.UpdateChildObject(updateJobSheetTruckingEdit.Attachments, oConfirmBookingGeneralDataUpdate, "ATTACHMENT", "LineId", updateJobSheetTruckingEdit.DocEntry.ToString());
                UpdateChild.UpdateChildObject(updateJobSheetTruckingEdit.LinesCostingEdit, oConfirmBookingGeneralDataUpdate, "JOBTRUCKCOSTING", "LineId", updateJobSheetTruckingEdit.DocEntry.ToString());
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                if (_company.InTransaction)
                {
                    _company.EndTransaction(BoWfTransOpt.wf_Commit);
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = updateJobSheetTruckingEdit.DocEntry;
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
