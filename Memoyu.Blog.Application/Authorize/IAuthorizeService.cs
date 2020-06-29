/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：IAuthorizeService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/27 17:47:58
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Threading.Tasks;
using Memoyu.Blog.ToolKits.Base;

namespace Memoyu.Blog.Application.Authorize
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取登录地址（GitHub）
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> GetLoginAddressAsync();
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetAccessTokenAsync(string code);
        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(string accessToken);
    }
}
