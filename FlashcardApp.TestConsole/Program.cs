using FlashcardApp.EntityFramework;
using FlashcardApp.EntityFramework.Services;
using FlashcardApp.Models;
using FlashcardApp.Services;

//IDataService<Deck> deckService = new GenericDataService<Deck>(new FlashcardAppDbContextFactory());
//Console.WriteLine(deckService.Delete(1).Result);

//Console.ReadLine();

List<Deck> Decks = new List<Deck>() { await Deck.CreateEmptyDeck("Default") };

var cardTemplate1 = new CardTemplate();

