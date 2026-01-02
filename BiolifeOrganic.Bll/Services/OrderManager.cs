using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.CheckOut;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Bll.ViewModels.Discount;
using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.ReadModels.Contact;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using BiolifeOrganic.MVC.Views.Admin.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BiolifeOrganic.Bll.Services;

public class OrderManager : IOrderService
{
    private readonly BasketManager _basketManager;
    private readonly IOrderRepository _orderRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IDiscountService _discountService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public OrderManager(IMapper mapper,IUserService userService, IDiscountService discountService,BasketManager basketManager, IContactRepository contactRepository, IOrderRepository respository)
    { 
        _basketManager = basketManager;
        _orderRepository = respository;
        _contactRepository = contactRepository;
        _discountService = discountService;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<List<OrderListViewModel>> GetUserOrdersAsync(string userId)
    {
        var orders = await _orderRepository.GetUserOrdersAsync(userId);

        return orders.Select(o => new OrderListViewModel
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            OrderDate = o.CreatedAt,
            Status = o.Status.ToString(),
            TotalAmount = o.TotalAmount,
            ItemCount = o.OrderItems.Count,
            DiscountAmount = o.DiscountAmount,
            SubtotalAmount = o.SubTotalAmount,
            DiscountCode = o.DiscountCode,
            DiscountPercentage = o.DiscountPercentage,

        }).ToList();
    }
   
