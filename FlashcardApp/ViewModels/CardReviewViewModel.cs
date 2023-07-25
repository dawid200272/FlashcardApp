﻿using FlashcardApp.Commands;
using FlashcardApp.Domain.Models;
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
        private readonly DeckViewModel _deckViewModel;
        private Deck _deck;
        private List<CardViewModel> _reviewCards;

        private int reviewCardIndex = 0;

        private bool _isLastReviewCard = false;
        private bool _isAnswerHidden = true;
        private bool _isAnswered = false;
        public CardReviewViewModel(/*IParameterRenavigator renavigator*/)

        public CardReviewViewModel()
        {
            ShowAnswerCommand = new ShowAnswerCommand(this);
            NextReviewCardCommand = new NextReviewCardCommand(this);
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
                if (_currentReviewCard != value)
                {
                _currentReviewCard = value;
                OnPropertyChanged();
            }
        }
        }

        private List<CardViewModel> _reviewCards;

        public ICommand ShowAnswerCommand { get; set; }
        public ICommand NextReviewCardCommand { get; set; }

        public bool IsLastReviewCard
        {
            get => _isLastReviewCard;
            set
            {
                if (_isLastReviewCard != value)
                {
                    _isLastReviewCard = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsAnswerHidden
        {
            get => _isAnswerHidden;
            set
            {
                if (_isAnswerHidden != value)
                {
                    _isAnswerHidden = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsAnswered
        {
            get => _isAnswered;
            set
            {
                if (_isAnswered != value)
                {
                    _isAnswered = value;
                    OnPropertyChanged();
                }
            }
        }

        public void LoadReviewCards(DeckViewModel deckViewModel, int reviewCardNumber)
        {
            //EndReviewCommand = new SelectDeckCommand(_renavigator, _deckViewModel);

            _deck = deckViewModel.Deck;

            _reviewCards = new List<CardViewModel>();

            var cardsToReview = _deck.Cards.Take(reviewCardNumber).ToList();

            foreach (var card in cardsToReview)
            {
                _reviewCards.Add(new CardViewModel(card));
            }

            NextReviewCard();
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

        public void NextReviewCard()
        {
            if (IsLastReviewCard == true)
            {
                return;
            }

            IsAnswerHidden = true;
            IsAnswered = false;

            _currentReviewCard = GetNextReviewCard();
            OnPropertyChanged(nameof(CurrentReviewCard));
        }

        public void ShowAnswer()
        {
            IsAnswerHidden = false;
        }
    }
}
