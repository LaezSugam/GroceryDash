using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDash.Models
{
    public class Store: BaseEntity{

        [Key]
        public int id {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public string Address1 {get; set;}

        public string Address2 {get; set;}

        public string City {get; set;}

        public string State {get; set;}

        [Display(Name = "Zip Code")]
        public string Zip {get; set;}

        public string Description {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public int CreatedByUserId {get; set;}
        public User CreatedByUser {get; set;}

        public List<Isle> Isles {get; set;}

        public Store(){
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            Isles = new List<Isle>();
        }
    }

    public class CreateStoreView: BaseEntity{

        [Required]
        public string Name {get; set;}

        [Required]
        public string Address1 {get; set;}

        public string Address2 {get; set;}

        [Required]
        public string City {get; set;}

        [Required]
        public string State {get; set;}

        [MinLength(5)]
        [Display(Name = "Zip Code")]
        public string Zip {get; set;}

        public string Description {get; set;}

    }
    
}