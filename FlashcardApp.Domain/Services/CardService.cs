using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Services;

public class CardService : ICardService
{
    public Task<Card> CreateCard(CardTemplate cardTemplate, Deck deck)
    {
        Card result = new Card(cardTemplate, deck);

        return Task.FromResult(result);
    }

    public async Task<List<Card>> CreateCards(List<CardTemplate> cardTemplates, Deck deck)
    {
        List<Card> result = new List<Card>();

        foreach (var cardTemplate in cardTemplates)
        {
            result.Add(await CreateCard(cardTemplate, deck));
        }

        return result;
    }
}
