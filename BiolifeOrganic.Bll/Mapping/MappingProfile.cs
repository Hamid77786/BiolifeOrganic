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
using BiolifeOrganic.Bll.ViewModels.User;
using BiolifeOrganic.Bll.ViewModels.UserDiscount;
using BiolifeOrganic.Bll.ViewModels.Order;


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

        CreateMap<AppUser, UserViewModel>()
           .ForMember(dest => dest.OrdersCount,
               opt => opt.MapFrom(src => src.Orders.Count))
           .ForMember(dest => dest.WishlistCount,
               opt => opt.MapFrom(src => src.Wishlists.Count))
           .ForMember(dest => dest.UserDiscountsCount,
               opt => opt.MapFrom(src => src.UserDiscounts.Count))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.IsBlocked,
                opt => opt.MapFrom(src =>
                    src.LockoutEnd.HasValue &&
                    src.LockoutEnd.Value > DateTimeOffset.UtcNow))

            .ForMember(dest => dest.IsAdmin,
                opt => opt.Ignore());

        CreateMap<AppUser, UserDetailsViewModel>()
            .ForMember(d => d.Orders, opt => opt.Ignore())
            .ForMember(d => d.Wishlists, opt => opt.Ignore())
            .ForMember(d => d.Discounts, opt => opt.Ignore())
            .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.UserDiscounts))
            .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.LockoutEnd != null && src.LockoutEnd > DateTimeOffset.UtcNow));

        CreateMap<UserDiscount, UserDiscountViewModel>()
            .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId))
            .ForMember(dest => dest.DiscountName, opt => opt.MapFrom(src => src.Discount != null ? src.Discount.Code : string.Empty))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.Discount != null ? src.Discount.Percentage : 0))
            .ForMember(dest => dest.IsUsed, opt => opt.MapFrom(src => src.IsUsed))
            .ForMember(dest => dest.UsedAt, opt => opt.MapFrom(src => src.UsedAt));




        CreateMap<Order, OrderDetailsViewModel>()
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.SubtotalAmount, opt => opt.MapFrom(src => src.SubTotalAmount))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
            .ForMember(dest => dest.DiscountCode, opt => opt.MapFrom(src => src.DiscountCode))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.Categories, opt => opt.Ignore());

        CreateMap<OrderItem, OrderItemViewModel>()
           .ForMember(d => d.Subtotal,
               o => o.MapFrom(s => s.Price * s.Quantity));

        CreateMap<Wishlist, WishlistItemViewModel>()
         .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
         .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
         .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product != null ? src.Product.ImageUrl : null))
         .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product != null ? src.Product.DiscountedPrice : 0))
         .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Product != null && src.Product.QuantityAvailable > 0));


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
        

        CreateMap<Review, ReviewViewModel>()
            .ForMember(d => d.AppUserName,
                o => o.MapFrom(s => s.AppUser != null ? s.AppUser.UserName : null))
            .ForMember(d => d.AppUserPhoto,
                o => o.MapFrom(s => s.AppUser != null ? s.AppUser.ProfileImagePath : null))
            .ForMember(d => d.ProductName,
                o => o.MapFrom(s => s.Product != null ? s.Product.Name : null));




    }
}

