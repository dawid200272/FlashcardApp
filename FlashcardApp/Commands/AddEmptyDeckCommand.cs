using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.Commands;

public class AddEmptyDeckCommand : CommandBase
{
    private readonly DeckListingViewModel _viewModel;

    public AddEmptyDeckCommand(DeckListingViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override async void Execute(object? parameter)
    {
        if (parameter is string deckName)
        {
            await _viewModel.AddDeck(deckName);
        }
    }
}
