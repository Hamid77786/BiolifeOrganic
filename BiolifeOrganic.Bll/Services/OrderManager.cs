using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class OrderManager : CrudManager<Order, OrderViewModel, CreateOrderViewModel, UpdateOrderViewModel>, IOrderService
{
    public OrderManager(IOrderRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }
}
