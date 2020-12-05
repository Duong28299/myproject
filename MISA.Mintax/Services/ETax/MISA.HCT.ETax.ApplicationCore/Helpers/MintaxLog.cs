using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.HCT.ETax.ApplicationCore.Helpers
{
    public class MintaxLog
    {
        private static readonly ILogger _log = GetLogger(typeof(Logger));
        private static ILogger Log { get => _log; }
        private static List<string> SensitiveParams = new List<string>() { "Password", "Token", "AccessToken", "RefreshToken", "SecretKey" };//, "RefreshToken"
        //private static readonly string StarCharn = "\"***\"";
        //private static readonly int MaxLength = 255;
        public static ILogger GetLogger(Type type)
        {
            return LogManager.GetCurrentClassLogger();
        }

        public static void AddSensitiveParams(IList<string> sensitiveExParams)
        {
            SensitiveParams.AddRange(sensitiveExParams);
            Log.Debug($"Properties contain sensitive data: \n" + JsonConvert.SerializeObject(SensitiveParams));
        }
        public static List<string> GetSensitiveParams()
        {
            return SensitiveParams;
        }
        public static void Debug(string message)
        {
            Log.Debug(message);
        }
        public static void Debug(string title, object message)
        {
            var logMessage = $"{title}:{EncryptTokenForLog(message)}";
            Log.Debug(logMessage);
        }

        public static void Info(string message)
        {
            _log.Info(message);
        }
        public static void Info(string title, object message)
        {
            var logMessage = $"{title}:{EncryptTokenForLog(message)}";
            _log.Info(logMessage);
        }

        public static void Warn(string message)
        {
            _log.Warn(message);
        }
        public static void Warn(string title, object message)
        {
            var logMessage = $"{title}:{EncryptTokenForLog(message)}";
            _log.Warn(logMessage);
        }

        public static void Error(string message)
        {
            _log.Error(message);
        }
        public static void Error(string title, object message)
        {
            var logMessage = $"{title}:{EncryptTokenForLog(message)}";
            _log.Error(logMessage);
        }

        public static ILogger GetLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Mã hóa Token thành giá trị "***"
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string EncryptTokenForLog(object param)
        {
            var result = "EncryptTokenForLog has been exception!";
            try
            {
                var jsonResolver = new ShouldSerializeContractResolver();
                var jsonSettings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = jsonResolver
                };
                result = JsonConvert.SerializeObject(param, Formatting.None, jsonSettings);

            }
            catch (Exception ex)
            {
                var guidId = Guid.NewGuid().ToString();
                MintaxLog.Error($"EncryptTokenForLog-Ex [{guidId}]: {ex.ToString()}");
                //No thing
                result = $"EncryptTokenForLog-Ex with param [{guidId}]: {JsonConvert.SerializeObject(param)}";
            }
            return result;
        }
    }

    /// <summary>
    /// Thay đổi giá trị của những property nhạy cảm (Token, password)
    /// </summary>
    class ShouldSerializeContractResolver : DefaultContractResolver
    {
        private static readonly string StarCharn = "***";
        protected override JsonProperty CreateProperty(MemberInfo member,
                                         MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            IValueProvider valueProvider = base.CreateMemberValueProvider(member);
            //TODO Chú ý phần contains - Chữ hoa chữ thường.
            if (!string.IsNullOrEmpty(property.PropertyName) && MintaxLog.GetSensitiveParams().Contains(property.PropertyName))
                property.ValueProvider = new SensitiveValueProvider(valueProvider, StarCharn);

            return property;
        }

    }

    /// <summary>
    /// Đối tượng cung cấp giá trị cho thuộc tính khi Serialize đối tượng ra chuỗi
    /// </summary>
    class SensitiveValueProvider : IValueProvider
    {
        private IValueProvider _provider;
        private string _sensitiveData;

        public SensitiveValueProvider(IValueProvider provider, string sensitiveData)
        {
            _provider = provider;
            _sensitiveData = sensitiveData;
        }

        public object GetValue(object target)
        {
            //Nếu giá trị null hoặc empty thì trả về giá trị empty -> Khi đọc log thấy empty thì nghĩa là Client gửi empty tới server.
            if (_provider.GetValue(target) == null || string.IsNullOrEmpty(_provider.GetValue(target).ToString()))
            {
                return string.Empty;
            }
            return _sensitiveData;
        }

        public void SetValue(object target, object value)
        {
            throw new NotSupportedException();
        }
    }
}
