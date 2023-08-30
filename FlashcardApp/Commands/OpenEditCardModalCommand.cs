using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class OpenEditCardModalCommand : AsyncCommandBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly CreateViewModel<EditCardViewModel> _createViewModel;
    private readonly CardListingItemViewModel _cardListingItemViewModel;

    public OpenEditCardModalCommand(ModalNavigationStore modalNavigationStore,
        CreateViewModel<EditCardViewModel> createViewModel,
        CardListingItemViewModel cardListingItemViewModel)
    {
        _modalNavigationStore = modalNavigationStore;
        _createViewModel = createViewModel;
        _cardListingItemViewModel = cardListingItemViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        EditCardViewModel editCardViewModel = _createViewModel();

        editCardViewModel.LoadEditCardViewModel(_cardListingItemViewModel);

        _modalNavigationStore.CurrentViewModel = editCardViewModel;
    }
}
