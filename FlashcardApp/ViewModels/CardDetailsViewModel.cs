using FlashcardApp.Domain.Models;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.ViewModels;
public class CardDetailsViewModel : ViewModelBase
{
    private readonly SelectedCardStore _selectedCardStore;

    public CardDetailsViewModel(SelectedCardStore selectedCardStore)
    {
        _selectedCardStore = selectedCardStore;

        _selectedCardStore.SelectedCardChanged += SelectedCardStore_SelectedCardChanged;
    }

    public override void Dispose()
    {
        _selectedCardStore.SelectedCardChanged -= SelectedCardStore_SelectedCardChanged;

        base.Dispose();
    }

    private void SelectedCardStore_SelectedCardChanged()
    {
        OnPropertyChanged(nameof(SelectedCard));
        OnPropertyChanged(nameof(Front));
        OnPropertyChanged(nameof(Back));
        OnPropertyChanged(nameof(HasSelectedCard));
    }

    private Card? SelectedCard => _selectedCardStore.SelectedCard;

    public string Front => SelectedCard?.Front ?? "NO VALUE";
    public string Back => SelectedCard?.Back ?? "NO VALUE";

    public bool HasSelectedCard => SelectedCard is not null;
}
