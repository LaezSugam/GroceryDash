using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class ShoppingListsProducts: BaseEntity{

        [Key]
        public int id {get; set;}
        
        public int ShoppingListId {get; set;}
        public ShoppingList ShoppingList {get; set;}

        public int ProductId {get; set;}
        public Product Product {get; set;}

        public int Repeat {get; set;}
    }
    
}