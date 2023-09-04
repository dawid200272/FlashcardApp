#define TEST

using FlashcardApp.AnkiConnectAPI.JsonDTOs;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlashcardApp.AnkiConnectAPI.Services;
public class DeckExportService : IDeckExportService
{
    private const string MediaType = "application/json";
    private const int AnkiConnectVersion = 6;
    private const string AnkiConnectModelName = "Basic";
    private const string AnkiConnectAPIAdress = "http://localhost:8765";
    private const string TagForExportedCards = "exported-from-FlashcardApp";

    public async Task<bool> Export(Deck deck)
    {
        using (HttpClient client = new HttpClient())
        {
            var endpoint = new Uri(AnkiConnectAPIAdress);

            client.BaseAddress = endpoint;

            #region DeckNamesAction

            var deckNamesRequest = new
            {
                action = "deckNames",
                version = AnkiConnectVersion
            };

            string deckNamesRequestJson = JsonSerializer.Serialize(deckNamesRequest);

            StringContent deckNamesContent = new StringContent(deckNamesRequestJson, Encoding.UTF8, MediaType);

            var deckNamesResponse = await client.PostAsync("", deckNamesContent);

            string deckNamesResponseJson = await deckNamesResponse.Content.ReadAsStringAsync();

            DeckNamesResultJsonDto? deckNamesResult = JsonSerializer.Deserialize<DeckNamesResultJsonDto>(deckNamesResponseJson);

            if (deckNamesResult is null ||
                deckNamesResult.Result is null)
            {
                // HACK: temporary solution
                throw new Exception("Result is null");
            }

            if (deckNamesResult.Result.Contains(deck.Name))
            {
                // HACK: change that to maybe ResultObject
                throw new Exception("Deck with given name already exists in Anki deck collection");
            }

            #endregion

            #region CreateDeckAction

            var createDeckRequest = new
            {
                action = "createDeck",
                version = AnkiConnectVersion,
                @params = new
                {
                    deck = deck.Name
                }
            };

            string createDeckRequestJson = JsonSerializer.Serialize(createDeckRequest);

            StringContent createDeckContent = new StringContent(createDeckRequestJson, Encoding.UTF8, MediaType);

            var createDeckResponse = await client.PostAsync("", createDeckContent);
            //var createDeckResponse = await client.PostAsJsonAsync("", createDeckRequest);

            string createDeckResultJson = await createDeckResponse.Content.ReadAsStringAsync();

            CreateDeckResultJsonDto? createDeckResult = JsonSerializer.Deserialize<CreateDeckResultJsonDto>(createDeckResultJson);

            if (createDeckResult is null ||
                createDeckResult.Result is null)
            {
                throw new Exception("Deck was not created");
            }

            #endregion

            if (!deck.Cards.Any())
            {
                return true;
            }

            #region AddNotesAction

            var options = new
            {
                allowDuplicate = false,
                duplicateScope = "deck"
            };

            var notesJsonDto = deck.Cards.Select(c => new
            {
                deckName = deck.Name,
                modelName = AnkiConnectModelName,
                fields = new
                {
                    Front = c.Front,
                    Back = c.Back
                },
                options = options,
                tags = new[]
                {
                    TagForExportedCards
                }
            });

            var addNotesRequest = new
            {
                action = "addNotes",
                version = AnkiConnectVersion,
                @params = new
                {
                    notes = notesJsonDto
                }
            };

            var addNotesRequestJson = JsonSerializer.Serialize(addNotesRequest);

            StringContent addNotesContent = new StringContent(addNotesRequestJson, Encoding.UTF8, MediaType);

            var addNotesResponse = await client.PostAsync("", addNotesContent);
            //var addNotesResponse = await client.PostAsJsonAsync("", addNotesRequest);

            string addNotesResultJson = await addNotesResponse.Content.ReadAsStringAsync();

            AddNotesResultJsonDto? addNotesResult = JsonSerializer.Deserialize<AddNotesResultJsonDto>(addNotesResultJson);

            if (addNotesResult is null ||
                addNotesResult.Result is null)
            {
                throw new Exception("Export of deck cards was not successful");
            }

            #endregion

            // TODO: Change return type to some ResultObject
            return true;

//            requestJsonDto requestJsonDTO = new requestJsonDto()
//            {
//                Action = "modelNamesAndIds",
//                Version = AnkiConnectVersion
//            };

//            requestWithParamsJsonDto requestWithParamsJsonDTO = new requestWithParamsJsonDto()
//            {
//                Action = "modelFieldNames",
//                Version = AnkiConnectVersion,
//                Params = new
//                {
//                    modelName = AnkiConnectModelName
//                }
//            };

//            //var deckNamesRequest = new
//            //{
//            //    action = "modelFieldNames",
//            //    version = ankiConnectVersion,
//            //    @params = new
//            //    {
//            //        modelName = AnkiConnectModelName
//            //    }
//            //};

//#if TEST
//            CardTemplate cardTemplate = new CardTemplate("front 1 content", "back 1 content");

//            var model = cardTemplate.TemplateType.ToString();

//            Deck deck1 = new Deck("deck");

//            Card card = new Card(cardTemplate, deck1);

//            deck1.AddCard(card);
//            deck1.AddCard(card);
//            deck1.AddCard(card);
//            deck1.AddCard(card);
//#endif

//            var notes = deck1.Cards.Select(c => new
//            {
//                deckName = deck.Name,
//                modelName = AnkiConnectModelName,
//                fields = new
//                {
//                    Front = c.Front,
//                    Back = c.Back
//                },
//                tags = new[]
//                {
//                    TagForExportedCards
//                }
//            });

//            var request = new
//            {
//                action = "addNotes",
//                version = AnkiConnectVersion,
//                @params = new
//                {
//                    notes = notes
//                }
//            };

//            var jsonRequest = JsonSerializer.Serialize(deckNamesRequest);

//            StringContent content = new StringContent(jsonRequest, Encoding.UTF8, MediaType);

//            var response = await client.PostAsync(endpoint, content);

//            var jsonResponse = await response.Content.ReadAsStringAsync();

//            //deckNamesResultJsonDto? responseResultDTO = JsonSerializer.Deserialize<deckNamesResultJsonDto>(jsonResponse);

//            AddNotesResultJsonDto? responseResultDTO = JsonSerializer.Deserialize<AddNotesResultJsonDto>(jsonResponse);

//            var error = responseResultDTO?.Error;

//            if (error is not null)
//            {
//                throw new Exception(error);
//            }

//            object? result = responseResultDTO?.Result;

//            if (result is null)
//            {
//                return false;
//            }

//            string? json = result.ToString();

//            if (string.IsNullOrWhiteSpace(json))
//            {
//                return false;
//            }

//            if (deckNamesRequest.action == "modelNamesAndIds")
//            {
//                List<ModelJsonDto> objects = GetModelJsonDTOList(json);
//            }

//            if (deckNamesRequest.action == "deckNamesAndIds")
//            {
//                var objects = GetDeckJsonDTOList(json);
//            }

//            return true;
        }
    }

    /// <summary>
    /// Get list of <see cref="ModelJsonDto"/>s
    /// </summary>
    /// <param name="json">json string to get models from</param>
    /// <returns>List of <see cref="ModelJsonDto"/></returns>
    private List<ModelJsonDto> GetModelJsonDTOList(string json)
    {
        json = json.Trim('{', '}');

        string[] jsonObjects = json.Split(',');

        for (int i = 0; i < jsonObjects.Length; i++)
        {
            var strings = jsonObjects[i].Split(':');

            strings[1] = strings[1].TrimStart();

            var @object = "{" + "\"modelName\": " + strings[0] + ", " + "\"id\": " + strings[1] + "}";

            jsonObjects[i] = @object;
        }

        json = string.Join(", ", jsonObjects);

        json = "[" + json + "]";

        var objects = JsonSerializer.Deserialize<List<ModelJsonDto>>(json);

        return objects ?? new List<ModelJsonDto>();
    }

    private List<DeckJsonDto> GetDeckJsonDTOList(string json)
    {
        //var result = new
        //{
        //    deckName = "Default",
        //    id = 1
        //};

        var result = new List<DeckJsonDto>();

        return result;
    }
}
