using BiolifeOrganic.Bll.ViewModels.OrderItem;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IOrderItemService : ICrudService<OrderItem, OrderItemViewModel, CreateOrderItemViewModel, UpdateOrderItemViewModel>
{
}

