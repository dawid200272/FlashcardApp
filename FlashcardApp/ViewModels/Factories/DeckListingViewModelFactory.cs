using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.WPF.State.Navigators;
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
        private readonly IParameterRenavigator _renavigator;

        private readonly DeckStore _deckStore;
        private readonly IDeckService _deckService;

        public DeckListingViewModelFactory(IParameterRenavigator renavigator, DeckStore deckStore, IDeckService deckService)
        {
            _renavigator = renavigator;

            _deckStore = deckStore;
            _deckService = deckService;
        }

        public DeckListingViewModel CreateViewModel()
        {
            return new DeckListingViewModel(_renavigator, _deckStore, _deckService);
        }
    }
}
