using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels.Factories
{
    public class DeckDetailsViewModelFactory : IFlashcardAppViewModelFactory<DeckDetailsViewModel>
    {
        private readonly IReturnableRenavigator _renavigator;

        public DeckDetailsViewModelFactory(IReturnableRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        public DeckDetailsViewModel CreateViewModel()
        {
            return new DeckDetailsViewModel(_renavigator);
        }
    }
}
