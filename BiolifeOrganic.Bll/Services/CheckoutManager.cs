using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Basket;
using BiolifeOrganic.Bll.ViewModels.CheckOut;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Bll.ViewModels.Discount;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BiolifeOrganic.Bll.Services;

public class CheckoutManager:ICheckoutService
{
    private readonly BasketManager _basketManager;
    private readonly IContactService _contactService;
    private readonly IOrderService _orderService;
    private readonly UserManager<AppUser> _userManager;

    public CheckoutManager(IOrderService orderService,BasketManager basketManager, IContactService contactService, UserManager<AppUser> userManager)
    {
        _basketManager = basketManager;
        _contactService = contactService;
        _userManager = userManager;
        _orderService = orderService;
        
    }

    public async Task<CheckoutViewModel?> BuildCheckoutViewModelAsync(ClaimsPrincipal user)
    {
        var basket = await _basketManager.GetBasketAsync();

        if (basket == null || !basket.Items.Any())
            return null;

        var cartItems = basket.Items.Select(item => new CartItemViewModel
        {
            ProductId = item.ProductId,
            ProductName = item.ProductName!,
            ImageUrl = item.ProductImageUrl,
            Quantity = item.Quantity,
            Price = item.Price
        }).ToList();

        var model = new CheckoutViewModel
        {
            Subtotal = cartItems.Sum(c => c.Subtotal),
            CartItems = cartItems,
            TotalAmount = cartItems.Sum(x => x.Subtotal),
            TotalCount = cartItems.Sum(x => x.Quantity),
            PaymentMethod = "BankTransfer"
        };

        if (user.Identity?.IsAuthenticated == true)
        {
            var userId = _userManager.GetUserId(user);

            if (!string.IsNullOrEmpty(userId))
            {
                var contacts = await _contactService.GetUserAddressesAsync(userId);
                var defaultContacts = contacts.FirstOrDefault(a => a.IsDefault);

                if (defaultContacts != null)
                {
                    model.ShippingContactId = defaultContacts.Id;

                    model.FirstName = defaultContacts.FirstName;
                    model.LastName = defaultContacts.LastName;
                    model.Address = defaultContacts.Address!;
                    model.City = defaultContacts.City!;
                    model.Country = defaultContacts.Country!;
                    model.PostalCode = defaultContacts.PostalCode;
                    model.PhoneNumber = defaultContacts.Phone!;
                    model.Email = defaultContacts.Email;
                }
            }
        }

        return model;
    }



    public async Task<CheckoutResult> ProcessCheckoutAsync(ClaimsPrincipal user, CheckoutViewModel model)
    {
        var basket = await _basketManager.GetBasketAsync();

        if (basket == null || !basket.Items.Any())
            return new CheckoutResult { Error = "Basket is empty" };

        model.CartItems = basket.Items.Select(i => new CartItemViewModel
        {
            ProductId = i.ProductId,
            ProductName = i.ProductName!,
            ImageUrl = i.ProductImageUrl,
            Quantity = i.Quantity,
            Price = i.Price
            
        }).ToList();

       

        string? userId = null;

        if (!model.IsGuest)
        {
            userId = _userManager.GetUserId(user);

            if (string.IsNullOrEmpty(userId))
                return new CheckoutResult { Error = "User not authenticated" };


            var contacts = await _contactService.GetUserAddressesAsync(userId!);
            var defaultContact = contacts.FirstOrDefault(c => c.IsDefault);

            if (defaultContact == null)
            {
                model.ShippingContactId = await _contactService.CreateAddressAsync(
                    userId,
                    new CreateContactViewModel
                    {
                        FirstName = model.FirstName!,
                        LastName = model.LastName!,
                        Address = model.Address!,
                        City = model.City!,
                        Country = model.Country!,
                        PostalCode = model.PostalCode ?? "00000",
                        PhoneNumber = model.PhoneNumber!,
                        Email = model.Email!,
                        IsDefault = true
                    });
            }
            else
            {
                model.ShippingContactId = defaultContact.Id;
            }
        }

            
            var orderId = await _orderService.PlaceOrderAsync(userId, model);

            return new CheckoutResult
            {
                Success = true,
                OrderId = orderId
            };
      
    }
    












}



