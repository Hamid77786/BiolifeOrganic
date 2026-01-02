using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.DataContext;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using System;

namespace BiolifeOrganic.Dll.ReadModels.Order;

public class OrderItemRM
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal SubTotal { get; set; }
}

public class OrderListRM
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public decimal SubtotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public decimal? DiscountPercentage { get; set; }
    public string? DiscountCode { get; set; }

    public int ItemCount { get; set; }
}

public class OrderDetailsRM
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }

    public decimal SubtotalAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public string? DiscountCode { get; set; }

    public int ShippingContactId { get; set; }
    public string ShippingFirstName { get; set; } = string.Empty;
    public string ShippingLastName { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public string ShippingCountry { get; set; } = string.Empty;
    public string ShippingPostalCode { get; set; } = string.Empty;
    public string ShippingPhone { get; set; } = string.Empty;
    public string ShippingEmail { get; set; } = string.Empty;


    public string? CourierService { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Warehouse { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ShippedDate { get; set; }

    public List<OrderItemRM> OrderItems { get; set; } = [];
    public List<string> Categories { get; set; } = [];
}











