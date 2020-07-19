/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：PagedList
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/24 18:00:25
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using Memoyu.Blog.BlazorApp.Response.Base.Page;
using Memoyu.Blog.ToolKits.Base.Page;

namespace Memoyu.Blog.BlazorApp.Response.Base
{
    public class PagedList<T> : ListResult<T>, IPagedList<T>
    {
        public PagedList()
        {

        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Total { get; set; }
        public PagedList(int total, IReadOnlyList<T> result) : base(result)
        {
            Total = total;
        }
    }
}
