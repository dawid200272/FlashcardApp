using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.ViewModels.Factories;

public class FlashcardAppViewModelFactory : IFlashcardAppViewModelFactory
{
    private readonly CreateViewModel<DeckListingViewModel> _createDeckListingViewModel;
    private readonly CreateViewModel<CardReviewViewModel> _createCardReviewViewModel;
    private readonly CreateViewModel<DeckDetailsViewModel> _createDeckDetailsViewModel;
    private readonly CreateViewModel<AddCardViewModel> _createAddCardViewModel;

    public FlashcardAppViewModelFactory(CreateViewModel<DeckListingViewModel> createDeckListingViewModel,
        CreateViewModel<CardReviewViewModel> createCardReviewViewModel,
        CreateViewModel<DeckDetailsViewModel> createDeckDetailsViewModel,
        CreateViewModel<AddCardViewModel> createAddCardViewModel)
    {
        _createDeckListingViewModel = createDeckListingViewModel;
        _createCardReviewViewModel = createCardReviewViewModel;
        _createDeckDetailsViewModel = createDeckDetailsViewModel;
        _createAddCardViewModel = createAddCardViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.DeckListing => _createDeckListingViewModel(),
            ViewType.CardReview => _createCardReviewViewModel(),
            ViewType.DeckDetails => _createDeckDetailsViewModel(),
            ViewType.AddCard => _createAddCardViewModel(),
            _ => throw new ArgumentException("The ViewType does not have a ViewModel.",
                nameof(viewType)),
        };
    }
}
