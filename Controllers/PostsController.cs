using BlogBackEnd.DTO.Blog;
using BlogBackEnd.Service.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.IdentityModel.Tokens;

namespace BlogBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostCreateDTO dto)
        {
            var userId = User.FindFirst("uid")?.Value;
            if (userId.IsNullOrEmpty()) return BadRequest();
            await _postService.CreatePostAsync(dto, userId);
            return Ok();

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostCreateDTO dto)
        {
            await _postService.UpdatePostAsync(id, dto);
            return Ok("Post updated successfully");
        }
         
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePostAsync(id);
            return Ok("Post deleted successfully");
        }
    }
 }