using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Bll.ViewModels.OrderItem;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.ViewModels.Order;

public class OrderViewModel
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? DiscountCode { get; set; }
    public OrderStatus Status { get; set; }

    public int ShippingContactId { get; set; }
    public ContactViewModel ShippingContact { get; set; } = null!;

    public int? BillingContactId { get; set; }
    public ContactViewModel? BillingContact { get; set; }

    public string AppUserId { get; set; } = null!;
    public string AppUserEmail { get; set; } = null!;

    public int OrganizationId { get; set; }
    public string? OrganizationName { get; set; }

    public string? CourierService { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Warehouse { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ProcessingStartedDate { get; set; }
    public DateTime? PackagedDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

    public List<OrderItemViewModel> OrderItems { get; set; } = [];
}

public class CreateOrderViewModel
{
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string? DiscountCode { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.OnHold;

    public int ShippingContactId { get; set; }
    public int? BillingContactId { get; set; }

    public string AppUserId { get; set; } = null!;
    public int OrganizationId { get; set; }

    public string? CourierService { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Warehouse { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }

    public List<CreateOrderItemViewModel> OrderItems { get; set; } = [];
}

public class UpdateOrderViewModel
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? DiscountCode { get; set; }
    public OrderStatus Status { get; set; }

    public int ShippingContactId { get; set; }
    public int? BillingContactId { get; set; }

    public string? CourierService { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Warehouse { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ProcessingStartedDate { get; set; }
    public DateTime? PackagedDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

    public List<UpdateOrderItemViewModel> OrderItems { get; set; } = [];
}


