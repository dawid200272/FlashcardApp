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

    public async Task<DeckExportResult> Export(Deck deck)
    {
        using HttpClient client = new HttpClient();

        var endpoint = new Uri(AnkiConnectAPIAdress);

        client.BaseAddress = endpoint;

        #region DeckNamesAction

        object deckNamesRequest = GetRequestJsonDto(actionName: "deckNames", version: AnkiConnectVersion);

        DeckNamesResultJsonDto deckNamesResult = await GetResultJsonDtoAsync<DeckNamesResultJsonDto>(requestJsonDto: deckNamesRequest, requestUri: "", client);

        if (deckNamesResult.Result is null)
        {
            return new DeckExportResult(IsSuccess: false, Message: "Collection of decks cannot be fetched from Anki Connect API.");
        }

        if (deckNamesResult.Result.Contains(deck.Name))
        {
            return new DeckExportResult(IsSuccess: false, Message: "Deck with given name already exists in Anki deck collection.");
        }

        #endregion

        #region CreateDeckAction

        object createDeckParams = new
        {
            deck = deck.Name
        };

        object createDeckRequest = GetRequestJsonDto("createDeck", AnkiConnectVersion, createDeckParams);

        CreateDeckResultJsonDto createDeckResult = await GetResultJsonDtoAsync<CreateDeckResultJsonDto>(requestJsonDto: createDeckRequest, requestUri: "", client);

        if (createDeckResult.Result is null)
        {
            return new DeckExportResult(IsSuccess: false, Message: "Deck was not exported.");
        }

        #endregion

        if (!deck.Cards.Any())
        {
            return new DeckExportResult(IsSuccess: true, Message: "Empty deck was exported successfully.");
        }

        #region AddNotesAction

        object options = new
        {
            allowDuplicate = false,
            duplicateScope = "deck"
        };

        object notesJsonDto = deck.Cards.Select(c => new
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

        object addNotesParams = new
        {
            notes = notesJsonDto
        };

        object addNotesRequest = GetRequestJsonDto("addNotes", AnkiConnectVersion, addNotesParams);

        AddNotesResultJsonDto addNotesResult = await GetResultJsonDtoAsync<AddNotesResultJsonDto>(requestJsonDto: addNotesRequest, requestUri: "", client);

        if (addNotesResult.Result is null)
        {
            return new DeckExportResult(IsSuccess: false, Message: "Export of deck cards was not successful.");
        }

        #endregion

        return new DeckExportResult(IsSuccess: true, Message: "Deck was exported successfully.");
    }

    #region Helper Methods

    /// <summary>
    /// Get request Json dto for given arguments.
    /// </summary>
    /// <param name="actionName">Name of Anki Connect API action.</param>
    /// <param name="version">Version of Anki Connect API.</param>
    /// <param name="params">Params object for Anki Connect API request.</param>
    /// <returns>Request Json dto without params if params is <see langword="null"/> and with params otherwise.</returns>
    private object GetRequestJsonDto(string actionName, int version, object? @params = null)
    {
        if (@params is null)
        {
            return new
            {
                action = actionName,
                version = version
            };
        }

        return new
        {
            action = actionName,
            version = version,
            @params = @params
        };
    }

    // TODO: Document this
    private async Task<T> GetResultJsonDtoAsync<T>(object requestJsonDto, string? requestUri, HttpClient client)
        where T : class, new()
    {
        StringContent requestContent = GetRequestContent(requestJsonDto);

        var response = await client.PostAsync("", requestContent);

        T resultJsonDto = await GetResultJsonDto<T>(response: response);

        return resultJsonDto;
    }

    // TODO: Document this
    private StringContent GetRequestContent(object requestJsonDto)
    {
        string requestJson = JsonSerializer.Serialize(requestJsonDto);

        StringContent requestContent = new StringContent(requestJson, Encoding.UTF8, MediaType);

        return requestContent;
    }

    // TODO: Document this
    private async Task<T> GetResultJsonDto<T>(HttpResponseMessage response)
        where T : class, new()
    {
        string responseJson = await response.Content.ReadAsStringAsync();

        T? resultJsonDto = JsonSerializer.Deserialize<T>(responseJson);

        return resultJsonDto ?? new T();
    }

    #endregion

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
