namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Slider:TimeStample
{
    public string HtmlContent { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }


}
