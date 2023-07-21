using FlashcardApp.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.State.Navigators;
using FlashcardApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IFlashcardAppViewModelAbstractFactory _viewModelFactory;
        private string _title;

        public MainWindowViewModel(INavigator navigator, IFlashcardAppViewModelAbstractFactory viewModelFactory, string title)
        {
            Navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _title = title;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.DeckListing);
        }

        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }

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
