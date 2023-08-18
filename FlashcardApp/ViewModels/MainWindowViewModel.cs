using FlashcardApp.WPF.Commands;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using FlashcardApp.WPF.State.Navigators;
using FlashcardApp.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IFlashcardAppViewModelFactory _viewModelFactory;
    private string _title;

    public MainWindowViewModel(INavigator navigator, IFlashcardAppViewModelFactory viewModelFactory, string title)
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