    public async Task<OrderDetailsViewModel?> GetOrderDetailsForAdminAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderWithDetailsAsync(orderId);
        return order == null ? null : _mapper.Map<OrderDetailsViewModel>(order);
    }

    public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId, string userId)
    {
        var order = await _orderRepository.GetOrderWithDetailsAsync(orderId, userId);
        if (order == null) return null;

        var subtotal = order.OrderItems.Sum(i => i.Price * i.Quantity);

        var discountAmount = order.TotalAmount - subtotal;

        var viewModel = new OrderDetailsViewModel
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status.ToString(),
            OrderDate = order.CreatedAt,
            SubtotalAmount = subtotal,
            DiscountAmount = discountAmount,
            TotalAmount = order.TotalAmount,
            CourierService = order.CourierService,
            TrackingNumber = order.TrackingNumber,
            Warehouse = order.Warehouse,
            EstimatedDeliveryDate = order.EstimatedDeliveryDate,
            ShippedDate = order.ShippedDate,
            OrderItems = order.OrderItems.Select(i => new OrderItemViewModel
            {
                Id = i.Id,
                ProductName = i.ProductName,
                ImageUrl = i.ImageUrl,
                Quantity = i.Quantity,
                Price = i.Price,
                Subtotal = i.Price * i.Quantity
            }).ToList()
        };

        if (order.ShippingContact != null)
        {
            viewModel.ShippingContact = new ContactViewModel
            {
                Id = order.ShippingContact.Id,
                FirstName = order.ShippingContact.FirstName,
                LastName = order.ShippingContact.LastName,
                Address = order.ShippingContact.Address,
                City = order.ShippingContact.City,
                Country = order.ShippingContact.Country,
                PostalCode = order.ShippingContact.PostalCode,
                Phone = order.ShippingContact.PhoneNumber,
                Email = order.ShippingContact.Email
            };
        }



        return viewModel;
    }
    public async Task<int> PlaceOrderAsync(string? userId, CheckoutViewModel model)
    {
        if (userId != null)
        {
            if (await _userService.IsAdminAsync(userId))
                throw new Exception("Admins are not allowed to place orders");

            if (await _userService.IsBlockedAsync(userId))
                throw new Exception("User account is blocked");
        }



        var basket = await _basketManager.GetBasketAsync();
        if (basket == null || !basket.Items.Any())
            throw new Exception("Basket is empty");

        decimal subtotal = basket.Items.Sum(x => x.Price * x.Quantity);

        decimal discountPercent = 0;
        decimal discountAmount = 0;
        decimal totalAmount = subtotal;

        DiscountValidationResult? discount = null;

        if (!string.IsNullOrWhiteSpace(model.DiscountCode))
        {
            discount = await _discountService.ValidateAsync(
                model.DiscountCode,
                userId,
                subtotal);

            if (discount.IsValid)
            {
                discountPercent = discount.Percentage;
                discountAmount = Math.Round(subtotal * discountPercent / 100m, 2);
                totalAmount = subtotal - discountAmount;

                model.DiscountPercent = discountPercent;
                model.DiscountAmount = discountAmount;
                model.TotalAmount = totalAmount;
                model.DiscountId = discount.DiscountId;
            }
            else
            {
                model.DiscountCode = null;
            }
        }

        Contact shippingContact;

        if (userId != null && model.ShippingContactId.HasValue)
        {
            shippingContact = await _contactRepository.GetAsync(
                c => c.Id == model.ShippingContactId.Value
            ) ?? throw new Exception("Shipping contact not found");
        }
        else
        {
            shippingContact = new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address!,
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode ?? "00000",
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                AppUserId = userId,
                IsDefault = userId != null && !model.ShippingContactId.HasValue
            };

            await _contactRepository.CreateAsync(shippingContact);
        }

        string orderNumber = $"ORD-{Random.Shared.Next(1000, 9999)}";

        if (discount != null)
        {
            model.DiscountPercent = discountPercent;
            model.DiscountAmount = discountAmount;
            model.TotalAmount = totalAmount;
        }
        else
        {
            model.DiscountPercent = 0;
            model.DiscountAmount = 0;
            model.TotalAmount = subtotal;
        }

        var order = new Order
        {
            AppUserId = userId,
            GuestEmail = userId == null ? model.Email : null,
            OrderNumber = orderNumber,
            SubTotalAmount = subtotal,
            DiscountPercentage = discountPercent,
            DiscountAmount = discountAmount,
            DiscountCode = model.DiscountCode,
            TotalAmount = totalAmount,
            PaymentMethod = model.PaymentMethod,
            Status = OrderStatus.OnHold,
            ShippingContactId = shippingContact.Id,
            BillingContactId = shippingContact.Id,
            OrderItems = basket.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName!,
                ImageUrl = item.ProductImageUrl,
                Quantity = item.Quantity,
                Price = item.Price,
                Subtotal = item.Price * item.Quantity
            }).ToList()
        };

        await _orderRepository.CreateAsync(order);

        if (discount != null && userId != null)
        {
            
            await _discountService.MarkAsUsedAsync(discount.DiscountId, userId);
            
        }

        _basketManager.RemoveAllFromBasket();

        return order.Id;
    }
    public async Task<OrderDetailsViewModel?> GetOrderDetailsForUserAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderDetailsAsync(orderId);
        return order == null ? null : _mapper.Map<OrderDetailsViewModel>(order);
    }

    public async Task<bool> DeleteUserOrderAsync(int orderId, string userId)
    {
        var order = await _orderRepository.GetAsync(
            o => o.Id == orderId && o.AppUserId == userId,
            include: q => q.Include(o => o.OrderItems)
        );

        if (order == null)
            return false;

        if (order.Status != OrderStatus.OnHold)
            throw new Exception("You cannot delete this order");

        await _orderRepository.DeleteAsync(order);
        return true;
    }

    public async Task UpdateShippingAddressAsync(UpdateContactViewModel model, string userId)
    {
        var readModel = new UpdateShippingContactRM
        {
            OrderId = model.OrderId,
            ContactId = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Company = model.Company,
            Address = model.Address,
            City = model.City,
            Country = model.Country,
            PostalCode = model.PostalCode,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            IsDefault = model.IsDefault
        };

        await _orderRepository.UpdateShippingContactAsync(readModel, userId);
    }


    public async Task<UpdateContactViewModel?> GetShippingContactForEditAsync(int orderId, string userId)
    {
        var contactRead = await _orderRepository.GetShippingContactForEditAsync(orderId, userId);

        if (contactRead == null)
            return null;

        return new UpdateContactViewModel
        {
            OrderId = contactRead.OrderId,
            Id = contactRead.ContactId,
            FirstName = contactRead.FirstName,
            LastName = contactRead.LastName,
            Company = contactRead.Company,
            Address = contactRead.Address,
            City = contactRead.City,
            Country = contactRead.Country,
            PostalCode = contactRead.PostalCode,
            PhoneNumber = contactRead.PhoneNumber,
            Email = contactRead.Email,
            IsDefault = contactRead.IsDefault
        };
    }































}


