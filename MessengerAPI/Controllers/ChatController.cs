using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessengerAPI.Models.MessengerModel;
namespace MessengerAPI.Controllers
{
    public class ChatController : Controller
    {
        MessengerContext context = new MessengerContext();
        // GET: Chat
        public ActionResult Chat(int? id)
        {
            int senderId = Convert.ToInt32(Session["user_id"]);
            OldMessages oldMessages = new OldMessages();
             oldMessages.Receiver= context.Users.FirstOrDefault(user0 => user0.UserId == id);
             oldMessages.Sender= context.Users.FirstOrDefault(user0 => user0.UserId == senderId);
              oldMessages.oldMessages = context.UserMessages.Where(m => ((m.SenderId ==senderId && m.ReceiverId == id) || (m.SenderId == id && m.ReceiverId == senderId))).ToList();

            if (id == null)
                return RedirectToAction("index", "users");
            else
                if(oldMessages.Receiver != null)
                 return View(oldMessages);
            return RedirectToAction("index", "users");


        }


    }
}