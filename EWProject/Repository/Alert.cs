using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public class Alert : IAlert
    {
        public Task<ResponseData<List<GetMessage>>> GetMessagesTop5AlertByUserID(string userID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetMessage>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetMessage>(new GetRecordByDataTable(
                             "GetMessagesTop5AlertByUserID",
                             userID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetMessage>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<ResponseData<GetMessage>> GetMessagesByAlertID(string alertID)
        {
            try
            {
                return Task.FromResult(new ResponseData<GetMessage>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetMessage>(new GetRecordByDataTable(
                             "GetMessagesByAlertID",
                             alertID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable())[0],
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<GetMessage>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
        public Task<ResponseData<List<GetMessage>>> GetMessagesByUser(string userID)
        {
            try
            {
                return Task.FromResult(new ResponseData<List<GetMessage>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<GetMessage>(new GetRecordByDataTable(
                             "GetMessagesByUser",
                             userID,
                             "",
                             "",
                             "",
                             "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<GetMessage>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }

        public Task<PostResponse> PostMessages(string userID, PostMessage message)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.Add,
                             "AddAlertMessage",
                                 $"INSERT INTO {ConnectionString.CompanyDB}.\"" +
                                 $"@TBALERT\"(\"Code\",\"Name\",\"U_USERID\",\"U_MESSAGEDESCRIPTION\",\"U_ISREAD\",\"U_CREATEBY\")" +
                                 $" VALUES(({ConnectionString.CompanyDB}.\"@TBALERT_S\".nextval)," +
                                 $"''{message.MessageHeader}'',''{message.UserID}'',''{message.MessageDescription}'',''N'',''{userID}'');",
                             "",
                             "",
                             "",
                             "").GetResultDataTable()).FirstOrDefault();
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse { ErrorCode = ex.HResult, ErrorMsg = ex.Message });
            }
        }

        public Task<PostResponse> PutMessage(string userID, string alertID)
        {
            try
            {
                var obj = QueryData.ConvertDataTable<PostResponse>(new GetRecordByDataTable(
                             GetRecordByDataTable.StoreType.Add,
                             "AddAlertMessage",
                             $"UPDATE {ConnectionString.CompanyDB}.\"@TBALERT\" SET \"U_ISREAD\"=''Y'' " +
                             $"WHERE \"U_USERID\"=''{userID}'' AND \"Code\"=''{alertID}'';",
                             "",
                             "",
                             "",
                             "").GetResultDataTable()).FirstOrDefault();
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PostResponse { ErrorCode = ex.HResult, ErrorMsg = ex.Message });
            }
        }

        public Task<ResponseData<List<CSName>>> GetCSNameResponseAsync()
        {
            try
            {
                return Task.FromResult(new ResponseData<List<CSName>>
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Data = QueryData.ConvertDataTable<CSName>(new GetRecordByDataTable(
                             "GetCSNameAlert",
                             "",
                             "",
                             "",
                             "",
                             "").GetResultDataTable()),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseData<List<CSName>>
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = null!
                });
            }
        }
    }
}
