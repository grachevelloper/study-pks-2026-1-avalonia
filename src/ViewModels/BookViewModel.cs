using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public partial class BookViewModel : ViewModelBase
{
    [ObservableProperty]
    private Book? _currentBook;

    [ObservableProperty]
    private bool _isNew;

    public BookViewModel()
    {
        CurrentBook = new Book();
        IsNew = true;
    }

    public BookViewModel(Book book)
    {
        CurrentBook = book;
        IsNew = false;
    }
}