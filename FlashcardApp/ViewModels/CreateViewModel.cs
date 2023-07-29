using FlashcardApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>()
        where TViewModel : ViewModelBase;
}
