using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.BookingSheetSeaAndAir
{
    public class UpdateCommodities
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public UpdateCommodities(string CreateUser, int docEntry, List<CommoditySeaAndAir> commodityRequest, HeaderCommodityUpdate headerCommodityUpdate, List<DestAgentUpdateCommodities> listDestAgent, List<ShipperUpdateCommodities> listShipper, List<ConsigneeUpdateCommodities> listConsignee, List<CustomerUpdateCommodities> listCustomer, Company _company)
        {
            _Update(CreateUser, docEntry, commodityRequest, headerCommodityUpdate,listDestAgent,listShipper,listConsignee, listCustomer, _company);
        }
        private void _Update(string CreateUser, int docEntry, List<CommoditySeaAndAir> commodityRequest, HeaderCommodityUpdate headerCommodityUpdate, List<DestAgentUpdateCommodities> listDestAgent, List<ShipperUpdateCommodities> listShipper, List<ConsigneeUpdateCommodities> listConsignee, List<CustomerUpdateCommodities> listCustomer, Company _company)
        {
            try
            {
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService companyService;
                companyService = _company.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("BOOKING_SEA_AIR");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", docEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(CreateUser.ToString()));
                if(headerCommodityUpdate!= null)
                {
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_GROSSWEIGHT", headerCommodityUpdate.GROSSWEIGHT);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_NETWEIGHT", headerCommodityUpdate.NETWEIGHT);
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_TOTALPACKAGE", ClearEmptyString.clearEmptyString(headerCommodityUpdate.TOTALPACKAGE));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_GOODSDESCRIPTION", ClearEmptyString.clearEmptyString(headerCommodityUpdate.GOODSDESCRIPTION));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_LOADINGDATE", ClearEmptyString.clearEmptyString(headerCommodityUpdate.LOADINGDATE));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_CROSSBORDERDATE", ClearEmptyString.clearEmptyString(headerCommodityUpdate.CROSSBORDERDATE));
                    oConfirmBookingGeneralDataUpdate.SetProperty("U_ETAREQUIREMENT", ClearEmptyString.clearEmptyString(headerCommodityUpdate.ETAREQUIREMENT));
                }
                if (commodityRequest != null) UpdateChild.UpdateChildObject(commodityRequest, oConfirmBookingGeneralDataUpdate, "COMMODITY_SEA_AIR", "LineId", docEntry.ToString());
                if (listDestAgent != null) UpdateChild.UpdateChildObject(listDestAgent, oConfirmBookingGeneralDataUpdate, "TBDESTAGENT", "LineId", docEntry.ToString());
                if (listConsignee != null) UpdateChild.UpdateChildObject(listConsignee, oConfirmBookingGeneralDataUpdate, "TBCONSIGNEE_SEA_AIR", "LineId", docEntry.ToString());
                if(listShipper!= null) UpdateChild.UpdateChildObject(listShipper, oConfirmBookingGeneralDataUpdate, "TBSHIPPER_SEA_AIR", "LineId", docEntry.ToString());
                if(listCustomer!= null) UpdateChild.UpdateChildObject(listCustomer, oConfirmBookingGeneralDataUpdate, "TBCUSTOMER", "LineId", docEntry.ToString());
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
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
