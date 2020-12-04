using MISA.Mintax.ETax.ApplicationCore.Common;
using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using MISA.Mintax.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Mintax.ETax.Infrastructure.Data.Repositories
{
    public class ProviderGroupRepository : BaseRepository<ProviderGroup>, IProviderGroupRepository
    {
        public Task<IReadOnlyList<ProviderGroup>> GetProviderGroup()
        {
            return Task.FromResult(GetListAsync()).Result;
        }
    }
}
