namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Organization:Contact
{
    public string Name { get; set; } = null!;
    public List<Order> Orders { get; set; } = [];

}
