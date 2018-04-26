using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Belt.Models;

namespace Belt.Controllers
{
    public class HomeController : Controller
    {
        private BeltContext _context;
 
        public HomeController(BeltContext context)
        {
            _context = context;
        }
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegistrationViewModel regUser)
        {

            // Check uniqueness of user's email
            if(_context.Users.Where(u => u.Email == regUser.Email).ToList().Count() > 0)
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            if(ModelState.IsValid)
            {
                PasswordHasher<RegistrationViewModel> hasher = new PasswordHasher<RegistrationViewModel>();

                User NewPerson = new User
                {
                    FirstName = regUser.FirstName,
                    LastName = regUser.LastName,
                    Email = regUser.Email,
                    Password = hasher.HashPassword(regUser, regUser.Password)
                };
            
                _context.Users.Add(NewPerson);
                _context.SaveChanges();
                int UserId = _context.Users.Last().UserId;
                
                // Console.WriteLine("User Id: " + UserId);
                HttpContext.Session.SetInt32("UserId", UserId);
                
                return RedirectToAction("BrightIdeas", "Idea");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("loginUser")]
        public IActionResult LoginUser(LoginViewModel logUser)
        {

            User thisUser = _context.Users.SingleOrDefault(user => user.Email == logUser.logEmail);
            
            PasswordHasher<LoginViewModel> hasher = new PasswordHasher<LoginViewModel>();

            if(_context.Users.Where(u => u.Email == logUser.logEmail).ToList().Count() == 0)
            {
                ModelState.AddModelError("logEmail", "Invalid Email/Password");
                return View("Index");

            }
            else
            {
                if(hasher.VerifyHashedPassword(logUser, thisUser.Password, logUser.logPassword) == 0)
                {
                    ModelState.AddModelError("logEmail", "Invalid Email/Password");
                    return View("Index");

                }
            }
            
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("userId", (int)thisUser.UserId);
            }
                return RedirectToAction("BrightIdeas", "Idea");
        }

        [Route("Logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
