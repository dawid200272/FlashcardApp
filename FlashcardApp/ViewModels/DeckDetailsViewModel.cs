using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.Stores;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;

public class DeckDetailsViewModel : ViewModelBase
{
    private readonly IParameterRenavigator _cardReviewRenavigator;
    private readonly IParameterRenavigator _cardBrowsingRenavigator;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<EditDeckDescriptionViewModel> _createEditDeckDescriptionViewModel;
    private readonly CreateViewModel<ChangeDeckNameViewModel> _createChangeDeckNameViewModel;
    private readonly DeckStore _deckStore;
    private readonly IParameterRenavigator _deckDetailsRenavigator;
    private readonly IDeckExportService _deckExportService;

    private DeckViewModel _deckViewModel;

    public DeckDetailsViewModel(IParameterRenavigator cardReviewRenavigator,
        IParameterRenavigator cardBrowsingRenavigator,
        ModalNavigationStore modalNavigationStore,
        CreateViewModel<EditDeckDescriptionViewModel> createEditDeckDescriptionViewModel,
        DeckStore deckStore,
        IParameterRenavigator deckDetailsRenavigator,
        CreateViewModel<ChangeDeckNameViewModel> createChangeDeckNameViewModel,
        IDeckExportService deckExportService)
    {
        _cardReviewRenavigator = cardReviewRenavigator;
        _cardBrowsingRenavigator = cardBrowsingRenavigator;
        _modalNavigationStore = modalNavigationStore;
        _createEditDeckDescriptionViewModel = createEditDeckDescriptionViewModel;
        _deckStore = deckStore;
        _deckDetailsRenavigator = deckDetailsRenavigator;
        _createChangeDeckNameViewModel = createChangeDeckNameViewModel;
        _deckExportService = deckExportService;

        _deckStore.DeckUpdated += DeckStore_DeckUpdated;
    }

    public override void Dispose()
    {
        _deckStore.DeckUpdated -= DeckStore_DeckUpdated;

        base.Dispose();
    }

    ~DeckDetailsViewModel() { }

    private void DeckStore_DeckUpdated(Deck updatedDeck)
    {
        if (updatedDeck.Id != _deckViewModel.Deck.Id)
        {
            return;
        }

        DeckViewModel newDeckViewModel = new DeckViewModel(_deckDetailsRenavigator,
            updatedDeck,
            _modalNavigationStore,
            _createChangeDeckNameViewModel,
            _deckStore,
            _deckExportService);

        LoadDeckDetailsViewModel(newDeckViewModel);
    }

    public void LoadDeckDetailsViewModel(DeckViewModel deckViewModel)
    {
        _deckViewModel = deckViewModel;

        _deckViewModel.UpdateCardsInfo();

        StartCardReviewCommand = new StartCardReviewCommand(_cardReviewRenavigator, _deckViewModel);
        StartCardBrowsingCommand = new StartCardBrowsingCommand(_cardBrowsingRenavigator, _deckViewModel);
        OpenEditDeckDescriptionModalCommand = new OpenEditDeckDescriptionModalCommand(_modalNavigationStore, _createEditDeckDescriptionViewModel, deckViewModel);

        UpdateCardsInfo();

        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(HasDescription));
    }

    public ICommand StartCardReviewCommand { get; set; }
    public ICommand StartCardBrowsingCommand { get; set; }
    public ICommand OpenEditDeckDescriptionModalCommand { get; set; }

    public string Name => _deckViewModel.Name;

    public string? Description => _deckViewModel.Description;

    public bool HasDescription => !string.IsNullOrEmpty(_deckViewModel.Description);
    public bool HasCards => CardsNumber > 0;

    public int NewCardsNumber => _deckViewModel.NewCardsNumber;

    public int CardsNumber => _deckViewModel.CardsNumber;

    public void UpdateCardsInfo()
    {
        OnPropertyChanged(nameof(NewCardsNumber));
        OnPropertyChanged(nameof(CardsNumber));
    }
}
