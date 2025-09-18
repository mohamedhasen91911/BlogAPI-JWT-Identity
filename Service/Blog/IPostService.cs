using BlogBackEnd.DTO.Blog;

namespace BlogBackEnd.Service.Blog
{
    public interface IPostService
    {
        Task<IEnumerable<PostReadDTO>> GetAllPostsAsync();
        Task<PostReadDTO> GetPostByIdAsync(int id);
        Task CreatePostAsync(PostCreateDTO dto, string userId);
        Task UpdatePostAsync(int id, PostCreateDTO dto);
        Task DeletePostAsync(int id);

    }
 }