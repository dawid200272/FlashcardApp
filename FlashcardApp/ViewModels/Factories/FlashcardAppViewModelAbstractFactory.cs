using FlashcardApp.Domain.Models;
using FlashcardApp.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels.Factories
{
    public class FlashcardAppViewModelAbstractFactory : IFlashcardAppViewModelAbstractFactory
    {
        private readonly IFlashcardAppViewModelFactory<DeckListingViewModel> _deckListingViewModelFactory;
        private readonly IFlashcardAppViewModelFactory<CardReviewViewModel> _cardReviewViewModelFactory;

        public FlashcardAppViewModelAbstractFactory(IFlashcardAppViewModelFactory<DeckListingViewModel> deckListingViewModelFactory, IFlashcardAppViewModelFactory<CardReviewViewModel> cardReviewViewModelFactory)
        {
            _deckListingViewModelFactory = deckListingViewModelFactory;
            _cardReviewViewModelFactory = cardReviewViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.DeckListing:
                    return _deckListingViewModelFactory.CreateViewModel();
                case ViewType.CardReview:
                    return _cardReviewViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(viewType));
            }
        }
    }
}
