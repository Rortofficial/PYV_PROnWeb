using Client.Connection;
using Client.Lib.InventoryTransfer;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Data.OleDb;

namespace Client.Repository
{
    public class Container : IContainer
    {
        #region Get
        public Task<PrintViewLayoutResponse> GetContainerStatusResponseAsync(IWebHostEnvironment _webHostEnvironment, string dateFrom, string dateTo)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\Report\\ContainerUpdateStatus.rdl";
                LocalReport lr = new LocalReport();
                var typeExtension = getTypeExport("EXCEL");
                Stream reportDefinition = File.OpenRead(path);
                lr.LoadReportDefinition(reportDefinition);
                DataTable dt = GetValueFromStore("GetContainerStatusUpdate", dateFrom, dateTo).Result;
                lr.DataSources.Add(new ReportDataSource("Report", dt));
                lr.Refresh();
                var result = lr.Render("EXCEL");
                return Task.FromResult(new PrintViewLayoutResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = result,
                    ApplicationType = typeExtension.Item2,
                    FileName = (typeExtension.Item1 == "PDF" && "ContainerUpdateStatus" == "") ? "" : $"{"ContainerUpdateStatus" + typeExtension.Item3}"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PrintViewLayoutResponse
                {
                    ErrorCode = ex.GetHashCode(),
                    ErrorMessage = ex.Message
                });
            }
        }
        public Task<ResponseData<List<GetContainerUpdateStatus>>> GetLoadExcelPathAsync(List<IFormFile> MyUploader)
        {
            try
            {
                if (MyUploader != null)
                {
                    //string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "mediaUpload");
                    string uploadsFolder = Path.Combine(Configure.FileUploadPath);
                    string filePath;
                    dynamic fileStream;
                    foreach (var MyUploaders in MyUploader)
                    {
                        filePath = Path.Combine(uploadsFolder, MyUploaders.FileName);
                        using (fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            MyUploaders.CopyTo(fileStream);
                        }
                        #region Export
                        var pathToExcel = filePath;
                        var sheetName = "Sheet1";

                        //Use this connection string if you have Office 2007+ drivers installed and 
                        //your data is saved in a .xlsx file
                        var connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;
                                                  Data Source={pathToExcel};
                                                  Extended Properties=""Excel 12.0 Xml;HDR=YES""";

                        //Creating and opening a data connection to the Excel sheet 
                        using (var conn = new OleDbConnection(connectionString))
                        {
                            conn.Open();

                            var cmd = conn.CreateCommand();
                            cmd.CommandText = $"SELECT * FROM [{sheetName}$]";
                            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                            dataAdapter = new OleDbDataAdapter(cmd);
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);
                            var obj = new List<GetContainerUpdateStatus>();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                if (dataTable.Rows.IndexOf(row) != 0 && !string.IsNullOrEmpty(row[0].ToString()))
                                {
                                    obj.Add(new GetContainerUpdateStatus
                                    {
                                        Type = row[0].ToString(),
                                        ContainerNo = row[1].ToString(),
                                        ContainerNo2 = row[2].ToString(),
                                        Owner = row[3].ToString(),
                                        ContainerType = row[4].ToString(),
                                        LoadingDate = row[5].ToString(),
                                        ETABorderDate = row[6].ToString(),
                                        CrossBorderDate = row[7].ToString(),
                                        JOBNO = row[8].ToString(),
                                        TruckProvince = row[9].ToString(),
                                        TrailerNo = row[10].ToString(),
                                        DriverName1 = row[11].ToString(),
                                        Transporter = row[12].ToString(),
                                        Shipper = row[13].ToString(),
                                        Consignee = row[14].ToString(),
                                        PlaceOfLoading = row[15].ToString(),
                                        PlaceOfDelivery = row[16].ToString(),
                                        LOLOYard = row[17].ToString(),
                                        STUFFING_UNSTUFFING = row[18].ToString(),
                                        ThaiForwarder = row[19].ToString(),
                                        OverseaForwarder = row[20].ToString(),
                                        OverseaTrucker = row[21].ToString(),
                                        RecordedBy = row[22].ToString(),
                                        OriginCode = row[23].ToString(),
                                        OriginName = row[24].ToString(),
                                        DestinationCode = row[25].ToString(),
                                        DestinationName = row[26].ToString(),
                                    });
                                }
                            }
                            return Task.FromResult(new ResponseData<List<GetContainerUpdateStatus>>
                            {
                                ErrorCode = 0,
                                Data = obj
                            });
                        }
                        #endregion
                    }

                    return Task.FromResult(new ResponseData<List<GetContainerUpdateStatus>>
                    {
                        ErrorCode = -1,
                        ErrorMessage = "Internal Error Contact System Admin",
                    });
                }
            }
            catch (Exception ex)
            {
                Task.FromResult(new ResponseData<List<GetContainerUpdateStatus>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                });
            }
            return Task.FromResult(new ResponseData<List<GetContainerUpdateStatus>>
            {
                ErrorCode = -1,
                ErrorMessage = "Internal Error Contact System Admin",
            });
        }
        public Task<ResponseData<List<GetListOfInventoryTransfer>>> GetListInventoryTransfer()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetListOfInventoryTransfer>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetListOfInventoryTransfer>(new GetRecordByDataTable(
                             "GetListOfInventoryTransfer",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable())
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetListOfInventoryTransfer>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<GetInventoryTransferDetail>> GetInventoryTransferDetailByDocEntry(string docEntry)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<GetInventoryTransferDetail>(new GetRecordByDataTable(
                             "GetInventoryTransferDetailByDocEntry",
                             docEntry,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())[0];
                obj.Lines = QueryData.ConvertDataTable<GetInventoryTransferDetailLine>(new GetRecordByDataTable(
                             "GetInventoryTransferDetailRowByDocEntry",
                             docEntry,
                             "",
                             "",
                             "",
                             "").GetResultDataTable());
                return Task.FromResult(new ResponseData<GetInventoryTransferDetail>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = obj
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetInventoryTransferDetail>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        #endregion
        #region Post
        public Task<PostResponse> InventoryTransfer(PostInventoryTransfer postInventoryTransfer)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = InventoryTransfers._Add(postInventoryTransfer, SAP_Driver_oCompany._CheckingStatusOCompany());
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
        #endregion
        #region Delete
        public Task<PostResponse> DeleteInventoryTransfer(string docEntry)
        {
            //SapConnection login = new();
            //if (login.LErrCode == 0)
            //{
            var a = InventoryTransfers._Cancel(Convert.ToInt32(docEntry), SAP_Driver_oCompany._CheckingStatusOCompany());
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
        #endregion
        #region Function Private
        private Tuple<string, string, string> getTypeExport(string type)
        {
            if (type == "PDF")
            {
                return Tuple.Create("PDF", "application/pdf", ".pdf");
            }
            else if (type == "WORD")
            {
                return Tuple.Create("WORD", "application/msword", ".doc");
            }
            else if (type == "EXCEL")
            {
                return Tuple.Create("EXCEL", "application/xlsx", ".xls");
            }
            return Tuple.Create("PDF", "application/pdf", "");
        }
        private Task<DataTable> GetValueFromStore(string type, string dateFrom, string dateTo)
        {
            return Task.FromResult(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.Transaction,
                             type,
                             dateFrom,
                             dateTo,
                             "",
                             "",
                             "").GetResultDataTable());
        }

        #endregion
    }
}
