using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Stores;
public class SelectedCardStore
{
	private Card? _selectedCard;
	public Card? SelectedCard
    {
        get => _selectedCard;
        set
        {
            if (SelectedCard == value)
            {
                return;
            }

            _selectedCard = value;
            SelectedCardChanged?.Invoke();
        }
    }

    public event Action? SelectedCardChanged;

    public void OnSelectedCardChanged()
    {
        SelectedCardChanged?.Invoke();
    }
}
