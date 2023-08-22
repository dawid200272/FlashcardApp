using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;

public class OpenChangeDeckNameModalCommand : AsyncCommandBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<ChangeDeckNameViewModel> _createViewModel;
    private readonly DeckViewModel _deckViewModel;

    public OpenChangeDeckNameModalCommand(ModalNavigationStore modalNavigationStore,
        CreateViewModel<ChangeDeckNameViewModel> createViewModel,
        DeckViewModel deckViewModel)
    {
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;
        _deckViewModel = deckViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        ChangeDeckNameViewModel changeDeckNameViewModel = _createViewModel();

        changeDeckNameViewModel.LoadChangeDeckNameViewModel(_deckViewModel);

        _modalNavigationStore.CurrentViewModel = changeDeckNameViewModel;
    }
}
