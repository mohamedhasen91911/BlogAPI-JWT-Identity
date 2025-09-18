using BlogBackEnd.DTO.Blog;
using BlogBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBackEnd.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts
                        .Include(P => P.Author)
                        .ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts
                        .Include(P => P.Author)
                        .FirstOrDefaultAsync(P => P.Id == id);
        }
        public async Task AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
             _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Post post)
        {
           
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
             

        }


    }
}