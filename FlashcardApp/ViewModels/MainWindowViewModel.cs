using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title;

        public MainWindowViewModel(INavigator navigator, string title)
        {
            Navigator = navigator;
            _title = title;

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.DeckListing);
        }

        public INavigator Navigator { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}
