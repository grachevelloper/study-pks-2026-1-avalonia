using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly LibraryContext _context;

    public GenreRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _context.Genres
            .Include(g => g.Books)
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        return await _context.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Genre?> GetByNameAsync(string name)
    {
        return await _context.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Name == name);
    }

    public async Task<IEnumerable<Genre>> SearchByNameAsync(string searchTerm)
    {
        return await _context.Genres
            .Include(g => g.Books)
            .Where(g => g.Name.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<Genre> AddAsync(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
        return genre;
    }

    public async Task UpdateAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre != null)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Genres.AnyAsync(g => g.Id == id);
    }

    public async Task<bool> HasBooksAsync(int genreId)
    {
        return await _context.Books.AnyAsync(b => b.GenreId == genreId);
    }
}