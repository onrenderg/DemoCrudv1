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
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                TempData["SuccessMessage"] = "User created successfully!";
                return RedirectToAction("Create");
            }

            return View(user);
        }


        // GET: Show all users immediately
        public ActionResult Read()
        {
            using (var context = new UserContext())
            {
                var users = context.Users.ToList();
                return View(users); // Directly pass the list to the view
            }
        }
        // Controller
        public ActionResult Update(int? editId = null)
        {
            using (var context = new UserContext())
            {
                ViewBag.EditId = editId;
                var users = context.Users.ToList();
                return View(users);
            }
        }

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
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("Update");
            }

            // If model invalid, stay in edit mode
            return RedirectToAction("Update", new { editId = user.Id });
        }



        // GET: Delete view and load users
        public ActionResult Delete()
        {
            using (var context = new UserContext())
            {
                var users = context.Users.ToList();
                return View(users); // Directly return users to the view
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
                    TempData["SuccessMessage"] = "User deleted successfully!";
                }
            }

            return RedirectToAction("Delete");
        }


    }
}