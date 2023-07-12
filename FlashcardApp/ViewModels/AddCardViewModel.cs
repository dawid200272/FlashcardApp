using FlashcardApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels
{
    public class AddCardViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }

    }
}
