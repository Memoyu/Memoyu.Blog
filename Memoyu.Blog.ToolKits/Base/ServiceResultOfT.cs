/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：ServiceResultOfT
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/24 17:47:48
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
using Memoyu.Blog.ToolKits.Base.Enum;

namespace Memoyu.Blog.ToolKits.Base
{
    /// <summary>
    /// 服务层响应实体（泛型）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T> : ServiceResult where T : class
    {
        /// <summary>
        /// 响应结果
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 响应成功-带结果
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public void IsSuccess(T result = null, string message = "")
        {
            Message = message;
            Result = result;
            Code = ServiceResultCode.Succeed;
        }
    }
}
