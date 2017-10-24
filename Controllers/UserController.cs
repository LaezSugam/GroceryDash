using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;

using GroceryDash.Models;

namespace GroceryDash.Controllers
{
    public class UserController : Controller
    {
        private GroceryDashContext _context;

        public UserController(GroceryDashContext context){
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginUserView model){

            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);

                if(CurrentUser == null){
                    ModelState.AddModelError("Email", "Email address not found.");
                    return View(model);
                }
                else if(0 == Hasher.VerifyHashedPassword(CurrentUser, CurrentUser.Password, model.Password)){
                    ModelState.AddModelError("Password", "Password is incorrect.");
                    return View(model);
                }
                
                HttpContext.Session.SetString("CurrentUserFirstName", CurrentUser.FirstName);
                HttpContext.Session.SetInt32("CurrentUserId", CurrentUser.id);

                ViewBag.CurrentUserFirstName = CurrentUser.FirstName;

                return RedirectToAction("Dashboard", "ShoppingList");
            }
            else{
                return View(model);
            }
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUserView model){

            if(ModelState.IsValid){

                User NewUser = new User{
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,  
                };

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, model.Password);

                _context.Users.Add(NewUser);
                _context.SaveChanges();

                User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                HttpContext.Session.SetString("CurrentUserFirstName", CurrentUser.FirstName);
                HttpContext.Session.SetInt32("CurrentUserId", CurrentUser.id);
                ViewBag.CurrentUserFirstName = CurrentUser.FirstName;


                
                return RedirectToAction("Dashboard", "ShoppingList");
            }
            else{
                
                return View(model);
                
            }
        }
    }
}
