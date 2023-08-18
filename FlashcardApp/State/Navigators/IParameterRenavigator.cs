using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.State.Navigators;

public interface IParameterRenavigator : IRenavigator
{
    void Renavigate(Action<ViewModelBase> action);
}
