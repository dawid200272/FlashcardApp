using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.Domain.Services;
using FlashcardApp.WPF.ViewModels;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FlashcardApp.WPF.ViewModels;

public class AddCardViewModel : ViewModelBase
{
    private readonly IRenavigator _renavigator;
    private readonly DeckStore _deckStore;

    public AddCardViewModel(IRenavigator renavigator,
        DeckStore deckStore)
    {
        _renavigator = renavigator;
        _deckStore = deckStore;

        AddCardCommand = new AddCardCommand(this, deckStore);
        CloseCommand = new CloseCommand(_renavigator);
    }

    // TODO: Add command to add card and implement all logic in it
    public ICommand AddCardCommand { get; }

    // TODO: Add command to close the add card view and implement all logic in it
    public ICommand CloseCommand { get; }

    private CardTemplateType _selectedtemplateType = CardTemplateType.Basic;

    public CardTemplateType SelectedTemplateType
    {
        get => _selectedtemplateType;
        set
        {
            if (_selectedtemplateType != value)
            {
                _selectedtemplateType = value;
                OnPropertyChanged();
            }
        }
    }
    public ObservableCollection<string> DeckNames => new ObservableCollection<string>(GetDeckNames(_deckStore));

    public Deck? SelectedDeck => GetDeckFromName(_selectedDeckName, _deckStore);

    private string _selectedDeckName;

    public string SelectedDeckName
    {
        get => _selectedDeckName;
        set
        {
            if (_selectedDeckName != value)
            {
                _selectedDeckName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedDeck));
                OnPropertyChanged(nameof(CanAddCard));
            }
        }
    }

    private string _front;
    public string Front
    {
        get => _front;
        set
        {
            if (_front != value)
            {
                _front = value;
                OnPropertyChanged(nameof(Front));
                OnPropertyChanged(nameof(CanAddCard));
            }
        }
    }

    private string _back;

    public string Back
    {
        get => _back;
        set
        {
            if (_back != value)
            {
                _back = value;
                OnPropertyChanged(nameof(Back));
                OnPropertyChanged(nameof(CanAddCard));
            }
        }
    }

    public bool CanAddCard => !string.IsNullOrWhiteSpace(Front) && 
        !string.IsNullOrWhiteSpace(Back) &&
        SelectedDeck is not null;

    private IEnumerable<string> GetDeckNames(DeckStore deckStore)
    {
        List<string> result = new List<string>();

        foreach (Deck deck in deckStore)
        {
            result.Add(deck.Name);
        }

        return result;
    }

    public Deck? GetDeckFromName(string name, DeckStore deckStore)
    {
        return deckStore.Decks.FirstOrDefault(d => d.Name == name);
    }

    // TODO: Reconsider this (maybe you can come up with sth better)
    public void ResetForm()
    {
        _front = string.Empty;
        _back = string.Empty;

        OnPropertyChanged(nameof(Front));
        OnPropertyChanged(nameof(Back));
    }
}
