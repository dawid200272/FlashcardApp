using FlashcardApp.Domain.Models;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.Commands
{
    public class SelectDeckCommand : CommandBase
    {
        private readonly INavigator _navigator;
        private readonly IFlashcardAppViewModelAbstractFactory _viewModelFactory;

        public SelectDeckCommand(INavigator navigator, IFlashcardAppViewModelAbstractFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is DeckViewModel deckViewModel)
            {
                Deck deck = deckViewModel.GetDeck();

                DeckDetailsViewModel viewModel = (DeckDetailsViewModel)_viewModelFactory.CreateViewModel(ViewType.DeckDetails);

                viewModel.LoadDeckDetailsViewModel(deck);
                
                _navigator.CurrentViewModel = viewModel;
            }
        }
    }
}
