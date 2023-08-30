using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.ViewModels;
public class CardListingViewModel : ViewModelBase
{
    private readonly DeckStore _deckStore;
    private readonly SelectedCardStore _selectedCardStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<EditCardViewModel> _createViewModel;

    private DeckViewModel _deckViewModel;
    private ObservableCollection<CardListingItemViewModel> _cardListingItemViewModels;

    public CardListingViewModel(DeckStore deckStore,
        SelectedCardStore selectedCardStore,
        ModalNavigationStore modalNavigationStore,
        CreateViewModel<EditCardViewModel> createViewModel)
    {
        _deckStore = deckStore;
        _selectedCardStore = selectedCardStore;
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;

        _cardListingItemViewModels = new ObservableCollection<CardListingItemViewModel>();

        LoadCardListingViewModel();

        _deckStore.DeckUpdated += DeckStore_DeckUpdated;
        _deckStore.CardUpdated += DeckStore_CardUpdated;
        _deckStore.CardDeleted += DeckStore_CardDeleted;
    }

    private void DeckStore_CardDeleted(Card obj)
    {
        if (obj.Id == _selectedCardListingItemViewModel?.Card.Id)
        {
            int index = _cardListingItemViewModels.IndexOf(_selectedCardListingItemViewModel);

            SelectedCardListingItemViewModel = null;

            OnPropertyChanged(nameof(SelectedCardListingItemViewModel));

            _cardListingItemViewModels.RemoveAt(index);

            _selectedCardStore.OnSelectedCardChanged();
        }
        else
        {
            CardListingItemViewModel? cardListingItemViewModel = _cardListingItemViewModels.FirstOrDefault(cli => cli.Card.Id == obj.Id);

            if (cardListingItemViewModel is null)
            {
                return;
            }

            int index = _cardListingItemViewModels.IndexOf(cardListingItemViewModel);

            _cardListingItemViewModels.RemoveAt(index);
        }
    }

    private void DeckStore_CardUpdated(Card obj)
    {
        if (obj.Id == _selectedCardListingItemViewModel?.Card.Id)
        {
            OnPropertyChanged(nameof(SelectedCardListingItemViewModel));

            _selectedCardStore.OnSelectedCardChanged();
        }
    }

    public override void Dispose()
    {
        _deckStore.DeckUpdated -= DeckStore_DeckUpdated;
        _deckStore.CardUpdated -= DeckStore_CardUpdated;
        _deckStore.CardDeleted -= DeckStore_CardDeleted;

        base.Dispose();
    }

    private void DeckStore_DeckUpdated(Deck obj)
    {
        LoadCardListingViewModel();
    }

    public void LoadCardListingViewModel()
    {
        _cardListingItemViewModels.Clear();

        List<CardListingItemViewModel> cardListingItemViewModels = new List<CardListingItemViewModel>();

        foreach (var deck in _deckStore.Decks)
        {
            int cardIndex = 0;

            foreach (var card in deck.Cards)
            {
                string cardDisplayName = $"Card { cardIndex + 1 }";

                cardListingItemViewModels.Add(new CardListingItemViewModel(_modalNavigationStore, _createViewModel, _deckStore, card, cardDisplayName));

                cardIndex++;
            }
        }

        _cardListingItemViewModels = new ObservableCollection<CardListingItemViewModel>(cardListingItemViewModels);
    }

    public IEnumerable<CardListingItemViewModel> CardListingItemViewModels => _cardListingItemViewModels;

    private CardListingItemViewModel? _selectedCardListingItemViewModel;
    public CardListingItemViewModel? SelectedCardListingItemViewModel
    {
        get => _selectedCardListingItemViewModel;
        set
        {
            if (_selectedCardListingItemViewModel == value)
            {
                return;
            }

            _selectedCardListingItemViewModel = value;
            OnPropertyChanged(nameof(SelectedCardListingItemViewModel));

            _selectedCardStore.SelectedCard = value?.Card;
        }
    }
}
