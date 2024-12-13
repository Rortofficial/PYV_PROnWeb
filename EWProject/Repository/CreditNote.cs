using Client.Connection;
using Client.Lib.CreditNote;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;
using static Client.Connection.GetRecordByDataTable;

namespace Client.Repository
{
    public class CreditNote : ICreditNote
    {
        #region Get
        public Task<ResponseData<List<GetARInvoice>>> GetARInvoiceResponseAsync(string Department)
        {
            try
            {
                var listGetARInvoice = new List<GetARInvoice>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetARInvoiceWithJobNumber",
                             Department,
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetARInvoice.Add(new GetARInvoice
                    {
                        ID = Convert.ToInt32(dataRow["DOCENTRY"].ToString()),
                        Row = Convert.ToInt32(dataRow["ROWNUM"].ToString()),
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        WebId = dataRow["WEBID"].ToString()!,
                        JobNumber = dataRow["JOBNUMBER"].ToString()!,
                        CustomerCode = dataRow["CustomerCode"].ToString()!,
                        CustomerName = dataRow["CustomerName"].ToString()!,
                        IssueDate = dataRow["ISSUEDATE"].ToString()!,
                        DocTotal = Convert.ToDouble(dataRow["DOCTOTAL"].ToString()),
                        Comment = dataRow["REMARK"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetARInvoice>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetARInvoice
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetARInvoice>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetARInvoiceWithJobNumberByDocEntry>> GetARInvoiceWithJobNumberByDocEntryResponseAsync(string docEntry)
        {
            try
            {
                var listGetARInvoiceWithJobNumberByDocEntry = new GetARInvoiceWithJobNumberByDocEntry();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetARInvoiceWithJobNumberByDocEntry",
                             "Header",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var lines = new List<GetARInvoiceWithJobNumberByDocEntryLine>();
                    foreach (DataRow dataRowLine in new GetRecordByDataTable(
                             "GetARInvoiceWithJobNumberByDocEntry",
                             "Lines",
                             docEntry,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                    {
                        lines.Add(new GetARInvoiceWithJobNumberByDocEntryLine
                        {
                            RowNum = Convert.ToInt32(dataRowLine["ROWNUM"].ToString()),
                            LineNumber = Convert.ToInt32(dataRowLine["LINENUM"].ToString()),
                            ItemCode = dataRowLine["ITEMCODE"].ToString()!,
                            ItemName = dataRowLine["ITEMNAME"].ToString()!,
                            ServiceType = dataRowLine["SERVICETYPE"].ToString()!,
                            LineTotal = Convert.ToDouble(dataRowLine["LINETOTAL"].ToString()),
                            Remarks = dataRowLine["REMARK"].ToString()!,
                        });
                    }
                    listGetARInvoiceWithJobNumberByDocEntry = new GetARInvoiceWithJobNumberByDocEntry
                    {
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        CustomerRef = dataRow["CUSTOMERREF"].ToString()!,
                        JobNumber = dataRow["JOBNUMBER"].ToString()!,
                        CustomerCode = dataRow["CustomerCode"].ToString()!,
                        CustomerName = dataRow["CustomerName"].ToString()!,
                        IssueDate = dataRow["ISSUEDATE"].ToString()!,
                        DocTotal = Convert.ToDouble(dataRow["DOCTOTAL"].ToString()),
                        Remark = dataRow["REMARK"].ToString()!,
                        Status = dataRow["STATUS"].ToString()!,
                        ContactPerson = dataRow["CONTACTPERSON"].ToString()!,
                        Currency = dataRow["CURRENCY"].ToString()!,
                        Lines = lines
                    };
                }
                return Task.FromResult(new ResponseData<GetARInvoiceWithJobNumberByDocEntry>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetARInvoiceWithJobNumberByDocEntry,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetARInvoiceWithJobNumberByDocEntry>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetAdvancePaymentClearing>>> GetCreditNoteListResponseAsync(string dateFrom, string dateTo, string value, string userID)
        {
            try
            {
                var listGetAdvancePaymentClearing = new List<GetAdvancePaymentClearing>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                            "GetARCreditMemoListOnReduceInvoice",
                            dateFrom,
                            dateTo,
                            value,
                            userID,
                            "").GetResultDataTable().Rows)
                {
                    listGetAdvancePaymentClearing.Add(new GetAdvancePaymentClearing
                    {
                        Row = dataRow["ROWNUM"].ToString()!,
                        DocEntry = dataRow["DOCENTRY"].ToString()!,
                        DocNum = dataRow["DOCNUM"].ToString()!,
                        RefNo = dataRow["REFNO"].ToString()!,
                        JobNumber = dataRow["JOBNUMBER"].ToString()!,
                        VendorCode = dataRow["VENDORCODE"].ToString()!,
                        VendorName = dataRow["VENDORNAME"].ToString()!,
                        IssueDate = dataRow["ISSUEDATE"].ToString()!,
                        DocTotal = Convert.ToDouble(dataRow["DOCTOTAL"].ToString()),
                        Remarks = dataRow["REMARKS"].ToString(),
                    });
                }
                return Task.FromResult(new ResponseData<List<GetAdvancePaymentClearing>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetAdvancePaymentClearing
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetAdvancePaymentClearing>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<GetDetailInformationDocumentApproveResponse> GetDetailCreditMemoResponseAsync(string docNum,string LayoutType)
        {
            try
            {
                var listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetDetailARCreditMemoByDocEntry",
                             "Header",
                             docNum,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    var lines = new List<PurchaseRequestLine>();
                    foreach (DataRow dataRowLine in new GetRecordByDataTable(
                             "GetDetailARCreditMemoByDocEntry",
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
                            Amount = Convert.ToDecimal(dataRowLine["PRICE"].ToString()),
                            remark = dataRowLine["REMARKS"].ToString()!,
                            ServiceType = dataRowLine["SERVICETYPE"].ToString()!
                        });
                    }
                    listGetDetailViewSalesQuotation = new Models.Request.PurchaseRequest
                    {
                        RefNo = dataRow["DOCNUM"].ToString(),
                        PRDocNum = dataRow["DOCNUMPR"].ToString(),
                        DocEntry = dataRow["DOCENTRY"].ToString(),
                        DueDate = dataRow["DUEDATE"].ToString(),
                        IssueDate = dataRow["ISSUEDATE"].ToString(),
                        VendorCode = dataRow["VENDORCODE"].ToString(),
                        VendorName = dataRow["VENDORNAME"].ToString(),
                        AmountCurrency = dataRow["CURRENCY"].ToString(),
                        IssueBy = Convert.ToInt32(dataRow["EMPLOYEEID"].ToString()),
                        IssueByName = dataRow["EMPLOYEENAME"].ToString(),
                        AmountTHB = Convert.ToDecimal(dataRow["AMOUNT"].ToString()),
                        Remarks = dataRow["REMARKS"].ToString(),
                        Lines = lines
                    };
                }
                return Task.FromResult(new GetDetailInformationDocumentApproveResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetDetailViewSalesQuotation,
                    ListLayoutPrint = GetLayoutShowByTypeResponsenAsync(LayoutType).Result.Data
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
        #endregion
        #region Post
        public Task<PostResponse> postARCreditMemoResponse(PostARCreditMemoRequest postARCreditMemoRequest)
        {
            var a = CreditNotes._Add(postARCreditMemoRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
        }
        #endregion
        #region Function
        private Task<ResponseData<List<GetLayoutShowByType>>> GetLayoutShowByTypeResponsenAsync(string LayoutType)
        {
            try
            {
                var listGetLayoutShowByType = new List<GetLayoutShowByType>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             storeType: StoreType.Transaction,
                             LayoutType,
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    listGetLayoutShowByType.Add(new GetLayoutShowByType
                    {
                        Code = dataRow["CODE"].ToString()!,
                        Name = dataRow["NAME"].ToString()!
                    });
                }
                return Task.FromResult(new ResponseData<List<GetLayoutShowByType>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = listGetLayoutShowByType
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetLayoutShowByType>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
    }
}
