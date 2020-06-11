using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(Message msg) 
        {
            await Clients.All.SendAsync("ReceiveMessage", msg);
        }
    }
}
