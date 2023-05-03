using FlashcardApp.EntityFramework;
using FlashcardApp.EntityFramework.Services;
using FlashcardApp.Models;
using FlashcardApp.Services;

IDataService<Deck> deckService = new GenericDataService<Deck>(new FlashcardAppDbContextFactory());
Console.WriteLine(deckService.Delete(1).Result);

Console.ReadLine();