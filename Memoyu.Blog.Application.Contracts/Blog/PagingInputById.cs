/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：PagingInputById
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/21 13:31:41
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.ComponentModel.DataAnnotations;

namespace Memoyu.Blog.Application.Contracts.Blog
{
    public class PagingInputById : PagingInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
