using BlazorAdmin.Servers.Core.Data.Constants;
using BlazorAdmin.Servers.Core.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorAdmin.Servers.Core.Auth
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly JwtHelper _jwtHelper;
        private AuthenticationState currentUser = new AuthenticationState(new ClaimsPrincipal());
        private readonly NavigationManager _navigationManager; // 用于重定向
        public JwtAuthStateProvider(ExternalAuthService service, IHttpContextAccessor httpContextAccessor, JwtHelper jwtHelper,


             NavigationManager navigationManager)
        {

            _navigationManager = navigationManager;
            _contextAccessor = httpContextAccessor;
            _jwtHelper = jwtHelper;

            // 监听用户变更事件，更新 AuthenticationState
            service.UserChanged += (newUser) =>
            {
                currentUser = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(currentUser));
            };
          
        }

        // 从 Cookie 获取 Token，并解析为 ClaimsPrincipal
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (currentUser.User.Identities.Any()&& currentUser.User.Claims.Any())
            {
                return Task.FromResult(currentUser);
            }
            var tokenCookie = _contextAccessor.HttpContext?.Request.Cookies
                .FirstOrDefault(c => c.Key == CommonConstant.UserToken).Value;

            // 如果 token 存在且有效，返回用户状态
            if (!string.IsNullOrEmpty(tokenCookie))
            {
                var user = _jwtHelper.ValidToken(tokenCookie);
                if (user != null)
                {
                    currentUser = new AuthenticationState(user);


                    return Task.FromResult(currentUser);
                }
            }
            else
            {
                var Test = tokenCookie;
            }
                //if (!_navigationManager.Uri.ToString().Contains("dashboard/login"))
                //{
                //    // 如果用户不存在或 token 无效，重定向到登录页面
                //     _navigationManager.NavigateTo("/dashboard/login", true);
                //}

                // 如果 token 不存在或无效，返回空用户状态
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))); // 匿名用户
        }
    }

    // 外部身份验证服务，管理用户状态变更
    public class ExternalAuthService
    {
        public event Action<ClaimsPrincipal>? UserChanged;
        private JwtHelper _jwtHelper;

        public ExternalAuthService(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        // 设置当前用户，触发 UserChanged 事件
        public void SetCurrentUser(string token)
        {
            var user = _jwtHelper.ValidToken(token);
            if (user == null)
            {
                user = new ClaimsPrincipal();
            }
            UserChanged?.Invoke(user);
        }
    }
}
