using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.OrderItem;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class OrderItemManager : CrudManager<OrderItem, OrderItemViewModel, CreateOrderItemViewModel, UpdateOrderItemViewModel>, IOrderItemService
{
    public OrderItemManager(IOrderItemRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }
}
