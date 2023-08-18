using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.ViewModels.Factories;

public interface IFlashcardAppViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}
