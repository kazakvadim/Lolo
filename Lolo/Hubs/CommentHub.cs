using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendMessage(string user, string content, int postId)
        {
            await Clients.All.SendAsync("ReceiveComment", user, content, postId);
        }
    }
}
