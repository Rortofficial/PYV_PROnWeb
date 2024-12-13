using Client.Connection;
using Client.Lib.ARDebitNote;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using System.Data;

namespace Client.Repository
{
    public class DeditNote : IDeditNote
    {
        public Task<ResponseData<GetARInvoiceWithJobNumberByDocEntry>> GetARInvoiceWithJobNumberByDocEntryResponseAsync(string docEntry, string department)
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
                             "LinesDebit",
                             docEntry,
                             department,
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
        public Task<ResponseData<List<GetARInvoice>>> GetARInvoiceResponseAsync(string department)
        {
            try
            {
                var listGetARInvoice = new List<GetARInvoice>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetARInvoiceWithJobNumberForDebit",
                             department,
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

        public Task<ResponseData<List<GetAdvancePaymentClearing>>> GetCreditNoteListResponseAsync(string dateFrom, string dateTo, string type, string userID)
        {
            try
            {
                var listGetAdvancePaymentClearing = new List<GetAdvancePaymentClearing>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GetARDeditMemoListOnReduceInvoice",
                             dateFrom,
                             dateTo,
                             type,
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

        public Task<PostResponse> postARDeditMemoResponse(PostARCreditMemoRequest postARCreditMemoRequest)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = ARDebitNotes._Add(postARCreditMemoRequest, SAP_Driver_oCompany._CheckingStatusOCompany());
            return Task.FromResult(new PostResponse
            {
                ErrorCode = a.ErroreCode,
                ErrorMsg = a.ErroreMessage,
                DocEntry = a.DocEntry.ToString()
            });
            //}
            //return Task.FromResult(new PostResponse
            //{
            //    ErrorCode = login.LErrCode,
            //    ErrorMsg = login.SErrMsg
            //});
        }
    }
}
