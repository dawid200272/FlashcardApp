using FlashcardApp.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.ViewModels
{
    public class DeckListingViewModel : ViewModelBase
    {
        private DeckCollection _deckCollection;
        private IDeckService _deckService;
        private ObservableCollection<DeckViewModel> _decks;

        public DeckListingViewModel(DeckCollection deckCollection, IDeckService deckService)
        {
            _deckCollection = deckCollection;
            _deckService = deckService;

            var deckViewModels = new List<DeckViewModel>();

            foreach (Deck deck in _deckCollection)
            {
                deckViewModels.Add(new DeckViewModel(deck));
            }

            _decks = new ObservableCollection<DeckViewModel>(deckViewModels);
        }

        public ObservableCollection<DeckViewModel> Decks
        {
            get
            {
                return _decks;
            }
            set
            {
                _decks = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddEmptyDeckCommand => new AddEmptyDeckCommand(this);

        //public ICommand SelectDeck => new SelectDeckCommand();

        public async Task AddDeck(string deckName)
        {
            Deck createdDeck = await _deckService.CreateEmptyDeck(deckName);

            _deckCollection.Add(createdDeck);
        }

    }
}
