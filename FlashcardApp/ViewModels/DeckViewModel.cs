using FlashcardApp.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.State.Navigators;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlashcardApp.ViewModels
{
    public class DeckViewModel : ViewModelBase
    {
		private readonly IParameterRenavigator _renavigator;
        private readonly Deck _deck;

        public DeckViewModel(IParameterRenavigator renavigator,
            Deck deck)
        {
            _renavigator = renavigator;
			_deck = deck;

            _name = _deck.Name;
            _description = _deck.Description;

            _newCardsNumber = _deck.Cards.Count(c => c.State == CardState.newCard);
            _cardsNumber = _deck.Cards.Count();
        }

        public ICommand SelectDeckCommand => new SelectDeckCommand(_renavigator, this);

        public Deck Deck => _deck;

		private string _name;
        public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (_name != value)
				{
					_name = value;
					OnPropertyChanged();
				}
			}
		}

        private string? _description;

        public string? Description
        {
			get
			{
				return _description;
			}
			set
			{
				if (_description != value)
				{
					_description = value;
					OnPropertyChanged();
				}
			}
		}

		private int _newCardsNumber;

        public int NewCardsNumber
		{
			get
			{
				return _newCardsNumber;
			}
			set
			{
				if (_newCardsNumber != value)
				{
					_newCardsNumber = value;
					OnPropertyChanged();
				}
			}
		}

        private int _cardsNumber;

        public int CardsNumber
        {
			get
			{
				return _cardsNumber;
			}
			set
			{
				if (_cardsNumber != value)
				{
					_cardsNumber = value;
					OnPropertyChanged();
				}
			}
		}

    }
}