/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：QueryPostDto
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/6 9:57:54
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

namespace Memoyu.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// 文章查询模型
    /// </summary>
    public class QueryPostDto
    {
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Posts
        /// </summary>
        public IEnumerable<PostBriefDto> Posts { get; set; }
    }
}
