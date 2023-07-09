using FlashcardApp.Domain.Models;
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
    public class StartCardReview : ICommand
    {
        private readonly INavigator _navigator;
        private readonly Deck _deck;

        // TODO: Move review card number to settings, maybe in a JSON file
        const int REVIEW_CARD_NUMBER = 15;

        public StartCardReview(INavigator navigator, Deck deck)
        {
            _navigator = navigator;
            _deck = deck;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.DeckDetails);

            CardReviewViewModel cardReviewViewModel = (CardReviewViewModel)_navigator.CurrentViewModel;

            cardReviewViewModel.LoadReviewCards(_deck, REVIEW_CARD_NUMBER);
        }
    }
}
