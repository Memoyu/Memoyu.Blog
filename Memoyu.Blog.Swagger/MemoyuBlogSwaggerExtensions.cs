/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Swagger
*   文件名称 ：MemoyuBlogSwaggerExtensions
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 19:03:34
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Memoyu.Blog.Domain.Configurations;
using Memoyu.Blog.Swagger.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;
namespace Memoyu.Blog.Swagger
{
    public static class MemoyuBlogSwaggerExtensions
    {
        /// <summary>
        /// 当前API版本，从appsettings.json获取
        /// </summary>
        private static readonly string version = $"v{AppSettings.ApiVersion}";
        /// <summary>
        /// Swagger描述信息
        /// </summary>
        private static readonly string description = @"<b>Blog</b>：<a target=""_blank"" href=""博客地址"">博客地址</a> <br/><br/>  <b>GitHub</b>：<a target=""_blank"" href=""https://github.com/Memoyu"">https://github.com/Memoyu</a> <br/><br/> <b>Hangfire</b>：<a target=""_blank"" href=""/hangfire"">任务调度中心</a> <br/><br/> <code>Powered by .NET Core 3.1 on Linux</code>";
        /// <summary>
        /// Swagger分组信息
        /// </summary>
        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>
        {
            new SwaggerApiInfo
            {
                Name = "博客前台接口",
                UrlPrefix = Grouping.GroupName_v1,
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "Memoyu - 博客前台接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                Name = "博客后台接口",
                UrlPrefix = Grouping.GroupName_v2,
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "Memoyu - 博客后台接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                Name = "通用公共接口",
                UrlPrefix = Grouping.GroupName_v3,
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "Memoyu - 通用公共接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                Name = "JWT授权接口",
                UrlPrefix = Grouping.GroupName_v4,
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "Memoyu - JWT授权接口",
                    Description = description
                }
            }
        };

        public static IServiceCollection AddSwagger(this IServiceCollection service)
        {
            return service.AddSwaggerGen(options =>
            {
                //遍历应用Swagger分组信息
                ApiInfos.ForEach(a => options.SwaggerDoc(a.UrlPrefix, a.OpenApiInfo));

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Memoyu.Blog.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Memoyu.Blog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Memoyu.Blog.Application.Contracts.xml"));

                #region 小绿锁，Swagger身份授权配置

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion
                // 应用Controller的API文档描述信息
                options.DocumentFilter<SwaggerDocumentFilter>();

            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {

            app.UseSwaggerUI(options =>
            {
                ApiInfos.ForEach(a=> options.SwaggerEndpoint($"/swagger/{a.UrlPrefix}/swagger.json", a.Name));

                //模型默认扩展深度，-1位完全隐藏
                options.DefaultModelExpandDepth(-1);
                //API仅展开标记
                options.DocExpansion(DocExpansion.List);
                //API前缀设为空
                options.RoutePrefix = string.Empty;
                //API页面标题
                options.DocumentTitle = "MemoyuBlogAPIDoc";

            });
        }
    }

    internal class SwaggerApiInfo
    {
        /// <summary>
        /// URL前缀
        /// </summary>
        public string UrlPrefix { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 可配置参数
        /// </summary>
        public OpenApiInfo OpenApiInfo { get; set; }
    }
}
