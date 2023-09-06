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
    private readonly GlobalMessageStore _globalMessageStore;

    public ChangeDeckNameCommand(ChangeDeckNameViewModel viewModel,
        DeckStore deckStore,
        GlobalMessageStore globalMessageStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
        _globalMessageStore = globalMessageStore;
    }

    public override async Task ExecuteAsync (object? parameter)
    {
        try
        {
            _globalMessageStore.ClearCurrentMessage();

            Deck deck = _viewModel.DeckViewModel.Deck;

            string newDeckName = _viewModel.NewDeckName;

            deck.Name = newDeckName;

            await _deckStore.UpdateAsync(deck);

            _viewModel.CloseCommand.Execute(null);

            _globalMessageStore.SetCurrentMessage("Deck name was changed.", MessageType.Status);
        }
        catch (Exception)
        {
            _globalMessageStore.SetCurrentMessage("Changing deck name failed.", MessageType.Error);
        }
    }
}
