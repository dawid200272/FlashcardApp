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

        private readonly DeckViewModel _deckViewModel;

        public SelectDeckCommand(INavigator navigator, IFlashcardAppViewModelAbstractFactory viewModelFactory, 
            DeckViewModel deckViewModel)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _deckViewModel = deckViewModel;
        }

        public override void Execute(object? parameter)
        {
            DeckDetailsViewModel viewModel = (DeckDetailsViewModel)_viewModelFactory.CreateViewModel(ViewType.DeckDetails);

            viewModel.LoadDeckDetailsViewModel(_deckViewModel);
                
            _navigator.CurrentViewModel = viewModel;
        }
    }
}
