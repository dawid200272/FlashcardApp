using FlashcardApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels
{
    public class DeckListingViewModel : ViewModelBase
    {
        private ObservableCollection<Deck> _decks;

        public ObservableCollection<Deck> Decks 
        { 
            get => _decks;
            set 
            {
                _decks = value;
                OnPropertyChanged();
            }
        }
    }
}
