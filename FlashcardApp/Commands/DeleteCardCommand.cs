using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class DeleteCardCommand : AsyncCommandBase
{
    private readonly CardListingItemViewModel _viewModel;
    private readonly DeckStore _deckStore;
    private readonly GlobalMessageStore _globalMessageStore;

    public DeleteCardCommand(CardListingItemViewModel viewModel,
        DeckStore deckStore,
        GlobalMessageStore globalMessageStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
        _globalMessageStore = globalMessageStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            _globalMessageStore.ClearCurrentMessage();

            Card card = _viewModel.Card;

            await _deckStore.RemoveCardAsync(card);

            _globalMessageStore.SetCurrentMessage("Card was removed.", MessageType.Status);
        }
        catch (Exception)
        {
            _globalMessageStore.SetCurrentMessage("Card removal failed.", MessageType.Error);
        }
    }
}
