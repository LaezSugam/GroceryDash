using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class ProductsProductCategories: BaseEntity{

        [Key]
        public int id {get; set;}
        
        public int ProductId {get; set;}
        public Product Product {get; set;}

        public int ProductCategoryId {get; set;}
        public ProductCategory ProductCategory {get; set;}
    }
    
}