using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class User: BaseEntity{

        [Key]
        public int id {get; set;}

        [Required]
        [MinLength(2)]
        public string FirstName {get; set;}

        [Required]
        [MinLength(2)]
        public string LastName {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public List<Product> ProductsCreated {get; set;}

        public List<Store> StoresCreated {get; set;}

        public List<ProductCategory> ProductCategoriesCreated {get; set;}

        public List<Isle> IslesCreated {get; set;}

        public List<UsersShoppingLists> MyShoppingLists {get; set;}

        public User(){
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            ProductsCreated = new List<Product>();
            StoresCreated = new List<Store>();
            ProductCategoriesCreated = new List<ProductCategory>();
            IslesCreated = new List<Isle>();
            MyShoppingLists = new List<UsersShoppingLists>();
        }
    }

    public class RegisterUserView: BaseEntity{
        [Required]
        [MinLength(2)]
        public string FirstName {get; set;}

        [Required]
        [MinLength(2)]
        public string LastName {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmPassword {get; set;}
    }

    public class LoginUserView: BaseEntity{
        
        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        
    }
    
}