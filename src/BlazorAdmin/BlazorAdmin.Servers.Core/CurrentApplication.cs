using Microsoft.AspNetCore.Builder;

namespace BlazorAdmin.Servers.Core
{
    public static class CurrentApplication
    {
        public static WebApplication Application { get; set; } = null!;

        public static string AdminPath { get; set; } = "/dashboard";
    }
}
