using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDeckService _deckService;
        private DeckCollection _deckCollection;
        private IFlashcardAppViewModelAbstractFactory _viewModelFactory;

        public MainWindowViewModel(INavigator navigator,
            DeckCollection deckCollection, IDeckService deckService, IFlashcardAppViewModelAbstractFactory viewModelFactory)
        {
            Navigator = navigator;

            _deckCollection = deckCollection;
            _deckService = deckService;

            _viewModelFactory = viewModelFactory;

            Navigator = new Navigator(_deckCollection, _deckService, viewModelFactory);

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.DeckListing);
        }

        public INavigator Navigator { get; set; }
    }
}
