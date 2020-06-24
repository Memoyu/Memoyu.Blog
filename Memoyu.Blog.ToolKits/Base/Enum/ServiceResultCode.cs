/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：ServiceResultCode
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/24 17:34:15
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

namespace Memoyu.Blog.ToolKits.Base.Enum
{
    /// <summary>
    /// 服务层响应枚举码
    /// </summary>
    public enum ServiceResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 1,
    }
}
