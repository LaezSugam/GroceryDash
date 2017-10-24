using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class Isle: BaseEntity{

        [Key]
        public int id {get; set;}

        [Required]
        public string Name {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public int CreatedByUserId {get; set;}
        public User CreatedByUser {get; set;}

        public List<IslesProductCategories> ProductCategories {get; set;}

        public int StoreId {get; set;}
        public Store Store {get; set;}

        public Isle(){
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            ProductCategories = new List<IslesProductCategories>();
        }
    }
    
}