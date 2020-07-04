using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Memoyu.Blog.HttpApi.Hosting.Filters
{
    public class MemoyuExceptionFilter : IExceptionFilter
    {
        private readonly ILog _log;
        public MemoyuExceptionFilter()
        {
            _log = LogManager.GetLogger(typeof(MemoyuExceptionFilter));
           
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            // 错误日志记录
            _log.Error($"{context.HttpContext.Request.Path}|{context.Exception.Message}", context.Exception);
        }
    }
}
