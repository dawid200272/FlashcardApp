using FlashcardApp.Commands;
using FlashcardApp.Models;
using FlashcardApp.Services;
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
        private ObservableCollection<Deck> _decks => (ObservableCollection<Deck>)_deckCollection.Decks;

        public DeckListingViewModel(DeckCollection deckCollection, IDeckService deckService)
        {
            _deckCollection = deckCollection;
            _deckService = deckService;
        }


        public ObservableCollection<Deck> Decks 
        { 
            get => _decks;
        }

        public ICommand AddEmptyDeckCommand => new AddEmptyDeckCommand(this);

        public async Task AddDeck(string deckName)
        {
            Deck createdDeck = await _deckService.CreateEmptyDeck(deckName);

            _deckCollection.Add(createdDeck);
        }

    }
}
