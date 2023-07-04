using FlashcardApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.Commands
{
    public class AddEmptyDeckCommand : ICommand
    {
        private readonly DeckListingViewModel _viewModel;

        public AddEmptyDeckCommand(DeckListingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            if(parameter is string deckName)
            {
                _viewModel.AddDeck(deckName);
            }

        }
    }
}
