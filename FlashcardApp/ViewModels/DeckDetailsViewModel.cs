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

namespace FlashcardApp.ViewModels
{
    public class DeckDetailsViewModel : ViewModelBase
    {
        private readonly IReturnableRenavigator _renavigator;
        private DeckViewModel _deckViewModel;

        public DeckDetailsViewModel(IReturnableRenavigator renavigator)
        {
            _renavigator = renavigator;

            StartCardReviewCommand = new StartCardReviewCommand(_renavigator, _deckViewModel);
        }

        public void LoadDeckDetailsViewModel(DeckViewModel deckViewModel)
        {
            _deckViewModel = deckViewModel;
        }

        public ICommand StartCardReviewCommand { get; set; }
        public ICommand StartCardBrowsingCommand { get; set; }

        public string Name => _deckViewModel.Name;

        public string? Description => _deckViewModel.Description;

        public int NewCardsNumber => _deckViewModel.NewCardsNumber;

        public int CardsNumber => _deckViewModel.CardsNumber;

        public void UdpateCardsInfo()
        {
            OnPropertyChanged(nameof(NewCardsNumber));
            OnPropertyChanged(nameof(CardsNumber));
        }
    }
}
