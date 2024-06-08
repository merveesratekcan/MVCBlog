using AspNetCoreHero.ToastNotification;
using HS14MVC.Infrastructure.AppContext;
using Microsoft.AspNetCore.Identity;

namespace HS14MVC.UI.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddNotyf(config =>
            {
                config.HasRippleEffect = true;
                config.DurationInSeconds = 5;
                config.Position = NotyfPosition.BottomRight;

            });
            return services;
        }
    }
}
