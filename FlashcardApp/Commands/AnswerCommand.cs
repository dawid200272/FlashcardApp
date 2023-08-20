using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class AnswerCommand : CommandBase
{
    private readonly CardReviewViewModel _viewModel;

    public AnswerCommand(CardReviewViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        _viewModel.IsAnswered = true;

        _viewModel.CurrentReviewCard.Card.State = CardState.learning;

        _viewModel.NextReviewCardCommand.Execute(null);
    }
}
