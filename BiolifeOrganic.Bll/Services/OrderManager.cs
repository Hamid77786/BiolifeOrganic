using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.CheckOut;
using BiolifeOrganic.Bll.ViewModels.Discount;
using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using BiolifeOrganic.MVC.Views.Admin.Order;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BiolifeOrganic.Bll.Services;

public class OrderManager : IOrderService
{
    private readonly BasketManager _basketManager;
    private readonly IOrderRepository _orderRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IDiscountService _discountService;

    public OrderManager(IDiscountService discountService,BasketManager basketManager, IContactRepository contactRepository, IOrderRepository respository)
    { 
        _basketManager = basketManager;
        _orderRepository = respository;
        _contactRepository = contactRepository;
        _discountService = discountService;
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
            viewModel.ShippingContact =
                $"{order.ShippingContact.Address}, {order.ShippingContact.City}, {order.ShippingContact.Country}";
        }

        viewModel.History = BuildOrderHistory(order);

        return viewModel;
    }
    public async Task<int> PlaceOrderAsync(string? userId, CheckoutViewModel model)
    {
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

        if (!model.IsGuest && model.ShippingContactId.HasValue)
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
                AppUserId = model.IsGuest ? null : userId,
                IsDefault = !model.IsGuest && !model.ShippingContactId.HasValue
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
            GuestEmail = model.IsGuest ? model.Email : null,
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
            await _discountService.MarkAsUsedAsync(discount.DiscountId, userId);

        _basketManager.RemoveAllFromBasket();

        return order.Id;
    }

    public async Task<bool> CancelOrderAsync(int orderId, string userId)
    {
        var order = await _orderRepository.GetAsync(o => o.Id == orderId && o.AppUserId == userId);

        if (order == null)
            return false;

        if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.InProgress || order.Status == OrderStatus.Completed)
            return false;

        order.Status = OrderStatus.Cancelled;
        await _orderRepository.UpdateAsync(order);
        return true;
    }
    private List<OrderHistoryItemViewModel> BuildOrderHistory(Order order)
    {
        var history = new List<OrderHistoryItemViewModel>();

        history.Add(new OrderHistoryItemViewModel
        {
            Event = "Order Placed",
            Timestamp = order.CreatedAt,
            IsCompleted = true
        });

        if (order.PackagedDate.HasValue)
        {
            history.Add(new OrderHistoryItemViewModel
            {
                Event = "Product Packaging",
                Timestamp = order.PackagedDate.Value,
                IsCompleted = true
            });
        }

        if (order.ShippedDate.HasValue)
        {
            history.Add(new OrderHistoryItemViewModel
            {
                Event = "Product Shipped",
                Timestamp = order.ShippedDate.Value,
                Details = $"Courier Service: {order.CourierService}\nTracking Number: {order.TrackingNumber}\nWarehouse: {order.Warehouse}",
                IsCompleted = true
            });
        }

        if (order.EstimatedDeliveryDate.HasValue)
        {
            history.Add(new OrderHistoryItemViewModel
            {
                Event = "Estimated Delivery",
                Timestamp = order.EstimatedDeliveryDate.Value,
                IsCompleted = order.Status == OrderStatus.Completed
            });
        }

        return history.OrderBy(h => h.Timestamp).ToList();
    }
    public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusViewModel model)
    {
        var order = await _orderRepository.GetAsync(o => o.Id == model.Id);

        if (order == null)
            return false;

        if (!Enum.TryParse<OrderStatus>(model.Status, out var status))
            return false;
        var oldStatus = order.Status;

        order.Status = Enum.Parse<OrderStatus>(model.Status);


        switch (order.Status)
        {
            case OrderStatus.Processing:
                if (!order.ProcessingStartedDate.HasValue)
                    order.ProcessingStartedDate = DateTime.UtcNow;
                break;

            case OrderStatus.InProgress:
                if (!order.PackagedDate.HasValue)
                    order.PackagedDate = DateTime.UtcNow;
                break;

            case OrderStatus.Shipped:
                if (!order.ShippedDate.HasValue)
                    order.ShippedDate = DateTime.UtcNow;

                order.CourierService = model.CourierService;
                order.TrackingNumber = model.TrackingNumber;
                order.Warehouse = model.Warehouse;
                order.EstimatedDeliveryDate = model.EstimatedDeliveryDate ?? DateTime.UtcNow.AddDays(3);
                break;

            case OrderStatus.Completed:
                if (!order.DeliveredDate.HasValue)
                    order.DeliveredDate = DateTime.UtcNow;
                break;
        }

        await _orderRepository.UpdateAsync(order);
        return true;
    }
    public async Task<bool> SoftDeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetAsync(o => o.Id == orderId);

        if (order == null)
            return false;

        order.IsDeleted = true;
        await _orderRepository.UpdateAsync(order);
        return true;
    }
    public async Task<bool> HasOrdersAsync(string userId)
    {
        return await _orderRepository.AnyAsync(o =>
            o.AppUserId == userId &&
            !o.IsDeleted);
    }

























}
