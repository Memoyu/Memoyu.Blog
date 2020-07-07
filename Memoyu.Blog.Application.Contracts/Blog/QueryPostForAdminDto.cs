/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：QueryPostForAdminDto
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/7 14:53:12
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
    /// <summary>
    /// 文章查询模型（管理员）
    /// </summary>
    public class QueryPostForAdminDto : QueryPostDto
    {
        /// <summary>
        /// 覆盖父类Posts属性
        /// </summary>
        public new IEnumerable<PostBriefForAdminDto> Posts { get; set; }
    }
}
