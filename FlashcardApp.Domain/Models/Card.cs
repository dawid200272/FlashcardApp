using FlashcardApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Models
{
    public class Card : DomainObject
    {
        private static int _cardCount = 0;

        public string Front => CardTemplate.Front;
        public string Back => CardTemplate.Back;

        public Deck Deck { get; set; }
        public CardTemplate CardTemplate { get; set; }

        public CardState State { get; set; }

        private Card() { }

        public Card(CardTemplate cardTemplate, Deck deck)
        {
            State = CardState.newCard;

            CardTemplate = cardTemplate;
            _cardCount++;

            ID = _cardCount;
        }
    }
}
