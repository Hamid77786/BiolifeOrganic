namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Order : TimeStample
{
    public string OrderNumber { get; set; } = null!;

    public decimal SubTotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public string? DiscountCode { get; set; }
    public decimal DiscountPercentage { get; set; }
    public string PaymentMethod { get; set; } = null!;

    public OrderStatus Status { get; set; }

    public int ShippingContactId { get; set; }
    public Contact ShippingContact { get; set; }=null!;
    public int? BillingContactId { get; set; }
    public Contact? BillingContact { get; set; }
    public Contact? OrganizationContact { get; set; }

    public string? CourierService { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Warehouse { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ProcessingStartedDate { get; set; }
    public DateTime? PackagedDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

    public string? GuestEmail { get; set; }
    public string? AppUserId { get; set; } 
    public AppUser? AppUser { get; set; }

    public int? OrganizationId { get; set; }
    public Organization? Organization { get; set; }

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
