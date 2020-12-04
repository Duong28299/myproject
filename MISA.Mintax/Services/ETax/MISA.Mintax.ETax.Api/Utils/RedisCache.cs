using Microsoft.Extensions.Caching.Distributed;
using MISA.Mintax.ETax.Api.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Mintax.ETax.Api.Utils
{
    public static class RedisCache
    {
        public static IDistributedCache _distributedCache;
        public static DistributedCacheEntryOptions _distributedCacheEntryOptions = new DistributedCacheEntryOptions();

        #region
        /// <summary>
        /// Thêm mới vao cache 
        /// </summary>
        /// <param name="key">Key của cache</param>
        /// <param name="obj">Obj của cache</param>
        /// <returns></returns>
        public static async Task SetCacheAsync(string key, object obj)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _distributedCacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(86400));
                await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)), _distributedCacheEntryOptions);
            }
        }
        /// <summary>
        /// Lấy thông tin cache bằng key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Object chưa giá trị của key</returns>
        public static async Task<object> GetCacheAsync<T>(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var obj = await _distributedCache.GetAsync(key);
                if (obj != null)
                {
                    return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(obj));
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// Xóa cache 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task ClearCacheAsync(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var obj = await _distributedCache.GetAsync(key);
                if (obj != null)
                {
                    await _distributedCache.RemoveAsync(key);
                }
            }
        }
        #endregion

    }
}
