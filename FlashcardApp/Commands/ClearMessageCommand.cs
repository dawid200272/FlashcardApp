using FlashcardApp.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class ClearMessageCommand : CommandBase
{
    private readonly GlobalMessageStore _globalMessageStore;

    public ClearMessageCommand(GlobalMessageStore globalMessageStore)
    {
        _globalMessageStore = globalMessageStore;
    }

    public override void Execute(object? parameter)
    {
        _globalMessageStore.ClearCurrentMessage();
    }
}
