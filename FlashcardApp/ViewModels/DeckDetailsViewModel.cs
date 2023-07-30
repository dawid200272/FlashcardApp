using FlashcardApp.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.State.Navigators;
using FlashcardApp.WPF.State.Navigators;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.ViewModels;

public class DeckDetailsViewModel : ViewModelBase
{
    private readonly IParameterRenavigator _renavigator;
    private DeckViewModel _deckViewModel;

    public DeckDetailsViewModel(IParameterRenavigator renavigator)
    {
        _renavigator = renavigator;
    }

    public void LoadDeckDetailsViewModel(DeckViewModel deckViewModel)
    {
        _deckViewModel = deckViewModel;

        StartCardReviewCommand = new StartCardReviewCommand(_renavigator, _deckViewModel);
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
