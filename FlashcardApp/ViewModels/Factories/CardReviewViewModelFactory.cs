﻿using FlashcardApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.ViewModels.Factories
{
    public class CardReviewViewModelFactory : IFlashcardAppViewModelFactory<CardReviewViewModel>
    {
        public CardReviewViewModel CreateViewModel()
        {
            return new CardReviewViewModel();
        }
    }
}