using CmsModels;
using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(LocalDbContext context) : ControllerBase
    {
        private static List<Post> posts = [];
        private readonly LocalDbContext _context = context;
        [HttpGet]
        public ActionResult<IEnumerable<Post>>Get()
        {
            return _context.Posts.ToList();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            post.Id = posts.Count + 1;
            posts.Add(post);
            return Ok(post);
        }
    }
}
