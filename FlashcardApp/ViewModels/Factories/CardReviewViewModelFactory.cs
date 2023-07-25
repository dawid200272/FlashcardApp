using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels.Factories
{
    public class CardReviewViewModelFactory : IFlashcardAppViewModelFactory<CardReviewViewModel>
    {
        private readonly IParameterRenavigator _renavigator;

        public CardReviewViewModelFactory()
        {
        }

        public CardReviewViewModelFactory(IParameterRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        public CardReviewViewModel CreateViewModel()
        {
            return new CardReviewViewModel(/*_renavigator)*/);
        }
    }
}
