using Microsoft.AspNetCore.Mvc;
using CmsModels;

namespace CmsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private static List<Posts> posts = new();

        [HttpGet]
        public IEnumerable<Posts> Get() => posts;

        [HttpPost]
        public IActionResult Create(Posts post)
        {
            post.Id = posts.Count + 1;
            posts.Add(post);
            return Ok(post);
        }
    }
}
