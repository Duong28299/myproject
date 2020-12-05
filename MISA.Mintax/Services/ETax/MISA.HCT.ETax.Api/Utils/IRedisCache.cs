using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Api.Utils
{
    public interface IRedisCache
    {
        /// <summary>
        /// Thêm vào cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task SetCacheAsync(string key, object obj);
        /// <summary>
        /// lấy thông tin cache theo id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<object> GetCacheAsync(string key);

        /// <summary>
        /// Xóa cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task ClearCacheAsync(string key);



    }
}
