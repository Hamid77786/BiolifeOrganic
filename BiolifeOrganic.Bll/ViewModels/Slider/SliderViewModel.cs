namespace BiolifeOrganic.Bll.ViewModels.Slider;

public class SliderViewModel
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string? Title { get; set; }
    public string? LinkUrl { get; set; }


}

public class CreateSliderViewModel
{
    public string? ImageUrl { get; set; }
    public string? Title { get; set; }
    public string? LinkUrl { get; set; }

}


public class UpdateSliderViewModel
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string? Title { get; set; }
    public string? LinkUrl { get; set; }

}

