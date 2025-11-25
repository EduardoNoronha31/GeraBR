using System.Threading.RateLimiting;

namespace GeraBR.Api.RateLimiting;

public static class RateLimitingExtensions
{
    private static string GetUserIp(HttpContext context)
    {
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(forwardedFor))
            return forwardedFor.Split(',')[0].Trim();

        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddPolicy("QueryPolicy", context =>
            {
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetUserIp(context),
                    factory: key => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 50,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    });
            });

            options.AddPolicy("CommandPolicy", context =>
            {
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetUserIp(context),
                    factory: key => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    });
            });


            options.RejectionStatusCode = 429;
        });

        return services;
    }
}