using FlashcardApp.AnkiConnectAPI.JsonDTOs;
using FlashcardApp.Domain.Models;
using FlashcardApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlashcardApp.AnkiConnectAPI.Services;
public class DeckExportService : IDeckExportService
{
    private const string MEDIA_TYPE = "application/json";
    private const int ANKI_CONNECT_VERSION = 6;

    public async Task<bool> Export(Deck deck)
    {
        using (HttpClient client = new HttpClient())
        {
            var endpoint = new Uri("http://localhost:8765");

            requestJsonDTO requestJsonDTO = new requestJsonDTO()
            {
                Action = "modelNamesAndIds",
                Version = ANKI_CONNECT_VERSION
            };

            requestWithParamsJsonDTO requestWithParamsJsonDTO = new requestWithParamsJsonDTO()
            {
                Action = "modelFieldNames",
                Version = ANKI_CONNECT_VERSION,
                Params = new
                {
                    modelName = "Basic"
                }
            };

            var request = new
            {
                action = "modelFieldNames",
                version = ANKI_CONNECT_VERSION,
                @params = new
                {
                    modelName = "Basic"
                }
            };

            var jsonRequest = JsonSerializer.Serialize(request);

            StringContent content = new StringContent(jsonRequest, Encoding.UTF8, MEDIA_TYPE);

            var response = await client.PostAsync(endpoint, content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            deckNamesAndIdsResultDTO? responseResultDTO = JsonSerializer.Deserialize<deckNamesAndIdsResultDTO>(jsonResponse);

            var error = responseResultDTO?.error;

            if (error is not null)
            {
                throw new Exception(error);
            }

            object? result = responseResultDTO?.result;

            if (result is null)
            {
                return false;
            }

            string? json = result.ToString();

            if (string.IsNullOrWhiteSpace(json))
            {
                return false;
            }

            if (request.action == "modelNamesAndIds")
            {
                List<ModelJsonDTO> objects = GetModelJsonDTOList(json);
            }

            return true;
        }
    }

    private List<ModelJsonDTO> GetModelJsonDTOList(string json)
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

        List<ModelJsonDTO> objects = JsonSerializer.Deserialize<List<ModelJsonDTO>>(json);

        return objects;
    }
}
