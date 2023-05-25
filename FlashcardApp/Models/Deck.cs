using FlashcardApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Models
{
    public class Deck : DomainObject, IDeckService
    {
        private static int _deckCount = 0;
        private List<Card> _cards;

        public string Name { get; set; }
        public string? Description { get; set; } = null;

        public IEnumerable<Card> Cards { get => _cards; }

        private Deck() { }

        public Deck(string name, string? description = null, List<Card>? cards = null)
        {
            //ID = ++_deckCount;
            Name = name;

            if (description is not null)
            {
                Description = description;
            }

            if (cards is not null)
            {
                _cards = cards;
            }
            else
            {
                _cards = new List<Card>();
            }

            _deckCount++;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void AddCardRange(IEnumerable<Card> cards)
        {
            _cards.AddRange(cards);
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }

        public void RemoveCardRange(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                _cards.Remove(card);
            }
        }

        public static Task<Deck> CreateEmptyDeck(string name)
        {
            Deck result = new Deck(name);

            return Task.FromResult(result);
        }

        public static async Task<Deck> CreateDeckWithCards(string name, List<CardTemplate> cardTemplates)
        {
            Deck result = new Deck(name);

            foreach (var cardTemplate in cardTemplates)
            {
                result.AddCard(await Card.CreateCard(cardTemplate, result));
            }

            return result;
        }

        public static Task<List<Deck>> CreateDecks(List<string> names, List<CardTemplate> cards)
        {
            throw new NotImplementedException();
        }
    }
}
