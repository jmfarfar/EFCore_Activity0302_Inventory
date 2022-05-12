using InventoryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public abstract class FullAuditModel : IIdentityModel, IActivatableModel, IAuditedModel
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CreatedByUserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LastModifiedUserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
