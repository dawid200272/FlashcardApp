using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.Domain.Services;
public interface IDeckExportService
{
    /// <summary>
    /// Export given deck
    /// </summary>
    /// <param name="deck">Deck to be exported</param>
    /// <returns>Result of export</returns>
    Task<bool> Export(Deck deck);
}
