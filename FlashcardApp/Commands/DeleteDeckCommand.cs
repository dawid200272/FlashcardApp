using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class DeleteDeckCommand : AsyncCommandBase
{
    private readonly DeckViewModel _viewModel;
    private readonly DeckStore _deckStore;

    public DeleteDeckCommand(DeckViewModel viewModel, DeckStore deckStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        Deck deck = _viewModel.Deck;

        if (deck.Cards.Any())
        {
            // do some popup with information
            // 'do want to delete this deck?'
        }

        await _deckStore.RemoveAsync(deck);
    }
}
