using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IOrderService : ICrudService<Order, OrderViewModel, CreateOrderViewModel, UpdateOrderViewModel>
{
}

