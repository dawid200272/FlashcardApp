using FlashcardApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.ViewModels
{
    public class CardReviewViewModel : ViewModelBase
    {
        private readonly Deck _deck;

        public CardReviewViewModel(Deck deck)
        {
            _deck = deck;
        }

        private CardViewModel _currentReviewCard;
        public CardViewModel CurrentReviewCard
        {
            get
            {
                return _currentReviewCard;
            }
            set
            {
                _currentReviewCard = value;
                OnPropertyChanged();
            }
        }

        private List<CardViewModel> _reviewCards;

        private int reviewCardIndex = 0;

        public bool IsLastReviewCard = false;
        public bool IsAnswerHidden = true;

        public ICommand ShowAnswerCommand;

        public void LoadReviewCards(int reviewCardNumber)
        {
            _reviewCards = new List<CardViewModel>();

            var cardsToReview = _deck.Cards.Take(reviewCardNumber).ToList();

            foreach (var card in cardsToReview)
            {
                _reviewCards.Add(new CardViewModel(card));
            }
        }

        public CardViewModel GetNextReviewCard()
        {
            CardViewModel reviewCard = _reviewCards[reviewCardIndex];

            reviewCardIndex++;

            if (reviewCardIndex >= _reviewCards.Count)
            {
                IsLastReviewCard = true;
            }

            return reviewCard;
        }
    }
}
