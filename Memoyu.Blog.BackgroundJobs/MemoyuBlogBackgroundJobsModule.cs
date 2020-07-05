/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.BackgroundJobs
*   文件名称 ：MemoyuBlogBackgroundJobsModule
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-05 10:14:46
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Memoyu.Blog.Domain.Configurations;
using Memoyu.Blog.Domain.Shared;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.BackgroundJobs
{
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule))]
    public class MemoyuBlogBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(options =>
                {
                    options.UseStorage(
                        new MySqlStorage(AppSettings.ConnectionString,//配置连接字符串
                        new MySqlStorageOptions
                        {
                            TablePrefix = MemoyuBlogConsts.DbTablePrefix + "hangfire"//配置表名前缀
                        }));
                });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseHangfireServer();//添加hangfire服务中间件
            app.UseHangfireDashboard(options: new DashboardOptions
            {
                Authorization = new[] {new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,//需要SSL连接才能访问HangFire Dahsboard。
                    SslRedirect = false,//是否将所有非SSL请求重定向到SSL URL
                    LoginCaseSensitive = true,//区分大小写
                    Users = new []//用户
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = AppSettings.Hangfire.Login,
                            PasswordClear = AppSettings.Hangfire.Password
                        },
                    }
                }), },
                DashboardTitle = "任务调度中心"
            });//添加hangfire仪表盘中间件,添加登陆认证

            var service = context.ServiceProvider;
            service.UseHangfireTest(); //启动后台作业
        }
    }
}
