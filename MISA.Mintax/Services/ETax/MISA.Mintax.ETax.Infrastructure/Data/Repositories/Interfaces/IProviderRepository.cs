using MISA.Mintax.ETax.ApplicationCore.Entities;
using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Mintax.ETax.Infrastructure.Data.Repositories.Interfaces
{
    public interface IProviderRepository : IBaseRepository<Provider>
    {
        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <param name="pageIndex">số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi</param>
        /// <returns></returns>
        Task<IEnumerable<Provider>> GetProviders(int? pageIndex, int? pageSize);

        /// <summary>
        /// Lấy Nhà cung cấp theo code
        /// </summary>
        /// <param name="providerCode">Mã nhà cung cấp</param>
        /// <returns></returns>
        Task<Provider> GetProviderByCode(string providerCode);

        /// <summary>
        /// Lấy nhà cung cấp theo ID
        /// </summary>
        /// <param name="providerID">ID nhà cung cấp</param>
        /// <returns></returns>
        Task<Provider> GetProviderByID(Guid providerID);

        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// <returns></returns>
        Task<object> InsertProvider(Provider provider);

        /// <summary>
        /// Sửa nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// <returns></returns>
        Task<int> UpdateProvider(Provider provider);

        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="providerID">id nhà cung cấp</param>
        /// <returns></returns>
        Task<int> DeleteProvider(Guid providerID);

        /// <summary>
        /// Lấy nhà cung cấp theo mã khi mode Edit
        /// </summary>
        /// <param name="providerID">id nhà cung cấp</param>
        /// <returns></returns>
        Task<Provider> GetProviderByCodeEdit(string providerCode, Guid providerID);

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        Task<int> GetToTalProvider();
    }
}
