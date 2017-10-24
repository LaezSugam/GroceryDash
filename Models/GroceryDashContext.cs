using Microsoft.EntityFrameworkCore;
 
namespace GroceryDash.Models
{
    public class GroceryDashContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public GroceryDashContext(DbContextOptions<GroceryDashContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}

        public DbSet<Isle> Isles {get; set;}

        public DbSet<IslesProductCategories> IslesProductCategories {get; set;}

        public DbSet<Product> Products {get; set;}

        public DbSet<ProductCategory> ProductCategories {get; set;}

        public DbSet<ProductsProductCategories> ProductsProductCategories {get; set;}

        public DbSet<ShoppingList> ShoppingLists {get; set;}

        public DbSet<ShoppingListsProducts> ShoppingListsProducts {get; set;}

        public DbSet<Store> Stores {get; set;}

        public DbSet<UsersShoppingLists> UsersShoppingLists {get; set;}
    }
}