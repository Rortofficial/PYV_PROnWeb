using Client.Connection;
using Client.Lib.AdvancePayment;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class ApproveDocument : IApproveDocument
    {
        #region GET
        public Task<GetDetailInformationDocumentApproveResponse> GetDetailInformationDocumentApproveResponseAsync(string docNum)
        {
            try
            {
                var listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "Header",
                             docNum,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var lines = new List<PurchaseRequestLine>();
                    foreach (DataRow dataRowLine in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "Lines",
                             docNum,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        lines.Add(new PurchaseRequestLine
                        {
                            ItemCode = dataRowLine["ITEMCODE"].ToString()!,
                            ItemName = dataRowLine["ITEMNAME"].ToString()!,
                            Amount = Convert.ToDecimal(dataRowLine["PRICE"].ToString()!),
                            Origin = dataRowLine["ORIGIN"].ToString()!,
                            Destination = dataRowLine["DESTINATION"].ToString()!,
                            remark = dataRowLine["REMARKS"].ToString()!,
                            ServiceType = dataRowLine["SERVICETYPE"].ToString()!,
                            ServiceTypeID = dataRowLine["SERVICETYPEID"].ToString()!,
                            LineNumPO = Convert.ToInt32(dataRowLine["LINENUMPO"].ToString()!),
                            LineNumAD = Convert.ToInt32(dataRowLine["LINENUMAD"].ToString()!),
                            LineNumPR = Convert.ToInt32(dataRowLine["LINENUMPR"].ToString()!),
                            DistributionRule = dataRowLine["DistributionRule"].ToString()!,
                            RefInv = dataRowLine["RefInv"].ToString()!,
                            VatCode = dataRowLine["TaxCode"].ToString()!,
                            VatRate = Convert.ToDouble(dataRowLine["TaxRate"])!,
                            JobNo = dataRowLine["JobNo"].ToString()!,
                            TruckNumber = dataRowLine["TruckNumber"].ToString()!,
                            TransportationNo = dataRowLine["TruckCode"].ToString()!,
                            BaseLineID = dataRowLine["BaseLineID"].ToString()!,
                            PlaceOfLoading = dataRowLine["PlaceOfLoading"].ToString()!,
                            PlaceOfDelivery = dataRowLine["PlaceOfDelivery"].ToString()!,
                            TrailerType = dataRowLine["TrailerType"].ToString()!,
                            TruckType = dataRowLine["SERVICETYPE"].ToString()!,
                            LoadingDate = dataRowLine["LoadingDate"].ToString()??""
                        });
                    }
                    var jobSheetDocEntry = new List<string>();
                    foreach (DataRow dataRowLine in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "GetDocEntryByJobSheetTrucking",
                             dataRow["PROJECTNUMBER"].ToString()!,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        jobSheetDocEntry.Add(dataRowLine["DocEntry"].ToString());
                    }
                    var confirmBookingDocEntry = new List<string>();
                    foreach (DataRow dataRowLine in new GetRecordByDataTable(
                             "GetDetailAdvancePaymentByDocEntry",
                             "GetDocEntryByConfirmBooking",
                             dataRow["PROJECTNUMBER"].ToString()!,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        confirmBookingDocEntry.Add(dataRowLine["DocEntry"].ToString());
                    }
                    listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest
                    {
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        RefNo = dataRow["DOCNUM"].ToString()!,
                        JobNo = dataRow["PROJECTNUMBER"].ToString()!,
                        IssueDate = dataRow["ISSUEDATE"].ToString(),
                        DueDate = dataRow["DUEDATE"].ToString(),
                        VendorCode = dataRow["VENDORCODE"].ToString()!,
                        VendorName = dataRow["VENDORNAME"].ToString()!,
                        AmountCurrency = dataRow["CURRENCY"].ToString()!,
                        IssueBy = Convert.ToInt32(dataRow["EMPLOYEEID"].ToString()),
                        IssueByName = dataRow["EMPLOYEENAME"].ToString(),
                        GrandTotal = Convert.ToDecimal(dataRow["AMOUNT"].ToString()),
                        AmountTHB = Convert.ToDecimal(dataRow["AMOUNTTHB"].ToString()),
                        BankAccount = dataRow["BANKACCOUNT"].ToString()!,
                        BranchName = dataRow["BRANCH"].ToString()!,
                        BankName = dataRow["BANKCOUNTRY"].ToString()!,
                        Country = dataRow["BANKNAME"].ToString()!,
                        SwiftCode = dataRow["SWIFTCODE"].ToString()!,
                        Remarks = dataRow["REMARKS"].ToString()!,
                        DocNum = dataRow["DOCNUMPR"].ToString()!,
                        Reason = dataRow["REASON"].ToString(),
                        PRDocEntry = dataRow["DOCENTRYPR"].ToString(),
                        PRDocNum = dataRow["DOCNUMPRAD"].ToString(),
                        Series = dataRow["PRSeries"].ToString(),
                        MaxLineNum = Convert.ToInt32(dataRow["MAXROWPO"].ToString()),
                        DocStatus = dataRow["DOCUMENTSTATUS"].ToString(),
                        DocEntryPurchaseRequest = Convert.ToInt32(dataRow["DOCENTRYPURCHASEREQUEST"].ToString()),
                        Lines = lines,
                        ListJobSheetTruckByDocEntry = jobSheetDocEntry,
                        ListConfirmBookingByDocEntry = confirmBookingDocEntry,
                        ConfirmBookingID = confirmBookingDocEntry.FirstOrDefault(),
                    };
                }
                return Task.FromResult(new GetDetailInformationDocumentApproveResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDetailViewSalesQuotation,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetDetailInformationDocumentApproveResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetListDocumentApproveByType>>> GetListDocumentApproveByTypeResponseAsync(string dateFrom, string dateTo, string documentType)
        {
            try
            {
                var listGetListDocumentApproveByType = new List<GetListDocumentApproveByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             StoreType.Transaction,
                             "GETAPPROVALLISTADVANCEPAYMENT",
                             dateFrom,
                             dateTo,
                             documentType,
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetListDocumentApproveByType.Add(new GetListDocumentApproveByType
                    {
                        DocEntry = dataRow["DOCENTRY"].ToString(),
                        DocNum = dataRow["DOCNUM"].ToString(),
                        DocNumPR = dataRow["DOCNUMPR"].ToString(),
                        DocumentType = dataRow["DOCUMENTSTATUS"].ToString(),
                        ProjectNumber = dataRow["PROJECTNUMBER"].ToString(),
                        IssueDate = dataRow["ISSUEDATE"].ToString(),
                        VendorCode = dataRow["VENDORCODE"].ToString(),
                        VendorName = dataRow["VENDORNAME"].ToString(),
                        Currency = dataRow["CURRENCY"].ToString(),
                        UserID = dataRow["EMPLOYEEID"].ToString(),
                        EmployeeName = dataRow["EMPLOYEENAME"].ToString(),
                        Amount = Convert.ToDouble(dataRow["AMOUNT"].ToString()),
                        Remarks = dataRow["REMARK"].ToString(),
                        Type = dataRow["Type"].ToString(),
                        ApproveBy = dataRow["ApproveBy"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetListDocumentApproveByType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetListDocumentApproveByType
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListDocumentApproveByType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Post
        public Task<DeleteResponse> OnApprovePurchaseRequestResponseAsync(string docNum, string RemarksReson,string ApproveBy,string CostCenter)
        {

            if (GetQuery.GetValueByID("GETSTATUSPURCHASEREQUESTAPPROVE", "ValueValid", docNum) == "0")
            {
                //Documents oGoodReturn;
                //Company oCompany;
                //var Retval = 0;
                //SapConnection login = new();
                //if (login.LErrCode == 0)
                //{
                var a = AdvancePayments._Approve(docNum: docNum, RemarksReson,ApproveBy, CostCenter, SAP_Driver_oCompany._CheckingStatusOCompany());
                return Task.FromResult(new DeleteResponse
                {
                    ErrorCode = a.ErroreCode,
                    ErrorMsg = a.ErroreMessage,
                    DocEntry = a.DocEntry.ToString()
                });
                //}
                //return Task.FromResult(new DeleteResponse
                //{
                //    ErrorCode = login.LErrCode,
                //    ErrorMsg = login.SErrMsg
                //});

            }
            else
            {
                return Task.FromResult(new DeleteResponse
                {
                    ErrorCode = -99,
                    ErrorMsg = "Document already Approve Or Reject already please kindly try to Refresh it again"
                });
            }
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            //    return Task.FromResult(new DeleteResponse
            //    {
            //        ErrorCode = login.LErrCode,
            //        ErrorMsg = login.SErrMsg
            //    });
            //}
            //return Task.FromResult(new DeleteResponse
            //{
            //    ErrorCode = login.LErrCode,
            //    ErrorMsg = login.SErrMsg
            //});

        }
        public Task<DeleteResponse> OnRejectPurchaseRequestResponseAsync(string docNum, string RemarksReson,string RejectBy)
        {
            try
            {
                if (GetQuery.GetValueByID("GETSTATUSPURCHASEREQUESTAPPROVE", "ValueValid", docNum) == "0")
                {
                    //SapConnection login = new();
                    //if (login.LErrCode == 0)
                    //{
                    var a = AdvancePayments._Reject(docNum: docNum, RemarksReson, _company: SAP_Driver_oCompany._CheckingStatusOCompany());
                    return Task.FromResult(new DeleteResponse
                    {
                        ErrorCode = a.ErroreCode,
                        ErrorMsg = a.ErroreMessage,
                        DocEntry = a.DocEntry.ToString()
                    });
                    //}
                    //return Task.FromResult(new DeleteResponse
                    //{
                    //    ErrorCode = login.LErrCode,
                    //    ErrorMsg = login.SErrMsg
                    //});
                }
                else
                {
                    return Task.FromResult(new DeleteResponse
                    {
                        ErrorCode = -99,
                        ErrorMsg = "Document already Approve Or Reject already please kindly try to Refresh it again"
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new DeleteResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMsg = ex.Message
                });
            }
        }
        #endregion
    }
}
