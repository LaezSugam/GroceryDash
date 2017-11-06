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

           ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");

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

           ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");

            ViewBag.Stores = _context.Stores;

            return View();
        }


        [HttpGet]
        [Route("storedetails/{id}")]
        public IActionResult StoreDetails(int id){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");

            ViewBag.Store = _context.Stores.Where(s => s.id == id).Include(s => s.Isles).ThenInclude(i => i.ProductCategories).ThenInclude(pc => pc.ProductCategory).SingleOrDefault();

            return View();
        }

        [HttpGet]
        [Route("updatestore/{id}")]
        public IActionResult UpdateStore(int id){
            Store currentStore = _context.Stores.SingleOrDefault(store => store.id == id);
            CreateStoreView storeViewModel = new CreateStoreView{
                Name = currentStore.Name,
                Address1 = currentStore.Address1,
                Address2 = currentStore.Address2,
                City = currentStore.City,
                State = currentStore.State,
                Zip = currentStore.Zip,
                Description = currentStore.Description
            };

            ViewBag.StoreId = id;
            return View(storeViewModel);
        }

        [HttpPost]
        [Route("updatestore/{id}")]
        public IActionResult UpdateStore(CreateStoreView model, int id){
            if(ModelState.IsValid){
                Store currentStore = _context.Stores.SingleOrDefault(store => store.id == id);

                currentStore.Name = model.Name;
                currentStore.Address1 = model.Address1;
                currentStore.Address2 = model.Address2;
                currentStore.City = model.City;
                currentStore.State = model.State;
                currentStore.Zip = model.Zip;
                currentStore.Description = model.Description;
                currentStore.updated_at = DateTime.Now;

                _context.Stores.Update(currentStore);
                _context.SaveChanges();
                return RedirectToAction("StoreDetails", new {id = id});
            }
            else{
                ViewBag.StoreId = id;
                return View(model);
            }
            
        }

    }
}
