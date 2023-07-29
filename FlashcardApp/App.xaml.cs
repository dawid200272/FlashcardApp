using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.EntityFramework;
using FlashcardApp.EntityFramework.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using FlashcardApp.WPF.ViewModels.Factories;
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

    #region Test purposes

    private ICardTemplateService _cardTemplateService;
    private ICardService _cardService;

    #endregion

    protected override async void OnStartup(StartupEventArgs e)
    {
        IServiceProvider serviceProvider = CreateServiceProvider();

        _deckStore = serviceProvider.GetRequiredService<DeckStore>();

        _deckService = serviceProvider.GetRequiredService<IDeckService>();

        //await _deckStore.LoadDecksAsync();

        if (!_deckStore.Decks.Any())
        {
            Task<Deck> task = _deckService.CreateEmptyDeck("Default");

            Deck defaultDeck = task.Result;

            #region Test

            _cardTemplateService = new CardTemplateService();
            _cardService = new CardService();

            CardTemplate testTemplate = await _cardTemplateService.CreateCardTemplate(
                Domain.Models.Enums.CardTemplateType.Basic,
                "test front",
                "test back");

            Card testCard = await _cardService.CreateCard(testTemplate, defaultDeck);

            defaultDeck.AddCard(testCard);

            #endregion

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
        //TODO: solve entity framework problem
        //services.AddSingleton<IDataService<Deck>, DeckDataService>();

        services.AddSingleton<FlashcardAppDbContextFactory>();

        services.AddSingleton<IFlashcardAppViewModelFactory, FlashcardAppViewModelFactory>();

        services.AddSingleton<DeckListingViewModel>(services => new DeckListingViewModel(
                services.GetRequiredService<ViewModelDelegateRenavigator<DeckDetailsViewModel>>(),
                services.GetRequiredService<DeckStore>(),
                services.GetRequiredService<IDeckService>()));

        services.AddSingleton<CreateViewModel<CardReviewViewModel>>(services =>
        {
            return () => new CardReviewViewModel();
        });

        services.AddSingleton<ViewModelDelegateRenavigator<CardReviewViewModel>>();

        services.AddSingleton<CreateViewModel<DeckDetailsViewModel>>(services =>
        {
            return () => new DeckDetailsViewModel(
                services.GetRequiredService<ViewModelDelegateRenavigator<CardReviewViewModel>>());
        });

        services.AddSingleton<ViewModelDelegateRenavigator<DeckDetailsViewModel>>();

        services.AddSingleton<CreateViewModel<DeckListingViewModel>>(services =>
        {
            return () => services.GetRequiredService<DeckListingViewModel>();
        });

        services.AddSingleton<ViewModelDelegateRenavigator<DeckListingViewModel>>();

        services.AddSingleton<CreateViewModel<AddCardViewModel>>(services =>
        {
            return () => new AddCardViewModel(
                services.GetRequiredService<ViewModelDelegateRenavigator<DeckListingViewModel>>(),
                services.GetRequiredService<DeckStore>());
        });

        services.AddScoped<INavigator, Navigator>();
        services.AddScoped<MainWindowViewModel>((services) =>
            new MainWindowViewModel(services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IFlashcardAppViewModelFactory>(),
                appTitle)
        );

        services.AddScoped<MainWindow>((services) =>
            new MainWindow(services.GetRequiredService<MainWindowViewModel>())
        );

        services.AddScoped<DeckStore>();

        return services.BuildServiceProvider();
    }
}
