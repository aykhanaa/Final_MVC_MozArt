using Final_MozArt.Services;
using Final_MozArt.Services.Interfaces;

namespace Final_MozArt
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ICategoryService, CategoryService>();







            return services;
        }
    }
}
