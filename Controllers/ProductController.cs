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
            return View();
        }

    }
}
