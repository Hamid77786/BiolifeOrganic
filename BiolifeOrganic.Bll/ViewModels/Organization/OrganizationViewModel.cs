using BiolifeOrganic.Bll.ViewModels.Order;

namespace BiolifeOrganic.Bll.ViewModels.Organization;

public class OrganizationViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<OrderListViewModel> Orders { get; set; } = [];

}
public class CreateOrganizationViewModel
{
    public string Name { get; set; } = null!;

}
public class UpdateOrganizationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
