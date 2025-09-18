

using BlogBackEnd.DTO.Blog;
using BlogBackEnd.Models;

namespace BlogBackEnd.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);



     }
 }