using FlashcardApp.WPF.Commands;
using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.ViewModels;
public class GlobalMessageViewModel : ViewModelBase
{
    private readonly GlobalMessageStore _globalMessageStore;

    public GlobalMessageViewModel(GlobalMessageStore globalMessageStore)
    {
        _globalMessageStore = globalMessageStore;

        ClearMessageCommand = new ClearMessageCommand(_globalMessageStore);

        _globalMessageStore.CurrentMessageChanged += GlobalMessageStore_CurrentMessageChanged;
        _globalMessageStore.CurrentMessageTypeChanged += GlobalMessageStore_CurrentMessageTypeChanged;
    }

    public ICommand ClearMessageCommand { get; }

    public string Message => _globalMessageStore.CurrentMessage;
    public MessageType MessageType => _globalMessageStore.CurrentMessageType;

    public bool HasMessage => _globalMessageStore.HasCurrentMessage;
    public bool IsStatus => _globalMessageStore.CurrentMessageType is MessageType.Status;
    public bool IsError => _globalMessageStore.CurrentMessageType is MessageType.Error;

    private void GlobalMessageStore_CurrentMessageChanged()
    {
        OnPropertyChanged(nameof(HasMessage));
        OnPropertyChanged(nameof(Message));
    }

    private void GlobalMessageStore_CurrentMessageTypeChanged()
    {
        OnPropertyChanged(nameof(MessageType));
        OnPropertyChanged(nameof(IsStatus));
        OnPropertyChanged(nameof(IsError));
    }

    public override void Dispose()
    {
        _globalMessageStore.CurrentMessageChanged -= GlobalMessageStore_CurrentMessageChanged;
        _globalMessageStore.CurrentMessageTypeChanged -= GlobalMessageStore_CurrentMessageTypeChanged;

        base.Dispose();
    }
}
