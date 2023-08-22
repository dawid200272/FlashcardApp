using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;

public class OpenAddEmptyDeckModalCommand : AsyncCommandBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<AddEmptyDeckViewModel> _createViewModel;

    public OpenAddEmptyDeckModalCommand(ModalNavigationStore modalNavigationStore,
        CreateViewModel<AddEmptyDeckViewModel> createViewModel)
    {
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        AddEmptyDeckViewModel addEmptyDeckViewModel = _createViewModel();

        _modalNavigationStore.CurrentViewModel = addEmptyDeckViewModel;
    }
}
