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

        public int Position {get; set;}

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
    

    public class CreateIsleView: BaseEntity{

        [Required]
        public string Name {get; set;}

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Position {get; set;}

        [Required]
        [Display(Name = "Product Categories")]
        public List<int> CategoryId {get; set;}
    }

    public class IsleProducts: Isle{

        public List<Product> Products {get; set;}

        public IsleProducts(): base(){
            Products = new List<Product>();
        }

        public IsleProducts(Isle isle): base(){
            Products = new List<Product>();
            id = isle.id;
            Name = isle.Name;
            Position = isle.Position;
            created_at = isle.created_at;
            updated_at = isle.updated_at;
            CreatedByUserId = isle.CreatedByUserId;
            CreatedByUser = isle.CreatedByUser;
            ProductCategories = isle.ProductCategories;
            StoreId = isle.StoreId;
            Store = isle.Store;
        }
    }
}