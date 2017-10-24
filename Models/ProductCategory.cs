using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class ProductCategory: BaseEntity{

        [Key]
        public int id {get; set;}

        [Required]
        public string Name {get; set;}

        public string Description {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public int CreatedByUserId {get; set;}
        public User CreatedByUser {get; set;}

        public List<ProductsProductCategories> Products {get; set;}

        public List<IslesProductCategories> Isles {get; set;}

        public ProductCategory(){
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            Products = new List<ProductsProductCategories>();
            Isles = new List<IslesProductCategories>();
        }
    }
    
}