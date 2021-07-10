using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.AOP.Attributes;
using Blog.Core.Common.Configs;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Core.Domains.Entities.Core;
using Blog.Core.Extensions;
using Blog.Core.Interface.IRepositories.Core;
using Blog.Service.Base;
using Blog.Service.Core.Auth.Input;
using Blog.Service.Core.Auth.Input.GitHub;
using Blog.Service.Core.Auth.Output.GitHub;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Blog.Service.Core.Auth
{
    public class AuthorizeSvc : ApplicationSvc, IAuthorizeSvc
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IAdminUserRepo _adminUserRepo;
        private readonly IUserRepo _userRepo;

        public AuthorizeSvc(IHttpClientFactory httpClient, IAdminUserRepo adminUserRepo, IUserRepo userRepo)
        {
            _httpClient = httpClient;
            _adminUserRepo = adminUserRepo;
            _userRepo = userRepo;
        }

        /// <summary>
        /// 构建GitHub登录地址
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetLoginUrlAsync()
        {
            var input = new AuthorizeInput();
            var url = string.Concat(new string[]
            {
                    GitHubConfig.AuthUrl,
                    "?client_id=", input.ClientId,
                    "&scope=", input.Scope,
                    "&state=", input.State,
                    "&redirect_uri=", input.RedirectUri
            });
            return await Task.FromResult(new ServiceResult<string>().IsSuccess(url));
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Cacheable(5 * 60)]
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ServiceResult<string>();
            if (string.IsNullOrEmpty(code))//判断Code
            {
                result.IsFailed("code为空");
                return result;
            }

            var input = new AccessTokenInput();
            var content = new StringContent($"code={code}&client_id={input.ClientId}&redirect_uri={input.RedirectUri}&client_secret={input.ClientSecret}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            using var client = _httpClient.CreateClient();//创建HttpClient请求
            var httpResponse = await client.PostAsync(GitHubConfig.AccessTokenUrl, content);//请求获取AccessToken
            var response = await httpResponse.Content.ReadAsStringAsync();

            if (response.StartsWith("access_token"))
                result.IsSuccess(response.Split("=")[1].Split("&").First());
            else
                result.IsFailed("code不正确");

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 登录GitHub成功，生成Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [Cacheable(5 * 60 * 60 - 10)]
        public async Task<ServiceResult<string>> GenerateTokenAsync(string accessToken)
        {
            var result = new ServiceResult<string>();
            if (string.IsNullOrEmpty(accessToken))//判断Code
            {
                result.IsFailed("accessToken为空");
                return result;
            }
            var url = $"{GitHubConfig.UserUrl}";//拼接请求GitHub用户信息Url
            using var client = _httpClient.CreateClient();//创建请求
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.14 Safari/537.36 Edg/83.0.478.13");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.StatusCode != HttpStatusCode.OK)//判断请求响应是否成功
            {
                result.IsFailed("accessToken不正确，获取用户信息失败");
                return result;
            }

            var content = await httpResponse.Content.ReadAsStringAsync();//获取响应内容
            var userInfo = content.FromJson<UserOutput>();
            if (userInfo == null)
            {
                result.IsFailed("未获取到用户信息");
                return result;
            }
            //将用户信息写入数据库，存在则更新
            var entity = Mapper.Map<UserEntity>(userInfo);
            await _userRepo.AddOrUpdateAsync(entity);
            var admins = await _adminUserRepo.GetsAsync();
            if (admins.All(a => a.UserId != userInfo.Id))
            {
                result.IsFailed("当前用户未授权");
                return result;
            }
            //创建使用者属性
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userInfo.Name),
                new Claim(ClaimTypes.Email,userInfo.Email??""),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(Appsettings.JwtBearer.Expires)).ToUnixTimeSeconds()}"),//token过期时间
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")//token在当前时间之前不生效
            };
            var key = new SymmetricSecurityKey(Appsettings.JwtBearer.SecurityKey.SerializeUtf8());
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//签名凭证
            var securityToken = new JwtSecurityToken(
                issuer: Appsettings.JwtBearer.Issuer,//发布者
                audience: Appsettings.JwtBearer.Audience,//可访问者
                claims: claims,//可访问者信息
                expires: DateTime.Now.AddMinutes(Appsettings.JwtBearer.Expires),//过期时间
                signingCredentials: signingCredentials//签名
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);//应用securityToken
            return await Task.FromResult(result.IsSuccess(token));
        }
    }
}
