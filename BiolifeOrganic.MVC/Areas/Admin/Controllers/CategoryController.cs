using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;


public class CategoryController : AdminController
{
    private readonly AppDbContext _dbContext;
    public CategoryController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IActionResult> Index()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (!category.ImageFile!.ContentType.Contains("image")) 
        {
            ModelState.AddModelError("ImageFile", "Image must be choose!");
            return View();
        }

        if(category.ImageFile.Length > 1024 * 1024)
        {
            ModelState.AddModelError("ImageFile", "Image file size must be 1mb !");
            return View();
        }

        var existCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);

        if (existCategory != null)
        {
            ModelState.AddModelError("Name", "That category name is already exist!");
            return View();
        }

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (!ModelState.IsValid) return View(category);

        var existCategory = await _dbContext.Categories.FindAsync(id);
        if (existCategory == null) return BadRequest();

        var hasName = await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == category.Name);

        if (hasName != null)
        {
            ModelState.AddModelError("Name", "That category name is already exist!");
            return View(category);
        }

        existCategory.Name = category.Name;

        _dbContext.Categories.Update(existCategory);

        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null) return NotFound();

        _dbContext.Categories.Remove(category);
         await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
