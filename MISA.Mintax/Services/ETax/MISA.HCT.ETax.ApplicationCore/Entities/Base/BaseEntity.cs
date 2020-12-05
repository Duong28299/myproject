
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static MISA.HCT.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.HCT.ETax.ApplicationCore.Entities
{
    public abstract class BaseEntity : IAggregateRoot
    {
        public EntityState EntityState = EntityState.GET;
    }
}
