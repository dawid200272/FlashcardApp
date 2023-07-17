﻿using FlashcardApp.Domain.Models;
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
        private readonly IFlashcardAppViewModelFactory<DeckDetailsViewModel> _deckDetailsViewModelFactory;

        public FlashcardAppViewModelAbstractFactory(
            IFlashcardAppViewModelFactory<DeckListingViewModel> deckListingViewModelFactory,
            IFlashcardAppViewModelFactory<CardReviewViewModel> cardReviewViewModelFactory,
            IFlashcardAppViewModelFactory<DeckDetailsViewModel> deckDetailsViewModelFactory)
        {
            _deckListingViewModelFactory = deckListingViewModelFactory;
            _cardReviewViewModelFactory = cardReviewViewModelFactory;
            _deckDetailsViewModelFactory = deckDetailsViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.DeckListing => _deckListingViewModelFactory.CreateViewModel(),
                ViewType.CardReview => _cardReviewViewModelFactory.CreateViewModel(),
                ViewType.DeckDetails => _deckDetailsViewModelFactory.CreateViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.",
                    nameof(viewType)),
            };
        }
    }
}
