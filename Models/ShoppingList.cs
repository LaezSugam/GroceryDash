using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class ShoppingList: BaseEntity{

        [Key]
        public int id {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public int Permanent {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<UsersShoppingLists> ShoppingListUsers {get; set;}

        public List<ShoppingListsProducts> Products {get; set;}

        public ShoppingList(){
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            ShoppingListUsers = new List<UsersShoppingLists>();
            Products = new List<ShoppingListsProducts>();
        }
    }

    public class CreateShoppingListView: BaseEntity{
        
        [Required]
        public string Name {get; set;}

        [Required]
        public int Permanent {get; set;}
    }
    
}