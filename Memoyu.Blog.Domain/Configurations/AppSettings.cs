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
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Memoyu.Blog.Domain.Configurations
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
        /// <summary>
        /// ApiVersion
        /// </summary>
        public static string ApiVersion => _configuration["ApiVersion"];

        /// <summary>
        /// GitHub
        /// </summary>
        public static class GitHub
        {
            public static int UserId => Convert.ToInt32(_configuration["Github:UserId"]);

            public static string Client_ID => _configuration["Github:ClientID"];

            public static string Client_Secret => _configuration["Github:ClientSecret"];

            public static string Redirect_Uri => _configuration["Github:RedirectUri"];

            public static string ApplicationName => _configuration["Github:ApplicationName"];
        }
        /// <summary>
        /// JWT
        /// </summary>
        public static class JWT
        {
            public static string Domain => _configuration["JWT:Domain"];

            public static string SecurityKey => _configuration["JWT:SecurityKey"];

            public static int Expires => Convert.ToInt32(_configuration["JWT:Expires"]);
        }

    }
}
