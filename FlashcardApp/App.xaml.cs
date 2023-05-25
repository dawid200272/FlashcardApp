using FlashcardApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlashcardApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<Deck> _decks;

        public IEnumerable<Deck> Decks => _decks;

        public App()
        {
            _decks = new List<Deck>
            {
                new Deck("Default")
            };
        }
    }
}
