using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.State.Navigators;

public class ViewModelDelegateRenavigator<TViewModel> : IParameterRenavigator
    where TViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    private readonly CreateViewModel<TViewModel> _createViewModel;

    public ViewModelDelegateRenavigator(INavigator navigator,
        CreateViewModel<TViewModel> createViewModel)
    {
        _navigator = navigator;
        _createViewModel = createViewModel;
    }

    public void Renavigate()
    {
        _navigator.CurrentViewModel = _createViewModel();
    }

    public void Renavigate(Action<ViewModelBase> action)
    {
        ViewModelBase viewModel = _createViewModel();

        action(viewModel);

        _navigator.CurrentViewModel = viewModel;
    }
}
