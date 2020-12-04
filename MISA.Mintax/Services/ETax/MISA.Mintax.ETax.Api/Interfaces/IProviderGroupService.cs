using MISA.Mintax.ETax.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Mintax.ETax.Api.Interfaces
{
    public interface IProviderGroupService
    {
        Task<ActionServiceResult> GetProviderGroup();

    }
}
