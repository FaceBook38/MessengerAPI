using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessengerAPI.Models.MessengerModel
{
    public class OldMessages
    {
        public List<UserMessage> oldMessages { get; set; }
          //  = new List<UserMessage>();
          public  User Sender { get; set; }
          public  User Receiver { get; set; }

    }
}