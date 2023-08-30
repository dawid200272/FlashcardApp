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

    public DeleteCardCommand(CardListingItemViewModel viewModel, DeckStore deckStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        Card card = _viewModel.Card;

        await _deckStore.RemoveCardAsync(card);
    }
}
