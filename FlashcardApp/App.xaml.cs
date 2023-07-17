using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.EntityFramework;
using FlashcardApp.EntityFramework.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.Stores;
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
    private DeckStore _deckStore;
    private IDeckService _deckService;

    protected override async void OnStartup(StartupEventArgs e)
    {
        IServiceProvider serviceProvider = CreateServiceProvider();

        _deckStore = serviceProvider.GetRequiredService<DeckStore>();

        _deckService = serviceProvider.GetRequiredService<IDeckService>();

        await _deckStore.LoadDecksAsync();

        if (_deckStore.Decks.Count() == 0)
        {
            Task<Deck> task = _deckService.CreateEmptyDeck("Default");

            Deck defaultDeck = task.Result;

            await _deckStore.AddAsync(defaultDeck);
        }

        Window window = serviceProvider.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }

    private IServiceProvider CreateServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();
        
        string appTitle = "FlashcardApp";

        services.AddSingleton<ICardService, CardService>();
        services.AddSingleton<ICardTemplateService, CardTemplateService>();
        services.AddSingleton<IDeckService, DeckService>();

        services.AddSingleton<IDataService<Card>, GenericDataService<Card>>();
        services.AddSingleton<IDataService<CardTemplate>, GenericDataService<CardTemplate>>();
        services.AddSingleton<IDataService<Deck>, GenericDataService<Deck>>();

        services.AddSingleton<FlashcardAppDbContextFactory>();

        services.AddSingleton<IFlashcardAppViewModelAbstractFactory, FlashcardAppViewModelAbstractFactory>();
        services.AddSingleton<IFlashcardAppViewModelFactory<CardReviewViewModel>, CardReviewViewModelFactory>();

        services.AddSingleton<IFlashcardAppViewModelFactory<DeckDetailsViewModel>>((services) =>
            new DeckDetailsViewModelFactory(new ViewModelFactoryRenavigator<CardReviewViewModel>(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IFlashcardAppViewModelFactory<CardReviewViewModel>>()
                ))
        );

        services.AddSingleton<IFlashcardAppViewModelFactory<DeckListingViewModel>>((services) =>
            new DeckListingViewModelFactory(new ViewModelFactoryRenavigator<DeckDetailsViewModel>(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IFlashcardAppViewModelFactory<DeckDetailsViewModel>>()),
                services.GetRequiredService<DeckStore>(),
                services.GetRequiredService<IDeckService>()
                )
        );
        

        services.AddScoped<INavigator, Navigator>();
        services.AddScoped<MainWindowViewModel>((services) =>
            new MainWindowViewModel(services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IFlashcardAppViewModelAbstractFactory>(),
                appTitle)
        );

        services.AddScoped<MainWindow>((services) =>
            new MainWindow(services.GetRequiredService<MainWindowViewModel>())
        );

        services.AddScoped<DeckStore>();

        return services.BuildServiceProvider();
    }
}
