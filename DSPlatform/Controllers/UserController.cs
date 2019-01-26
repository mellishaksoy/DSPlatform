using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using System.Configuration;
using DSPlatform.App_Start;
using DSPlatform.Models;
using MongoDB.Driver;

namespace DSPlatform.Controllers
{
    public class UserController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<User> userCollection;

        public UserController()
        {
            dbcontext = new MongoDBContext();
            userCollection = dbcontext.database.GetCollection<User>("user");
        }

        // GET: User
        public ActionResult Index()
        {
            List<User> users = userCollection.AsQueryable<User>().ToList();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<User>().SingleOrDefault(x => x.Id == userId);
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                userCollection.InsertOne(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<User>.Update
                    .Set("UserName", user.UserName)
                    .Set("Password", user.Password)
                    .Set("Name", user.Name)
                    .Set("Surname", user.Surname)
                    .Set("Age", user.Age);

                var result = userCollection.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, User user)
        {
            try
            {
                userCollection.DeleteOne(Builders<User>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
