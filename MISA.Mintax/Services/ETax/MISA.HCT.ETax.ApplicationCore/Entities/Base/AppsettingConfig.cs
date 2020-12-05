using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.HCT.ETax.ApplicationCore.Entities
{
    public class AppsettingConfig
    {
        public RedisCacheConfig RedisCacheConfig { get; set; }
    }
    public class RedisCacheConfig
    {
        public string InstanceName { get; set; }
        public string RedisHost { get; set; }

    }
}
