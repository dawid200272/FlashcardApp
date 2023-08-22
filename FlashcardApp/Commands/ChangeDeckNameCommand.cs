using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class ChangeDeckNameCommand : AsyncCommandBase
{
    private readonly ChangeDeckNameViewModel _viewModel;
    private readonly DeckStore _deckStore;

    public ChangeDeckNameCommand(ChangeDeckNameViewModel viewModel, DeckStore deckStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
    }

    public override async Task ExecuteAsync (object? parameter)
    {
        Deck deck = _viewModel.DeckViewModel.Deck;

        string newDeckName = _viewModel.NewDeckName;

        deck.Name = newDeckName;

        await _deckStore.UpdateAsync(deck);

        _viewModel.CloseCommand.Execute(null);
    }
}
