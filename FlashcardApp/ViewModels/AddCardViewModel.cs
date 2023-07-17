using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.ViewModels
{
    public class AddCardViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly DeckStore _deckStore;

        public AddCardViewModel(INavigator navigator, DeckStore deckStore)
        {
            _navigator = navigator;
            _deckStore = deckStore;
        }

        public ICommand AddCardCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        //public ICollection<CardTemplateType> CardTemplateTypes { get; set; }

        public CardTemplateType SelectedTemplateType { get; set; }

        public ObservableCollection<Deck> Decks => new ObservableCollection<Deck>(_deckStore.Decks);
        public Deck SelectedDeck { get; set; }

        private string _front;
        public string Front
        {
            get
            {
                return _front;
            }
            set
            {
                _front = value;
                OnPropertyChanged(nameof(Front));
            }
        }

        private string _back;

        public string Back
        {
            get
            {
                return _back;
            }
            set
            {
                _back = value;
                OnPropertyChanged(nameof(Back));
            }
        }

    }
}
