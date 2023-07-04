using FlashcardApp.Models;
using FlashcardApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Services
{
    public interface ICardTemplateService
    {
        Task<CardTemplate> CreateCardTemplate(CardTemplateType type, string front, string back);
    }
}
