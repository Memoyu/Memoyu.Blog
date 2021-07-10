using Autofac;
using Blog.Core.Common.Configs;
using Blog.Core.Domains.Common.Consts;
using Blog.Core.Domains.Common.Enums.Base;
using Blog.Core.Exceptions;
using Blog.Service.Core.Auth;
using Blog.Service.Core.Auth.Input;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;

namespace Blog.Controllers.Core
{
    /// <summary>
    /// 管理员授权相关
    /// </summary>
    [Route("api/account")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v2)]
    public class AccountController : ApiControllerBase
    {
        private readonly IAuthorizeSvc _authorizeSvc;
        public AccountController(IAuthorizeSvc authorizeSvc)
        {
            _authorizeSvc = authorizeSvc;
        }

        /// <summary>
        /// 获取登录GitHub地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("url")]
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeSvc.GetLoginUrlAsync();
        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("access_token")]
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            return await _authorizeSvc.GetAccessTokenAsync(code);
        }
        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("token")]
        public async Task<ServiceResult<string>> GenerateTokenAsync(string accessToken)
        {
            return await _authorizeSvc.GenerateTokenAsync(accessToken);
        }

    }
}
