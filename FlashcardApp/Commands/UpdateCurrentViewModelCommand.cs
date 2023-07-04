using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IFlashcardAppViewModelAbstractFactory _viewModelFactory;

        private readonly IDeckService _deckService;
        private DeckCollection _deckCollection;

        public UpdateCurrentViewModelCommand(INavigator navigator, DeckCollection deckCollection,
            IDeckService deckService, IFlashcardAppViewModelAbstractFactory viewModelFactory)
        {
            _navigator = navigator;
            _deckCollection = deckCollection;
            _deckService = deckService;
            _viewModelFactory = viewModelFactory;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
