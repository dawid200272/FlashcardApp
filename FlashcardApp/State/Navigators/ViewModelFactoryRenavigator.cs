using FlashcardApp.ViewModels;
using FlashcardApp.ViewModels.Factories;
using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.State.Navigators
{
    public class ViewModelFactoryRenavigator<TViewModel> : IReturnableRenavigator
        where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IFlashcardAppViewModelFactory<TViewModel> _viewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator, IFlashcardAppViewModelFactory<TViewModel> viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public ViewModelBase GetCurrentViewModel()
        {
            throw new NotImplementedException();
        }

        public void Renavigate()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }

        public ViewModelBase ReturnableRenavigate()
        {
            return _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }
    }
}
