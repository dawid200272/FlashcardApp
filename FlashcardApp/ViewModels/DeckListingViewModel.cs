using FlashcardApp.WPF.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;

public class DeckListingViewModel : ViewModelBase
{
    private readonly IParameterRenavigator _renavigator;
    private readonly DeckStore _deckStore;
    private readonly IDeckService _deckService;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<AddEmptyDeckViewModel> _createViewModel;
    private readonly CreateViewModel<ChangeDeckNameViewModel> _createChangeDeckNameViewModel;
    private readonly IDeckExportService _deckExportService;
    private readonly GlobalMessageStore _globalMessageStore;

    private ObservableCollection<DeckViewModel> _decks;

    public DeckListingViewModel(IParameterRenavigator renavigator,
        DeckStore deckStore,
        IDeckService deckService,
        ModalNavigationStore modalNavigationStore,
        CreateViewModel<AddEmptyDeckViewModel> createViewModel,
        CreateViewModel<ChangeDeckNameViewModel> createChangeDeckNameViewModel,
        IDeckExportService deckExportService,
        GlobalMessageStore globalMessageStore)
    {
        _renavigator = renavigator;
        _deckStore = deckStore;
        _deckService = deckService;
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;
        _createChangeDeckNameViewModel = createChangeDeckNameViewModel;
        _deckExportService = deckExportService;
        _globalMessageStore = globalMessageStore;

        OpenAddEmptyDeckModalCommand = new OpenAddEmptyDeckModalCommand(_modalNavigationStore, _createViewModel);

        _deckStore.DeckAdded += DeckStore_DeckCollectionChanged;
        _deckStore.DeckUpdated += DeckStore_DeckCollectionChanged;
        _deckStore.DeckDeleted += DeckStore_DeckCollectionChanged;

        _deckStore.CardAdded += DeckStore_DeckCardCollectionChanged;
        _deckStore.CardDeleted += DeckStore_CardDeleted;

        UpdateDecks(_deckStore);
    }

    private void DeckStore_CardDeleted(Card obj)
    {
        Deck deck = obj.Deck;

        DeckViewModel? deckViewModel = _decks.FirstOrDefault(dvm => dvm.Deck.Id == deck.Id);

        if (deckViewModel is null)
        {
            return;
        }

        deckViewModel.UpdateCardsInfo();
    }

    private void DeckStore_DeckCardCollectionChanged(Card obj)
    {
        UpdateDecks(_deckStore);
    }

    private void DeckStore_DeckCollectionChanged(Deck obj)
    {
        UpdateDecks(_deckStore);
    }

    // Use Dispose method only when view model is a transient,
    // it is not a singleton

    //public override void Dispose()
    //{
    //    _deckStore.DeckAdded -= DeckStore_DeckCollectionChanged;
    //    _deckStore.DeckUpdated -= DeckStore_DeckCollectionChanged;
    //    _deckStore.DeckDeleted -= DeckStore_DeckCollectionChanged;

    //    _deckStore.CardAdded -= DeckStore_CardAdded;

    //    base.Dispose();
    //}

    public ObservableCollection<DeckViewModel> Decks
    {
        get => _decks;
        set
        {
            _decks = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenAddEmptyDeckModalCommand { get; }

    public async Task AddDeck(string deckName)
    {
        Deck createdDeck = await _deckService.CreateEmptyDeck(deckName);

        await _deckStore.AddAsync(createdDeck);

        //UpdateDecks(_deckStore);
    }

    private void UpdateDecks(DeckStore deckStore)
    {
        List<DeckViewModel> deckViewModels = new List<DeckViewModel>();

        foreach (Deck deck in deckStore)
        {
            deckViewModels.Add(new DeckViewModel(_renavigator,
                deck,
                _modalNavigationStore,
                _createChangeDeckNameViewModel,
                _deckStore,
                _deckExportService,
                _globalMessageStore));
        }

        _decks = new ObservableCollection<DeckViewModel>(deckViewModels);

        OnPropertyChanged(nameof(Decks));
    }
}
