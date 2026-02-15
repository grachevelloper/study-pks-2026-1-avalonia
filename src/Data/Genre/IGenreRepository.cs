using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Data;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(int id);
    Task<Genre?> GetByNameAsync(string name);
    Task<IEnumerable<Genre>> SearchByNameAsync(string searchTerm);
    Task<Genre> AddAsync(Genre genre);
    Task UpdateAsync(Genre genre);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> HasBooksAsync(int genreId); 
}