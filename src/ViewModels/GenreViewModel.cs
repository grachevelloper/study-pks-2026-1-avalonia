using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public partial class GenreViewModel : ViewModelBase
{
    [ObservableProperty]
    private Genre? _currentGenre;

    public GenreViewModel()
    {
        CurrentGenre = new Genre();
    }

    public GenreViewModel(Genre genre)
    {
        CurrentGenre = genre;
    }
}