/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：ListResult
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/24 17:56:02
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

namespace Memoyu.Blog.ToolKits.Base.Page
{
    public class ListResult<T> : IListResult<T>
    {
        IReadOnlyList<T> _item;

        public IReadOnlyList<T> Item
        {
            get => _item ?? (Item = new List<T>());
            set => _item = value;
        }
        public ListResult()
        {

        }
        public ListResult(IReadOnlyList<T> item)
        {
            Item = item;
        }
    }
}
