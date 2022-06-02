using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class Category : FullAuditModel
    {
        [Required]
        [StringLength(InventoryModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();        
        public virtual CategoryDetail CategoryDetail { get; set; }  //again the virtual is for the ability to use the navigation properties that it will generate.

    }
}
