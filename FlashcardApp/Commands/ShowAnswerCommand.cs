using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.Commands;

public class ShowAnswerCommand : CommandBase
{
    private readonly CardReviewViewModel _viewModel;

    public ShowAnswerCommand(CardReviewViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override bool CanExecute(object? parameter)
    {
        if (_viewModel.IsAnswerHidden)
        {
            return true;
        }

        return false;
    }

    public override void Execute(object? parameter)
    {
        _viewModel.ShowAnswer();
    }
}
