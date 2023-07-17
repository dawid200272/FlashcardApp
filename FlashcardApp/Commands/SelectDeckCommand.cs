using FlashcardApp.Domain.Models;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.Commands
{
    public class SelectDeckCommand : CommandBase
    {
        private readonly IReturnableRenavigator _renavigator;
        private readonly DeckViewModel _deckViewModel;

        public SelectDeckCommand(IReturnableRenavigator renavigator,
            DeckViewModel deckViewModel)
        {
            _renavigator = renavigator;
            _deckViewModel = deckViewModel;
        }

        public override void Execute(object? parameter)
        {
            DeckDetailsViewModel viewModel = (DeckDetailsViewModel)_renavigator.ReturnableRenavigate();

            viewModel.LoadDeckDetailsViewModel(_deckViewModel);
        }
    }
}
