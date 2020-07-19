/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：PostForAdminDto
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 17:41:42
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;

namespace Memoyu.Blog.BlazorApp.Response.Blog
{
    public class PostForAdminDto:PostDto
    {
        /// <summary>
        /// 标签列表
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}
