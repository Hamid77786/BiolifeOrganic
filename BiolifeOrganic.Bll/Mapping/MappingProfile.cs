using AutoMapper;
using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Bll.ViewModels.Comment;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Bll.ViewModels.Logo;
using BiolifeOrganic.Bll.ViewModels.Organization;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.ProductImage;
using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Bll.ViewModels.WebContact;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Bll.ViewModels.Basket;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Bll.ViewModels.Discount;


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
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt =>
                opt.Condition(src => src.Name != null))
            .ForMember(dest => dest.ImageUrl, opt =>
                opt.Condition(src => src.ImageUrl != null))
            .ForMember(dest => dest.CategoryIcon, opt =>
                opt.Condition(src => src.CategoryIcon != null));



        CreateMap<Contact, ContactViewModel>()
            .ForMember(dest => dest.AppUserUserName,
                       opt => opt.MapFrom(src => src.AppUser!.UserName))
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );

        CreateMap<ContactViewModel, Contact>()
            .ForMember(
                dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName)
            )
            .ForMember(
                dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName)
            );




        CreateMap<CreateContactViewModel, Contact>();
        CreateMap<UpdateContactViewModel, Contact>().ReverseMap();


      

        CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductImages, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.ReviewCount,
                    opt => opt.MapFrom(src => src.Reviews.Count))
                .ForMember(dest => dest.AverageStars,
                    opt => opt.MapFrom(src =>
                        src.Reviews.Any()
                            ? Math.Round(src.Reviews.Average(r => r.Stars), 1)
                            : 0))
                .ForMember(dest => dest.DiscountedPrice,
                    opt => opt.MapFrom(src => src.DiscountedPrice));

        CreateMap<CreateProductViewModel, Product>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DiscountedPrice, opt => opt.Ignore());

        CreateMap<Product, UpdateProductViewModel>()
            .ForMember(dest => dest.ExistingImageUrl,
                opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.ExistingProductImages,
                opt => opt.MapFrom(src =>
                    src.ProductImages.Select(pi => new ProductImageViewModel
                    {
                        Id = pi.Id,
                        ImageUrl = pi.ImageUrl,
                        ProductId = src.Id
                    }).ToList()))
            .ForMember(dest => dest.NewImageFile, opt => opt.Ignore())
            .ForMember(dest => dest.NewProductImages, opt => opt.Ignore())
            .ForMember(dest => dest.ImagesToDelete, opt => opt.Ignore());

        CreateMap<UpdateProductViewModel, Product>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
            .ForMember(dest => dest.DiscountedPrice, opt => opt.Ignore());

        CreateMap<CreateLogoViewModel, Logo>()
           .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        
        CreateMap<UpdateLogoViewModel, Logo>()
            .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.ExistingLogoUrl))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        
        CreateMap<Logo, LogoViewModel>();

        CreateMap<Discount, DiscountViewModel>();

        CreateMap<CreateDiscountViewModel, Discount>();

        CreateMap<UpdateDiscountViewModel, Discount>()
            .ForMember(d => d.UserDiscounts, opt => opt.Ignore());














        CreateMap<Organization, OrganizationViewModel>().ReverseMap();
        CreateMap<CreateOrganizationViewModel, Organization>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UpdateOrganizationViewModel, Organization>();

        
        CreateMap<ProductImage, ProductImageViewModel>()
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null));

        CreateMap<CreateProductImageViewModel, ProductImage>()
          .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
          .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
          .ForMember(dest => dest.Product, opt => opt.Ignore());

        CreateMap<UpdateProductImageViewModel, ProductImage>()
           .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
               src.ImageFile != null ? null : src.ExistingImageUrl)) 
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.Product, opt => opt.Ignore());

       

        CreateMap<Slider, SliderViewModel>();
        CreateMap<CreateSliderViewModel, Slider>();
        CreateMap<UpdateSliderViewModel, Slider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<WebContact, WebContactViewModel>();
        CreateMap<CreateWebContactViewModel, WebContact>();
        CreateMap<UpdateWebContactViewModel, WebContact>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

       

        CreateMap<Comment, CommentViewModel>()
          .ForMember(dest => dest.AppUserName,
                     opt => opt.MapFrom(src => src.AppUser!.UserName));

        CreateMap<CreateCommentViewModel, Comment>();
        CreateMap<UpdateCommentViewModel, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // Wishlist mappings
        CreateMap<Wishlist, WishlistItemViewModel>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product != null ? src.Product.ImageUrl : null))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product != null ? src.Product.DiscountedPrice : 0))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Product != null && src.Product.QuantityAvailable > 0));

        CreateMap<List<Wishlist>, WishlistViewModel>()
            .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.FirstOrDefault() != null ? src.First().AppUserId : null))
            .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.FirstOrDefault() != null && src.First().AppUser != null ? src.First().AppUser!.FullName : null))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

        CreateMap<Wishlist, WishlistViewModel>()
            .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
            .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.FullName : null))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => new List<WishlistItemViewModel> { 
                new WishlistItemViewModel {
                    ProductId = src.ProductId,
                    ProductName = src.Product != null ? src.Product.Name : null,
                    ImageUrl = src.Product != null ? src.Product.ImageUrl : null,
                    Price = src.Product != null ? src.Product.DiscountedPrice : 0,
                    IsAvailable = src.Product != null && src.Product.QuantityAvailable > 0
                }
            }));

        CreateMap<CreateWishlistViewModel, Wishlist>()
            .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.AppUser, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        CreateMap<UpdateWishlistViewModel, Wishlist>()
            .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.AppUser, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());


        CreateMap<Review, ReviewViewModel>()
            .ForMember(d => d.AppUserName,
                o => o.MapFrom(s => s.AppUser != null ? s.AppUser.UserName : null))
            .ForMember(d => d.AppUserPhoto,
                o => o.MapFrom(s => s.AppUser != null ? s.AppUser.ProfileImagePath : null))
            .ForMember(d => d.ProductName,
                o => o.MapFrom(s => s.Product != null ? s.Product.Name : null));




    }
}

