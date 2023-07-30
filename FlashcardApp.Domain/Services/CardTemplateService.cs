using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Services;

public class CardTemplateService : ICardTemplateService
{
    public Task<CardTemplate> CreateCardTemplate(CardTemplateType type, string front, string back)
    {
        CardTemplate result = new CardTemplate(front, back);

        return Task.FromResult(result);
    }
}
