using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Mintax.ETax.Infrastructure.Data.Repositories.Interfaces
{
    public interface IProviderGroupRepository:IBaseRepository<ProviderGroup>
    {
        Task<IReadOnlyList<ProviderGroup>> GetProviderGroup();
    }
}
