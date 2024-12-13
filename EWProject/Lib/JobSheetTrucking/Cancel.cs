using Client.Lib.OtherFunction;
using Client.Models.Response;
using SAPbobsCOM;

namespace Client.Lib.JobSheetTrucking
{
    public class Cancel
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Cancel(int DocEntry, int docEntrySO, string updateBy, Company _company)
        {
            _Cancel(DocEntry, docEntrySO, updateBy, _company);
        }
        private void _Cancel(int DocEntry, int docEntrySO, string updateBy, Company _company)
        {
            try
            {
                #region Cancel Sales Order
                int RetVal = -1;
                Documents SalesOrder = (Documents)_company.GetBusinessObject(BoObjectTypes.oOrders);
                if (!SalesOrder.GetByKey(docEntrySO))
                {
                    if (DocEntry == 0)
                    {
                        _DocEntry = "0";
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
                        oGeneralService = companyService.GetGeneralService("JOBSHEETRUCKING");
                        oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                        oGeneralParams.SetProperty("DocEntry", DocEntry);
                        oGeneralService.Cancel(oGeneralParams);
                        _ErroreCode = 0;
                        _MessageErrore = "";
                        _DocEntry = DocEntry.ToString();
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
                    _DocEntry = docEntrySO.ToString();
                    return;
                }
                else
                {
                    #region Cancel Job Sheet Trucking
                    GeneralService oGeneralService;
                    GeneralDataParams oGeneralParams;
                    CompanyService companyService;
                    companyService = _company.GetCompanyService();
                    oGeneralService = companyService.GetGeneralService("JOBSHEETRUCKING");
                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", DocEntry);
                    oGeneralService.Cancel(oGeneralParams);
                    QueryData.ConvertDataTable<PostResponse>(new Connection.GetRecordByDataTable(
                             Connection.GetRecordByDataTable.StoreType.Add,
                            "UPDATENUMATCARDSO",
                             DocEntry.ToString(),
                             docEntrySO.ToString(),
                             "",
                             "",
                             "").GetResultDataTable());
                    #endregion
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = "0";
                }
                #endregion
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.HResult;
                _MessageErrore = ex.Message;
                _DocEntry = "";
            }
        }
    }
}
