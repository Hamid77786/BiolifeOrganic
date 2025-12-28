using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using BiolifeOrganic.Dll.Reprositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BiolifeOrganic.Dll;

public static class DataAccessLayerServiceRegistration
{
    public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(option=>
          option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), option =>
          {
              option.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
          }));

        services.AddScoped<DataInitializer>();
        services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
        services.AddScoped<ICategoryRepository,CategoryRepository>();
        services.AddScoped<IContactRepository,ContactRepository>();
        services.AddScoped<ILogoRepository, LogoRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<IWebContactRepository, WebContactRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<INewsletterSubscriberRepository, NewsletterSubscriberRepository>();
        services.AddScoped<IAdminContactRepository, AdminContactRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IUserDiscountRepository, UserDiscountRepository>();






        return services;
    }
}
