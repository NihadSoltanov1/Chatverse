namespace Chatverse.Infrastructure;

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
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
        services.AddSingleton<IDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

        services.AddScoped<IEmailService, EmailConfirmService>();
        services.AddScoped<IGoogleCloudService, GoogleCloudService>();
        services.AddScoped<IStoryScheduleService, StoryScheduleService>();
        services.AddScoped<ITokenHandler, Services.TokenHandler>();
        services.AddTransient<IDateTime, DateTimeService>();
        return services;
    }

}
