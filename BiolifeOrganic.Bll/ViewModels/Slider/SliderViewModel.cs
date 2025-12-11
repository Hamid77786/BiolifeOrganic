namespace BiolifeOrganic.Bll.ViewModels.Slider;

public class SliderViewModel
{
    public int Id { get; set; }
    public string HtmlContent { get; set; } = string.Empty;

}

public class CreateSliderViewModel
{
    public string HtmlContent { get; set; } = null!;


}


public class UpdateSliderViewModel
{
    public int Id { get; set; }
    public string HtmlContent { get; set; } = null!;


}

