using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.State.Navigators;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;

public class DeckDetailsViewModel : ViewModelBase
{
    private readonly IParameterRenavigator _cardReviewRenavigator;
    private readonly IParameterRenavigator _cardBrowsingRenavigator;
    private DeckViewModel _deckViewModel;

    public DeckDetailsViewModel(IParameterRenavigator cardReviewRenavigator,
        IParameterRenavigator cardBrowsingRenavigator)
    {
        _cardReviewRenavigator = cardReviewRenavigator;
        _cardBrowsingRenavigator = cardBrowsingRenavigator;
    }

    public void LoadDeckDetailsViewModel(DeckViewModel deckViewModel)
    {
        _deckViewModel = deckViewModel;

        _deckViewModel.UpdateCardsInfo();

        StartCardReviewCommand = new StartCardReviewCommand(_cardReviewRenavigator, _deckViewModel);
        StartCardBrowsingCommand = new StartCardBrowsingCommand(_cardBrowsingRenavigator, _deckViewModel);

        UpdateCardsInfo();
    }

    public ICommand StartCardReviewCommand { get; set; }
    public ICommand StartCardBrowsingCommand { get; set; }

    public string Name => _deckViewModel.Name;

    public string? Description => _deckViewModel.Description;

    public bool HasDescription => string.IsNullOrEmpty(_deckViewModel.Description);
    public bool HasCards => CardsNumber > 0;

    public int NewCardsNumber => _deckViewModel.NewCardsNumber;

    public int CardsNumber => _deckViewModel.CardsNumber;

    public void UpdateCardsInfo()
    {
        OnPropertyChanged(nameof(NewCardsNumber));
        OnPropertyChanged(nameof(CardsNumber));
    }
}
