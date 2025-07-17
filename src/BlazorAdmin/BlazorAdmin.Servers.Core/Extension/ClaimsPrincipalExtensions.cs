using BlazorAdmin.Servers.Core.Data.Constants;
using System.Security.Claims;

namespace BlazorAdmin.Servers.Core.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            try
            {
                var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimConstant.UserId)!.Value;
                return int.Parse(userId);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimConstant.UserName)!.Value;
        }
    }
}
