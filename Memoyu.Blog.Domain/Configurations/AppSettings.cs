/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Configurations
*   文件名称 ：AppSettings
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 22:16:20
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Memoyu.Blog.Configurations
{
    public class AppSettings
    {
        private static readonly IConfigurationRoot _configuration;

        static AppSettings()
        {
            //加载AppSetting.json,并构建ConfigurationRoot
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", true, true);
            _configuration = builder.Build();
        }
        /// <summary>
        /// 开启状态的Db
        /// </summary>
        public static string EnableDb => _configuration["ConnectionStrings:Enable"];
        /// <summary>
        /// Db连接配置信息
        /// </summary>
        public static string ConnectionString => _configuration.GetConnectionString(EnableDb);
    }
}
