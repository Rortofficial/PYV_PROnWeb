using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Response;
using SAPbobsCOM;

namespace Client.Lib.JobSheetTruckingSeaAir
{
    public class Cancel
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Cancel(int DocEntry, int docEntrySO,List<GetPurchaseOrderCancelJobSheetSeaAir> docEntryPO, string updateBy, Company _company)
        {
            _Cancel(DocEntry, docEntrySO, docEntryPO, updateBy, _company);
        }
        private void _Cancel(int DocEntry, int docEntrySO, List<GetPurchaseOrderCancelJobSheetSeaAir> docEntryPO, string updateBy, Company _company)
        {
            try
            {
                if (_company.InTransaction == true) _company.EndTransaction(BoWfTransOpt.wf_RollBack); else _company.StartTransaction();
                #region Cancel Sales Order
                int RetVal = -1;
                Documents SalesOrder = (Documents)_company.GetBusinessObject(BoObjectTypes.oOrders);
                if (!SalesOrder.GetByKey(docEntrySO))
                {
                    if (DocEntry == 0)
                    {
                        _DocEntry = docEntrySO.ToString();
                        _ErroreCode = 141;
                        _MessageErrore = "Invalid DocEntry";
                        return;
                    }
                    else
                    {
                        GeneralService oGeneralService;
                        GeneralDataParams oGeneralParams;
                        CompanyService companyService;
                        companyService = _company.GetCompanyService();
                        oGeneralService = companyService.GetGeneralService("TB_JOBSHEET_SEA_AIR");
                        oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                        oGeneralParams.SetProperty("DocEntry", DocEntry);
                        oGeneralService.Cancel(oGeneralParams);
                        _ErroreCode = 0;
                        _MessageErrore = "";
                        _DocEntry = DocEntry.ToString();
                        _company.EndTransaction(BoWfTransOpt.wf_Commit);
                        return;
                    }

                }
                SalesOrder.NumAtCard = "";
                RetVal = SalesOrder.Update();
                SalesOrder = (Documents)_company.GetBusinessObject(BoObjectTypes.oOrders);
                SalesOrder.GetByKey(docEntrySO);
                RetVal = SalesOrder.Cancel();
                if (RetVal != 0)
                {
                    _company.GetLastError(out _ErroreCode, out _MessageErrore);
                    _DocEntry = "0";
                    if (_company.InTransaction == true) _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                    return;
                }
                else
                {
                    #region Purchase Order
                    foreach(var ObjPurchaseOrder in docEntryPO)
                    {
                        if (ObjPurchaseOrder.PurchaseOrderDocEntry != 0)
                        {
                            int RetValPO = -1;
                            Documents PurchaseOrder = (Documents)_company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                            if (!PurchaseOrder.GetByKey(ObjPurchaseOrder.PurchaseOrderDocEntry))
                            {
                                if (DocEntry == 0)
                                {
                                    _DocEntry = ObjPurchaseOrder.PurchaseOrderDocEntry.ToString();
                                    _ErroreCode = 141;
                                    _MessageErrore = "Invalid DocEntry";
                                    return;
                                }
                            }
                            PurchaseOrder.NumAtCard = "";
                            RetValPO = PurchaseOrder.Update();
                            PurchaseOrder = (Documents)_company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                            PurchaseOrder.GetByKey(ObjPurchaseOrder.PurchaseOrderDocEntry);
                            RetValPO = PurchaseOrder.Cancel();
                            if (RetValPO != 0)
                            {
                                _company.GetLastError(out _ErroreCode, out _MessageErrore);
                                _DocEntry = "0";
                                if (_company.InTransaction == true) _company.EndTransaction(BoWfTransOpt.wf_RollBack);
                                return;
                            }
                        }
                    }
                    #endregion
                    #region Cancel Job Sheet Trucking
                    GeneralService oGeneralService;
                    GeneralDataParams oGeneralParams;
                    GeneralData oGeneralDataUpdate;
                    CompanyService companyService;
                    companyService = _company.GetCompanyService();
                    oGeneralService = companyService.GetGeneralService("TB_JOBSHEET_SEA_AIR");
                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", DocEntry);
                    oGeneralDataUpdate = oGeneralService.GetByParams(oGeneralParams);
                    oGeneralDataUpdate.SetProperty("U_SALESORDERDOCNUM", "0");
                    oGeneralDataUpdate.SetProperty("U_CONFIRMBOOKINGID", "0");
                    oGeneralService.Update(oGeneralDataUpdate);
                    oGeneralService.Cancel(oGeneralParams);
                    #endregion
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = "0";
                    _company.EndTransaction(BoWfTransOpt.wf_Commit);
                }
                #endregion
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.HResult;
                _MessageErrore = ex.Message;
                _DocEntry = "0";
                if (_company.InTransaction == true) _company.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
