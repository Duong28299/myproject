using MISA.HCT.ETax.ApplicationCore.Entities;
using MISA.HCT.ETax.ApplicationCore.Entities.ETax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Api.Interfaces
{
    public interface IProviderService
    {
        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <param name="pageIndex">số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi</param>
        /// <returns></returns>
        Task<ActionServiceResult> GetProviders(int? pageIndex, int? pageSize);

        /// <summary>
        /// Lấy nhà cung cấp theo ID
        /// </summary>
        /// <param name="providerID">ID nhà cung cấp</param>
        /// <returns></returns>
        Task<ActionServiceResult> GetProviderByID(string providerID);

        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// <returns></returns>
        Task<ActionServiceResult> InsertProvider(Provider provider);

        /// <summary>
        /// Sửa nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// <returns></returns>
        Task<ActionServiceResult> UpdateProvider(Provider provider);

        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="providerIDs">Danh sách ID nhà cung cấp</param>
        /// <returns></returns>
        Task<ActionServiceResult> DeleteProvider(List<string> providerIDs);
    }
}
