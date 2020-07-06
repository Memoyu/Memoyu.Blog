/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：PagingInput
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/6 10:25:03
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.ComponentModel.DataAnnotations;

namespace Memoyu.Blog.Application.Contracts
{
    /// <summary>
    /// 分页请求入参
    /// </summary>
    public class PagingInput
    {

        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// 限制条数
        /// </summary>
        [Range(10, 30)]
        public int Limit { get; set; } = 10;
    }
}
