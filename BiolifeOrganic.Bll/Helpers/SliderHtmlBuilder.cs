using BiolifeOrganic.Bll.ViewModels.Slider;

namespace BiolifeOrganic.Bll.Helpers;

public static  class SliderHtmlBuilder
{
    public static string Build(SliderViewModel slider)
    {
        if (slider == null || string.IsNullOrWhiteSpace(slider.HtmlContent))
            return string.Empty;

        return slider.HtmlContent
            .Replace("{{IMAGE}}", string.IsNullOrEmpty(slider.ImageUrl)
                ? ""
                : $"/images/sliders/{slider.ImageUrl}")
            .Replace("{{TITLE}}", slider.Title ?? "")
            .Replace("{{SUBTITLE}}", slider.SubTitle ?? "")
            .Replace("{{DESCRIPTION}}", slider.Description ?? "")
            .Replace("{{BUTTON_TEXT}}", slider.ButtonText ?? "")
            .Replace("{{BUTTON_LINK}}", slider.ButtonLink ?? "#")
            .Trim();
    }
}
