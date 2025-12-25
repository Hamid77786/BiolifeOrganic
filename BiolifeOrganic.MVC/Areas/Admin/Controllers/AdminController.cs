using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class AdminController : Controller
{
   
}
