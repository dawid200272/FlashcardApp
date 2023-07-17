using FlashcardApp.Domain.Models;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.Commands
{
    public class StartCardReviewCommand : CommandBase
    {
        private readonly INavigator _navigator;
        private readonly Deck _deck;

        // TODO: Move review card number to settings, maybe in a JSON file
        const int REVIEW_CARD_NUMBER = 15;

        public StartCardReviewCommand(INavigator navigator, Deck deck)
        {
            _navigator = navigator;
            _deck = deck;
        }

        public override void Execute(object? parameter)
        {
            _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.DeckDetails);

            CardReviewViewModel cardReviewViewModel = (CardReviewViewModel)_navigator.CurrentViewModel;

            cardReviewViewModel.LoadReviewCards(_deck, REVIEW_CARD_NUMBER);
        }
    }
}
