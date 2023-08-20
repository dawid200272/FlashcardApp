using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.EntityFramework;
using FlashcardApp.EntityFramework.Services;
using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using FlashcardApp.WPF.ViewModels.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlashcardApp.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    private DeckStore _deckStore;
    private IDeckService _deckService;

    #region Test purposes

    // TODO: Add test project to test maybe some services
    // TODO: Add API project to hold all API related code
    // TODO: Add export deck service interface in domain project
    // TODO: Get rid of test console app project
    // and use test project to do actual testing in there

    // TODO: Do prototype of UI in figma

    private ICardTemplateService _cardTemplateService;
    private ICardService _cardService;

    #endregion

    public App()
    {
        _host = CreateHostBuilder().Build();
    }

    public static IHostBuilder CreateHostBuilder(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.default.json");
                config.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((context, services)=>
            {
                string appTitle = "FlashcardApp";
                string connectionString = context.Configuration.GetConnectionString("Default");

                services.AddSingleton<ICardService, CardService>();
                services.AddSingleton<ICardTemplateService, CardTemplateService>();
                services.AddSingleton<IDeckService, DeckService>();

                services.AddSingleton<IDataService<Card>, GenericDataService<Card>>();
                services.AddSingleton<IDataService<CardTemplate>, GenericDataService<CardTemplate>>();
                //services.AddSingleton<IDataService<Deck>, GenericDataService<Deck>>();
                //TODO: Solve entity framework problem (probably solved)
                services.AddSingleton<IDataService<Deck>, DeckDataService>();

                services.AddDbContext<FlashcardAppDbContext>(options => options.UseSqlServer(connectionString));
                services.AddSingleton<FlashcardAppDbContextFactory>(
                    new FlashcardAppDbContextFactory(connectionString));

                services.AddSingleton<IFlashcardAppViewModelFactory, FlashcardAppViewModelFactory>();

                services.AddSingleton<DeckListingViewModel>(services => new DeckListingViewModel(
                        services.GetRequiredService<ViewModelDelegateRenavigator<DeckDetailsViewModel>>(),
                        services.GetRequiredService<DeckStore>(),
                        services.GetRequiredService<IDeckService>()));

                services.AddSingleton<CreateViewModel<CardReviewViewModel>>(services =>
                {
                    return () => new CardReviewViewModel(
                        services.GetRequiredService<ViewModelDelegateRenavigator<DeckDetailsViewModel>>());
                });

                services.AddSingleton<CreateViewModel<CardBrowsingViewModel>>(services =>
                {
                    return () => new CardBrowsingViewModel(
                        services.GetRequiredService<ViewModelDelegateRenavigator<DeckDetailsViewModel>>());
                });

                services.AddSingleton<ViewModelDelegateRenavigator<CardReviewViewModel>>();

                services.AddSingleton<ViewModelDelegateRenavigator<CardBrowsingViewModel>>();

                services.AddSingleton<CreateViewModel<DeckDetailsViewModel>>(services =>
                {
                    return () => new DeckDetailsViewModel(
                        services.GetRequiredService<ViewModelDelegateRenavigator<CardReviewViewModel>>(),
                        services.GetRequiredService<ViewModelDelegateRenavigator<CardBrowsingViewModel>>());
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

                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<MainWindowViewModel>((services) =>
                    new MainWindowViewModel(services.GetRequiredService<INavigator>(),
                        services.GetRequiredService<IFlashcardAppViewModelFactory>(),
                        appTitle)
                );

                services.AddSingleton<MainWindow>((services) =>
                    new MainWindow(services.GetRequiredService<MainWindowViewModel>())
                );

                services.AddSingleton<DeckStore>();
            });
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        _deckStore = _host.Services.GetRequiredService<DeckStore>();
        _deckService = _host.Services.GetRequiredService<IDeckService>();

        await _deckStore.LoadDecksAsync();

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

        #region Test
        if (!_deckStore.Decks.Any(d => d.Name == "test"))
        {

            Task<Deck> task1 = _deckService.CreateEmptyDeck("test");

            Deck testDeck = task1.Result;

            _cardTemplateService = new CardTemplateService();
            _cardService = new CardService();

            CardTemplate cardTemplate1 = await _cardTemplateService.CreateCardTemplate(
                Domain.Models.Enums.CardTemplateType.Basic,
                "test front 1",
                "test back 1");

            CardTemplate cardTemplate2 = await _cardTemplateService.CreateCardTemplate(
                Domain.Models.Enums.CardTemplateType.Basic,
                "test front 2",
                "test back 2");

            Card testCard1 = await _cardService.CreateCard(cardTemplate1, testDeck);
            Card testCard2 = await _cardService.CreateCard(cardTemplate2, testDeck);

            testDeck.AddCard(testCard1);
            testDeck.AddCard(testCard2);

            await _deckStore.AddAsync(testDeck); 
        }

        #endregion

        Window window = _host.Services.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }

    // TODO: Use generic .NET Host instead of this
    // and use host builder extension methods to clear this a bit

    // TODO: Add appsettings.json and use them in app
    // and learn more 'bout
    // to use appsettings.json as file storing user settings
    // and appsettings.default.json to store default app settings,
    // which user can override in appsettings.json.
    // Use probably null values in appsettings.json file
    // when app should get values from appsettings.default.json
    // and use non null values when app should override default setting with user defined value

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}
