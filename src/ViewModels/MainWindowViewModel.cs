using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly IGenreService _genreService;

    // Коллекции для отображения
    [ObservableProperty]
    private ObservableCollection<Book> _books = new();

    [ObservableProperty]
    private ObservableCollection<Author> _authors = new();

    [ObservableProperty]
    private ObservableCollection<Genre> _genres = new();

    [ObservableProperty]
    private ObservableCollection<string> _authorFilterItems = new();

    [ObservableProperty]
    private ObservableCollection<string> _genreFilterItems = new();

    // Выбранные элементы
    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private string? _selectedAuthorFilter;

    [ObservableProperty]
    private string? _selectedGenreFilter;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private int _totalBooksCount;

    [ObservableProperty]
    private int _filteredBooksCount;

    [ObservableProperty]
    private bool _isLoading;

    public MainWindowViewModel(
        IBookService bookService, 
        IAuthorService authorService, 
        IGenreService genreService)
    {
        _bookService = bookService;
        _authorService = authorService;
        _genreService = genreService;
        
        // Загружаем данные при создании
        _ = LoadDataAsync();
    }

    // Команды
    public IAsyncRelayCommand LoadDataCommand => new AsyncRelayCommand(LoadDataAsync);
    public IAsyncRelayCommand AddBookCommand => new AsyncRelayCommand(AddBookAsync);
    public IAsyncRelayCommand EditBookCommand => new AsyncRelayCommand(EditBookAsync, () => SelectedBook != null);
    public IAsyncRelayCommand DeleteBookCommand => new AsyncRelayCommand(DeleteBookAsync, () => SelectedBook != null);
    public IRelayCommand ManageAuthorsCommand => new RelayCommand(ManageAuthors);
    public IRelayCommand ManageGenresCommand => new RelayCommand(ManageGenres);
    public IRelayCommand SearchCommand => new RelayCommand(ApplyFilters);
    public IRelayCommand ClearFiltersCommand => new RelayCommand(ClearFilters);

    private async Task LoadDataAsync()
    {
        try
        {
            IsLoading = true;
            
            var books = await _bookService.GetAllBooksAsync();
            Books = new ObservableCollection<Book>(books);
            
            var authors = await _authorService.GetAllAuthorsAsync();
            Authors = new ObservableCollection<Author>(authors);
            
            var genres = await _genreService.GetAllGenresAsync();
            Genres = new ObservableCollection<Genre>(genres);

            // Заполняем фильтры
            AuthorFilterItems = new ObservableCollection<string>(
                authors.Select(a => $"{a.FirstName} {a.LastName}").Prepend("Все авторы")
            );
            
            GenreFilterItems = new ObservableCollection<string>(
                genres.Select(g => g.Name).Prepend("Все жанры")
            );

            SelectedAuthorFilter = "Все авторы";
            SelectedGenreFilter = "Все жанры";
            
            UpdateCounts();
        }
        finally
        {
            IsLoading = false;
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        ApplyFilters();
    }

    partial void OnSelectedAuthorFilterChanged(string? value)
    {
        ApplyFilters();
    }

    partial void OnSelectedGenreFilterChanged(string? value)
    {
        ApplyFilters();
    }

    private async void ApplyFilters()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            
            var allBooks = await _bookService.GetAllBooksAsync();
            
            // Применяем фильтры
            var filtered = allBooks.AsEnumerable();

            // Поиск по названию
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(b => 
                    b.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            // Фильтр по автору
            if (SelectedAuthorFilter != null && SelectedAuthorFilter != "Все авторы")
            {
                filtered = filtered.Where(b => 
                    $"{b.Author.FirstName} {b.Author.LastName}" == SelectedAuthorFilter);
            }

            // Фильтр по жанру
            if (SelectedGenreFilter != null && SelectedGenreFilter != "Все жанры")
            {
                filtered = filtered.Where(b => b.Genre.Name == SelectedGenreFilter);
            }

            Books = new ObservableCollection<Book>(filtered);
            UpdateCounts();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void ClearFilters()
    {
        SearchText = string.Empty;
        SelectedAuthorFilter = "Все авторы";
        SelectedGenreFilter = "Все жанры";
        ApplyFilters();
    }

    private void UpdateCounts()
    {
        TotalBooksCount = Books.Count;
        FilteredBooksCount = Books.Count;
    }

    private async Task AddBookAsync()
    {
        // Будет открывать окно добавления книги
        // Пока заглушка
        await Task.CompletedTask;
    }

    private async Task EditBookAsync()
    {
        if (SelectedBook == null) return;
        // Будет открывать окно редактирования книги
        await Task.CompletedTask;
    }

    private async Task DeleteBookAsync()
    {
        if (SelectedBook == null) return;
        // Будет удалять книгу
        await _bookService.DeleteBookAsync(SelectedBook.Id);
        await LoadDataAsync();
    }

    private void ManageAuthors()
    {
        // Будет открывать окно управления авторами
    }

    private void ManageGenres()
    {
        // Будет открывать окно управления жанрами
    }
}