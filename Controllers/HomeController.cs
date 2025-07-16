using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserManagementCrudApp.Models;



namespace UserManagementCrudApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // Create View
        public ActionResult Create()
        {
            return View(); 
        }


        // CreateView Operation
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (var context = new UserContext())
                {
                    context.Users.Add(user);        // Add user to DbSet
                    context.SaveChanges();          // Commit to database
                }
                return RedirectToAction("Index");   // Redirect after success
            }

            return View(user); // Show form again if validation fails
        }
        //using (var context = new UserContext()): Creates an instance of the EF context(disposes after use).

        //context.Users.Add(user): Stages the new user for insertion.

        //context.SaveChanges(): Executes the SQL INSERT to persist the data in the Users table.


        // GET: Show form only
        public ActionResult Read()
        {
            return View(); // Model is null at first
        }

        // POST: Fetch and show all users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Read(string load)
        {
            using (var context = new UserContext())
            {
                var users = context.Users.ToList();
                return View(users); // Pass list of users to view
            }
        }


        // GET: Show update page
        public ActionResult Update()
        {
            return View();
        }

        // POST: Load users into view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string load)
        {
            using (var context = new UserContext())
            {
                var users = context.Users.ToList();
                return View(users); // Return list to Update.cshtml
            }
        }

        // POST: Update operation handler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOperation(User user)
        {
            if (ModelState.IsValid)
            {
                using (var context = new UserContext())
                {
                    context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Update");
            }

            return View("UpdateOperation", user); // Optional edit view
        }




        // GET: Delete view
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Show all users with delete buttons
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string load)
        {
            using (var context = new UserContext())
            {
                var users = context.Users.ToList();
                return View(users);
            }
        }

        // POST: Delete by ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOperation(int id)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.Find(id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Delete");
        }



    }
}