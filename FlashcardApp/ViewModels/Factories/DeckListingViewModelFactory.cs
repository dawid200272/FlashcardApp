using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels.Factories
{
    public class DeckListingViewModelFactory : IFlashcardAppViewModelFactory<DeckListingViewModel>
    {
        private readonly INavigator _navigator;
        private readonly IFlashcardAppViewModelAbstractFactory _viewModelFactory;

        private readonly DeckStore _deckStore;
        private readonly IDeckService _deckService;

        public DeckListingViewModelFactory(INavigator navigator, IFlashcardAppViewModelAbstractFactory viewModelFactory, DeckStore deckStore, IDeckService deckService)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            _deckStore = deckStore;
            _deckService = deckService;
        }

        public DeckListingViewModel CreateViewModel()
        {
            return new DeckListingViewModel(_navigator, _viewModelFactory, _deckStore, _deckService);
        }
    }
}
