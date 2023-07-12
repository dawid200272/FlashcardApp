using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Models.Enums;
using FlashcardApp.Domain.Services;
using FlashcardApp.EntityFramework;
using FlashcardApp.EntityFramework.Services;

//IDataService<Deck> deckService = new GenericDataService<Deck>(new FlashcardAppDbContextFactory());
//Console.WriteLine(deckService.Delete(1).Result);

//Console.ReadLine();


var cardTemplateService = new CardTemplateService();
var deckService = new DeckService();

List<Deck> Decks = new List<Deck>() { await deckService.CreateEmptyDeck("Default") };
Decks.Add(await deckService.CreateEmptyDeck("test"));

var cardTemplate1 = cardTemplateService.CreateCardTemplate(CardTemplateType.Basic, "question", "answer");
var cardTemplate2 = cardTemplateService.CreateCardTemplate(CardTemplateType.Basic, "f", "b");



Console.ReadLine();