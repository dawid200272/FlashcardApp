using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using System.Linq;

namespace FlashcardApp.ViewModels
{
    public class DeckViewModel : ViewModelBase
    {
        private readonly Deck _deck;

        public DeckViewModel(Deck deck)
        {
            _deck = deck;

			_name = _deck.Name;
            _description = _deck.Description;

            _newCardsNumber = _deck.Cards.Count(c => c.State == CardState.newCard);

			_cardsNumber = _deck.Cards.Count();
        }

		private string _name;
        public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged();
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
				_description = value;
				OnPropertyChanged();
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
				_newCardsNumber = value;
                OnPropertyChanged();
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
                _cardsNumber = value;
                OnPropertyChanged();
			}
		}

		public Deck GetDeck()
		{
			return _deck;
		}
	}
}