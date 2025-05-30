using Microsoft.AspNetCore.Mvc;

namespace CmsApi.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
