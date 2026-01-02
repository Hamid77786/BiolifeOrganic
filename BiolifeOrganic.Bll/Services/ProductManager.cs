using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.ProductImage;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BiolifeOrganic.Bll.Services;

public class ProductManager : CrudManager<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryService _categoryService;
    private readonly FileService _fileService;
    private readonly IMapper _mapper;
    private readonly INewsletterService _newsletterService;

    public ProductManager(INewsletterService newsletterService,IProductRepository repository,ICategoryService categoryService,FileService fileService, IMapper mapper) : base(repository, mapper)
    {
        _productRepository = repository;
        _categoryService = categoryService;
        _fileService = fileService;
        _mapper = mapper;
        _newsletterService = newsletterService;
    }



    public IQueryable<Product> GetProductsQuery(string? priceFilter = null, string? availabilityFilter = null)
    {
        var query = GetQuery(
            p => !p.IsDeleted,
            q => q
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.Reviews),
            AsNoTracking: true
        );

        
        if (!string.IsNullOrEmpty(priceFilter) && priceFilter != "all")
        {
            query = priceFilter switch
            {
                "class-1st" => query.Where(p => p.OriginalPrice < 5),
                "class-2nd" => query.Where(p => p.OriginalPrice >= 5 && p.OriginalPrice <= 10),
                "class-3rd" => query.Where(p => p.OriginalPrice > 10 && p.OriginalPrice <= 20),
                "class-4th" => query.Where(p => p.OriginalPrice > 20 && p.OriginalPrice <= 45),
                "class-5th" => query.Where(p => p.OriginalPrice > 45 && p.OriginalPrice <= 100),
                "class-6th" => query.Where(p => p.OriginalPrice > 100 && p.OriginalPrice <= 150),
                "class-7th" => query.Where(p => p.OriginalPrice > 150),
                _ => query
            };
        }

        if (!string.IsNullOrEmpty(availabilityFilter) && availabilityFilter != "all")
        {
            if (availabilityFilter == "in")
                query = query.Where(p => p.QuantityAvailable > 0); 
            else if (availabilityFilter == "out")
                query = query.Where(p => p.QuantityAvailable == 0); 
            else if (availabilityFilter == "IsOnSale")
                query = query.Where(p => p.IsOnSale);
            else if (availabilityFilter == "IsRated")
                query = query.Where(p => p.Reviews.Any());
        }




        return query;
    }

   

    public async Task<ProductViewModel?> GetByIdWithDetailsAsync(int id)
    {
        var product = await _productRepository.GetByIdWithDetailsAsync(id);

        if (product == null) return null;

        var productViewModel = _mapper.Map<ProductViewModel>(product);

        productViewModel.CategoryName = product.Category?.Name;

        productViewModel.ProductImages = product.ProductImages
       .Select(pi => new ProductImageViewModel
       {
           Id = pi.Id,
           ImageUrl = pi.ImageUrl,
           ProductId = product.Id
       })
       .ToList();

        return productViewModel;
    }

    public override async Task CreateAsync(CreateProductViewModel model)
    {
        if (model.ImageFile == null || !_fileService.IsImageFile(model.ImageFile))
            throw new ArgumentException("Invalid image file.");

        var coverImageName = await _fileService.SaveFileAsync(
            model.ImageFile,
            "wwwroot/images/products"
        );

        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            AdditionalInformation = model.AdditionalInformation,
            OriginalPrice = model.OriginalPrice,
            QuantityAvailable = model.Stock,
            IsAvailable = model.Stock > 0,
            IsOnSale = model.IsOnSale,
            DiscountPercent = model.DiscountPercent,
            CategoryId = model.CategoryId,
            ImageUrl = coverImageName,
            IsBestSeller = false,
            SaleStartDate = null,
            SaleEndDate = null,
            ProductImages = new List<ProductImage>()
        };

        if (model.ProductImages != null && model.ProductImages.Any())
        {
            foreach (var file in model.ProductImages)
            {
                if (!_fileService.IsImageFile(file))
                    continue;

                var imageName = await _fileService.SaveFileAsync(
                    file,
                    "wwwroot/images/products"
                );

                product.ProductImages.Add(new ProductImage
                {
                    ImageUrl = imageName,
                    IsMain = false,
                    IsSecondary = false
                });
            }
        }

        await _productRepository.CreateAsync(product);

        if (_newsletterService != null)
        {
            string subject = $"New Product: {product.Name}";
            string htmlMessage = $@"
            <h2>New Product Added!</h2>
            <p>{product.Name} is now available at Biolife Organic.</p>
            <p>{product.Description}</p> ";
            

            await _newsletterService.SendPromotionEmailAsync(subject, htmlMessage);
        }
    }

    public  async Task<bool> UpdateProductAsync(int id, UpdateProductViewModel model)
    {
        var product = await _productRepository.GetByIdWithDetailsAsync(id);
        if (product == null)
            throw new Exception("Product not found");

        bool wasOnSale = product.IsOnSale;



        product.Name = model.Name!;
        product.Description = model.Description;
        product.AdditionalInformation = model.AdditionalInformation;
        product.OriginalPrice = model.OriginalPrice;
        product.QuantityAvailable = model.Stock;
        product.CategoryId = model.CategoryId;
        product.IsAvailable = model.IsAvailable;
        product.IsBestSeller = model.IsBestSeller;
        product.IsOnSale = model.IsOnSale;
        product.IsDeleted = model.IsDeleted;
        product.IsRated = model.IsRated;

        if (model.NewImageFile != null)
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                _fileService.DeleteFile(
                    "wwwroot/images/products", product.ImageUrl
                );
            }

            product.ImageUrl = await _fileService.SaveFileAsync(
                model.NewImageFile!,
                "wwwroot/images/products"
            );
        }

        product.ProductImages ??= new List<ProductImage>();


        if (model.ImagesToDelete != null && model.ImagesToDelete.Any())
        {
            var imagesToRemove = product.ProductImages
                .Where(pi => model.ImagesToDelete.Contains(pi.Id))
                .ToList();

            foreach (var image in imagesToRemove)
            {
                _fileService.DeleteFile(
                    "wwwroot/images/products", image.ImageUrl!
                );

                product.ProductImages.Remove(image);
            }
        }

        if (model.NewProductImages != null && model.NewProductImages.Any())
        {
            product.ProductImages ??= new List<ProductImage>();

            foreach (var file in model.NewProductImages)
            {

                if (file == null) continue;

                var imageName = await _fileService.SaveFileAsync(
                    file,
                    "wwwroot/images/products"
                );

                product.ProductImages.Add(new ProductImage
                {
                    ImageUrl = imageName,
                    ProductId = product.Id
                });
            }
        }

        await _productRepository.UpdateAsync(product);

        if (!wasOnSale && product.IsOnSale && _newsletterService != null)
        {
            string subject = $"Sale Alert: {product.Name} is now on sale!";
            string htmlMessage = $@"
            <h2>{product.Name} is on Sale!</h2>
            <p>{product.Description}</p>
            <p>Don't miss the chance to get it at a discounted price!</p> ";
       

            await _newsletterService.SendPromotionEmailAsync(subject, htmlMessage);
        }

        return true;
    }






    public async Task<bool> DeleteProductAndImagesAsync(int id)
    {
        var product = await _productRepository.GetByIdWithDetailsAsync(id);
        if (product == null)
            return false;

        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            _fileService.DeleteFile(
                "wwwroot/images/products", product.ImageUrl
            );
        }

        if (product.ProductImages != null && product.ProductImages.Any())
        {
            foreach (var image in product.ProductImages)
            {
                if (!string.IsNullOrEmpty(image.ImageUrl))
                {
                    _fileService.DeleteFile(
                        "wwwroot/images/products", image.ImageUrl
                    );
                }
            }
        }

        await _productRepository.DeleteAsync(product);
        return true;
    }






















}
