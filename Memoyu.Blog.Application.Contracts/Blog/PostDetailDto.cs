/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：PostDetailDto
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/26 17:26:17
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;

namespace Memoyu.Blog.Application.Contracts.Blog
{
    public class PostDetailDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// HTML
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Markdown
        /// </summary>
        public string Markdown { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public CategoryDto Category { get; set; }

        /// <summary>
        /// 标签列表
        /// </summary>
        public IEnumerable<TagDto> Tags { get; set; }

        /// <summary>
        /// 上一篇
        /// </summary>
        public PostForPagedDto Previous { get; set; }

        /// <summary>
        /// 下一篇
        /// </summary>
        public PostForPagedDto Next { get; set; }
    }
}
