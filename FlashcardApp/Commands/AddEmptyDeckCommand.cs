using FlashcardApp.WPF.Stores;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.Commands;

public class AddEmptyDeckCommand : AsyncCommandBase
{
    private readonly AddEmptyDeckViewModel _viewModel;
    private readonly DeckStore _deckStore;
    private readonly GlobalMessageStore _globalMessageStore;

    public AddEmptyDeckCommand(AddEmptyDeckViewModel viewModel,
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

            string deckName = _viewModel.NewDeckName;

            await _deckStore.AddAsync(deckName);

            _viewModel.CloseCommand.Execute(null);

            _globalMessageStore.SetCurrentMessage("Deck was added.", MessageType.Status);
        }
        catch (Exception)
        {
            _globalMessageStore.SetCurrentMessage("Adding deck failed.", MessageType.Error);
        }
    }
}
