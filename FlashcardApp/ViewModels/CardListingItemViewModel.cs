using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;
public class CardListingItemViewModel : ViewModelBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<EditCardViewModel> _createViewModel;
    private readonly DeckStore _deckStore;
    private readonly Card _card;

    public CardListingItemViewModel(ModalNavigationStore modalNavigationStore,
        CreateViewModel<EditCardViewModel> createViewModel,
        DeckStore deckStore,
        Card card,
        string cardDisplayName)
    {
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;
        _deckStore = deckStore;
        _card = card;

        CardDisplayName = cardDisplayName;
        DeckName = card.Deck.Name;

        EditCardCommand = new OpenEditCardModalCommand(_modalNavigationStore, _createViewModel, this);
        DeleteCardCommand = new DeleteCardCommand(this, _deckStore);
    }

    public string CardDisplayName { get; }
    public string DeckName { get; }

    public string Front => _card.Front;
    public string Back => _card.Back;

    public ICommand EditCardCommand { get; }
    public ICommand DeleteCardCommand { get; }

    public Card Card => _card;
}
