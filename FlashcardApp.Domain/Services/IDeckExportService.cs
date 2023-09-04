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
    /// Export given <paramref name="deck"/>.
    /// </summary>
    /// <param name="deck"><see cref="Deck"/> to be exported.</param>
    /// <returns><see cref="DeckExportResult"/></returns>
    Task<DeckExportResult> Export(Deck deck);
}

/// <summary>
/// Result object for <see cref="IDeckExportService"/>.
/// </summary>
/// <param name="IsSuccess">It is <see langword="true"/> when deck was exported successfully; otherwise it is <see langword="false"/>.</param>
/// <param name="Message">When <see cref="IsSuccess"/> is <see langword="true"/> it is success message; otherwise it is error message.</param>
public record DeckExportResult(bool IsSuccess, string Message);
