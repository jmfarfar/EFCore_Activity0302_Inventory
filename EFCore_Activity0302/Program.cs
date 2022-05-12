// See https://aka.ms/new-console-template for more information


using EFCore_DBLibrary;
using InventoryHelpers;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore_Activity0302
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        private static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;

        static void Main(string[] args)
        {
            BuildOptions();
            EnsureItems();
            ListInventory();
        }

        private static void ListInventory()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var items = db.Items.OrderBy(x => x.Name).ToList();
                items.ForEach(x =>
                    Console.WriteLine($"New Item: {x.Name}"));
            }
        }

        private static void EnsureItems()
        {
            EnsureItem("Batman Begins");
            EnsureItem("Inception");
            EnsureItem("Remember the Titans");
            EnsureItem("Star Wars");
            EnsureItem("Top Gun");
        }

        private static void EnsureItem(string name)
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                //determine if item wxists
                var existingItem = db.Items.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

                if (existingItem == null)
                {
                    //does not exist... add it
                    var item = new Item()
                    {
                        Name = name,
                    };
                    db.Items.Add(item);
                    db.SaveChanges();
                }
            }
        }

        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));


        }
    }
}
