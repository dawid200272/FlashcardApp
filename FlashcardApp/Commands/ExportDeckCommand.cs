using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.WPF.Stores;
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
    private readonly GlobalMessageStore _globalMessageStore;

    public ExportDeckCommand(IDeckExportService deckExportService,
        DeckViewModel deckViewModel,
        GlobalMessageStore globalMessageStore)
    {
        _deckExportService = deckExportService;
        _deckViewModel = deckViewModel;
        _globalMessageStore = globalMessageStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            _globalMessageStore.ClearCurrentMessage();

            Deck deck = _deckViewModel.Deck;

            DeckExportResult exportResult = await _deckExportService.Export(deck);

            // TODO: Add error handling
            if (!exportResult.IsSuccess)
            {
                _globalMessageStore.SetCurrentMessage(exportResult.Message, MessageType.Error);
                return;
            }

            _globalMessageStore.SetCurrentMessage(exportResult.Message, MessageType.Status);
        }
        catch (Exception)
        {
            _globalMessageStore.SetCurrentMessage("Deck export failed.", MessageType.Error);
        }
    }
}
