using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Purchase
{
    public class UpdatePurchaseOrAdvancePayment
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _docEntry;
        //public Company _Company () => _company;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _docEntry;
        //private Company _company;
        public UpdatePurchaseOrAdvancePayment(UpdatePurchaseRequest purchaseRequests, Company _company)
        {
            Update(purchaseRequests, _company);
        }
        void Update(UpdatePurchaseRequest purchaseRequests, Company _company)
        {
            CompanyService companyService;
            companyService = _company.GetCompanyService();
            #region Update DocEntry Of BookingSheet link with Confirm BookingSheet
            GeneralService oGeneralServiceUpdateAdvancePayment;
            GeneralData oAdvancePaymentGeneralDataUpdate;
            GeneralData oAdvancePaymentChildUpdate;
            GeneralDataCollection oAdvancePaymentChildrenUpdate;
            GeneralDataParams oAdvancePaymentGeneralParamsUpdate;
            oGeneralServiceUpdateAdvancePayment = companyService.GetGeneralService("ADVANCEPAYMENT");
            oAdvancePaymentGeneralParamsUpdate = (GeneralDataParams)oGeneralServiceUpdateAdvancePayment.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
            oAdvancePaymentGeneralParamsUpdate.SetProperty("DocEntry", purchaseRequests.DocEntryAD);
            oAdvancePaymentGeneralDataUpdate = oGeneralServiceUpdateAdvancePayment.GetByParams(oAdvancePaymentGeneralParamsUpdate);
            //oBookingGeneralDataUpdate.SetProperty("U_CONFIRMBOOKINGSHEETID", DocEntryConfirmBookingSheet);
            //Add Childrence
            oAdvancePaymentChildrenUpdate = oAdvancePaymentGeneralDataUpdate.Child("ADVANCEPAYMENTROW");
            foreach (var a in purchaseRequests.Lines)
            {
                oAdvancePaymentChildUpdate = oAdvancePaymentChildrenUpdate.Item(a.LineNumAD - 1);
                oAdvancePaymentChildUpdate.SetProperty("U_Paid", a.Paid);
            }
            oGeneralServiceUpdateAdvancePayment.Update(oAdvancePaymentGeneralDataUpdate);
            #endregion
            _docEntry = purchaseRequests.DocEntryAD;
            _company.GetLastError(out _ErroreCode, out _MessageErrore);
        }
    }
}
