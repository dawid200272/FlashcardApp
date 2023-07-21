using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.ViewModels.Factories
{
    public class AddCardViewModelFactory : IFlashcardAppViewModelFactory<AddCardViewModel>
    {
        private readonly IRenavigator _renavigator;
        private readonly DeckStore _deckStore;

        public AddCardViewModelFactory(IRenavigator renavigator, DeckStore deckStore)
        {
            _renavigator = renavigator;
            _deckStore = deckStore;
        }

        public AddCardViewModel CreateViewModel()
        {
            return new AddCardViewModel(_renavigator, _deckStore);
        }
    }
}
