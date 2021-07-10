using System;

namespace Blog.Core.AOP.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheableAttribute : Attribute
    {
        public CacheableAttribute()
        {
        }

        public CacheableAttribute(int expire)
        {
            Expire = expire;
        }

        public CacheableAttribute(string cacheKey, int expire)
        {
            CacheKey = cacheKey;
            Expire = expire;
        }

        public string CacheKey { get; set; }

        public int Expire { get; set; }

    }
}
