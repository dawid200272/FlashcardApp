using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlashcardApp.WPF.Helpers;

namespace FlashcardApp.WPF.ViewModels;
public class CardBrowsingViewModel : ViewModelBase
{
    private readonly DeckViewModel _deckViewModel;
    private readonly IParameterRenavigator _renavigator;

    private Deck _deck;
    private List<CardViewModel> _reviewCards;

    private bool _isAnswerHidden = false;

    public CardBrowsingViewModel(IParameterRenavigator renavigator)
    {
        _renavigator = renavigator;

        PreviousCardCommand = new PreviousCardCommand(this);
        NextCardCommand = new NextCardCommand(this);
        // TODO: Find better solution to this
        ShowAnswerCommand = null;
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

    public ICommand PreviousCardCommand { get; set; }
    public ICommand NextCardCommand { get; set; }
    public ICommand EndBrowsingCommand { get; set; }
    public ICommand ShowAnswerCommand { get; set; }

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

    public void LoadReviewCards(DeckViewModel deckViewModel)
    {
        if (EndBrowsingCommand is null)
        {
            EndBrowsingCommand = new SelectDeckCommand(_renavigator, deckViewModel);
        }

        _deck = deckViewModel.Deck;

        _reviewCards = new List<CardViewModel>();

        var cardsToReview = _deck.Cards.ToList();

        foreach (var card in cardsToReview)
        {
            _reviewCards.Add(new CardViewModel(card));
        }

        NextReviewCard();
    }

    public void NextReviewCard()
    {
        _currentReviewCard = _reviewCards.Next(_currentReviewCard);
        OnPropertyChanged(nameof(CurrentReviewCard));
    }

    public void PreviousReviewCard()
    {
        _currentReviewCard = _reviewCards.Prev(_currentReviewCard);
        OnPropertyChanged(nameof(CurrentReviewCard));
    }
}
