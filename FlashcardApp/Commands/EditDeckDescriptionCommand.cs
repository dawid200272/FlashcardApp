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
    private readonly GlobalMessageStore _globalMessageStore;

    public EditDeckDescriptionCommand(EditDeckDescriptionViewModel viewModel,
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

            Deck deck = _viewModel.DeckViewModel.Deck;

            string? newDeckDescription = _viewModel.DeckDescription;

            deck.Description = newDeckDescription;

            await _deckStore.UpdateAsync(deck);

            _viewModel.CloseCommand.Execute(null);

            _globalMessageStore.SetCurrentMessage("Desription was edited.", MessageType.Status);
        }
        catch (Exception)
        {
            _globalMessageStore.SetCurrentMessage("Editing description failed.", MessageType.Error);
        }
    }
}
