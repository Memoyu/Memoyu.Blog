/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Controllers
*   文件名称 ：HelloWorldController
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 0:25:20
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using Memoyu.Blog.HelloWorld;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;

namespace Memoyu.Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController:AbpController
    {
        private readonly IHelloWorldService _helloWorldService;
        public HelloWorldController(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }
        [HttpGet]
        public string HelloWorld()
        {
            return _helloWorldService.HelloWorld();
        }
    }
}
