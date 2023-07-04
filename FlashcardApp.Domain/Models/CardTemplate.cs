using FlashcardApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Models
{
    public class CardTemplate : DomainObject
    {
        public string Front { get; set; }
        public string Back { get; set; }
        public CardTemplateType TemplateType { get; set; } = CardTemplateType.Basic;

        public CardTemplate(string front, string back)
        {
            Front = front;
            Back = back;
        }
    }
}
