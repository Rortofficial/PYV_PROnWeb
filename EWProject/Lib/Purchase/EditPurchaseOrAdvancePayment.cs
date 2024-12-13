using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using SAPbobsCOM;
using System.Data;

namespace Client.Lib.Purchase
{
    public class EditPurchaseOrAdvancePayment
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _docEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _docEntry;
        public EditPurchaseOrAdvancePayment(string docEntry, EditPurchaseRequest purchaseRequests, Company _company, int projectNum, string type, string remarks)
        {
            Update(docEntry, purchaseRequests, _company, projectNum, type, remarks);
        }
        void Update(string docEntry, EditPurchaseRequest purchaseRequests, Company _company, int projectNum, string type, string remarks)
        {
            try
            {
                GeneralService oGeneralService;
                GeneralData oGeneralData;
                GeneralDataParams oGeneralParamsUpdate;
                CompanyService companyService;
                companyService = _company.GetCompanyService();
                oGeneralService = companyService.GetGeneralService("ADVANCEPAYMENT");
                oGeneralParamsUpdate = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParamsUpdate.SetProperty("DocEntry", docEntry);
                oGeneralData = oGeneralService.GetByParams(oGeneralParamsUpdate);
                var absEntry = "";
                var jobNumber = purchaseRequests.Header.Project;
                if (projectNum == 0)
                {
                    if (purchaseRequests.Header.Project != null && purchaseRequests.Header.Project != "")
                    {
                        absEntry = GetValueByID("GetAbsEntryProjectManagement", "ABSENTRY", purchaseRequests.Header.Project);
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
                purchaseRequests.Header.Project = absEntry;
                UpdateHeader.UpdateHeaderObject(purchaseRequests.Header, oGeneralData);
                oGeneralData.SetProperty("Remark", ClearEmptyString.clearEmptyString(remarks));
                UpdateChild.UpdateChildObject(purchaseRequests.EditPurchaseRows, oGeneralData, "ADVANCEPAYMENTROW", "LineId", docEntry);
                oGeneralService.Update(oGeneralData);
                if (purchaseRequests.ListOfBaseLine != null)
                {
                    foreach (var a in purchaseRequests.ListOfBaseLine)
                    {
                        QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                                     GetRecordByDataTable.StoreType.Add,
                                     "UpdateLineStatusInactivePRCOS",//define new Type for Script
                                     docEntry,
                                     a.ToString(),
                                     "",
                                     "",
                                     "").GetResultDataTable()).FirstOrDefault();
                    }
                }
                if (projectNum != 0)
                {
                    foreach (var a in purchaseRequests.EditPurchaseRows)
                    {
                        if (a.LineId != 0)
                        {
                            QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                                     GetRecordByDataTable.StoreType.Add,
                                     "UpdateLineStatusActivePRCOS",//define new Type for Script
                                     docEntry,
                                     a.LineId.ToString(),
                                     "",
                                     "",
                                     "").GetResultDataTable()).FirstOrDefault();
                        }
                    }
                }
                _docEntry = Convert.ToInt32(docEntry);
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
