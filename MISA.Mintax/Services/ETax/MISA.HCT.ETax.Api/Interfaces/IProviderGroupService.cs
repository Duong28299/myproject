using MISA.HCT.ETax.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Api.Interfaces
{
    public interface IProviderGroupService
    {
        Task<ActionServiceResult> GetProviderGroup();

    }
}
