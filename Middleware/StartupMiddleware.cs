using mHealthProject.Interfaces;
using mHealthProject.Models.Configuration;
using mHealthProject.Services;
using Microsoft.IdentityModel.Tokens;

namespace mHealthProject.Middleware;

public static class StartupMiddleware
{
    public static void MPathStartupServices(this IServiceCollection services)
    {
        var appConfig = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var MPathConfig = new MHealthConfiguration()
        {
            //map app settings.json to mHealth configuration to be used in app
            ConnectionString = appConfig.GetSection("DbConnection").Value ?? "", 
            JwtSecret = appConfig.GetSection("JwtSecret").Value ?? "",
        };
        
        services.AddSingleton(MPathConfig);
        services.AddSingleton<ImHealthDB, MHealthDBService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<ISecurityService, SecurityService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IPatientService, PatientService>();
        
        
        
    }
}