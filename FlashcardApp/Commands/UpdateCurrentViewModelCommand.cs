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

namespace FlashcardApp.WPF.Commands;

public class UpdateCurrentViewModelCommand : CommandBase
{
    private readonly INavigator _navigator;
    private readonly IFlashcardAppViewModelFactory _viewModelFactory;

    public UpdateCurrentViewModelCommand(INavigator navigator, IFlashcardAppViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
    }

    public override void Execute(object? parameter)
    {
        if (parameter is ViewType viewType)
        {
            _navigator.CurrentViewModel?.Dispose();

            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }
    }
}
