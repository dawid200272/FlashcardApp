using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;
public class EditCardViewModel : ViewModelBase
{
    private readonly DeckStore _deckStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly GlobalMessageStore _globalMessageStore;

    private CardListingItemViewModel _cardListingItemViewModel;

    public EditCardViewModel(DeckStore deckStore,
        ModalNavigationStore modalNavigationStore,
        GlobalMessageStore globalMessageStore)
    {
        _deckStore = deckStore;
        _modalNavigationStore = modalNavigationStore;
        _globalMessageStore = globalMessageStore;

        CloseCommand = new CloseModalCommand(_modalNavigationStore);
    }

    public void LoadEditCardViewModel(CardListingItemViewModel cardListingItemViewModel)
    {
        _cardListingItemViewModel = cardListingItemViewModel;

        _front = _cardListingItemViewModel.Front;
        _back = _cardListingItemViewModel.Back;

        EditCardCommand = new EditCardCommand(this, _deckStore, _globalMessageStore);
    }

    public ICommand EditCardCommand { get; private set; }

    public ICommand CloseCommand { get; }

    private string _front;
    public string Front
    {
        get => _front;
        set
        {
            if (_front != value)
            {
                _front = value;
                OnPropertyChanged(nameof(Front));
                OnPropertyChanged(nameof(CanEditCard));
            }
        }
    }

    private string _back;
    public string Back
    {
        get => _back;
        set
        {
            if (_back != value)
            {
                _back = value;
                OnPropertyChanged(nameof(Back));
                OnPropertyChanged(nameof(CanEditCard));
            }
        }
    }

    public bool CanEditCard => !string.IsNullOrWhiteSpace(Front) &&
        !string.IsNullOrWhiteSpace(Back) &&
        (_front != _cardListingItemViewModel.Front ||
        _back != _cardListingItemViewModel.Back);

    public CardListingItemViewModel CardListingItemViewModel => _cardListingItemViewModel;
}
