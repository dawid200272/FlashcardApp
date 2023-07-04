using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.EntityFramework;
using FlashcardApp.EntityFramework.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlashcardApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private DeckCollection _decksCollection;
    private IDeckService _deckService;

    protected override void OnStartup(StartupEventArgs e)
    {
        IServiceProvider serviceProvider = CreateServiceProvider();

        _decksCollection = serviceProvider.GetRequiredService<DeckCollection>();

        _deckService = serviceProvider.GetRequiredService<IDeckService>();

        Task<Deck> task = _deckService.CreateEmptyDeck("Default");

        Deck defaultDeck = task.Result;

        _decksCollection.Add(defaultDeck);

        Window window = serviceProvider.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }

    private IServiceProvider CreateServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<ICardService, CardService>();
        services.AddSingleton<ICardTemplateService, CardTemplateService>();
        services.AddSingleton<IDeckService, DeckService>();

        services.AddSingleton<FlashcardAppDbContextFactory>();

        services.AddSingleton<IFlashcardAppViewModelAbstractFactory, FlashcardAppViewModelAbstractFactory>();
        services.AddSingleton<IFlashcardAppViewModelFactory<DeckListingViewModel>, DeckListingViewModelFactory>();
        services.AddSingleton<IFlashcardAppViewModelFactory<CardReviewViewModel>, CardReviewViewModelFactory>();

        services.AddScoped<INavigator, Navigator>();
        services.AddScoped<MainWindowViewModel>();

        services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

        services.AddScoped<DeckCollection>();

        return services.BuildServiceProvider();
    }
}
