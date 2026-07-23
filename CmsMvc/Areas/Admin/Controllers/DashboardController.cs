using CmsMvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CmsMvc.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new PageCreateViewModel());
    }
}