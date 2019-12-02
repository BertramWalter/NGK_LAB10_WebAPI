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
        public async Task Register()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "1", CancellationToken.None);
        }
    }
}
