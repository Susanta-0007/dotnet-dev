using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class UserController : Controller
    {
        // Intialize the hardcoded data of users!
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Address = "123 Main St" },
            new User { Id = 2, Name = "Jane Smith", Address = "456 Oak Ave" },
            new User { Id = 3, Name = "Bob Wilson", Address = "789 Pine Rd" }
        };

        // To get all the users!
        public ActionResult Index()
        {
            return View(users);
        }

        // To get a specific user by his ID
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return PartialView("_EditModal", user);
        }

        // Update/Edit a existing user
        [HttpPost]
        public ActionResult Edit(User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Address = updatedUser.Address;
            }
            return RedirectToAction("Index");
        }

        // Delete a specific user
        [HttpPost]
        public ActionResult Delete(int id)
        {
            users.RemoveAll(u => u.Id == id);
            return RedirectToAction("Index");
        }
    }
}