using Chatverse.Application.Common.Interfaces;
using Chatverse.Domain.Identity;
using Chatverse.Infrastructure.Persistance.Interceptors;
using Chatverse.Infrastructure.Persistance;
using Chatverse.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chatverse.Application.Common.Security.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Chatverse.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<AppDbContextInitialiser>();
           
            services.AddScoped<IEmailService, EmailConfirmService>();
            services.AddScoped<IGoogleCloudService, GoogleCloudService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }

    }
}
