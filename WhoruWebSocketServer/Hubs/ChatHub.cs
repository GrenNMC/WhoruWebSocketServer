using Microsoft.AspNetCore.SignalR;
namespace WhoruWebSocketServer.Hubs
{
    public class ChatHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceviedMessage", $"{Context.ConnectionId} has connected");
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.SendAsync("ReceviedMessage", $"{Context.ConnectionId} has Added to {groupName}");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.SendAsync("ReceviedMessage", $"{Context.ConnectionId} has Outed to {groupName}");
        }

        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }
    }
}
