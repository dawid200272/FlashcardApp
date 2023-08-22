using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;

public class EditDeckDescriptionCommand : AsyncCommandBase
{
    private readonly EditDeckDescriptionViewModel _viewModel;
    private readonly DeckStore _deckStore;

    public EditDeckDescriptionCommand(EditDeckDescriptionViewModel viewModel, DeckStore deckStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        Deck deck = _viewModel.DeckViewModel.Deck;

        string? newDeckDescription = _viewModel.DeckDescription;

        deck.Description = newDeckDescription;

        await _deckStore.UpdateAsync(deck);

        _viewModel.CloseCommand.Execute(null);
    }
}
