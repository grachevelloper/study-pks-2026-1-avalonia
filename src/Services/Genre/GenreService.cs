using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _genreRepository.GetAllAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(int id)
    {
        return await _genreRepository.GetByIdAsync(id);
    }

    public async Task<Genre?> GetGenreByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;
            
        return await _genreRepository.GetByNameAsync(name);
    }

    public async Task<IEnumerable<Genre>> SearchGenresByNameAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllGenresAsync();
            
        return await _genreRepository.SearchByNameAsync(searchTerm);
    }

    public async Task<Genre> CreateGenreAsync(Genre genre)
    {
       
        if (string.IsNullOrWhiteSpace(genre.Name))
            throw new ArgumentException("Genre name is required");
            
       
        var existing = await _genreRepository.GetByNameAsync(genre.Name);
        if (existing != null)
            throw new InvalidOperationException($"Genre '{genre.Name}' already exists");
            
        return await _genreRepository.AddAsync(genre);
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        if (!await _genreRepository.ExistsAsync(genre.Id))
            throw new KeyNotFoundException($"Genre with Id {genre.Id} not found");
            
    
        var existing = await _genreRepository.GetByNameAsync(genre.Name);
        if (existing != null && existing.Id != genre.Id)
            throw new InvalidOperationException($"Genre '{genre.Name}' already exists");
            
        await _genreRepository.UpdateAsync(genre);
    }

    public async Task DeleteGenreAsync(int id)
    {
       
        if (await _genreRepository.HasBooksAsync(id))
            throw new InvalidOperationException("Cannot delete genre with existing books");
            
        await _genreRepository.DeleteAsync(id);
    }

    public async Task<bool> GenreExistsAsync(int id)
    {
        return await _genreRepository.ExistsAsync(id);
    }

    public async Task<bool> GenreHasBooksAsync(int genreId)
    {
        return await _genreRepository.HasBooksAsync(genreId);
    }
}