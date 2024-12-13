using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IAlert
    {
        Task<PostResponse> PostMessages(string userID, PostMessage message);
        Task<PostResponse> PutMessage(string userID, string alertID);
        Task<ResponseData<List<GetMessage>>> GetMessagesTop5AlertByUserID(string userID);
        Task<ResponseData<List<GetMessage>>> GetMessagesByUser(string userID);
        Task<ResponseData<GetMessage>> GetMessagesByAlertID(string alertID);
        Task<ResponseData<List<CSName>>> GetCSNameResponseAsync();
    }
}
