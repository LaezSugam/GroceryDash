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
    public class ProductCategoryController : Controller
    {
        private GroceryDashContext _context;

        public ProductCategoryController(GroceryDashContext context){
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("createproductcategory")]
        public IActionResult CreateProductCategory()
        {
            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            ViewBag.Categories = _context.ProductCategories;

            return View();
        }

        [HttpPost]
        [Route("createproductcategory")]
        public IActionResult CreateProductCategory(CreateProductCategoryView model){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }


            if(ModelState.IsValid){
                ProductCategory newProductCategory = new ProductCategory{
                    Name = model.Name,
                    Description = model.Description,
                    CreatedByUserId = (int)HttpContext.Session.GetInt32("CurrentUserId")
                };

                _context.ProductCategories.Add(newProductCategory);
                _context.SaveChanges();


                return RedirectToAction("Dashboard", "ShoppingList");

            }
            else{
                return View(model);
            }
        }

    }
}
