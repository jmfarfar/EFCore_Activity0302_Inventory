using System.ComponentModel.DataAnnotations;

namespace InventoryModels
{
    public class Item : FullAuditModel
    {        
        [Required]
        [StringLength(InventoryModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }        
        [Range(InventoryModelConstants.MINIMUM_QUANTITY, InventoryModelConstants.MAXIMUM_QUANTITY)]
        public int Quantity { get; set; }
        [StringLength(InventoryModelConstants.MAX_DESCRIPTION_LENGTH)]
        [Required]
        public string Description { get; set; }
        [StringLength(InventoryModelConstants.MAX_NOTES_LENGTH, MinimumLength = 10)]
        public string Notes { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        [Range(InventoryModelConstants.MINIMUM_PRICE, InventoryModelConstants.MAXIMUM_PRICE)]
        public decimal? PurchasePrice { get; set; }
        [Range(InventoryModelConstants.MINIMUM_PRICE, InventoryModelConstants.MAXIMUM_PRICE)]
        public decimal? CurrentOrFinalPrice { get; set; }

        // adding a reference to Category for the one to many relationship
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; } //why virtual? because this way EF can override and or extend the properties to support lazy loading of the relational data.
                                                       //this will become very important when you want to use LINQ to select data and include relationship data in the model state

        public virtual List<Player> Players { get; set; } = new List<Player>();  //instanciado por default para evitar null exception en caso de que ya hayan registros en la base de datos que no contendran info en este campo

        public virtual List<ItemGenre> ItemGenres { get; set; } = new List<ItemGenre>();




    }
}