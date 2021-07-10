using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;

namespace Blog.Service.Core.Auth
{
    public interface IAuthorizeSvc
    {
        /// <summary>
        /// 获取登录地址（GitHub）
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> GetLoginUrlAsync();
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
