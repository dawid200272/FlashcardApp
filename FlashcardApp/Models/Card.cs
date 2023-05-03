using FlashcardApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Models
{
    public class Card
    {
        private static int _cardCount = 0;

        public int ID { get; set; }

        public string Front { get; set; }
        public string Back { get; set; }

        public Deck Deck { get; set; }

        public CardState State { get; set; }

        public Card(string front, string back)
        {
            ID = ++_cardCount;

            Front = front;
            Back = back;

            State = CardState.newCard;

            _cardCount++;
        }

    }
}
