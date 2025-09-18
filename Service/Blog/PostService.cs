using BlogBackEnd.DTO.Blog;
using BlogBackEnd.Models;
using BlogBackEnd.Repository;
using Microsoft.AspNetCore.Identity;

namespace BlogBackEnd.Service.Blog
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostService(IPostRepository postRepository, UserManager<ApplicationUser> userManager)
        {
            _postRepository = postRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<PostReadDTO>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(p => new PostReadDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                AuthorName = p.Author?.UserName
            });
        }

        public async Task<PostReadDTO> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return null;

            return new PostReadDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                AuthorName = post.Author.UserName

            };

        }

        public async Task CreatePostAsync(PostCreateDTO dto, string userId)
        {
            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = userId
            };
            await _postRepository.AddAsync(post);
        }

        public async Task UpdatePostAsync(int id, PostCreateDTO dto)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                post.Title = dto.Title;
                post.Content = dto.Content;

                await _postRepository.UpdateAsync(post);
            }
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                 await _postRepository.DeleteAsync(post);
             }
         }
    }
 }