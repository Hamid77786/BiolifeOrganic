using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BiolifeOrganic.Bll.Services;

public class ProductManager : CrudManager<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryService _categoryService;
    private readonly FileService _fileService;
    private readonly IMapper _mapper;

    public ProductManager(IProductRepository respository,ICategoryService categoryService,FileService fileService, IMapper mapper) : base(respository, mapper)
    {
        _productRepository = respository;
        _categoryService = categoryService;
        _fileService = fileService;
        _mapper = mapper;
    }

    public IQueryable<Product> GetProductsQuery(
        string? priceFilter = null,
        string? availabilityFilter = null)
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

        
        if (availabilityFilter == "IsOnSale")
            query = query.Where(p => p.IsOnSale);

        if (availabilityFilter == "IsRated")
            query = query.Where(p => p.Reviews.Any());

        return query;
    }

    public async Task<List<ProductViewModel>> GetAllWithDetailsAsync()
    {
        var products = await Repository.GetAllAsync(
                include: source => source.Include(p => p.Category!)
            );

        return Mapper.Map<List<ProductViewModel>>(products);
    }

    public async Task<ProductViewModel?> GetByIdWithDetailsAsync(int id)
    {
        var product = await _productRepository.GetByIdWithDetailsAsync(id);

        if (product == null) return null;

        var productViewModel = _mapper.Map<ProductViewModel>(product);

        productViewModel.CategoryName = product.Category?.Name;

        return productViewModel;
    }



    public async Task<List<ProductViewModel>> GetRelatedProductsAsync(int categoryId, int id)
    {
        var products = await _productRepository.GetProductsByCategoryAsync(categoryId, id);

        var relatedProductsViewModel = _mapper.Map<List<ProductViewModel>>(products);

        return relatedProductsViewModel;
    }

    //public async Task<CreateProductViewModel> GetCreateProductViewModelAsync()
    //{
    //    var createProductViewModel = new CreateProductViewModel();

    //    createProductViewModel.CategorySelectListItems = await _categoryService.GetCategorySelectListItemsAsync();

    //    return createProductViewModel;
    //}


//    public override async Task CreateAsync(CreateProductViewModel model)
//    {
//        var coverImageName = await _fileService.SaveFileAsync(model.ImageFile);

//        var imageNames = new List<string>();
//        if (model.ProductImages != null && model.ProductImages.Any())
//        {
//            foreach (var file in model.ProductImages)
//            {
//                var imageName = await _fileService.SaveFileAsync(file);
//                imageNames.Add(imageName);
//            }
//        }

       

//        var product = new Product
//        {
//            Name = model.Name,
//            Description = model.Description,
//            AdditionalInformation = model.AdditionalInformation,
//            OriginalPrice = model.OriginalPrice,
//            QuantityAvailable = model.Stock,
//            CategoryId = model.CategoryId,
//            ImageUrl = coverImageName,
//            IsBestSeller = false,
//            SaleStartDate = null,
//            SaleEndDate = null,


//            ProductImages = new List<ProductImage>
//            {
//                new ProductImage
//                {
//                    ImageUrl = coverImageName,
//                    IsMain = true,
//                    IsSecondary = false
//                }
//            },

            
//        };

//        if (imageNames.Any())
//        {
//            foreach (var imageName in imageNames)
//            {
//                product.ProductImages.Add(new ProductImage
//                {
//                    ImageUrl = imageName,
//                    IsMain = false,
//                    IsSecondary = false
//                });
//            }
//        }

//        await _repository.CreateAsync(product);
//    }
   
//    //public async Task<UpdateProductViewModel> GetUpdateViewModelAsync(int id)
//    //{
//    //    var product = await Repository.GetAsync(
//    //        predicate: p => p.Id == id,
//    //        include: source => source
//    //            .Include(p => p.ProductImages!)
//    //            .Include(p => p.Category!)
//    //    );

//    //    if (product == null) return null!;

//    //    var updateProductViewModel = Mapper.Map<UpdateProductViewModel>(product);
//    //    updateProductViewModel.CategorySelectListItems = await _categoryService.GetCategorySelectListItemsAsync();

//    //    if (product.ProductImages != null && product.ProductImages.Any())
//    //    {
//    //        updateProductViewModel.ExistingProductImages = product.ProductImages
//    //            .Select(i => i.ImageUrl ?? string.Empty) // Ensure null values are replaced with an empty string
//    //            .ToList();
//    //    }

//    //    return updateProductViewModel;
//    //}



//    public override async Task<bool> UpdateAsync(int id, UpdateProductViewModel model)
//    {
//        var product = await Repository.GetAsync(
//            predicate: p => p.Id == id,
//            include: source => source
//                .Include(p => p.ProductImages!)
//        );

//        if (product == null)
//            return false;

//        product.Name = model.Name!;
//        product.Description = model.Description;
//        product.AdditionalInformation = model.AdditionalInformation;
//        product.OriginalPrice = model.OriginalPrice;
//        product.QuantityAvailable = model.Stock;
//        product.CategoryId = model.CategoryId;

//        if (model.NewImageFile != null && model.NewImageFile.Length > 0)
//        {
//            if (!string.IsNullOrEmpty(product.ImageUrl))
//            {
//                _fileService.DeleteFile(product.ImageUrl);
//            }

//            var oldCoverImage = product.ProductImages!.FirstOrDefault(i => i.IsMain);
//            if (oldCoverImage != null)
//            {
//                product.ProductImages!.Remove(oldCoverImage);
//            }

//            var newCoverImageName = await _fileService.SaveFileAsync(model.NewImageFile);
//            product.ImageUrl = newCoverImageName;

//            product.ProductImages!.Add(new ProductImage
//            {
//                ImageUrl = newCoverImageName,
//                IsMain = true,
//                IsSecondary= false,
//                ProductId = product.Id
//            });
//        }

//        if (model.NewProductImages != null && model.NewProductImages.Any())
//        {
//            foreach (var file in model.NewProductImages)
//            {
//                if (file != null && file.Length > 0)
//                {
//                    var imageName = await _fileService.SaveFileAsync(file);
//                    product.ProductImages!.Add(new ProductImage
//                    {
//                        ImageUrl = imageName,
//                        IsMain = false,
//                        IsSecondary = false,
//                        ProductId = product.Id
//                    });
//                }
//            }
//        }

//        await Repository.UpdateAsync(product);
//        return true;
//    }

 
}
