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
        if (parameter is CardReviewAnswerResult answerResult)
        {
            _viewModel.SetAnswer(answerResult);
        }

            _viewModel.SetAnswer();
    }
}

// TODO: Move to separate file
public enum CardReviewAnswerResult
{
    Right,
    Wrong
}