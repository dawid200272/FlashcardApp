using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlashcardApp.AnkiConnectAPI.JsonDTOs;
public class DeckNamesResultJsonDto
{
    [JsonPropertyName("result")]
    public List<string>? Result { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}

public class CreateDeckResultJsonDto
{
    [JsonPropertyName("result")]
    public long? Result { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}

public class Root
{
    [JsonPropertyName("models")]
    public List<ModelJsonDto> Models { get; } = new List<ModelJsonDto>();
}

public class ModelJsonDto
{
    [JsonPropertyName("modelName")]
    public string ModelName { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }
}

public class DeckJsonDto
{
    [JsonPropertyName("deckName")]
    public string DeckName { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }
}

public class ResultJsonDto
{
    [JsonPropertyName("result")]
    public object? Result { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}

public class AddNotesResultJsonDto
{
    [JsonPropertyName("result")]
    public long?[]? Result { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
