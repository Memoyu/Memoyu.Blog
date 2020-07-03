/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：BlogService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 16:22:30
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
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.ToolKits.Base;

namespace Memoyu.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        #region Posts

        public Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
