using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Services;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetAllGenresAsync();
    Task<Genre?> GetGenreByIdAsync(int id);
    Task<Genre?> GetGenreByNameAsync(string name);
    Task<IEnumerable<Genre>> SearchGenresByNameAsync(string searchTerm);
    Task<Genre> CreateGenreAsync(Genre genre);
    Task UpdateGenreAsync(Genre genre);
    Task DeleteGenreAsync(int id);
    Task<bool> GenreExistsAsync(int id);
    Task<bool> GenreHasBooksAsync(int genreId);
}