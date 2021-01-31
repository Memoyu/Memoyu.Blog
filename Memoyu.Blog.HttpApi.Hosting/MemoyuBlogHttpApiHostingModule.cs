using System;
using System.Linq;
using System.Threading.Tasks;
using Memoyu.Blog.BackgroundJobs;
using Memoyu.Blog.Domain.Configurations;
using Memoyu.Blog.EntityFrameworkCore;
using Memoyu.Blog.HttpApi.Hosting.Filters;
using Memoyu.Blog.HttpApi.Hosting.Middleware;
using Memoyu.Blog.Swagger;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Base.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
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
        typeof(MemoyuBlogFrameworkCoreModule),
        typeof(MemoyuBlogBackgroundJobsModule)
    )]
    public class MemoyuBlogHttpApiHostingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<MvcOptions>(options =>
            {
                //获得AbpExceptionFilter拦截器
                var filterMetadata = options.Filters.FirstOrDefault(f =>
                    f is ServiceFilterAttribute attribute && attribute.ServiceType == typeof(AbpExceptionFilter));
                //移除拦截器
                options.Filters.Remove(filterMetadata);
                //添加自定义拦截器
                options.Filters.Add<MemoyuExceptionFilter>();
            });
            Configure<MvcNewtonsoftJsonOptions>(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            // 跨域配置
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            // 路由配置
            context.Services.AddRouting(options =>
            {
                // 设置URL为小写
                options.LowercaseUrls = true;
                // 在生成的URL后面添加斜杠
                options.AppendTrailingSlash = true;
            });
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
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        ServiceResult result = new ServiceResult();
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                            result.IsSuccess("授权失败！");
                            result.Code = ServiceResultCode.UnAuth;
                        }
                        return Task.FromResult(result);
                    }
                };

                #region 仅能捕获授权异常-废弃
                ////应用程序提供的对象，用于处理承载引发的事件，身份验证处理程序
                //options.Events = new JwtBearerEvents
                //{
                //    OnChallenge = async context =>
                //    {
                //        //跳过默认的处理逻辑，返回以下数据模型
                //        context.HandleResponse();
                //        context.Response.ContentType = "application/json;charset=utf-8";
                //        context.Response.StatusCode = StatusCodes.Status200OK;

                //        var result = new ServiceResult();
                //        result.IsFailed("UnAuthorized");

                //        await context.Response.WriteAsync(result.ToJson());
                //    }
                //}; 
                #endregion

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
            // 使用HSTS的中间件，该中间件添加了严格传输安全头
            app.UseHsts();
            // 转发将标头代理到当前请求，配合 Nginx 使用，获取用户真实IP
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //路由
            app.UseRouting();
            // 跨域
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //异常处理中间件
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            //身份验证
            app.UseAuthentication();
            //认证授权
            app.UseAuthorization();
            // HTTP => HTTPS
            app.UseHttpsRedirection();
            //路由映射
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
