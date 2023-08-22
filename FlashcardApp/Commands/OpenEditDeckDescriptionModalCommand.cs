using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;

public class OpenEditDeckDescriptionModalCommand : AsyncCommandBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<EditDeckDescriptionViewModel> _createViewModel;
    private readonly DeckViewModel _deckViewModel;

    public OpenEditDeckDescriptionModalCommand(ModalNavigationStore modalNavigationStore,
        CreateViewModel<EditDeckDescriptionViewModel> createViewModel,
        DeckViewModel deckViewModel)
    {
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;
        _deckViewModel = deckViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        EditDeckDescriptionViewModel editDeckDescriptionViewModel = _createViewModel();

        editDeckDescriptionViewModel.LoadEditDeckDescriptionViewModel(_deckViewModel);

        _modalNavigationStore.CurrentViewModel = editDeckDescriptionViewModel;
    }
}
