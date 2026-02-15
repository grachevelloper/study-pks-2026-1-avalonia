using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using LibraryManagement.Data;
using LibraryManagement.Data.Repositories;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using LibraryManagement.Views;

namespace LibraryManagement;

public partial class App : Application
{
    private IServiceProvider? _services;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Настраиваем DI контейнер
        var services = new ServiceCollection();
        ConfigureServices(services);
        _services = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Отключаем дублирующую валидацию Avalonia
            DisableAvaloniaDataAnnotationValidation();

            // Получаем MainWindowViewModel через DI
            var mainViewModel = _services.GetRequiredService<MainWindowViewModel>();

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var connectionString = "Host=localhost;Port=5432;Database=librarydb;Username=postgres;Password=postgres;";

        services.AddDbContext<LibraryContext>(options =>
            options.UseNpgsql(connectionString));


        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();


        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IGenreService, GenreService>();


        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<BookViewModel>();
        services.AddTransient<AuthorViewModel>();
        services.AddTransient<GenreViewModel>();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {

        var dataValidationPluginsToRemove = BindingPlugins
            .DataValidators
            .OfType<DataAnnotationsValidationPlugin>()
            .ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}