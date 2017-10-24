using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class UsersShoppingLists: BaseEntity{


        [Key]
        public int id {get; set;}

        public int UserId {get; set;}
        public User User {get; set;}

        public int ShoppingListId {get; set;}
        public ShoppingList ShoppingList {get; set;}

        public int Access {get; set;}
    }
    
}