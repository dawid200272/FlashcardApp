using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.Commands;

public class SelectDeckCommand : CommandBase
{
    private readonly IParameterRenavigator _renavigator;
    private readonly DeckViewModel _deckViewModel;

    public SelectDeckCommand(IParameterRenavigator renavigator,
        DeckViewModel deckViewModel)
    {
        _renavigator = renavigator;
        _deckViewModel = deckViewModel;
    }

    public override void Execute(object? parameter)
    {
        _renavigator.Renavigate((viewModelBase) =>
        {
            DeckDetailsViewModel viewModel = (DeckDetailsViewModel)viewModelBase;

            viewModel.LoadDeckDetailsViewModel(_deckViewModel);
        });
    }
}
