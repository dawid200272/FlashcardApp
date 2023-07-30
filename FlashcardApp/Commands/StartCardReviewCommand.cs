using FlashcardApp.Domain.Models;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.Commands;

public class StartCardReviewCommand : CommandBase
{
    private readonly IParameterRenavigator _renavigator;
    private readonly DeckViewModel _deckViewModel;

    // TODO: Move review card number to settings, maybe in a JSON file
    const int REVIEW_CARD_NUMBER = 15;

    public StartCardReviewCommand(IParameterRenavigator renavigator, DeckViewModel deckViewModel)
    {
        _renavigator = renavigator;
        _deckViewModel = deckViewModel;
    }

    public override void Execute(object? parameter)
    {
        _renavigator.Renavigate((viewModelBase) =>
        {
            CardReviewViewModel viewModel = (CardReviewViewModel)viewModelBase;

            viewModel.LoadReviewCards(_deckViewModel, REVIEW_CARD_NUMBER);
        });
    }
}
