using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace GroceryDash.Controllers
{
    public class ControllerAssistant: Controller{
        public IActionResult CheckLogin(){
            if(HttpContext.Session.GetString("CurrentUserFirstName") == null){
               return RedirectToAction("Index", "Home");
           }
           else{
               return null;
           }
        }
    }
}