/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.BackgroundJobs
*   文件名称 ：MemoyuBlogBackgroundJobsExtensions
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-05 12:01:06
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using Hangfire;
using Memoyu.Blog.BackgroundJobs.Jobs.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Memoyu.Blog.BackgroundJobs
{
    public static class MemoyuBlogBackgroundJobsExtensions
    {
        public static void UseHangfireTest(this IServiceProvider service)
        {
            var job = service.GetService<HangfireTestJob>();
            RecurringJob.AddOrUpdate("定时任务测试", () => job.ExecuteAsync(), CronType.Minute());
        }
    }
}
