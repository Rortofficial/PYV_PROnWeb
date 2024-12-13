using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.AdvancePayment
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PostAdvancePaymentRequest postAdvancePaymentRequest, Company _company)
        {
            _Add(postAdvancePaymentRequest, _company);
        }
        private void _Add(PostAdvancePaymentRequest postAdvancePaymentRequest, Company oCompany)
        {
            try
            {
                #region Code 
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                GeneralData oChild;
                GeneralDataCollection oChildren;
                //GeneralDataParams oGeneralParams;
                CompanyService companyService;
                oCompany.StartTransaction();
                companyService = oCompany.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("ADVANCEPAYMENT");
                oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                oGeneralData.SetProperty("U_WebID", (DateTime.Now.Day.ToString()
                                        + DateTime.Now.Month.ToString()
                                        + DateTime.Now.Year.ToString()
                                        + DateTime.Now.DayOfYear.ToString()
                                        + DateTime.Now.Hour.ToString()
                                        + DateTime.Now.Minute.ToString()
                                        + DateTime.Now.Second.ToString()
                                        + DateTime.Now.Millisecond.ToString()).ToString());
                oGeneralData.SetProperty("U_PostingDate", postAdvancePaymentRequest.DocDate);
                oGeneralData.SetProperty("U_Status", "P");
                oGeneralData.SetProperty("U_Project", ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.ProjectManagement));
                oGeneralData.SetProperty("U_Series", postAdvancePaymentRequest.Series);
                oGeneralData.SetProperty("U_Ref1", ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref1));
                oGeneralData.SetProperty("U_Ref2", ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref2));
                oGeneralData.SetProperty("U_Ref3", ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Ref3));
                oGeneralData.SetProperty("Remark", ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.Remark));
                oGeneralData.SetProperty("U_UserID", ClearEmptyString.clearEmptyString(postAdvancePaymentRequest.UserID));
                if (postAdvancePaymentRequest.Lines != null)
                {
                    oChildren = oGeneralData.Child("ADVANCEPAYMENTROW");
                    foreach (var a in postAdvancePaymentRequest.Lines)
                    {
                        oChild = oChildren.Add();
                        oChild.SetProperty("U_EmployeeCode", ClearEmptyString.clearEmptyString(a.BpCode));
                        oChild.SetProperty("U_AccountCode", ClearEmptyString.clearEmptyString(a.AccountCode));
                        oChild.SetProperty("U_Amount", a.amount);
                    }
                }
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                #endregion
                _ErroreCode = 0;
                _MessageErrore = "";
                oCompany.GetLastError(out _ErroreCode, out _MessageErrore);
                _DocEntry = (int)responseoGeneralService.GetProperty("DocEntry");
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}
