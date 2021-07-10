using Blog.Core.Common.Configs;

namespace Blog.Core.Domains.Common.Consts
{
    public class GitHubConfig
    {
        /// <summary>
        /// GET请求，跳转GitHub登录界面，获取用户授权，得到code
        /// </summary>
        public static string AuthUrl = "https://github.com/login/oauth/authorize";

        /// <summary>
        /// POST请求，根据code得到access_token
        /// </summary>
        public static string AccessTokenUrl = "https://github.com/login/oauth/access_token";

        /// <summary>
        /// GET请求，根据access_token得到用户信息
        /// </summary>
        public static string UserUrl = "https://api.github.com/user";

        /// <summary>
        /// Github UserId
        /// </summary>
        public static int UserId = Appsettings.GitHub.UserId;

        /// <summary>
        /// Client ID
        /// </summary>
        public static string ClientId = Appsettings.GitHub.ClientId;

        /// <summary>
        /// Client Secret
        /// </summary>
        public static string ClientSecret = Appsettings.GitHub.ClientSecret;

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public static string RedirectUri = Appsettings.GitHub.RedirectUri;

        /// <summary>
        /// Application name
        /// </summary>
        public static string AppName = Appsettings.GitHub.AppName;
    }
}
