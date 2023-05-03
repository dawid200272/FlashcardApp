using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Models
{
    public class Deck
    {
        private static int _deckCount = 0;

        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; } = null;

        public List<Card> Cards { get; }

        public Deck(string name, string? description = null, List<Card>? cards = null)
        {
            Id = ++_deckCount;
            Name = name;

            if (description is not null)
            {
                Description = description;
            }

            if (cards is not null)
            {
                Cards = cards;
            }
            else
            {
                Cards = new List<Card>();
            }

            _deckCount++;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void AddCardRange(IEnumerable<Card> cards)
        {
            Cards.AddRange(cards);
        }

        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }

        public void RemoveCardRange(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                Cards.Remove(card);
            }
        }
    }
}
