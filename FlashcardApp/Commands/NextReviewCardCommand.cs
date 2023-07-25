using FlashcardApp.ViewModels;
using FlashcardApp.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Commands
{
    public class NextReviewCardCommand : CommandBase
    {
        private readonly CardReviewViewModel _viewModel;

        public NextReviewCardCommand(CardReviewViewModel cardReviewViewModel)
        {
            _viewModel = cardReviewViewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.NextReviewCard();
        }
    }
}
