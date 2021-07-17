using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Blog.Core.AOP.Attributes;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Service.Core.Monitor;
using Blog.Service.Core.Monitor.Output;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Core
{
    /// <summary>
    /// 监控信息
    /// </summary>
    [Route("api/monitor")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v1)]
    public class MonitorController : ApiControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMonitorSvc _monitorSvc;

        public MonitorController(IWebHostEnvironment env, IMonitorSvc monitorSvc)
        {
            _env = env;
            _monitorSvc = monitorSvc;
        }

        /// <summary>
        /// 服务器配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("server-info")]
        [Cacheable]
        public async Task<ServiceResult<ServerInfoDto>> GetServerInfo()
        {
            return await Task.FromResult(ServiceResult<ServerInfoDto>.Successed(new ServerInfoDto()
            {
                EnvironmentName = _env.EnvironmentName,
                OsArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                ContentRootPath = _env.ContentRootPath,
                WebRootPath = _env.WebRootPath,
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                MemoryFootprint = (Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + " MB",
                WorkingTime = TimeSubTract(DateTime.Now, Process.GetCurrentProcess().StartTime)
            }));

            string TimeSubTract(DateTime time1, DateTime time2)
            {
                TimeSpan subTract = time1.Subtract(time2);
                return $"{subTract.Days} 天 {subTract.Hours} 时 {subTract.Minutes} 分 ";
            }
        }

        /// <summary>
        /// 博客统计信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("blog-info")]
        [Cacheable]
        public async Task<ServiceResult<BlogInfoDto>> GetBlogInfo()
        {
            return await Task.FromResult(ServiceResult<BlogInfoDto>.Successed(await _monitorSvc.GetBlogInfo()));
        }
    }
}
