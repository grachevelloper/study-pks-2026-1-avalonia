using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public partial class AuthorViewModel : ViewModelBase
{
    [ObservableProperty]
    private Author? _currentAuthor;

    public AuthorViewModel()
    {
        CurrentAuthor = new Author();
    }

    public AuthorViewModel(Author author)
    {
        CurrentAuthor = author;
    }
}