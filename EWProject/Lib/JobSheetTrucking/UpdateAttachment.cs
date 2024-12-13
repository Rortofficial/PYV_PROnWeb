using Client.Lib.OtherFunction;
using Client.Models.Gets;
using SAPbobsCOM;

namespace Client.Lib.JobSheetTrucking
{
    public class UpdateAttachment
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public UpdateAttachment(GetJobSheetTruckingEdit updateJobSheetTruckingEdit, Company _company)
        {
            _UpdateAttachment(updateJobSheetTruckingEdit, _company);
        }
        private void _UpdateAttachment(GetJobSheetTruckingEdit updateJobSheetTruckingEdit, Company _company)
        {
            try
            {
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                companyService = _company.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("JOBSHEETRUCKING");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", updateJobSheetTruckingEdit.DocEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(updateJobSheetTruckingEdit.UpdateBy));
                UpdateChild.UpdateChildObject(updateJobSheetTruckingEdit.Attachments, oConfirmBookingGeneralDataUpdate, "ATTACHMENT", "LineId", updateJobSheetTruckingEdit.DocEntry.ToString());
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = updateJobSheetTruckingEdit.DocEntry;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
