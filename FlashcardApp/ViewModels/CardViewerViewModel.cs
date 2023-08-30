using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;
public class CardViewerViewModel : ViewModelBase
{
    private readonly IRenavigator _renavigator;
    private readonly DeckStore _deckStore;
    private readonly SelectedCardStore _selectedCardStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<EditCardViewModel> _createViewModel;

    public CardViewerViewModel(IRenavigator renavigator,
        DeckStore deckStore,
        SelectedCardStore selectedCardStore,
        ModalNavigationStore modalNavigationStore,
        CreateViewModel<EditCardViewModel> createViewModel)
    {
        _renavigator = renavigator;
        _deckStore = deckStore;
        _selectedCardStore = selectedCardStore;
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;

        CardListingViewModel = new CardListingViewModel(_deckStore, _selectedCardStore, _modalNavigationStore, _createViewModel);
        CardDetailsViewModel = new CardDetailsViewModel(_selectedCardStore);
    }

    public CardListingViewModel CardListingViewModel { get; }
    public CardDetailsViewModel CardDetailsViewModel { get; }

    public ICommand AddCardCommand { get; }
    public ICommand ReturnCommand { get; }
}
