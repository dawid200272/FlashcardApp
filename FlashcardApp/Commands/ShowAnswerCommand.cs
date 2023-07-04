using FlashcardApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.Commands
{
    public class ShowAnswerCommand : ICommand
    {
        private readonly CardReviewViewModel _viewModel;

        public ShowAnswerCommand(CardReviewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (_viewModel.IsAnswerHidden)
            {
                return true;
            }

            return false;
        }

        public void Execute(object? parameter)
        {
            _viewModel.IsAnswerHidden = false;
        }
    }
}
