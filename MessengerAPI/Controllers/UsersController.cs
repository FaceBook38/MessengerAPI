using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MessengerAPI.Models.ViewModels;
using MessengerAPI;
using MessengerAPI;

namespace MessengerAPI.Controllers
{
    public class UsersController : Controller
    {
        private MessengerContext db = new MessengerContext();

        // GET: Users
        public ActionResult Index()
        {

            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,UserEmail,UserPassword,UserImage")] User user, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                // string path = System.IO.Path.Combine(Server.MapPath("~/images/profile"), pic);
                //  file.SaveAs(pic);

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    user.UserImage= ms.GetBuffer();
                }

            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,UserEmail,UserPassword,UserImage")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            try
            {
                //var usrResult = LoginUser(login);

                User user = db.Users.FirstOrDefault(user1 =>
                    user1.UserEmail == login.user_email && user1.UserPassword == login.user_password);

                // TODO: Add insert logic here

                if (user != null)
                {
                    Session["user_id"] = user.UserId;
                    ChatHub.userId = user.UserId;

                    {
                        //var hubConnection = new HubConnection("http://www.contoso.com/");
                        //IHubProxy stockTickerHubProxy = hubConnection.CreateHubProxy("StockTickerHub");
                        //stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));
                        //await hubConnection.Start();
                    }
                    return RedirectToAction("Index");
                }
                    else
                    {
                        return RedirectToAction("Login");
                    }
              
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session["user_id"] = null;
            return RedirectToAction("Login");
        }
    }
}
