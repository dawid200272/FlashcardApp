using FlashcardApp.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;
        private DeckCollection _deckCollection;
        private IDeckService _deckService;

        public Navigator(DeckCollection deckCollection, IDeckService deckService)
        {
            _deckCollection = deckCollection;
            _deckService = deckService;
        }

        public ViewModelBase CurrentViewModel
        { 
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this,
            _deckCollection, _deckService);
    }
}
