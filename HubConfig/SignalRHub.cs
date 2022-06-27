using Microsoft.AspNetCore.SignalR;

namespace SignalRWeb.HubConfig
{
    public class SignalRHub : Hub
    {
        public async Task askServer(string someTestText)
        {
            string stringTemplate;
            if(someTestText == "hey")
            {
                stringTemplate = "The message was working properly";
            }
            else
            {
                stringTemplate = "The message was null";
            }
            await Clients.Clients(this.Context.ConnectionId).SendAsync("askServerResponse", stringTemplate);
        }

        public async Task sendMessage(string welcomeMessage)
        {
            welcomeMessage = "Karibu Nairobi";
            await Clients.All.SendAsync("Greetings", welcomeMessage);
        }

    }
}
