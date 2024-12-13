using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.ConfirmBookingSheet
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
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("CONFIRMBOOKINGSHEET");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", putBookingSheetRequest.ConfirmBookingID);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_UpdateBy", ClearEmptyString.clearEmptyString(putBookingSheetRequest.CreateUser));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_CSName", ClearEmptyString.clearEmptyString(putBookingSheetRequest.CSName));
                oConfirmBookingGeneralDataUpdate.SetProperty("Remark", ClearEmptyString.clearEmptyString(putBookingSheetRequest.Remarks));
                oConfirmBookingGeneralDataUpdate.SetProperty("U_PRICELIST", (putBookingSheetRequest.PriceList == "-1") ? "0" : ClearEmptyString.clearEmptyString(putBookingSheetRequest.PriceList));
                #region Create Item Master Data 
                SAPbobsCOM.Items oItems = null!;
                foreach (var item in putBookingSheetRequest.Lines.Where(x => x.TYPE == "EXCHANGE" && x.ExchangeType == "New"))
                {
                    oItems = (SAPbobsCOM.Items)_company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                    oItems.ItemCode = item.CONTAINERNO2;
                    oItems.ItemName = item.CONTAINERTYPE;
                    oItems.ItemType = SAPbobsCOM.ItemTypeEnum.itItems;
                    oItems.ItemsGroupCode = Configure.ItemGroup;
                    var rs = oItems.Add();
                    if (rs != 0)
                    {
                        _company.GetLastError(out _ErroreCode, out _MessageErrore);
                        //oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        throw new Exception(_MessageErrore);
                    }
                }
                #endregion
                #region Create Good Receipt Stock
                if (putBookingSheetRequest.Lines.Where(x => x.TYPE == "EXCHANGE" && x.ExchangeType == "New").Count() != 0)
                {
                    Documents oGoodReceipt;
                    oGoodReceipt = (Documents)_company.GetBusinessObject(BoObjectTypes.oInventoryGenEntry);
                    oGoodReceipt.DocDate = DateTime.Now;
                    foreach (var l in putBookingSheetRequest.Lines.Where(x => x.TYPE == "EXCHANGE" && x.ExchangeType == "New"))
                    {
                        oGoodReceipt.Lines.ItemCode = l.CONTAINERNO2;
                        oGoodReceipt.Lines.Quantity = 1;
                        oGoodReceipt.Lines.UnitPrice = 0;
                        oGoodReceipt.Lines.WarehouseCode = putBookingSheetRequest.Destination;
                        oGoodReceipt.Lines.Add();
                    }

                    var RetvalGoodReceipt = oGoodReceipt.Add();
                    if (RetvalGoodReceipt != 0)
                    {
                        _company.GetLastError(out _ErroreCode, out _MessageErrore);
                        //oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        throw new Exception(_MessageErrore);
                    }
                }
                #endregion  
                UpdateChild.UpdateChildObject(putBookingSheetRequest.Lines, oConfirmBookingGeneralDataUpdate, "TRUCKINFORMATION", "LineId", putBookingSheetRequest.ConfirmBookingID.ToString());
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
