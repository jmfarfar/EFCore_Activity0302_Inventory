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
        private const string _systemUserId = "8457ghty-84g5-43i9-f88h3k901uth";
        public const string _loggedInUserId = "e3e4ght7-a34t-4151-8574-eb7afe3i90et";
        private static IConfigurationRoot _configuration;
        private static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;

        static void Main(string[] args)
        {
            BuildOptions();
            //DeleteAllItems();
            EnsureItems();
            UpdateItems();
            ListInventory();
        }

        private static void UpdateItems()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var items = db.Items.ToList();
                foreach (var item in items)
                {
                    item.CurrentOrFinalPrice = 9.99M;
                }
                db.Items.UpdateRange(items);
                db.SaveChanges();
            }
        }

        private static void DeleteAllItems()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var items = db.Items.ToList();
                db.Items.RemoveRange(items);
                db.SaveChanges();
            }
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
            EnsureItem("Batman Begins", "You either die the hero or live long enough to see yourself become the villain", "Christian Bale, Katie Holmes");
            EnsureItem("Inception", "You mustn't be afraid to dream a little bigger,darling", "Leonardo DiCaprio, Tom Hardy, Joseph Gordon - Levitt" );
            EnsureItem("Remember the Titans", "Left Side, Strong Side", "Denzell Washington, Will Patton" );
            EnsureItem("Star Wars: The Empire Strikes Back", "He will join us or die, master", "Harrison Ford, Carrie Fisher, Mark Hamill");
            EnsureItem("Top Gun", "I feel the need, the need for speed!", "Tom Cruise, Anthony Edwards, Val Kilmer");
        }

        private static void EnsureItem(string name, string description, string notes)
        {
            Random r = new Random();
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
                        CreatedByUserId = _loggedInUserId,
                        IsActive = true,
                        Quantity = r.Next(1,1000),
                        Description = description,
                        Notes = notes
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
