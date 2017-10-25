using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class Product: BaseEntity{

        [Key]
        public int id {get; set;}

        [Required]
        public string Name {get; set;}

        public string Description {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public int CreatedByUserId {get; set;}
        public User CreatedByUser {get; set;}

        public List<ShoppingListsProducts> ShoppingLists {get; set;}

        public List<ProductsProductCategories> ProductCategories {get; set;}

        public Product(){
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            ShoppingLists = new List<ShoppingListsProducts>();
            ProductCategories = new List<ProductsProductCategories>();
        }
    }

    public class CreateProductView: BaseEntity{

        [Required]
        public string Name {get; set;}

        [Required]
        [Display(Name = "Category")]
        public List<int> CategoryId {get; set;}

        [Display(Name = "Description (Optional)")]
        public string Description {get; set;}

    }
    
}