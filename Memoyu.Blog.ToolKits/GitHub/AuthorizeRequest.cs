/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：AuthorizeRequest
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/27 18:10:02
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Memoyu.Blog.Domain.Configurations;

namespace Memoyu.Blog.ToolKits.GitHub
{
    public class AuthorizeRequest
    {
        /// <summary>
        /// 客户端Id
        /// </summary>
        public string Client_ID = GitHubConfig.Client_ID;
        /// <summary>
        /// 授权回调地址
        /// </summary>
        public string Redirect_Uri = GitHubConfig.Redirect_Uri;
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; } = Guid.NewGuid().ToString("N");
        /// <summary>
        /// 该参数可选，需要调用Github哪些信息，可以填写多个，以逗号分割，比如：scope=user,public_repo。
        /// 如果不填写，那么你的应用程序将只能读取Github公开的信息，比如公开的用户信息，公开的库(repository)信息以及gists信息
        /// </summary>
        public string Scope { get; set; } = "user,public_repo";
    }
}
