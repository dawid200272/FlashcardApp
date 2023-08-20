using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class StartCardBrowsingCommand : CommandBase
{
    private readonly IParameterRenavigator _renavigator;
    private readonly DeckViewModel _deckViewModel;

    public StartCardBrowsingCommand(IParameterRenavigator renavigator, DeckViewModel deckViewModel)
    {
        _renavigator = renavigator;
        _deckViewModel = deckViewModel;
    }

    public override void Execute(object? parameter)
    {
        _renavigator.Renavigate((viewModelBase) =>
        {
            CardBrowsingViewModel viewModel = (CardBrowsingViewModel)viewModelBase;

            viewModel.LoadReviewCards(_deckViewModel);
        });
    }
}
