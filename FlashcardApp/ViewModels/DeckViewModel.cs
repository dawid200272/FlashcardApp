using FlashcardApp.WPF.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.WPF.State.Navigators;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using FlashcardApp.WPF.Stores;
using FlashcardApp.Domain.Services;

namespace FlashcardApp.WPF.ViewModels;

public class DeckViewModel : ViewModelBase
{
    private readonly IParameterRenavigator _renavigator;
    private readonly Deck _deck;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<ChangeDeckNameViewModel> _createViewModel;
    private readonly DeckStore _deckStore;
    private readonly IDeckExportService _deckExportService;
    private readonly GlobalMessageStore _globalMessageStore;

    public DeckViewModel(IParameterRenavigator renavigator,
        Deck deck,
        ModalNavigationStore modalNavigationStore,
        CreateViewModel<ChangeDeckNameViewModel> createViewModel,
        DeckStore deckStore,
        IDeckExportService deckExportService,
        GlobalMessageStore globalMessageStore)
    {
        _renavigator = renavigator;
        _deck = deck;
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;
        _deckStore = deckStore;
        _deckExportService = deckExportService;
        _globalMessageStore = globalMessageStore;

        _name = _deck.Name;
        _description = _deck.Description;

        SelectDeckCommand = new SelectDeckCommand(_renavigator, this);

        ChangeDeckNameCommand = new OpenChangeDeckNameModalCommand(_modalNavigationStore,
            _createViewModel,
            this);
        DeleteDeckCommand = new DeleteDeckCommand(this, _deckStore, _globalMessageStore);

        ExportDeckCommand = new ExportDeckCommand(_deckExportService, this, _globalMessageStore);

        UpdateCardsInfo();
    }

    public ICommand ChangeDeckNameCommand { get; }
    public ICommand DeleteDeckCommand { get; }
    public ICommand ExportDeckCommand { get; }

    public ICommand SelectDeckCommand { get; }

    public void UpdateCardsInfo()
    {
        _newCardsNumber = _deck.Cards.Count(c => c.State == CardState.newCard);
        _cardsNumber = _deck.Cards.Count();
    }

    public Deck Deck => _deck;

    private string _name;
    public string Name
    {
        get => _name;

        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _description;

    public string? Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    private int _newCardsNumber;

    public int NewCardsNumber
    {
        get => _newCardsNumber;
        set
        {
            if (_newCardsNumber != value)
            {
                _newCardsNumber = value;
                OnPropertyChanged();
            }
        }
    }

    private int _cardsNumber;

    public int CardsNumber
    {
        get => _cardsNumber;
        set
        {
            if (_cardsNumber != value)
            {
                _cardsNumber = value;
                OnPropertyChanged();
            }
        }
    }

}