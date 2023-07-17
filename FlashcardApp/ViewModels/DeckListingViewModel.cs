using FlashcardApp.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.Stores;
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
        private readonly INavigator _navigator;
        private readonly IFlashcardAppViewModelAbstractFactory _viewModelFactory;

        private readonly DeckStore _deckStore;
        private readonly IDeckService _deckService;

        private ObservableCollection<DeckViewModel> _decks;

        public DeckListingViewModel(INavigator navigator, IFlashcardAppViewModelAbstractFactory viewModelFactory, 
            DeckStore deckStore, IDeckService deckService)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            _deckStore = deckStore;
            _deckService = deckService;

            UpdateDecks(_deckStore);
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

        public async Task AddDeck(string deckName)
        {
            Deck createdDeck = await _deckService.CreateEmptyDeck(deckName);

            await _deckStore.AddAsync(createdDeck);

            UpdateDecks(_deckStore);
        }

        private void UpdateDecks(DeckStore deckStore)
        {
            List<DeckViewModel> deckViewModels = new List<DeckViewModel>();

            foreach (Deck deck in deckStore)
            {
                deckViewModels.Add(new DeckViewModel(_navigator, _viewModelFactory, deck));
            }

            _decks = new ObservableCollection<DeckViewModel>(deckViewModels);
        }
    }
}
