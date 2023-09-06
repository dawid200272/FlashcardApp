using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;

public class AddEmptyDeckViewModel : ViewModelBase
{
    private readonly DeckStore _deckStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly GlobalMessageStore _globalMessageStore;

    private string _newDeckName;

    public AddEmptyDeckViewModel(DeckStore deckStore,
        ModalNavigationStore modalNavigationStore,
        GlobalMessageStore globalMessageStore)
    {
        _deckStore = deckStore;
        _modalNavigationStore = modalNavigationStore;
        _globalMessageStore = globalMessageStore;

        AddEmptyDeckCommand = new AddEmptyDeckCommand(this, _deckStore, _globalMessageStore);
        CloseCommand = new CloseModalCommand(_modalNavigationStore);
    }

    public ICommand AddEmptyDeckCommand { get; }
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

    public bool CanSubmit => !string.IsNullOrWhiteSpace(_newDeckName);
}
