using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
    {
        return await _bookRepository.GetByAuthorAsync(authorId);
    }

    public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
    {
        return await _bookRepository.GetByGenreAsync(genreId);
    }

    public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return await GetAllBooksAsync();
            
        return await _bookRepository.SearchByTitleAsync(title);
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        
        if (string.IsNullOrWhiteSpace(book.Title))
            throw new ArgumentException("Title is required");
            
        if (book.PublishYear < 1400 || book.PublishYear > DateTime.Now.Year)
            throw new ArgumentException("Invalid publish year");
            
        return await _bookRepository.AddAsync(book);
    }

    public async Task UpdateBookAsync(Book book)
    {
        if (!await _bookRepository.ExistsAsync(book.Id))
            throw new KeyNotFoundException($"Book with Id {book.Id} not found");
            
        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }

    public async Task<bool> BookExistsAsync(int id)
    {
        return await _bookRepository.ExistsAsync(id);
    }
}