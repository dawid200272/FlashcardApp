using FlashcardApp.Models;
using FlashcardApp.Services;
using FlashcardApp.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DeckCollection _deckCollection;
        private IDeckService _deckService;

        public MainWindowViewModel(DeckCollection deckCollection, IDeckService deckService)
        {
            _deckCollection = deckCollection;
            _deckService = deckService;
        }

        public INavigator Navigator { get; set; } = new Navigator(_deckCollection, _deckService);
    }
}
