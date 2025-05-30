using Microsoft.AspNetCore.Mvc;
using CmsModels;

namespace CmsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private static List<Post> posts = new();

        [HttpGet]
        public IEnumerable<Post> Get() => posts;

        [HttpPost]
        public IActionResult Create(Post post)
        {
            post.Id = posts.Count + 1;
            posts.Add(post);
            return Ok(post);
        }
    }
}
