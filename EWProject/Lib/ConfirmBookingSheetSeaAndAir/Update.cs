using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.ConfirmBookingSheetSeaAndAir
{
    public class Update
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Update(PutConfirmBookingSheetRequest putBookingSheetRequest, Company _company)
        {
            _Update(putBookingSheetRequest, _company);
        }
        private void _Update(PutConfirmBookingSheetRequest putBookingSheetRequest, Company _company)
        {
            _company.StartTransaction();
            try
            {
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                companyService = _company.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("CON_BOOKING_S_A");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", putBookingSheetRequest.ConfirmBookingID);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_CreateUser", ClearEmptyString.clearEmptyString(putBookingSheetRequest.CreateUser));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_CSName", ClearEmptyString.clearEmptyString(putBookingSheetRequest.CSName));
                oConfirmBookingGeneralDataUpdate.SetProperty("Remark", ClearEmptyString.clearEmptyString(putBookingSheetRequest.Remarks));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_PRICELIST", ClearEmptyString.clearEmptyString(putBookingSheetRequest.PriceList));
                UpdateChild.UpdateChildObject(putBookingSheetRequest.Lines, oConfirmBookingGeneralDataUpdate, "TRUCK_INFO_S_A", "LineId", putBookingSheetRequest.ConfirmBookingID.ToString());
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = putBookingSheetRequest.ConfirmBookingID;
                _company.EndTransaction(BoWfTransOpt.wf_Commit);
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
