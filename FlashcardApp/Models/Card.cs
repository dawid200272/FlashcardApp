using FlashcardApp.Models.Enums;
using FlashcardApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Models
{
    public class Card : DomainObject, ICardService
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
            //ID = ++_cardCount;

            State = CardState.newCard;

            CardTemplate = cardTemplate;
            _cardCount++;
        }

        public static Task<Card> CreateCard(CardTemplate cardTemplate, Deck deck)
        {
            Card result = new Card(cardTemplate, deck);

            return Task.FromResult(result);
        }

        public static async Task<List<Card>> CreateCardsAsync(List<CardTemplate> cardTemplates, Deck deck)
        {
            List<Card> result = new List<Card>();

            foreach (var cardTemplate in cardTemplates)
            {
                result.Add(await CreateCard(cardTemplate, deck));
            }

            return result;
        }
    }
}
