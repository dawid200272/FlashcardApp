using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FlashcardApp.WPF.Stores;

public enum MessageType
{
    Status,
    Error
}

public class GlobalMessageStore
{
    private readonly Timer _messageClearingTimer;

    private const int DefaultMessageClearDelayInMiliseconds = 5000;

    public GlobalMessageStore()
    {
        _currentMessage = string.Empty;

        _messageClearingTimer = new Timer();

        _messageClearingTimer.Elapsed += MessageClearingTimer_Elapsed;
    }

    private string _currentMessage;
	public string CurrentMessage
    {
        get => _currentMessage;
        private set
        {
            if (value == _currentMessage)
            {
                return;
            }

            _currentMessage = value;
            OnCurrentMessageChanged();
        }
    }

    private MessageType _currentMessageType;
    public MessageType CurrentMessageType
    {
        get => _currentMessageType;
        private set
        {
            if (_currentMessageType == value)
            {
                return;
            }

            _currentMessageType = value;
            OnCurrentMessageTypeChanged();
        }
    }

    public bool HasCurrentMessage => !string.IsNullOrEmpty(CurrentMessage);

    public event Action? CurrentMessageChanged;
    public event Action? CurrentMessageTypeChanged;

    public void SetCurrentMessage(string message, MessageType messageType, int? messageClearDelayInMiliseconds = null)
    {
        CurrentMessage = message;
        CurrentMessageType = messageType;

        if (messageType is MessageType.Status)
        {
            _messageClearingTimer.Interval = messageClearDelayInMiliseconds ?? DefaultMessageClearDelayInMiliseconds;

            _messageClearingTimer.Start();
        }
    }

    public void ClearCurrentMessage()
    {
        CurrentMessage = string.Empty;
    }

    private void MessageClearingTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        _messageClearingTimer?.Stop();

        ClearCurrentMessage();
    }

    private void OnCurrentMessageChanged()
	{
        CurrentMessageChanged?.Invoke();
	}

    private void OnCurrentMessageTypeChanged()
    {
        CurrentMessageTypeChanged?.Invoke();
    }
}
