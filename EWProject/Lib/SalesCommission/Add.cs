using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.SalesCommission
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PostSalesCommissions postSalesCommissions, Company _company)
        {
            _Add(postSalesCommissions, _company);
        }
        private void _Add(PostSalesCommissions postSalesCommissions, Company _company)
        {
            //_company.StartTransaction();
            CompanyService companyService;
            companyService = _company.GetCompanyService();
            GeneralService oGeneralService;
            GeneralData oGeneralData;
            GeneralData oChild;
            GeneralDataCollection oChildren;
            oGeneralService = companyService.GetGeneralService("TBCOMMISSION");
            oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
            oGeneralData.SetProperty("U_ApproveStatus", "P");
            oGeneralData.SetProperty("U_UserID", postSalesCommissions.UserID);
            oGeneralData.SetProperty("U_SlpCode", postSalesCommissions.SlpCode);
            oGeneralData.SetProperty("U_IssueDate", DateTime.Now);
            oGeneralData.SetProperty("U_DueDate", ClearEmptyString.clearEmptyString(postSalesCommissions.DueDate));
            oGeneralData.SetProperty("U_RemarkCommission", ClearEmptyString.clearEmptyString(postSalesCommissions.Remark));
            //Add Line
            foreach (var a in postSalesCommissions.Lines)
            {
                oChildren = oGeneralData.Child("TBCOMMISSIONROW");
                oChild = oChildren.Add();
                oChild.SetProperty("U_AccountCode", a.AccountCode);
                oChild.SetProperty("U_JobNumber", ClearEmptyString.clearEmptyString(a.JobNumber));
                oChild.SetProperty("U_ARInvoice", ClearEmptyString.clearEmptyString(a.ARDocEntry));
                oChild.SetProperty("U_GrandTotalCosting", a.GrandTotalCosting);
                oChild.SetProperty("U_GrandTotalSelling", a.GrandTotalSelling);
                oChild.SetProperty("U_GrossProfit", a.GrossProfit);
                oChild.SetProperty("U_Percentage", a.Percentage);
                oChild.SetProperty("U_TotalSaleCommission", a.TotalSaleCommission);
            }
            var responseoGeneralService = oGeneralService.Add(oGeneralData);
            _DocEntry = (int)responseoGeneralService.GetProperty("DocEntry");
            _company.GetLastError(out _ErroreCode, out _MessageErrore);
            //if (_company.InTransaction)
            //{
            //    _company.EndTransaction(BoWfTransOpt.wf_Commit);
            //    _ErroreCode = 0;
            //    _MessageErrore = "";
            //    _DocEntry = responseoGeneralService.GetProperty("DocEntry");
            //}
            //else
            //{
            //    _company.EndTransaction(BoWfTransOpt.wf_RollBack);
            //    _company.GetLastError(out _ErroreCode, out _MessageErrore);
            //}
        }
    }
}
