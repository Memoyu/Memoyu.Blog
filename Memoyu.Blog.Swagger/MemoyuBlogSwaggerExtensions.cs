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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Memoyu.Blog.Swagger
{
    public static class MemoyuBlogSwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection service)
        {
            return service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "博客接口",
                    Description = "接口描述"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Memoyu.Blog.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Memoyu.Blog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Memoyu.Blog.Application.Contracts.xml"));
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "默认接口");
            });
        }
    }
}
