using AutoMapper;
using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Bll.ViewModels.Logo;
using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Bll.ViewModels.OrderItem;
using BiolifeOrganic.Bll.ViewModels.Organization;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryViewModel>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<CreateCategoryViewModel, Category>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) 
            .ForMember(dest => dest.Products, opt => opt.Ignore());
        
        CreateMap<UpdateCategoryViewModel, Category>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom((src, dest) =>
                src.NewImageFile != null
                ? dest.ImageUrl 
                : src.ImageUrl
            ))
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt =>
                opt.Condition(src => src.Name != null))
            .ForMember(dest => dest.CategoryIcon, opt =>
                opt.Condition(src => src.CategoryIcon != null));

        
        CreateMap<Contact, ContactViewModel>()
            .ForMember(dest => dest.AppUserUserName,
                       opt => opt.MapFrom(src => src.AppUser!.FirstName));
       
        CreateMap<CreateContactViewModel, Contact>();
        CreateMap<UpdateContactViewModel, Contact>().ReverseMap();


       CreateMap<LogoViewModel, Logo>().ReverseMap();
       CreateMap<CreateLogoViewModel, Logo>().ReverseMap();
      
       CreateMap<CreateLogoViewModel, Logo>()
              .ForMember(dest => dest.LogoUrl, opt => opt.Ignore());
       
       CreateMap<UpdateLogoViewModel, Logo>()
            .ForMember(dest => dest.LogoUrl, opt => opt.Ignore());

        CreateMap<Order, OrderViewModel>()
            .ForMember(dest => dest.AppUserEmail,
                opt => opt.MapFrom(src => src.AppUser.Email))
            .ForMember(dest => dest.OrganizationName,
                opt => opt.MapFrom(src => src.Organization != null ? src.Organization.Name : null))
            .ReverseMap();

        CreateMap<CreateOrderViewModel, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<UpdateOrderViewModel, Order>()
           .ForMember(dest => dest.OrderNumber, opt => opt.Ignore()) 
           .ForMember(dest => dest.AppUserId, opt => opt.Ignore()) 
           .ForMember(dest => dest.OrganizationId, opt => opt.Ignore())
           .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal));

        
       
        CreateMap<UpdateOrderItemViewModel, OrderItem>()
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        
        CreateMap<OrderItem, UpdateOrderItemViewModel>();

        CreateMap<Organization, OrganizationViewModel>().ReverseMap();
        CreateMap<CreateOrganizationViewModel, Organization>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UpdateOrganizationViewModel, Organization>();

        CreateMap<Product, ProductViewModel>()
               .ForMember(dest => dest.CategoryName,
                   opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
               .ForMember(dest => dest.ProductImageUrls,
                   opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()))
               .ForMember(dest => dest.DiscountedPrice,
                   opt => opt.MapFrom(src => src.DiscountedPrice));

       
        CreateMap<CreateProductViewModel, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) 
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore()) 
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

       
        CreateMap<UpdateProductViewModel, Product>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) 
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore()) 
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId.HasValue ? src.CategoryId.Value : 0))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        
        CreateMap<Product, UpdateProductViewModel>()
            .ForMember(dest => dest.ExistingImageUrl,
                opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.NewImageFile, opt => opt.Ignore())
            .ForMember(dest => dest.ExistingProductImages,
                opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()))
            .ForMember(dest => dest.NewProductImages, opt => opt.Ignore());









    }
}
