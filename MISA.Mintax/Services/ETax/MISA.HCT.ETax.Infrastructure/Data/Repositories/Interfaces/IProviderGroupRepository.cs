using MISA.HCT.ETax.ApplicationCore.Entities.ETax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Infrastructure.Data.Repositories.Interfaces
{
    public interface IProviderGroupRepository:IBaseRepository<ProviderGroup>
    {
        /// <summary>
        /// Lấy danh sách 
        /// </summary>
        /// createdDate 4.12.2020
        /// created by tcduong
        /// <returns></returns>
        Task<IReadOnlyList<ProviderGroup>> GetProviderGroup();
    }
}
