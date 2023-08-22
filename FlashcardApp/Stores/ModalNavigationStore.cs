using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Stores;

public class ModalNavigationStore
{
	private ViewModelBase? _currentViewModel;
	public ViewModelBase? CurrentViewModel
	{
		get
		{
			return _currentViewModel;
		}
		set
		{
			if (value != _currentViewModel)
			{
				_currentViewModel = value;

				//_currentViewModel.Dispose();
				CurrentViewModelChanged?.Invoke();
			}
		}
	}

    public bool IsOpen => _currentViewModel != null;

    public event Action CurrentViewModelChanged;

    public void Close()
    {
		CurrentViewModel = null;
    }
}
