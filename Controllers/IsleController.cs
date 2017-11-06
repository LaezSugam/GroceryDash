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

        [HttpGet]
        [Route("createisle/{id}")]
        public IActionResult CreateIsle(int id)
        {
            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");

            ViewBag.Categories = _context.ProductCategories;
            ViewBag.StoreId = id;

            return View();
        }

        [HttpGet]
        [Route("updateisle/{id}")]
        public IActionResult UpdateIsle(int id)
        {
            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");

            ViewBag.Isle = _context.Isles.Where(isle => isle.id == id).Include(isle => isle.ProductCategories).ThenInclude(ipc => ipc.ProductCategory).SingleOrDefault();
            ViewBag.Categories = _context.ProductCategories;

            return View();
        }

        [HttpPost]
        [Route("createisle/{id}")]
        public IActionResult CreateIsle(CreateIsleView model, int id){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");

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

        [HttpPost]
        [Route("updateisle/{id}")]
        public IActionResult UpdateIsle(CreateIsleView model, int id){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            Isle currentIsle = _context.Isles.Where(isle => isle.id == id).Include(isle => isle.ProductCategories).ThenInclude(ipc => ipc.ProductCategory).SingleOrDefault();

            if(ModelState.IsValid){
                
                currentIsle.Name = model.Name;
                currentIsle.Position = model.Position;


                _context.Isles.Update(currentIsle);

                foreach(IslesProductCategories ipc in currentIsle.ProductCategories){
                    if(model.CategoryId.Contains(ipc.ProductCategoryId)){
                        model.CategoryId.Remove(ipc.ProductCategoryId);
                    }
                    else{
                        _context.IslesProductCategories.Remove(ipc);
                    } 
                }

                foreach(int catId in model.CategoryId){
                    IslesProductCategories newIPC = new IslesProductCategories{
                        ProductCategoryId = catId,
                        IsleId = currentIsle.id
                    };
                    _context.IslesProductCategories.Add(newIPC);
                }

                _context.SaveChanges();


                return RedirectToAction("StoreDetails", "Store", new {id = currentIsle.StoreId});

            }
            else{
                ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");
                ViewBag.Categories = _context.ProductCategories;
                ViewBag.Isle = currentIsle;

                return View(model);
            }
        }

    }
}
