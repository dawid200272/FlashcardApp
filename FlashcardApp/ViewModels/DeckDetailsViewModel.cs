using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels
{
    public class DeckDetailsViewModel : ViewModelBase
    {
        private Deck _deck;

        public void LoadDeckDetailsViewModel(Deck deck)
        {
            _deck = deck;
        }


    }
}
