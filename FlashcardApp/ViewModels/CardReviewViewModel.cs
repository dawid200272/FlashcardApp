using FlashcardApp.WPF.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.WPF.Stores;

namespace FlashcardApp.WPF.ViewModels;

public class CardReviewViewModel : ViewModelBase
{
    private readonly DeckViewModel _deckViewModel;
    private readonly IParameterRenavigator _renavigator;
    private readonly DeckStore _deckStore;

    private Deck _deck;
    private List<CardViewModel> _reviewCards;

    private int reviewCardIndex = 0;

    private bool _isLastReviewCard = false;
    private bool _isAnswerHidden = true;
    private bool _isAnswered = false;

    private readonly List<Card> _cardsToUpdate = new List<Card>();

    public CardReviewViewModel(IParameterRenavigator renavigator,
        DeckStore deckStore)
    {
        _renavigator = renavigator;

        ShowAnswerCommand = new ShowAnswerCommand(this);
        NextReviewCardCommand = new NextReviewCardCommand(this);
        AnswerCommand = new AnswerCommand(this);
        _deckStore = deckStore;
    }

    ~CardReviewViewModel()
    {
        UpdateCards();
    }

    private CardViewModel _currentReviewCard;
    public CardViewModel CurrentReviewCard
    {
        get => _currentReviewCard;
        set
        {
            if (_currentReviewCard != value)
            {
                _currentReviewCard = value;
                OnPropertyChanged();
            }
        }
    }

    // TODO: Make commands for this 2 actions
    // and update next review card command
    public ICommand ShowAnswerCommand { get; set; }
    public ICommand NextReviewCardCommand { get; set; }
    public ICommand EndReviewCommand { get; set; }
    public ICommand AnswerCommand { get; set; }

    public bool IsLastReviewCard
    {
        get => _isLastReviewCard;
        set
        {
            if (_isLastReviewCard != value)
            {
                _isLastReviewCard = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsReviewEnded));
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
                OnPropertyChanged(nameof(IsReviewEnded));
            }
        }
    }

    public bool IsReviewEnded => IsLastReviewCard && IsAnswered;

    public void LoadReviewCards(DeckViewModel deckViewModel, int reviewCardNumber)
    {
        if (EndReviewCommand is null)
        {
            EndReviewCommand = new SelectDeckCommand(_renavigator, deckViewModel);
        }

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
            EndReviewCommand.Execute(null);
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

    public void SetAnswer(CardReviewAnswerResult? answerResult = null)
    {
        Card card = CurrentReviewCard.Card;

        if (card.State == CardState.newCard)
        {
            card.State = CardState.learning;
        }

        _cardsToUpdate.Add(card);

        IsAnswered = true;

        NextReviewCardCommand.Execute(null);
    }

    private async Task UpdateCards()
    {
        await _deckStore.UpdateAsync(_deck);
    }
}
