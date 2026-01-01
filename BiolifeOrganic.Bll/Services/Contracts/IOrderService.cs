using BiolifeOrganic.Bll.ViewModels.CheckOut;
using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.MVC.Views.Admin.Order;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IOrderService 
{
    Task<List<OrderListViewModel>> GetUserOrdersAsync(string userId);
    Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId, string userId);
    Task<int> PlaceOrderAsync(string userId, CheckoutViewModel model);
    Task<OrderDetailsViewModel?> GetOrderDetailsForUserAsync(int orderId);


}

