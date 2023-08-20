using FlashcardApp.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;
public class CloseCommand : CommandBase
{
    private readonly IRenavigator _renavigator;

    public CloseCommand(IRenavigator renavigator)
    {
        _renavigator = renavigator;
    }

    public override void Execute(object? parameter)
    {
        _renavigator.Renavigate();
    }
}
