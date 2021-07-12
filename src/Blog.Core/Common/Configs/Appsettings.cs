using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Blog.Core.Common.Configs
{

    public static class Appsettings
    {
        #region System

        public static IConfiguration Configuration  { get; } = new ConfigurationBuilder()//配置配置文件
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"Configs/SerilogConfig.json", optional: true, reloadOnChange: true)//添加Serilog配置
            .AddJsonFile($"Configs/RateLimitConfig.json", optional: true, reloadOnChange: true)//添加限流配置
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        /// <summary>
        /// 接口版本
        /// </summary>
        public static string ApiVersion => Configuration["ApiVersion"];

        #endregion

        #region Cors

        public static class Cors
        {
            /// <summary>
            /// 跨域策略名
            /// </summary>
            public static string CorsName => Configuration["Cors:Name"];
            /// <summary>
            /// 跨域源
            /// </summary>
            public static string CorsOrigins => Configuration["Cors:Origins"];
        }

        #endregion

        #region FileStorage

        public static class FileStorage
        {
            /// <summary>
            /// 上传文件总大小
            /// </summary>
            public static long MaxFileSize => long.Parse(Configuration["FileStorage:MaxFileSize"]);
            /// <summary>
            /// 多文件上传时，支持的最大文件数量
            /// </summary>
            public static int NumLimit => int.Parse(Configuration["FileStorage:NumLimit"]);
            /// <summary>
            /// 允许某些类型文件上传，文件格式以,隔开
            /// </summary>
            public static string Include => Configuration["FileStorage:Include"];
            /// <summary>
            /// 禁止某些类型文件上传，文件格式以,隔开
            /// </summary>
            public static string Exclude => Configuration["FileStorage:Exclude"];
            /// <summary>
            /// 服务名
            /// </summary>
            public static string ServiceName => Configuration["FileStorage:ServiceName"];

            /// <summary>
            /// 本地文件信息
            /// </summary>
            public static string LocalFilePrefixPath => Configuration["FileStorage:LocalFile:PrefixPath"];
            public static string LocalFileHost => Configuration["FileStorage:LocalFile:Host"];
        }

        #endregion

        #region Authentication


        /// <summary>
        /// GitHub
        /// </summary>
        public class GitHub
        {
            public static int UserId => Convert.ToInt32(Configuration["Github:UserId"]);

            public static string ClientId => Configuration["Github:ClientID"];

            public static string ClientSecret => Configuration["Github:ClientSecret"];

            public static string RedirectUri => Configuration["Github:RedirectUri"];

            public static string AppName => Configuration["Github:ApplicationName"];

        }

        /// <summary>
        /// Jwt Token Config
        /// </summary>
        public class JwtBearer
        {
            /// <summary>
            /// 密钥
            /// </summary>
            public static string SecurityKey => Configuration["Authentication:JwtBearer:SecurityKey"];

            /// <summary>
            /// Audience
            /// </summary>
            public static string Audience => Configuration["Authentication:JwtBearer:Audience"];

            /// <summary>
            /// 过期时间(分钟)
            /// </summary>
            public static double Expires => Convert.ToDouble(Configuration["Authentication:JwtBearer:Expires"]);

            /// <summary>
            /// 签发者
            /// </summary>
            public static string Issuer => Configuration["Authentication:JwtBearer:Issuer"];
        }


        #endregion

        #region Db

        /// <summary>
        /// 获取配置默认Db Code
        /// </summary>
        public static string DbTypeCode => Configuration["ConnectionStrings:DefaultDB"];

        /// <summary>
        /// 获取配置 MySql ConnectionString
        /// </summary>
        public static string MySqlCon => Configuration["ConnectionStrings:MySql"];

        /// <summary>
        /// 获取配置默认Db ConnectionString 
        /// </summary>
        public static string DbConnectionString(string dbTypeCode) => Configuration[$"ConnectionStrings:{dbTypeCode}"];
        #endregion

        #region Cache

        /// <summary>
        /// 是否开启Cache
        /// </summary>
        public static bool CacheEnable => Convert.ToBoolean(Configuration["Cache:Enable"]);

        /// <summary>
        /// 默认缓存过期时间
        /// </summary>
        public static int CacheExpire => Convert.ToInt32(Configuration["Cache:ExpireSeconds"]);

        #endregion

        #region IP

        /// <summary>
        /// 是否开启IP记录
        /// </summary>
        public static bool IpLogEnable => Convert.ToBoolean(Configuration["Middleware:IPLog:Enabled"]);

        /// <summary>
        /// 是否开启IP限流
        /// </summary>
        public static bool IpRateLimitEnable => Convert.ToBoolean(Configuration["Middleware:IpRateLimit:Enabled"]);
        public static IConfigurationSection IpRateLimitingConfig => Configuration.GetSection("IpRateLimiting");
        public static IConfigurationSection IpRateLimitPoliciesConfig => Configuration.GetSection("IpRateLimitPolicies");

        #endregion

        #region CAP

        /// <summary>
        /// Cap默认存储表前缀
        /// </summary>
        public static string CapStorageTablePrefix => Configuration["CAP:TableNamePrefix"];

        /// <summary>
        /// Cap默认存储
        /// </summary>
        public static string CapDefaultStorage => Configuration["CAP:DefaultStorage"];

        /// <summary>
        /// Cap默认队列
        /// </summary>
        public static string CapDefaultMessageQueue => Configuration["CAP:DefaultMessageQueue"];

        /// <summary>
        /// Cap RabbitMq 连接信息
        /// </summary>
        public class CapRabbitMq
        {
            public static string HostName => Configuration["CAP:RabbitMQ:HostName"];
            public static string UserName => Configuration["CAP:RabbitMQ:UserName"];
            public static string Password => Configuration["CAP:RabbitMQ:Password"];
            public static string VirtualHost => Configuration["CAP:RabbitMQ:VirtualHost"];
        }

        #endregion

        #region Redis

        /// <summary>
        /// CsRedis连接字符串
        /// </summary>
        public static string CsRedisCon => Configuration["ConnectionStrings:CsRedis"];

        #endregion

    }
}
