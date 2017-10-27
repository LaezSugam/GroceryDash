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
    public class ShoppingListController : Controller
    {
        private GroceryDashContext _context;

        public ShoppingListController(GroceryDashContext context){
            _context = context;
        }

       [HttpGet]
       [Route("dashboard")]
       public IActionResult Dashboard(){

           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ViewBag.CurrentUserFirstName = HttpContext.Session.GetString("CurrentUserFirstName");

            ViewBag.FullUser = _context.Users.Where(user => user.id == HttpContext.Session.GetInt32("CurrentUserId")).Include(user => user.MyShoppingLists).ThenInclude(slist => slist.ShoppingList).SingleOrDefault();

           return View();
       }

       [HttpGet]
       [Route("createlist")]
       public IActionResult CreateList(){

           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           return View();
       }

       [HttpPost]
       [Route("createlist")]
       public IActionResult CreateList(CreateShoppingListView model){

           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           if(ModelState.IsValid){
               ShoppingList newList = new ShoppingList{
                   Name = model.Name,
                   Permanent = model.Permanent
               };

               _context.ShoppingLists.Add(newList);
               _context.SaveChanges();

               newList = _context.ShoppingLists.Last();

               UsersShoppingLists listConnect = new UsersShoppingLists{
                   UserId = (int)HttpContext.Session.GetInt32("CurrentUserId"),
                    ShoppingListId = newList.id,
                    Access = 3
               };

               _context.UsersShoppingLists.Add(listConnect);
               _context.SaveChanges();

                return RedirectToAction("ListDetails", new {id = newList.id});
           }
           else{
               return View(model);
           }
           
       }

       [HttpGet]
       [Route("listdetails/{id}")]
       public IActionResult ListDetails(int id){
           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ShoppingList currentList = _context.ShoppingLists.Where(sl => sl.id == id).Include(sl => sl.ShoppingListUsers).Include(sl => sl.Products).ThenInclude(p => p.Product).SingleOrDefault();

           UsersShoppingLists myConnection = currentList.ShoppingListUsers.SingleOrDefault(su => su.UserId == HttpContext.Session.GetInt32("CurrentUserId"));

           if(myConnection == null){
               return RedirectToAction("Dashboard");
           }

            ViewBag.Access = myConnection.Access;

            ViewBag.CurrentList = currentList;

            ViewBag.AllProducts = _context.Products;

           return View();
       }

       [HttpPost]
       [Route("addtolist")]
       public IActionResult AddToList(int productId, int repeat, int listId){

           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           if(productId >= 0){
               ShoppingListsProducts toAdd = new ShoppingListsProducts{
                   ShoppingListId = listId,
                   ProductId = productId,
                   Repeat = repeat
               };

               _context.ShoppingListsProducts.Add(toAdd);
               _context.SaveChanges();
           }

           

           return RedirectToAction("ListDetails", new {id = listId});
       }

       [HttpGet]
       [Route("goshopping/{listId}")]
       public IActionResult GoShopping(int listId){

           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           ViewBag.Stores = _context.Stores;
           ViewBag.ShoppingList = _context.ShoppingLists.SingleOrDefault(sl => sl.id == listId);

           return View();
       }

       [HttpPost]
       [Route("goshopping/{listId}")]
       public IActionResult GoShopping(int listId, int storeId){

           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           TempData["listId"] = listId;
           TempData["storeId"] = storeId;

           return RedirectToAction("ShoppingTrip");
       }

       [HttpGet]
       [Route("shoppingtrip")]
       public IActionResult ShoppingTrip(){

           if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }

           if(TempData["listId"] == null || TempData["storeId"] == null){
               return RedirectToAction("Dashboard");
           }

           int storeId = (int)TempData["storeId"];
           int listId = (int)TempData["listId"];

           Store store = _context.Stores.Where(s => s.id == storeId).Include(s => s.Isles).ThenInclude(i => i.ProductCategories).ThenInclude(pc => pc.ProductCategory).ThenInclude(pc => pc.Products).ThenInclude(ppc => ppc.Product).SingleOrDefault();

            store.Isles.Sort((i1, i2) => i1.Position.CompareTo(i2.Position));

           ShoppingList sList = _context.ShoppingLists.Where(sl => sl.id == listId).Include(sl => sl.ShoppingListUsers).Include(sl => sl.Products).ThenInclude(slp => slp.Product).ThenInclude(p => p.ProductCategories).SingleOrDefault();

           List<IsleProducts> isles = new List<IsleProducts>();

           foreach(Isle isle in store.Isles){
               IsleProducts ip = new IsleProducts(isle);
               List<Product> inventory = new List<Product>();

               foreach(IslesProductCategories cat in ip.ProductCategories){
                   foreach(ProductsProductCategories product in cat.ProductCategory.Products){
                       inventory.Add(product.Product);
                   } 
               }
               foreach(ShoppingListsProducts product in sList.Products){
                   if(inventory.Contains(product.Product)){
                       ip.Products.Add(product.Product);
                   };
               }

               isles.Add(ip);

           }

           ViewBag.Store = store;
           ViewBag.ShoppingList = sList;
           ViewBag.Isles = isles;

           return View();
       }
    }
}
