using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.Bll.ViewModels.Discount;

public class DiscountViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public decimal Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool OnlyForNewUsers { get; set; }
    public bool OnlyForExistingUsers { get; set; }
    public bool IsActive { get; set; }
    public int? MaxUsageCount { get; set; }
    public int? UsedCount { get; set; }
}

public class CreateDiscountViewModel
{
    [Required]
    public string Code { get; set; } = null!;

    [Required]
    [Range(0, 100)]
    public decimal Percentage { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }


    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public bool OnlyForNewUsers { get; set; }
    public bool OnlyForExistingUsers { get; set; }
    public bool IsActive { get; set; } = true;

    [Range(0, int.MaxValue)]
    public int? MaxUsageCount { get; set; }
}

public class UpdateDiscountViewModel
{
    public int Id { get; set; } 
    public string Code { get; set; } = null!;

    
    [Range(0, 100)]
    public decimal Percentage { get; set; }

    
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

   
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public bool OnlyForNewUsers { get; set; }
    public bool OnlyForExistingUsers { get; set; }
    public bool IsActive { get; set; }
   
    [Range(0, int.MaxValue)]
    public int? MaxUsageCount { get; set; }
    public int? UsedCount { get; set; }




}



