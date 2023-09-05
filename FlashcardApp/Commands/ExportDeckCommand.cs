using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class ExportDeckCommand : AsyncCommandBase
{
    private readonly IDeckExportService _deckExportService;
    private readonly DeckViewModel _deckViewModel;

    public ExportDeckCommand(IDeckExportService deckExportService, DeckViewModel deckViewModel)
    {
        _deckExportService = deckExportService;
        _deckViewModel = deckViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        Deck deck = _deckViewModel.Deck;

        DeckExportResult exportResult = await _deckExportService.Export(deck);

        // TODO: Add error handling
        if (!exportResult.IsSuccess)
        {
            throw new Exception(exportResult.Message);
        }
    }
}
