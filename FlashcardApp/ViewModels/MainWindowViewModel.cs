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
using FlashcardApp.WPF.Stores;

namespace FlashcardApp.WPF.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IFlashcardAppViewModelFactory _viewModelFactory;
    private readonly ModalNavigationStore _modalNavigationStore;

    private string _title;

    public MainWindowViewModel(INavigator navigator,
        IFlashcardAppViewModelFactory viewModelFactory,
        string title,
        ModalNavigationStore modalNavigationStore)
    {
        Navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _title = title;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
        UpdateCurrentViewModelCommand.Execute(ViewType.DeckListing);
        _modalNavigationStore = modalNavigationStore;

        _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
    }

    public override void Dispose()
    {
        _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

        base.Dispose();
    }

    private void ModalNavigationStore_CurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));
    }

    public INavigator Navigator { get; set; }
    public ICommand UpdateCurrentViewModelCommand { get; }

    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public bool IsModalOpen => _modalNavigationStore.IsOpen;

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
