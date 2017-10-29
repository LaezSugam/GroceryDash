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
    public class IsleController : Controller
    {
        private GroceryDashContext _context;

        public IsleController(GroceryDashContext context){
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("createisle/{id}")]
        public IActionResult CreateIsle(int id)
        {
            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            ViewBag.Categories = _context.ProductCategories;
            ViewBag.StoreId = id;

            return View();
        }

        [HttpPost]
        [Route("createisle/{id}")]
        public IActionResult CreateIsle(CreateIsleView model, int id){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ViewBag.Categories = _context.ProductCategories;
           ViewBag.StoreId = id;

            if(ModelState.IsValid){
                Isle newIsle = new Isle{
                    Name = model.Name,
                    Position = model.Position,
                    CreatedByUserId = (int)HttpContext.Session.GetInt32("CurrentUserId"),
                    StoreId = id
                };

                _context.Isles.Add(newIsle);
                _context.SaveChanges();
                newIsle = _context.Isles.Last();

                foreach(int catId in model.CategoryId){
                    IslesProductCategories newIPC = new IslesProductCategories{
                        ProductCategoryId = catId,
                        IsleId = newIsle.id
                    };
                    _context.IslesProductCategories.Add(newIPC);
                }

                _context.SaveChanges();


                return RedirectToAction("StoreDetails", "Store", new {id = id});

            }
            else{
                return View(model);
            }
        }

    }
}
