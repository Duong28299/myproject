using MISA.HCT.ETax.ApplicationCore.Common;
using MISA.HCT.ETax.ApplicationCore.Entities.ETax;
using MISA.HCT.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Infrastructure.Data.Repositories
{
    public class ProviderGroupRepository : BaseRepository<ProviderGroup>, IProviderGroupRepository
    {
        /// <summary>
        /// Lấy danh sách 
        /// </summary>
        /// createdDate 4.12.2020
        /// created by tcduong
        /// <returns></returns>
        public Task<IReadOnlyList<ProviderGroup>> GetProviderGroup()
        {
            return Task.FromResult(GetListAsync()).Result;
        }
    }
}
