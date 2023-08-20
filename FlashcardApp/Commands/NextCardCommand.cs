using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class NextCardCommand : CommandBase
{
    private readonly CardBrowsingViewModel _viewModel;

    public NextCardCommand(CardBrowsingViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        _viewModel.NextReviewCard();
    }
}
