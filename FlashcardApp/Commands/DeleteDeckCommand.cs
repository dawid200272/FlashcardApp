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
    private readonly GlobalMessageStore _globalMessageStore;

    public DeleteDeckCommand(DeckViewModel viewModel,
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

            Deck deck = _viewModel.Deck;

            if (deck.Cards.Any())
            {
                // do some popup with information
                // 'do want to delete this deck?'
            }

            await _deckStore.RemoveAsync(deck);

            int cardCount = deck.Cards.Count();

            _globalMessageStore.SetCurrentMessage($"Deck with {cardCount} cards was removed.", MessageType.Status);
        }
        catch (Exception)
        {
            _globalMessageStore.SetCurrentMessage("Deck removal failed.", MessageType.Error);
        }
    }
}
