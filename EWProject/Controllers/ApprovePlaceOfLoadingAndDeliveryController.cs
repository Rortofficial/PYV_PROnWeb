using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAPbobsCOM;

namespace Client.Controllers
{
    [Authorize]
    public class ApprovePlaceOfLoadingAndDeliveryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RetriveDataPlaceOfLoadingDate(string dateFrom, string dateTo)
        {
            try
            {
                return Ok(QueryData.ConvertDataTable<ListOfApproveLoadingAndDelivery>(new GetRecordByDataTable(
                             "ListOfApproveLoadingAndDelivery",
                             dateFrom,
                             dateTo,
                             "",
                             "",
                             "").GetResultDataTable()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult GetDataInformationPlaceOfLoadingDate(string Code, string BookingID)
        {
            try
            {
                return Ok(QueryData.ConvertDataTable<ListOfApproveLoadingAndDelivery>(new GetRecordByDataTable(
                             "GetDataInformationPlaceOfLoadingDate",
                             Code,
                             BookingID,
                             "",
                             "",
                             "").GetResultDataTable())!.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public IActionResult ApproveOrReject(string Code, string BookingID, string type, string CreateUser)
        {
            try
            {
                var a = QueryData.ConvertDataTable<ListOfApproveLoadingAndDelivery>(new GetRecordByDataTable(
                              "GetDataPlaceOfLoadingAndDeliveryApprove",
                              Code,
                              BookingID,
                              "",
                              "",
                              "").GetResultDataTable())!.FirstOrDefault();
                if (type == "Approve")
                {
                    GeneralService oConfirmBookingGeneralServiceUpdate;
                    GeneralData oConfirmBookingGeneralDataUpdate;
                    GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                    CompanyService companyService;
                    companyService = SAP_Driver_oCompany._CheckingStatusOCompany().GetCompanyService();
                    oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("BOOKINGSHEET");
                    oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", BookingID);
                    oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(a.CreateBy.ToString()));
                    UpdateChild.UpdateChildObject(new List<PlaceOfLoading> { new PlaceOfLoading{
                        PLACELOADING= a.PlaceOfLoading,
                        District=a.LoadingOfDistrict,
                    } }, oConfirmBookingGeneralDataUpdate, "PLACEOFLOADING", "LineId", BookingID);
                    oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                    UpdateChild.UpdateChildObject(new List<PlaceOfDelivery> { new PlaceOfDelivery{
                        PLACEDELIVERY= a.PlaceOfDelivery,
                        District=a.DeliveryOfDistrict,
                    } }, oConfirmBookingGeneralDataUpdate, "PLACEOFDELIVERY", "LineId", BookingID);
                    GeneralService oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate;
                    GeneralData oUpdateLoadingAndADeliveryStatusGeneralDataUpdate;
                    GeneralDataParams oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate;
                    CompanyService companyServiceoUpdateLoadingAndADeliveryStatus;
                    companyServiceoUpdateLoadingAndADeliveryStatus = SAP_Driver_oCompany._CheckingStatusOCompany().GetCompanyService();
                    oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate = companyServiceoUpdateLoadingAndADeliveryStatus.GetGeneralService("TB_PLACE_L_D");
                    oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate = (GeneralDataParams)oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate.SetProperty("Code", Code);
                    oUpdateLoadingAndADeliveryStatusGeneralDataUpdate = oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate.GetByParams(oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate);
                    oUpdateLoadingAndADeliveryStatusGeneralDataUpdate.SetProperty("U_STATUS", "A");
                    oUpdateLoadingAndADeliveryStatusGeneralDataUpdate.SetProperty("U_CreateBy", CreateUser);
                    oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                    oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate.Update(oUpdateLoadingAndADeliveryStatusGeneralDataUpdate);

                }
                else if (type == "Reject")
                {
                    GeneralService oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate;
                    GeneralData oUpdateLoadingAndADeliveryStatusGeneralDataUpdate;
                    GeneralDataParams oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate;
                    CompanyService companyServiceoUpdateLoadingAndADeliveryStatus;
                    companyServiceoUpdateLoadingAndADeliveryStatus = SAP_Driver_oCompany._CheckingStatusOCompany().GetCompanyService();
                    oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate = companyServiceoUpdateLoadingAndADeliveryStatus.GetGeneralService("TB_PLACE_L_D");
                    oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate = (GeneralDataParams)oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate.SetProperty("Code", Code);
                    oUpdateLoadingAndADeliveryStatusGeneralDataUpdate = oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate.GetByParams(oUpdateLoadingAndADeliveryStatusGeneralParamsUpdate);
                    oUpdateLoadingAndADeliveryStatusGeneralDataUpdate.SetProperty("U_STATUS", "R");
                    oUpdateLoadingAndADeliveryStatusGeneralDataUpdate.SetProperty("U_CreateBy", CreateUser);
                    oUpdateLoadingAndADeliveryStatusGeneralServiceUpdate.Update(oUpdateLoadingAndADeliveryStatusGeneralDataUpdate);
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
