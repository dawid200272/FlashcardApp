using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private readonly Card _card;

        public CardViewModel(Card card)
        {
            _card = card;
        }

		public string Front => _card.Front;
        public string Back => _card.Back;
	}
}
