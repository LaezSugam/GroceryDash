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
    public class ProductController : Controller
    {
        private GroceryDashContext _context;

        public ProductController(GroceryDashContext context){
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("createproduct")]
        public IActionResult CreateProduct()
        {
            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            ViewBag.Categories = _context.ProductCategories;

            return View();
        }

        [HttpPost]
        [Route("createproduct")]
        public IActionResult CreateProduct(CreateProductView model){

            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

            if(ModelState.IsValid){
                Product newProduct = new Product{
                    Name = model.Name,
                    Description = model.Description,
                    CreatedByUserId = (int)HttpContext.Session.GetInt32("CurrentUserId")
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();
                newProduct = _context.Products.Last();

                return RedirectToAction("Dashboard", "ShoppingList");

            }
            else{
                return View(model);
            }
        }

    }
}
