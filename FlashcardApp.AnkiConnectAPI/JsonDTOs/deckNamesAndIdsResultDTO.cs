using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlashcardApp.AnkiConnectAPI.JsonDTOs;
public class deckNamesAndIdsResultDTO
{
    public object? result { get; set; }
    public string? error { get; set; }

    //public class Result
    //{
    //    public List<DeckJsonDTO> Decks { get; set; }

    //    public Result(List<DeckJsonDTO> decks)
    //    {
    //        Decks = decks;
    //    }
    //}

    //public class DeckJsonDTO
    //{
    //    public string DeckName { get; set; }
    //    public long Id { get; set; }
    //}
}

public class Root
{
    [JsonPropertyName("models")]
    public List<ModelJsonDTO> Models { get; } = new List<ModelJsonDTO>();
}

public class ModelJsonDTO
{
    [JsonPropertyName("modelName")]
    public string ModelName { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }
}
