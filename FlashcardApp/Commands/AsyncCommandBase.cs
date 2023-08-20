﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlashcardApp.WPF.Commands;
public abstract class AsyncCommandBase : ICommand
{
    private bool _isExecuting;
    public bool IsExecuting
    {
        get => _isExecuting;
        set
        {
            _isExecuting = value;
            OnCanExecuteChanged();
        }
    }

    public event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter)
    {
        return !_isExecuting;
    }

    public async void Execute(object? parameter)
    {
        IsExecuting = true;

        await ExecuteAsync(parameter);

        IsExecuting = false;
    }

    public abstract Task ExecuteAsync(object? parameter);

    protected void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}