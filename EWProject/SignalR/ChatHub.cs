using Client.Repository;
using Microsoft.AspNetCore.SignalR;


namespace Client.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IAlert alert;

        public ChatHub(IAlert alert)
        {
            this.alert = alert;
        }
        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //}
        public async Task SendMessage(string CreateByuser, string userID
                                    , string HeaderMessage, string DescriptionMessage)
        {
            await alert.PostMessages(CreateByuser, new Models.Request.PostMessage { UserID = userID, MessageHeader = HeaderMessage, MessageDescription = DescriptionMessage });
            await Clients.All.SendAsync("ReceiveMessage", userID);
        }
    }
}
