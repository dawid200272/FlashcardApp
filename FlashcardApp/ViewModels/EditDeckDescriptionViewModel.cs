using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;

public class EditDeckDescriptionViewModel : ViewModelBase
{
    private readonly DeckStore _deckStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    private DeckViewModel _deckViewModel;
    private string? _deckDescription;

    public EditDeckDescriptionViewModel(DeckStore deckStore,
        ModalNavigationStore modalNavigationStore)
    {
        _deckStore = deckStore;
        _modalNavigationStore = modalNavigationStore;

        CloseCommand = new CloseModalCommand(_modalNavigationStore);
    }

    public void LoadEditDeckDescriptionViewModel(DeckViewModel deckViewModel)
    {
        _deckViewModel = deckViewModel;
        _deckDescription = _deckViewModel.Description;

        EditDeckDescriptionCommand = new EditDeckDescriptionCommand(this, _deckStore);
    }

    public ICommand EditDeckDescriptionCommand { get; set; }
    public ICommand CloseCommand { get; }

    public string? DeckDescription
    {
        get => _deckDescription;
        set
        {
            if (_deckDescription != value)
            {
                _deckDescription = value;
                OnPropertyChanged(nameof(DeckDescription));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
    }

    public bool CanSubmit
    {
        get
        {
            if (_deckDescription == _deckViewModel.Description)
            {
                return false;
            }

            return true;
        }
    }

    public DeckViewModel DeckViewModel => _deckViewModel;
}
