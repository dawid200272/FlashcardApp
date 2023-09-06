using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class EditCardCommand : AsyncCommandBase
{
    private readonly EditCardViewModel _viewModel;
    private readonly DeckStore _deckStore;
    private readonly GlobalMessageStore _globalMessageStore;

    public EditCardCommand(EditCardViewModel viewModel,
        DeckStore deckStore,
        GlobalMessageStore globalMessageStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
        _globalMessageStore = globalMessageStore;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.CanEditCard && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            _globalMessageStore.ClearCurrentMessage();

            string front = _viewModel.Front;
            string back = _viewModel.Back;

            Card card = _viewModel.CardListingItemViewModel.Card;

            await _deckStore.UpdateCardAsync(card, front, back);

            _viewModel.CloseCommand.Execute(null);

            _globalMessageStore.SetCurrentMessage("Card was edited.", MessageType.Status);
        }
        catch (Exception)
        {
            _globalMessageStore.SetCurrentMessage("Editing card failed.", MessageType.Error);
        }
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.CanEditCard))
        {
            OnCanExecuteChanged();
        }
    }
}
