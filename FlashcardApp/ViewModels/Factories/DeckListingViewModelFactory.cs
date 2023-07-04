using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels.Factories
{
    public class DeckListingViewModelFactory : IFlashcardAppViewModelFactory<DeckListingViewModel>
    {
        private readonly DeckCollection _deckCollection;
        private readonly IDeckService _deckService;

        public DeckListingViewModelFactory(DeckCollection deckCollection, IDeckService deckService)
        {
            _deckCollection = deckCollection;
            _deckService = deckService;
        }

        public DeckListingViewModel CreateViewModel()
        {
            return new DeckListingViewModel(_deckCollection, _deckService);
        }
    }
}
