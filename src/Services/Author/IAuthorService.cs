using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Services;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<IEnumerable<Author>> GetAuthorsByCountryAsync(string country);
    Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string firstName, string lastName);
    Task<Author> CreateAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(int id);
    Task<bool> AuthorExistsAsync(int id);
    Task<bool> AuthorHasBooksAsync(int authorId);
}