using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using SAPbobsCOM;
using System.Data;

namespace Client.Lib.Purchase
{
    public class AddPurchaseOrAdvancePayment
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _docEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _docEntry;
        public AddPurchaseOrAdvancePayment(PurchaseRequest purchaseRequests, Company _company, int projectNum, string type)
        {
            Add(purchaseRequests, _company, projectNum, type);
        }
        void Add(PurchaseRequest purchaseRequests, Company _company, int projectNum, string type)
        {
            try
            {
                CompanyService companyService;
                companyService = _company.GetCompanyService();
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                GeneralData oChild;
                GeneralDataCollection oChildren;
                var absEntry = "";
                if (projectNum == 0)
                {
                    if (purchaseRequests.JobNo != null && purchaseRequests.JobNo != "")
                    {
                        absEntry = GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", purchaseRequests.JobNo);
                    }
                    else
                    {
                        absEntry = "";
                    }
                }
                else
                {
                    absEntry = projectNum.ToString();
                }
                oGeneralService = companyService.GetGeneralService("ADVANCEPAYMENT");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                oGeneralData.SetProperty("Remark", ClearEmptyString.clearEmptyString(purchaseRequests.Remarks));
                oGeneralData.SetProperty("U_NumAtCard", ClearEmptyString.clearEmptyString(purchaseRequests.RefNo));
                oGeneralData.SetProperty("U_Project", absEntry);
                oGeneralData.SetProperty("U_UserID", ClearEmptyString.clearEmptyString(purchaseRequests.IssueBy.ToString()));
                oGeneralData.SetProperty("U_IssueDate", ClearEmptyString.clearEmptyString(purchaseRequests.IssueDate));
                oGeneralData.SetProperty("U_DueDate", ClearEmptyString.clearEmptyString(purchaseRequests.DueDate));
                oGeneralData.SetProperty("U_PRSeries", ClearEmptyString.clearEmptyString(purchaseRequests.Series));
                oGeneralData.SetProperty("U_VendorCode", ClearEmptyString.clearEmptyString(purchaseRequests.VendorCode));
                oGeneralData.SetProperty("U_AmountTHB", Convert.ToDouble(purchaseRequests.AmountTHB));
                oGeneralData.SetProperty("U_Amount", Convert.ToDouble(purchaseRequests.GrandTotal));
                oGeneralData.SetProperty("U_AdvanceType", type);
                oGeneralData.SetProperty("U_ApproveStatus", "P");
                oGeneralData.SetProperty("U_CurrencyType", ClearEmptyString.clearEmptyString(purchaseRequests.AmountCurrency));
                //Add Line
                foreach (var a in purchaseRequests.Lines)
                {
                    oChildren = oGeneralData.Child("ADVANCEPAYMENTROW");
                    oChild = oChildren.Add();
                    oChild.SetProperty("U_ItemCode", ClearEmptyString.clearEmptyString(a.ItemCode));
                    oChild.SetProperty("U_Qty", 1);
                    oChild.SetProperty("U_Price", Convert.ToDouble(a.Amount));
                    oChild.SetProperty("U_Origin", ClearEmptyString.clearEmptyString(a.Origin));
                    oChild.SetProperty("U_Destination", ClearEmptyString.clearEmptyString(a.Destination));
                    oChild.SetProperty("U_Remarks", ClearEmptyString.clearEmptyString(a.remark));
                    oChild.SetProperty("U_PriceList", ClearEmptyString.clearEmptyString(purchaseRequests.AmountCurrency));
                    oChild.SetProperty("U_DistributionRule", ClearEmptyString.clearEmptyString(a.DistributionRule));
                    oChild.SetProperty("U_RefInv", ClearEmptyString.clearEmptyString(a.RefInv));
                    oChild.SetProperty("U_JobNumber", ClearEmptyString.clearEmptyString(a.JobNo));
                    oChild.SetProperty("U_TaxCode", ClearEmptyString.clearEmptyString(a.VatCode));
                    oChild.SetProperty("U_TruckNo", ClearEmptyString.clearEmptyString(a.TruckNumber));
                    oChild.SetProperty("U_ContainerType", ClearEmptyString.clearEmptyString(a.ServiceTypeID));
                }
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                _docEntry = (int)responseoGeneralService.GetProperty("DocEntry");
                foreach (var a in purchaseRequests.Lines)
                {
                    if (a.LineNumber != 0)
                    {
                        var obj = QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                                 GetRecordByDataTable.StoreType.Add,
                                 "UpdateConfirmBookingStatus",
                                 purchaseRequests.ConfirmBookingID,
                                 a.LineNumber.ToString(),
                                 "",
                                 "",
                                 "").GetResultDataTable()).FirstOrDefault();
                    }
                }
            }catch(Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
        string GetValueByID(string type, string field, string id)
        {
            try
            {
                var getBookingSheetByUsersList = new List<GetBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             type,
                             id,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    return dataRow[field].ToString()!;
                }
                return "-1";
            }
            catch
            {
                return "-1";
            }
        }
    }
}
