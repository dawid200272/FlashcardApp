using FlashcardApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Services
{
    public interface ICardService
    {
        Task<Card> CreateCard(CardTemplate cardTemplate, Deck deck);

        Task<List<Card>> CreateCards(List<CardTemplate> cardTemplates, Deck deck);
    }
}
