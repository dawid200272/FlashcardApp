using FlashcardApp.Models;
using FlashcardApp.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
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
        private readonly DeckCollection _deckCollection;
        private readonly IDeckService _deckService;
        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator, DeckCollection deckCollection,
            IDeckService deckService)
        {
            _navigator = navigator;
            _deckCollection = deckCollection;
            _deckService = deckService;
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
                switch (viewType)
                {
                    case ViewType.DeckListing:
                        _navigator.CurrentViewModel = new DeckListingViewModel(_deckCollection, _deckService);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
