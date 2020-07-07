/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：PostBriefForAdminDto
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/7 14:52:30
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
    /// 文章简要（管理员）
    /// </summary>
    public class PostBriefForAdminDto : PostBriefDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
    }
}
