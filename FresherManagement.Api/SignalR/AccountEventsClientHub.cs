using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FresherManagement.Api.SignalR
{
    public class AccountEventsClientHub : Hub
    {
        public async Task SendMessage(string user, string message, string myProjectId, string myProjectVal)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, myProjectId, myProjectVal);
        }
    }
}
