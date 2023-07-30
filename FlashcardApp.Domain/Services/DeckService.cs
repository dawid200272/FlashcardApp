using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Services;

public class DeckService : IDeckService
{
    private readonly ICardService _cardService;

    public DeckService()
    {
        _cardService = new CardService();
    }

    public Task<Deck> CreateEmptyDeck(string name)
    {
        Deck result = new Deck(name);

        return Task.FromResult(result);
    }

    public async Task<Deck> CreateDeckWithCards(string name, List<CardTemplate> cardTemplates)
    {
        Deck result = new Deck(name);

        foreach (var cardTemplate in cardTemplates)
        {
            result.AddCard(await _cardService.CreateCard(cardTemplate, result));
        }

        return result;
    }

    public Task<List<Deck>> CreateDecks(List<string> names, List<CardTemplate> cards)
    {
        throw new NotImplementedException();
    }
}
