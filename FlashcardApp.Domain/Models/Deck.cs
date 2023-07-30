using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Models;

public class Deck : DomainObject
{
    private readonly List<Card> _cards = new List<Card>();

    public string Name { get; set; }
    public string? Description { get; set; } = null;

    public IEnumerable<Card> Cards { get => _cards; }

    private Deck() { }

    public Deck(string name, string? description = null, List<Card>? cards = null)
    {
        Name = name;

        if (description is not null)
        {
            Description = description;
        }

        if (cards is not null)
        {
            _cards = cards;
        }
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
}
