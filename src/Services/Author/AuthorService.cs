using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _authorRepository.GetAllAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await _authorRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Author>> GetAuthorsByCountryAsync(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return await GetAllAuthorsAsync();
            
        return await _authorRepository.GetByCountryAsync(country);
    }

    public async Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            return await GetAllAuthorsAsync();
            
        return await _authorRepository.GetByNameAsync(firstName ?? "", lastName ?? "");
    }

    public async Task<Author> CreateAuthorAsync(Author author)
    {
        // Валидация
        if (string.IsNullOrWhiteSpace(author.FirstName))
            throw new ArgumentException("First name is required");
            
        if (string.IsNullOrWhiteSpace(author.LastName))
            throw new ArgumentException("Last name is required");
            
        if (author.BirthDate > DateTime.Now)
            throw new ArgumentException("Birth date cannot be in the future");
            
        return await _authorRepository.AddAsync(author);
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        if (!await _authorRepository.ExistsAsync(author.Id))
            throw new KeyNotFoundException($"Author with Id {author.Id} not found");
            
        await _authorRepository.UpdateAsync(author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        // Проверяем, есть ли у автора книги
        if (await _authorRepository.HasBooksAsync(id))
            throw new InvalidOperationException("Cannot delete author with existing books");
            
        await _authorRepository.DeleteAsync(id);
    }

    public async Task<bool> AuthorExistsAsync(int id)
    {
        return await _authorRepository.ExistsAsync(id);
    }

    public async Task<bool> AuthorHasBooksAsync(int authorId)
    {
        return await _authorRepository.HasBooksAsync(authorId);
    }
}