using MISA.HCT.ETax.ApplicationCore.Common;
using MISA.HCT.ETax.ApplicationCore.Entities;
using MISA.HCT.ETax.ApplicationCore.Entities.ETax;
using MISA.HCT.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Infrastructure.Data.Repositories
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="providerID">id nhà cung cấp</param>
        /// <returns></returns>
        public async Task<int> DeleteProvider(string providerIDs)
        {
            return await Delete(StoreProcedure.Provider.Proc_DeleteProvider, new object[] { providerIDs });
        }

        /// <summary>
        /// Lấy Nhà cung cấp theo code
        /// </summary>
        /// <param name="providerCode">Mã nhà cung cấp</param>
        /// <returns></returns>
        public Task<bool> CheckProviderByCode(string providerCode)
        {
            return Task.FromResult(CheckEntity(StoreProcedure.Provider.Proc_CheckProviderByCode, new object[] { providerCode })).Result;
        }

        /// <summary>
        /// Lấy nhà cung cấp theo mã khi mode Edit
        /// </summary>
        /// <param name="providerID">id nhà cung cấp</param>
        /// <returns></returns>
        public Task<bool> CheckProviderByCodeEdit(string providerCode, Guid providerID)
        {
            return Task.FromResult(CheckEntity(StoreProcedure.Provider.Proc_CheckProviderByCodeEdit, new object[] { providerCode, providerID })).Result;
        }

        /// <summary>
        /// Lấy nhà cung cấp theo ID
        /// </summary>
        /// <param name="providerID">ID nhà cung cấp</param>
        /// <returns></returns>
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

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetToTalProvider()
        {
            var totalRecord =await GetTotalEntity(StoreProcedure.Provider.Proc_GetTotalProvider);
            return Convert.ToInt32(totalRecord);
        }

        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// <returns></returns>
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
