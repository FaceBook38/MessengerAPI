using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessengerAPI.Controllers
{
    public class ChatController : Controller
    {
        MessengerContext context = new MessengerContext();
        // GET: Chat
        public ActionResult Chat(int? id)
        {
            User reciver_user = context.Users.FirstOrDefault(user0 => user0.UserId == id);
            if (id == null)
                return RedirectToAction("index", "users");
            else
                if(reciver_user != null)
                 return View(reciver_user);

            return RedirectToAction("index", "users");


        }


    }
}