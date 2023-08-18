using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;

public class AddCardViewModel : ViewModelBase
{
    private readonly IRenavigator _renavigator;
    private readonly DeckStore _deckStore;

    public AddCardViewModel(IRenavigator renavigator, DeckStore deckStore)
    {
        _renavigator = renavigator;
        _deckStore = deckStore;
    }

    public ICommand AddCardCommand { get; set; }
    public ICommand CloseCommand { get; set; }

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
            }
        }
    }

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

    public void ResetForm()
    {
        _front = string.Empty;
        _back = string.Empty;

        OnPropertyChanged(nameof(Front));
        OnPropertyChanged(nameof(Back));
    }
}
