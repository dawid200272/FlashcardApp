using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Stores;

public class DeckStore : IEnumerable<Deck>
{
    private readonly IDeckService _deckService;
    private readonly ICardService _cardService;
    private readonly ICardTemplateService _cardTemplateService;
    private readonly IDataService<Deck> _deckDataService;
    private readonly IDataService<Card> _cardDataService;
    private readonly IDataService<CardTemplate> _cardTemplateDataService;

    private readonly List<Deck> _decks = new List<Deck>();

    public DeckStore(IDeckService deckService, ICardService cardService, 
        ICardTemplateService cardTemplateService, IDataService<Deck> deckDataService, 
        IDataService<Card> cardDataService, IDataService<CardTemplate> cardTemplateDataService)
    {
        _deckService = deckService;
        _cardService = cardService;
        _cardTemplateService = cardTemplateService;
        _deckDataService = deckDataService;
        _cardDataService = cardDataService;
        _cardTemplateDataService = cardTemplateDataService;
    }

    public IEnumerable<Deck> Decks => _decks;

    public async Task LoadDecksAsync()
    {
        IEnumerable<Deck> decks = await _deckDataService.GetAll();

        _decks.AddRange(decks);
    }

    public async Task AddAsync(Deck deck)
    {
        _decks.Add(deck);

        await _deckDataService.Create(deck);
    }

    public async Task AddRangeAsync(IEnumerable<Deck> decks)
    {
        _decks.AddRange(decks);

        foreach (Deck deck in _decks)
        {
            await _deckDataService.Create(deck);
        }
    }

    public async Task RemoveAsync(Deck deck)
    {
        _decks.Remove(deck);

        await _deckDataService.Delete(deck.Id);
    }

    public IEnumerator<Deck> GetEnumerator()
    {
        return _decks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
