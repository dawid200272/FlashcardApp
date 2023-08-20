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

    private ObservableCollection<DeckViewModel> _decks;

    public DeckListingViewModel(IParameterRenavigator renavigator,
        DeckStore deckStore, IDeckService deckService)
    {
        _renavigator = renavigator;

        _deckStore = deckStore;
        _deckService = deckService;

        _deckStore.DeckAdded += DeckStore_DeckCollectionChanged;
        _deckStore.DeckUpdated += DeckStore_DeckCollectionChanged;
        _deckStore.DeckDeleted += DeckStore_DeckCollectionChanged;

        _deckStore.CardAdded += DeckStore_CardAdded;

        UpdateDecks(_deckStore);
    }

    private void DeckStore_CardAdded(Card obj)
    {
        UpdateDecks(_deckStore);
    }

    private void DeckStore_DeckCollectionChanged(Deck obj)
    {
        UpdateDecks(_deckStore);
    }

    public override void Dispose()
    {
        _deckStore.DeckAdded -= DeckStore_DeckCollectionChanged;
        _deckStore.DeckUpdated -= DeckStore_DeckCollectionChanged;
        _deckStore.DeckDeleted -= DeckStore_DeckCollectionChanged;

        _deckStore.CardAdded -= DeckStore_CardAdded;

        base.Dispose();
    }

    public ObservableCollection<DeckViewModel> Decks
    {
        get => _decks;
        set
        {
            _decks = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddEmptyDeckCommand => new AddEmptyDeckCommand(this);

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
            deckViewModels.Add(new DeckViewModel(_renavigator, deck));
        }

        _decks = new ObservableCollection<DeckViewModel>(deckViewModels);
    }
}
