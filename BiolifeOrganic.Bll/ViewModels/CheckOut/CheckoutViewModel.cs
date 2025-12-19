using BiolifeOrganic.Bll.ViewModels.Basket;
using BiolifeOrganic.Bll.ViewModels.Contact;
using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.Bll.ViewModels.CheckOut;
public class CheckoutViewModel
{
    [Required(ErrorMessage = "First name is required")]
    public string? FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    public string? LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required")]
    public string? Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    public string? City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; } = string.Empty;
    public string? PostalCode { get; set; }
    public int? ShippingContactId { get; set; }


    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; } = string.Empty;

    public string? OrderNotes { get; set; }

    [Required(ErrorMessage = "Please select a payment method")]
    public string PaymentMethod { get; set; } = "BankTransfer";

    public string? DiscountCode { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal TotalCount { get; set; }
    public decimal DiscountedPrice { get; set; } = 10;
    public decimal SubtotalAfterDiscount => DiscountedPrice > 0
    ? Math.Round(TotalAmount * (1 - DiscountedPrice / 100), 2)
    : TotalAmount;

    public decimal Total => SubtotalAfterDiscount;

    public bool IsGuest { get; set; } = false;
    public List<CartItemViewModel> CartItems { get; set; } = new();
}

public class CartItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal => Quantity * Price;


   
}
public class CheckoutResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public int? OrderId { get; set; }
}

public class DiscountResult
{
    public bool IsValid { get; set; }
    public decimal Percentage { get; set; }
}

public class MustBeTrueAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is bool b && b;
    }
}

