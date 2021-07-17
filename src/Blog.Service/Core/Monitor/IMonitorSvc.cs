using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Service.Core.Monitor.Output;

namespace Blog.Service.Core.Monitor
{
    public interface IMonitorSvc
    {
        /// <summary>
        /// 获取博客统计信息
        /// </summary>
        /// <returns></returns>
        Task<BlogInfoDto> GetBlogInfo();
    }
}
