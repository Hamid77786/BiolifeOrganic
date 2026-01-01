using Microsoft.AspNetCore.Http;

namespace BiolifeOrganic.Bll.ViewModels.Slider;

public class SliderViewModel
{
    public int Id { get; set; }
    public string? HtmlContent { get; set; }
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public string? Title { get; set; } 
    public string? SubTitle { get; set; }
    public string? Description { get; set; }

    public string? ButtonText { get; set; }
    public string? ButtonLink { get; set; }

}

public class CreateSliderViewModel
{
    public string? HtmlContent { get; set; }

    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? ButtonText { get; set; }
    public string? ButtonLink { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public IFormFile? Image { get; set; } 
    public string? ImageUrl { get; set; }
    public string? ExistingImageUrl { get; set; }



}


public class UpdateSliderViewModel
{
    public int Id { get; set; }
    public string? HtmlContent { get; set; }

    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? ButtonText { get; set; }
    public string? ButtonLink { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public IFormFile? Image { get; set; }
    public string? ExistingImageUrl { get; set; } 
    public string? ImageUrl { get; set; }


}

