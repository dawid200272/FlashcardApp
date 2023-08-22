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

    public AddEmptyDeckCommand(AddEmptyDeckViewModel viewModel, DeckStore deckStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        string deckName = _viewModel.NewDeckName;

        await _deckStore.AddAsync(deckName);

        _viewModel.CloseCommand.Execute(null);
    }
}
