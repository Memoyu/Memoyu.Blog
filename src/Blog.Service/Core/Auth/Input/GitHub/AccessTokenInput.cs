using Blog.Core.Domains.Common.Consts;

namespace Blog.Service.Core.Auth.Input.GitHub
{
    public class AccessTokenInput
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientId = GitHubConfig.ClientId;

        /// <summary>
        /// Client Secret
        /// </summary>
        public string ClientSecret = GitHubConfig.ClientSecret;

        /// <summary>
        /// 调用API_Authorize获取到的Code值
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public string RedirectUri = GitHubConfig.RedirectUri;

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
    }
}
