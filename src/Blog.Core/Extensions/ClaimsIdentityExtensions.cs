using Blog.Core.Domains.Common.Consts;
using System.Linq;
using System.Security.Claims;

namespace Blog.Core.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static int FindUserId(this ClaimsPrincipal principal)
        {
            Claim userIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdOrNull == null || string.IsNullOrWhiteSpace(userIdOrNull.Value))
            {
                return 0;
            }

            return int.Parse(userIdOrNull.Value);
        }

        public static string FindUserName(this ClaimsPrincipal principal)
        {
            Claim userName = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            return userName == null ? "Unkown" : userName.Value;
        }

    }
}
