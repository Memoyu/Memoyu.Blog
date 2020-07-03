using System;
using Memoyu.Blog.Domain.Configurations;
using Memoyu.Blog.EntityFrameworkCore;
using Memoyu.Blog.Swagger;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.HttpApi.Hosting
{
    /// <summary>
    /// 相当于一个web项目，但这里主要依赖于HttpApi模块，用来暴露我们的API的。
    /// </summary>
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(MomeyuBlogHttpApiModule),
        typeof(MemoyuBlogSwaggerModule),
        typeof(MemoyuBlogFrameworkCoreModule)
        )]
    public class MemoyuBlogHttpApiHostingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证颁发者
                    ValidateIssuer = true,
                    //是否验证访问群体
                    ValidateAudience = true,
                    //是否验证生存周期
                    ValidateLifetime = true,
                    //验证Token的时间偏移量
                    ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,
                    // 访问群体
                    ValidAudience = AppSettings.JWT.Domain,
                    //颁发者
                    ValidIssuer = AppSettings.JWT.Domain,
                    //安全秘钥
                    IssuerSigningKey = new SymmetricSecurityKey(AppSettings.JWT.SecurityKey.GetBytes())

                };
                //应用程序提供的对象，用于处理承载引发的事件，身份验证处理程序
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        //跳过默认的处理逻辑，返回以下数据模型
                        context.HandleResponse();
                        context.Response.ContentType = "application/json;charset=utf-8";
                        context.Response.StatusCode = StatusCodes.Status200OK;

                        var result = new ServiceResult();
                        result.IsFailed("UnAuthorized");

                        await context.Response.WriteAsync(result.ToJson());
                    }
                };

            });
            //认证授权
            context.Services.AddAuthentication();
            //Http请求
            context.Services.AddHttpClient();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            //判断环境变量，开发环境，
            if (env.IsDevelopment())
            {
                //生成异常页面
                app.UseDeveloperExceptionPage();
            }
            //路由
            app.UseRouting();
            //路由映射
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
