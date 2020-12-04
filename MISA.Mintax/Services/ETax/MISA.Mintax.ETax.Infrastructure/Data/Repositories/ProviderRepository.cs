using MISA.Mintax.ETax.ApplicationCore.Common;
using MISA.Mintax.ETax.ApplicationCore.Entities;
using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using MISA.Mintax.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Mintax.ETax.Infrastructure.Data.Repositories
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        public async Task<int> DeleteProvider(Guid providerID)
        {
            return await Delete(StoreProcedure.Provider.Proc_DeleteProvider, new object[] { providerID });
        }

        public Task<Provider> GetProviderByCode(string providerCode)
        {
            return Task.FromResult(GetEntity(StoreProcedure.Provider.Proc_GetProviderByCode, new object[] { providerCode })).Result;
        }

        public Task<Provider> GetProviderByCodeEdit(string providerCode, Guid providerID)
        {
            return Task.FromResult(GetEntity(StoreProcedure.Provider.Proc_GetProviderByCodeEdit, new object[] { providerCode, providerID })).Result;
        }

        public Task<Provider> GetProviderByID(Guid providerID)
        {
            return Task.FromResult(GetEntity(StoreProcedure.Provider.Proc_GetProviderByID, new object[] { providerID })).Result;
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <param name="pageIndex">số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi</param>
        /// <returns></returns>
        public Task<IEnumerable<Provider>> GetProviders(int? pageIndex, int? pageSize)
        {
            return Task.FromResult(GetEntities(StoreProcedure.Provider.Proc_GetProviderPagping,new object[]{pageIndex,pageSize}));
        }

        public async Task<int> GetToTalProvider()
        {
            var totalRecord =await GetListAsync();
            return Convert.ToInt32(totalRecord.Count);
        }

        public Task<object> InsertProvider(Provider provider)
        {
            using (var databaseConnector = new DatabaseConnector())
            {
                var res = InsertAndReturnSingleValue(StoreProcedure.Provider.Proc_InsertProvider,provider,databaseConnector);
                databaseConnector.CommitTransaction();
                return res;
            }
        }

        /// <summary>
        /// Sửa nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// created by tcduong
        /// <returns></returns>
        public Task<int> UpdateProvider(Provider provider)
        {
            using (var databaseConnector = new DatabaseConnector())
            {
                var res = Update(provider, databaseConnector).Result;
                databaseConnector.CommitTransaction();
                return Task.FromResult(res);
            }
        }
    }
}
