using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class IslesProductCategories: BaseEntity{

        [Key]
        public int id {get; set;}

        public int IsleId {get; set;}
        public Isle Isle {get; set;}

        public int ProductCategoryId {get; set;}
        public ProductCategory ProductCategory {get; set;}
    }
    
}