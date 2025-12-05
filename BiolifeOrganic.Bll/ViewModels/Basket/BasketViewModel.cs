namespace BiolifeOrganic.Bll.ViewModels.Basket;



    public class BasketViewModel
    {
        public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
        public decimal TotalCount => Items.Sum(item => item.Quantity);
    }

    public class BasketItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImageUrl { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
        

    }

    public class BasketCookieItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

