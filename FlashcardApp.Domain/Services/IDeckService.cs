using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Services
{
    public interface IDeckService
    {
        /// <summary>
        /// Returns an empty deck
        /// </summary>
        /// <param name="name">name of created deck</param>
        /// <returns></returns>
        Task<Deck> CreateEmptyDeck(string name);
        /// <summary>
        /// Returns deck with cards created from given card templates
        /// </summary>
        /// <param name="name">name of created deck</param>
        /// <param name="cardTemplates">list of card templates used to create cards from</param>
        /// <returns></returns>
        Task<Deck> CreateDeckWithCards(string name, List<CardTemplate> cardTemplates);
        /// <summary>
        /// Returns a list of decks with cards created from given card templates
        /// </summary>
        /// <param name="names"></param>
        /// <param name="cards"></param>
        /// <returns></returns>
        Task<List<Deck>> CreateDecks(List<string> names, List<CardTemplate> cards);
    }
}
