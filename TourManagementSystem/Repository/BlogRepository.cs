using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Data;
using TourManagementSystem.Models;

public class BlogRepository
{
    private readonly DataContext _context;

    public BlogRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Blog blog)
    {
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
    }

    public async Task<Blog> GetByIdAsync(int id)
    {
        return await _context.Blogs.FindAsync(id);
    }

    public async Task<List<Blog>> GetAllAsync()
    {
        return await _context.Blogs.ToListAsync();
    }
    public async Task UpdateAsync(Blog blog)
    {
        _context.Entry(blog).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Blog blog)
    {
        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
    }
}
