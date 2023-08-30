﻿using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.Domain.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

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

    public event Action DecksLoaded;
    public event Action<Deck> DeckAdded;
    public event Action<Deck> DeckUpdated;
    public event Action<Deck> DeckDeleted;

    public async Task LoadDecksAsync()
    {
        IEnumerable<Deck> decks = await _deckDataService.GetAll();

        _decks.Clear();
        _decks.AddRange(decks);

        DecksLoaded?.Invoke();
    }

    #region test purpose

    public event Action<Card> CardAdded;
    public event Action<Card> CardUpdated;
    public event Action<Card> CardDeleted;

    public async Task AddCardAsync(CardTemplateType templateType, string front, string back, Deck deck)
    {
        CardTemplate template = await _cardTemplateService.CreateCardTemplate(templateType, front, back);

        CardTemplate templateFromDb = await _cardTemplateDataService.Create(template);

        Card card = await _cardService.CreateCard(templateFromDb, deck);

        templateFromDb.Card = card;

        deck.AddCard(card);

        await UpdateAsync(deck);

        CardAdded?.Invoke(card);
    }

    public async Task UpdateCardAsync(Card card, string front, string back)
    {
        CardTemplate template = card.CardTemplate;

        int id = template.Id;

        template.Front = front;
        template.Back = back;
        
        Deck deck = card.Deck;

        //Card newCard = await _cardService.CreateCard(template, deck);

        CardTemplate result = await _cardTemplateDataService.Update(id, template);

        //Card result = await _cardDataService.Update(id, newCard);

        //await UpdateAsync(deck);

        CardUpdated?.Invoke(card);
    }

    public async Task RemoveCardAsync(Card card)
    {
        bool isDeleted = await _cardDataService.Delete(card.Id);

        if (!isDeleted)
        {
            return;
        }

        Deck deck = card.Deck;

        if (deck.Cards.Contains(card))
        {
            deck.RemoveCard(card);
        }

        CardTemplate cardTemplate = card.CardTemplate;

        bool result = await _cardTemplateDataService.Delete(cardTemplate.Id);

        CardDeleted?.Invoke(card);
    }

    #endregion

    public async Task AddAsync(string deckName)
    {
        Deck deck = await _deckService.CreateEmptyDeck(deckName);

        Deck result = await _deckDataService.Create(deck);

        _decks.Add(result);

        DeckAdded?.Invoke(result);
    }

    // TODO: Get rid of that method after UI testing complete
    public async Task AddAsync(Deck deck)
    {
        Deck result = await _deckDataService.Create(deck);

        _decks.Add(result);

        DeckAdded?.Invoke(result);
    }

    public async Task AddRangeAsync(IEnumerable<Deck> decks)
    {
        List<Deck> result = new List<Deck>();

        foreach (Deck deck in _decks)
        {
            result.Add(await _deckDataService.Create(deck));
        }

        _decks.AddRange(result);
    }

    public async Task UpdateAsync(Deck deck)
    {
        int id = deck.Id;

        Deck result = await _deckDataService.Update(id, deck);

        int currentIndex = _decks.FindIndex(d => d.Id == id);

        if (currentIndex != -1)
        {
            _decks[currentIndex] = result;
        }
        else
        {
            _decks.Add(result);
        }

        DeckUpdated?.Invoke(result);
    }

    public async Task RemoveAsync(Deck deck)
    {
        bool isDeleted = await _deckDataService.Delete(deck.Id);

        List<CardTemplate> cardTemplates = new List<CardTemplate>();

        foreach (Card card in deck.Cards)
        {
            cardTemplates.Add(card.CardTemplate);
        }

        if (!isDeleted)
        {
            return;
        }

        _decks.Remove(deck);

        foreach (CardTemplate cardTemplate in cardTemplates)
        {
            bool result = await _cardTemplateDataService.Delete(cardTemplate.Id);
        }

        DeckDeleted?.Invoke(deck);
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
