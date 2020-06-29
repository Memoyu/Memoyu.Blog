/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：AuthorizeService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/27 17:48:22
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Memoyu.Blog.Domain.Configurations;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;
using Memoyu.Blog.ToolKits.GitHub;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Memoyu.Blog.Application.Authorize.Impl
{
    public class AuthorizeService : ServiceBase, IAuthorizeService
    {
        private readonly IHttpClientFactory _httpClient;
        public AuthorizeService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// 构建GitHub登录地址
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            var result = new ServiceResult<string>();

            var request = new AuthorizeRequest();
            var address = string.Concat(new string[]
            {
               GitHubConfig.API_Authorize,
               "?client_id=", request.Client_ID,
               "&scope=", request.Scope,
               "&state=", request.State,
               "&redirect_uri=", request.Redirect_Uri
            });
            result.IsSuccess(address);
            return await Task.FromResult(result);
        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ServiceResult<string>();
            if (string.IsNullOrEmpty(code))//判断Code
            {
                result.IsFailed("code为空");
                return result;
            }

            var request = new AccessTokenRequest();
            var content = new StringContent($"code={code}&client_id={request.Client_ID}&redirect_uri={request.Redirect_Uri}&client_secret={request.Client_Secret}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            using var client = _httpClient.CreateClient();//创建HttpClient请求
            var httpResponse = await client.PostAsync(GitHubConfig.API_AccessToken, content);//请求获取AccessToken
            var response = await httpResponse.Content.ReadAsStringAsync();

            if (response.StartsWith("access_token"))
                result.IsSuccess(response.Split("=")[1].Split("&").First());
            else
                result.IsFailed("code不正确");

            return result;
        }
        /// <summary>
        /// 登录GitHub成功，生成Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GenerateTokenAsync(string accessToken)
        {
            var result = new ServiceResult<string>();
            if (string.IsNullOrEmpty(accessToken))//判断Code
            {
                result.IsFailed("accessToken为空");
                return result;
            }

            var url = $"{GitHubConfig.API_User}?access_token={accessToken}";//拼接请求GitHub用户信息Url
            using var client = _httpClient.CreateClient();//创建请求
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.14 Safari/537.36 Edg/83.0.478.13");
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.StatusCode != HttpStatusCode.OK)//判断请求响应是否成功
            {
                result.IsFailed("accessToken不正确，获取用户信息失败");
                return result;
            }

            var content = await httpResponse.Content.ReadAsStringAsync();//获取响应内容
            var userInfo = content.FromJson<UserResponse>();
            if (userInfo == null)
            {
                result.IsFailed("未获取到用户信息");
                return result;
            }
            if (userInfo.Id != (GitHubConfig.UserId))
            {
                result.IsFailed("当前用户未授权");
                return result;
            }
            //创建使用者属性
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userInfo.Name),
                new Claim(ClaimTypes.Email,userInfo.Email??""),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(AppSettings.JWT.Expires)).ToUnixTimeSeconds()}"),//token过期时间，30分钟
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")//token在当前时间之前不生效
            };
            var key = new SymmetricSecurityKey(AppSettings.JWT.SecurityKey.SerializeUtf8());
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//签名凭证
            var securityToken = new JwtSecurityToken(
                issuer: AppSettings.JWT.Domain,//发布者
                audience: AppSettings.JWT.Domain,//可访问者
                claims: claims,//可访问者信息
                expires: DateTime.Now.AddMinutes(AppSettings.JWT.Expires),//过期时间，30分钟
                signingCredentials: signingCredentials//签名
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);//应用securityToken
            result.IsSuccess(token);
            return result;
        }
    }
}
