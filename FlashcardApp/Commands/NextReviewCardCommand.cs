﻿using FlashcardApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.WPF.Commands;

public class NextReviewCardCommand : CommandBase
{
    private readonly CardReviewViewModel _viewModel;

    public NextReviewCardCommand(CardReviewViewModel cardReviewViewModel)
    {
        _viewModel = cardReviewViewModel;
    }

    public override void Execute(object? parameter)
    {
        _viewModel.NextReviewCard();
    }
}
