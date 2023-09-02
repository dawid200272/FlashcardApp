using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlashcardApp.AnkiConnectAPI.JsonDTOs;
public class requestJsonDTO
{
    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }
}

public class requestWithParamsJsonDTO
{
    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("params")]
    public object Params { get; set; }
}
