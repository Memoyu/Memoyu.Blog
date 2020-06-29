/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：AccessTokenRequest
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/27 18:21:46
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Memoyu.Blog.Domain.Configurations;

namespace Memoyu.Blog.ToolKits.GitHub
{
    public class AccessTokenRequest
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public string Client_ID = GitHubConfig.Client_ID;

        /// <summary>
        /// Client Secret
        /// </summary>
        public string Client_Secret = GitHubConfig.Client_Secret;

        /// <summary>
        /// 调用API_Authorize获取到的Code值
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public string Redirect_Uri = GitHubConfig.Redirect_Uri;

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
    }
}
