using BiolifeOrganic.Bll.Mapping;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BiolifeOrganic.Bll;

public static class BussinessLogicLayerServiceRegistration
{
    public static IServiceCollection AddBussinessLogicLayerServices(this IServiceCollection services)
    {
        services.AddAutoMapper(confg => confg.AddProfile<MappingProfile>());
        services.AddScoped(typeof(ICrudService<,,,>), typeof(CrudManager<,,,>));
        services.AddScoped<IHomeService,HomeManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<ISliderService, SliderManager>();
        services.AddScoped<ICommentService, CommentManager>();
        services.AddScoped<IOrderService, OrderManager>();
        services.AddScoped<IOrganizationService, OrganizationManager>();
        services.AddScoped<IContactService, ContactManager>();
        services.AddScoped<ILogoService, LogoManager>();
        services.AddScoped<FileService>();
        services.AddScoped<IReviewService, ReviewManager>();
        services.AddScoped<IWebContactService, WebContactManager>();
        services.AddScoped<IWishlistService, WishlistManager>();
        services.AddScoped<IEmailService, EmailManager>();
        services.AddScoped<ISearchService, SearchManager>();
        services.AddScoped<IHeaderService, HeaderManager>();
        services.AddScoped<BasketManager>();
        services.AddScoped<IShopService, ShopManager>();
        services.AddScoped<INewsletterService, NewsletterManager>();
        services.AddScoped<ICheckoutService, CheckoutManager>();
        services.AddScoped<IDiscountService,DiscountManager>();

        return services;
    }
}
