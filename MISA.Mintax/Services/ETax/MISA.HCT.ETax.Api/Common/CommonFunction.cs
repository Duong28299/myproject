using MISA.HCT.ETax.ApplicationCore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MISA.HCT.ETax.Api.Common
{
    public static class CommonFunction
    {
        /// <summary>
        /// Convert an object to a byte array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray<T>(T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Convert a byte array to an Object
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
        /// <summary>
        /// Convert Object to ByteArray
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Lấy claim jwt từ token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="claimName"></param>
        /// <returns></returns>
        public static string GetClaimFromToken(string token, string claimName)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(claimName))
            {
                var jwtToken = new JwtSecurityToken(token.StartsWith("Bearer ") || token.StartsWith("bearer ") ? token.Substring(7) : token);
                result = jwtToken.Claims.First(c => c.Type == claimName)?.Value ?? string.Empty;
            }
            return result;
        }

    }
    /// <summary>
    /// Gọi Api ra ngoài
    /// </summary>
    public class CallService
    {
		/// <summary>
		/// Phương thức chung call Http Client API
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="url"></param>
		/// <param name="action"></param>
		/// <param name="jsonData"></param>
		/// <param name="contentType"></param>
		/// <param name="header"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		/// created nvbinh2 01.10.2020
		public static T CallHttpClient<T>(string url, HttpMethod action, string jsonData, string contentType, Dictionary<string, string> header = null)
        {
            //1. Url + Method
            var httpRequest = new HttpRequestMessage();
            if (action == HttpMethod.Get)
                httpRequest.Method = HttpMethod.Get;
            else
                httpRequest.Method = HttpMethod.Post;
            httpRequest.RequestUri = new Uri(url);

            //2. Header
            if (header != null && header.Count > 0)
            {
                foreach (var item in header)
                {
                    httpRequest.Headers.Add(item.Key, item.Value);
                }
            }
            httpRequest.Content = new StringContent(jsonData, Encoding.UTF8, contentType);
            var httpResPonse = new HttpResponseMessage();
            using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(5) })
            {
                httpResPonse = client.SendAsync(httpRequest).Result;
            }
            if (httpResPonse.StatusCode == HttpStatusCode.OK)
            {
                var resultstring = httpResPonse.Content.ReadAsStringAsync();
                var resultAmisRes = JsonConvert.DeserializeObject<T>(resultstring.Result);

                return resultAmisRes;
            }
            else
            {
                return default(T);
            }

        }

    }

}
