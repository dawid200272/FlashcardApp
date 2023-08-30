using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.State.Navigators;

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
}

public enum ViewType
{
    DeckListing,
    DeckDetails,
    CardReview,
    AddCard,
    CardViewer
}
