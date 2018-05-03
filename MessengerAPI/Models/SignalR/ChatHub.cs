using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Security.Provider;

namespace MessengerAPI
{
    [HubName("Chat")]
    public class ChatHub : Hub
    {
        MessengerContext _ctx = new MessengerContext();
        public   static List<ConnectedUser> ConnectedUsers = new List<ConnectedUser>();
        public static int userId;
        public  void connect(int userId) {
             
                string cId = Context.ConnectionId;
               // check if connection id exists
                if (ConnectedUsers.Count(u => u.ConnectionId == cId) == 0)
                {
                    

                    //client side method
                    Clients.AllExcept(cId).NewUserOnline(userId, cId);
                }
            }

        public override Task OnConnected()
        {
            string cId = Context.ConnectionId;
            
            if (ConnectedUsers.Count(u => u.ConnectionId == cId) == 0)
            {
                ConnectedUsers.Add(new ConnectedUser() { UserId = userId, ConnectionId = cId });

                //client side method
                Clients.AllExcept(cId).NewUserOnline(userId, cId);
            }
            return base.OnConnected();
        }

        public void sendmessage(string message, int receiverId, int senderId)
        {
            //implement in client to get called by server and executed in client

            //recevier connection id
            ConnectedUser receiver = ConnectedUsers.FirstOrDefault(c => c.UserId == receiverId);
            if (receiver != null)
            {
                Clients.Client(receiver.ConnectionId).send(message);
            }
            //
            //Clients.All.send(message);
            Clients.Caller.send(message);

            //save message in DB
            _ctx.UserMessages.Add(new UserMessage()
            {
                MessageContent = message,
                SenderId = senderId,
                ReceiverId = receiverId,
            });
            _ctx.SaveChanges();
        }
    }
}
