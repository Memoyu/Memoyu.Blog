using System;
using System.Collections.Generic;
using FreeSql.DataAnnotations;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Blog.Core.Domains.Common.Base;
using Blog.Core.Security;
using Blog.Core.AOP.Attributes;

namespace Blog.Core.Common
{
    public class DomainReflexUtil
    {
        /// <summary>
        /// 扫描 IEntity类所在程序集，反射得到类上有特性标签为TableAttribute 的所有类
        /// </summary>
        /// <returns></returns>
        public static Type[] GetTypesByTableAttribute()
        {
            List<Type> tableAssembies = new List<Type>();
            foreach (Type type in Assembly.GetAssembly(typeof(IEntity)).GetExportedTypes())
            {
                foreach (Attribute attribute in type.GetCustomAttributes())
                {
                    if (attribute is TableAttribute tableAttribute)
                    {
                        if (tableAttribute.DisableSyncStructure == false)
                        {
                            tableAssembies.Add(type);
                        }
                    }
                }
            };
            return tableAssembies.ToArray();
        }

        /// <summary>
        /// 获取Method的http特性
        /// </summary>
        /// <param name="methodInfo">method信息</param>
        /// <returns></returns>
        private static HttpMethodAttribute GetMethodHttpAttribute(MethodInfo methodInfo)
        {
            HttpMethodAttribute methodAttribute = methodInfo.GetCustomAttributes().OfType<HttpGetAttribute>().FirstOrDefault();
            if (methodAttribute != null) return methodAttribute;

            methodAttribute = methodInfo.GetCustomAttributes().OfType<HttpDeleteAttribute>().FirstOrDefault();
            if (methodAttribute != null) return methodAttribute;

            methodAttribute = methodInfo.GetCustomAttributes().OfType<HttpPutAttribute>().FirstOrDefault();
            if (methodAttribute != null) return methodAttribute;

            methodAttribute = methodInfo.GetCustomAttributes().OfType<HttpPostAttribute>().FirstOrDefault();
            return methodAttribute;

        }
    }
}
