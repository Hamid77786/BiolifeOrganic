namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Order : TimeStample
{
    public string OrderNumber { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string? DiscountCode { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.OnHold;
    public int ShippingContactId { get; set; }
    public Contact ShippingContact { get; set; }=null!;
    public int? BillingContactId { get; set; }
    public Contact? BillingContact { get; set; }
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public int OrganizationId { get; set; }
    public Organization? Organization { get; set; }

    public string? CourierService { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Warehouse { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ProcessingStartedDate { get; set; }
    public DateTime? PackagedDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }


    public List<OrderItem> OrderItems { get; set; } = [];
}

public enum OrderStatus
{
    OnHold,
    Processing,
    InProgress,
    Shipped,
    Completed,
    Cancelled
}
