using System.IdentityModel.Tokens.Jwt;
using mHealthProject.Interfaces;
using mHealthProject.Models.User;
using mHealthProject.Services;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace mHealthProject.Middleware;

public class PipelineMiddleware(RequestDelegate next)
{
    private readonly ISecurityService? _secSvc = new SecurityService();

    private readonly List<string> _noAuthNeededList =
    [
        "/util/ping",
        "/auth",
        "/account",
        "/account/create",
        "patient/list"
    ];

    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();

        var isAuthNeededForPath = true;

        context.Response.Headers.Append("Access-Control-Allow-Headers",
            context.Request.Headers.AccessControlRequestHeaders);
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
        context.Response.ContentType = "application/json";

        var reqPath = context.Request.Path.Value.ToLower();

        try
        {
            if (!string.IsNullOrEmpty(reqPath))
            {
                // check if the path is on the whitelist
                if (_noAuthNeededList.Contains(reqPath))
                {
                    isAuthNeededForPath = false;
                    await next(context);
                }
                else
                {
                    var tokenValues = new StringValues();
                    context.Request.Headers.TryGetValue("MHP-TOKEN", out tokenValues);

                    // if there is no token, then unauthorized
                    if (tokenValues.Count.Equals(0))
                    {
                        // return  
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync("Unauthorized User");
                    }

                    // Extract JWT payload from the JWT Token
                    JwtPayload payload = extractJwtToken(tokenValues);
                    
                    // Check if the payload is null if so then respond with 401 
                    if (payload == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("Unauthorized user");
                    }

                    // Check to see if the user is not an admin attempting to perform an admin action
                    var userType = Enum.Parse<UserTypeEnum>(GetClaimAsString(payload, "userType"));

                    // If it is a user type of user
                    if (userType == UserTypeEnum.User)
                    {
                        // if the path is to view patient data then send unauthorized
                        if ((reqPath != "/patient/list") && (reqPath != "/patient/search"))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync("Unauthorized action");                            
                        }
                    }

                    // Continue the request to the path
                    await next(context);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format($"An Error has occurred: {ex.StackTrace}"));
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var resMsg = ex.Message.ToLower().Contains("object ref") ? "Oops, something went wrong" : ex.Message;
            await context.Response.WriteAsync(resMsg);
        }
    }
    
    private JwtPayload extractJwtToken(string token)
    {
        return _secSvc.RetrieveJwtPayload(token);
    }
    
    private string GetClaimAsString(JwtPayload jwt, string claimName)
    {
        return jwt.ContainsKey(claimName) ? jwt.GetValueOrDefault(claimName).ToString() : "";
    }
}

public static class MHealthMiddlewareExtension
{
    public static IApplicationBuilder UseMHealthMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PipelineMiddleware>();
    }
}