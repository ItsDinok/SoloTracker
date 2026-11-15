using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace SoloTracker;

/*
    I managed to write the json reader wrong. It needs to take a list of rows and then write them all to a json file
*/

public static class JsonManager
{
    static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() }
    };

    public static List<Row> Read_Json()
    {
        if (!File.Exists("table.json")) return new();
        using var stream = File.OpenRead("table.json");
        return JsonSerializer.Deserialize<List<Row>>(stream, Options) ?? new();
    }

    public static void Write_Json(List<Row> rows)
    {
        using var stream = File.Create("table.json");
        JsonSerializer.Serialize(stream, rows, Options);
    }
}
