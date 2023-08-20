using AspNetCoreAngularTemplate.Application.Common.Interfaces;
using AspNetCoreAngularTemplate.Domain.Constants;
using AspNetCoreAngularTemplate.Infrastructure.Data;
using AspNetCoreAngularTemplate.Infrastructure.Data.Interceptors;
using AspNetCoreAngularTemplate.Infrastructure.Email;
using AspNetCoreAngularTemplate.Infrastructure.Email.Templates;
using AspNetCoreAngularTemplate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        // todo: 
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IAccountManager, AccountManager>();
        
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddOptions<EmailSenderOptions>().Bind(configuration.GetSection(nameof(EmailSenderOptions))).ValidateDataAnnotations();

        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddOptions<EmailTemplateServiceOptions>().Bind(configuration.GetSection(nameof(EmailTemplateServiceOptions))).ValidateDataAnnotations();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
