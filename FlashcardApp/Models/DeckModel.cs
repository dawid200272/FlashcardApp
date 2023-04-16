using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Models
{
    public class DeckModel
    {
        private static int _deckCount = 0;

        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; } = null;

        public List<CardModel> Cards { get; }

        public DeckModel(string name, string? description = null, List<CardModel>? cards = null)
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
                Cards = new List<CardModel>();
            }

            _deckCount++;
        }

        public void AddCard(CardModel card)
        {
            Cards.Add(card);
        }

        public void AddCardRange(IEnumerable<CardModel> cards)
        {
            Cards.AddRange(cards);
        }

        public void RemoveCard(CardModel card)
        {
            Cards.Remove(card);
        }

        public void RemoveCardRange(IEnumerable<CardModel> cards)
        {
            foreach (var card in cards)
            {
                Cards.Remove(card);
            }
        }
    }
}
