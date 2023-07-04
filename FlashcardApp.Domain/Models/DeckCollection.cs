using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Models
{
    public class DeckCollection
    {
        private List<Deck> _decks;

        public IEnumerable<Deck> Decks => _decks;

        public void Add(Deck deck)
        {
            _decks.Add(deck);
        }

        public void AddRange(IEnumerable<Deck> decks)
        {
            _decks.AddRange(decks);
        }

        public void Remove(Deck deck)
        {
            _decks.Remove(deck);
        }
    }
}
