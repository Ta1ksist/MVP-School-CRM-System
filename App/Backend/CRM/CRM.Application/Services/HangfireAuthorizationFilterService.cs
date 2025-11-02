using Hangfire.Dashboard;
using Microsoft.Extensions.Hosting;

namespace CRM.Application.Services;

public class HangfireAuthorizationFilterService: IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment == Environments.Development)
            return true;
        
        var httpContext = context.GetHttpContext();
        return httpContext.User.Identity?.IsAuthenticated == true;
    }
}