using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DSPlatform.App_Start;
using DSPlatform.Models;
using MongoDB.Driver;

namespace DSPlatform.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private MongoDBContext dbcontext;
        private IMongoCollection<User> userCollection;

        public HomeController()
        {
            dbcontext = new MongoDBContext();
            userCollection = dbcontext.database.GetCollection<User>("user");
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserInfo()
        {
            Models.User user = GetUserInfo("melis", "12345");
            return View();
        }
        public Models.User GetUserInfo(string userName, string password)
        {
            Models.User user = new Models.User { Name = "Melis Sevil", Surname = "Aksoy", Age = 27 };

            return user;
            
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Models.User model)
        {
            ViewBag.Message = "Your login page.";

            var user = userCollection.AsQueryable<User>().SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            if (user != null)
            {
                var authTicket = new FormsAuthenticationTicket(
                                                  1,
                                                 model.UserName,  //user id
                                                  DateTime.Now,
                                                  DateTime.Now.AddMinutes(20),  // expiry
                                                  model.RememberMe,  //true to remember
                                                  "", //roles 
                                                  "/"
                                                );

                //encrypt the ticket and add it to a cookie
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                FormsAuthentication.RedirectFromLoginPage(model.UserName, false);

                return RedirectToAction("Index","User");
            }
            else
            {
                ViewBag.Failedcount = model;
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}