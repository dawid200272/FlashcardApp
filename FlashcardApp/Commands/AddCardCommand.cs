using FlashcardApp.Domain.Services;
using FlashcardApp.WPF.ViewModels;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.Domain.Models;

namespace FlashcardApp.WPF.Commands;
public class AddCardCommand : AsyncCommandBase
{
    private readonly AddCardViewModel _viewModel;
    private readonly DeckStore _deckStore;

    public AddCardCommand(AddCardViewModel viewModel,
        DeckStore deckStore)
    {
        _viewModel = viewModel;
        _deckStore = deckStore;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.CanAddCard && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        string front = _viewModel.Front;
        string back = _viewModel.Back;
        CardTemplateType templateType = _viewModel.SelectedTemplateType;

        Deck? selectedDeck = _viewModel.SelectedDeck;

        await _deckStore.AddCardAsync(templateType, front, back, selectedDeck);

        _viewModel.ResetForm();
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.CanAddCard))
        {
            OnCanExecuteChanged();
        }
    }
}
