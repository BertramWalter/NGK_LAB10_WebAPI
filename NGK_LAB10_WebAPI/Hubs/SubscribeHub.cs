using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NGK_LAB10_WebAPI.Models;

namespace NGK_LAB10_WebAPI.Hubs
{
    public class SubscribeHub: Hub
    {
        public void Register()
        {
            
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
