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
    public class StoreController : Controller
    {
        private GroceryDashContext _context;

        public StoreController(GroceryDashContext context){
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("createstore")]
        public IActionResult CreateStore()
        {
            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            return View();
        }

        [HttpPost]
        [Route("createstore")]
        public IActionResult CreateStore(CreateStoreView model){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            if(ModelState.IsValid){
                Store newStore = new Store{
                    Name = model.Name,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    Description = model.Description,
                    CreatedByUserId = (int)HttpContext.Session.GetInt32("CurrentUserId")
                };

                _context.Stores.Add(newStore);
                _context.SaveChanges();
                newStore = _context.Stores.Last();


                return RedirectToAction("StoreDetails", new {id = newStore.id});

            }
            else{
                return View(model);
            }
        }

        [HttpGet]
        [Route("stores")]
        public IActionResult Stores(){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            ViewBag.Stores = _context.Stores;

            return View();
        }


        [HttpGet]
        [Route("storedetails/{id}")]
        public IActionResult StoreDetails(int id){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            ViewBag.Store = _context.Stores.Where(s => s.id == id).Include(s => s.Isles).ThenInclude(i => i.ProductCategories).ThenInclude(pc => pc.ProductCategory).SingleOrDefault();

            return View();
        }

    }
}
