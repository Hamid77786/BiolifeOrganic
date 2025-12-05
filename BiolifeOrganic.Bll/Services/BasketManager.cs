using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Basket;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BiolifeOrganic.Bll.Services;

public class BasketManager
{
    private const string BasketCookieName = "basket";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductService _productService;

    public BasketManager(IProductService productService,IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _productService = productService;
    }

    private List<BasketCookieItemViewModel> GetBasketFromCookie()
    {
        var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[BasketCookieName];
        if (string.IsNullOrEmpty(cookie))
        {
            return new List<BasketCookieItemViewModel>();
        }
        try
        {
            var list = JsonSerializer.Deserialize<List<BasketCookieItemViewModel>>(cookie);
            return list ?? new List<BasketCookieItemViewModel>();
        }
        catch
        {
            return new List<BasketCookieItemViewModel>();
        }
    }

    private void SaveBasketToCookie(List<BasketCookieItemViewModel> basket)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            HttpOnly = true,
        };
        var cookieValue = System.Text.Json.JsonSerializer.Serialize(basket);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(BasketCookieName, cookieValue, cookieOptions);
    }

    public async Task<BasketViewModel> ChangeQuantityAsync(int productId, int quantity)
    {
        var basket = GetBasketFromCookie();
        var basketItem = basket.FirstOrDefault(item => item.ProductId == productId);

        if (basketItem != null)
        {
            if (quantity <= 0)
            {
                basket.Remove(basketItem);
            }
            else
            {
                basketItem.Quantity = quantity;
            }

            SaveBasketToCookie(basket);
        }

        var basketViewModel = new BasketViewModel();
        foreach (var item in basket)
        {
            var product = await _productService.GetByIdAsync(item.ProductId);
            if (product != null)
            {
                basketViewModel.Items.Add(new BasketItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name!,
                    ProductImageUrl = product.ImageUrl!,
                    Price = product.OriginalPrice,
                    Quantity = item.Quantity
                });
            }
        }

        return basketViewModel;
    }

    public async Task<BasketViewModel> GetBasketAsync()
    {
        var basket = GetBasketFromCookie();
        var basketViewModel = new BasketViewModel();
        foreach (var item in basket)
        {
            var product = await _productService.GetByIdAsync(item.ProductId);
            if (product != null)
            {
                basketViewModel.Items.Add(new BasketItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name!,
                    ProductImageUrl = product.ImageUrl!,
                    Price = product.OriginalPrice,
                    OldPrice = product.DiscountedPrice,
                    Quantity = item.Quantity
                });
            }
        }
        return basketViewModel;
    }

    public void AddToBasket(int productId)
    {
        var basket = GetBasketFromCookie();
        var basketItem = basket.FirstOrDefault(item => item.ProductId == productId);
        if (basketItem != null)
        {
            basketItem.Quantity += 1;
        }
        else
        {
            basket.Add(new BasketCookieItemViewModel
            {
                ProductId = productId,
                Quantity = 1
            });
        }
        SaveBasketToCookie(basket);
    }

    public void RemoveFromBasket(int productId)
    {
        var basket = GetBasketFromCookie();
        var basketItem = basket.FirstOrDefault(item => item.ProductId == productId);
        if (basketItem != null)
        {
            basket.Remove(basketItem);
            SaveBasketToCookie(basket);
        }
    }

    public void RemoveAllFromBasket()
    {
        var basket = GetBasketFromCookie();
        if (basket.Any())
        {
            basket.Clear(); 
            SaveBasketToCookie(basket);
        }
    }



}
