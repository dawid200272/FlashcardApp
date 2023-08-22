using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;
public class ChangeDeckNameViewModel : ViewModelBase
{
    private readonly DeckStore _deckStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    private DeckViewModel _deckViewModel;
    private string _newDeckName;

    public ChangeDeckNameViewModel(DeckStore deckStore, ModalNavigationStore modalNavigationStore)
    {
        _deckStore = deckStore;
        _modalNavigationStore = modalNavigationStore;

        CloseCommand = new CloseModalCommand(_modalNavigationStore);
    }

    public void LoadChangeDeckNameViewModel(DeckViewModel deckViewModel)
    {
        _deckViewModel = deckViewModel;
        _newDeckName = _deckViewModel.Name;

        ChangeDeckNameCommand = new ChangeDeckNameCommand(this, _deckStore);
    }

    public ICommand ChangeDeckNameCommand { get; set; }
    public ICommand CloseCommand { get; }
    public string NewDeckName
    {
        get => _newDeckName;
        set
        {
            if (value != _newDeckName)
            {
                _newDeckName = value;
                OnPropertyChanged(nameof(NewDeckName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
    }

    public bool CanSubmit
    {
        get
        {
            if (_newDeckName == _deckViewModel.Name)
            {
                return false;
            }

            return true;
        }
    }

    public DeckViewModel DeckViewModel => _deckViewModel;
}
