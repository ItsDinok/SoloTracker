using System.Text;
using System.Text.Json;

namespace SoloTracker;

public class JsonManager
{
    public static void Write_Json(
        string encounter,
        float difficulty,
        bool is_completed,
        bool uploaded,
        bool is_warlock,
        bool is_titan,
        bool is_hunter,
        Raid raid,
        string? variants = null,
        string? upload_notes = null,
        string? additional = null,
        bool is_archived = false
    )
    {
        var options = new JsonWriterOptions{Indented = true};

        using var stream = File.Create("table.json");
        using (var writer = new Utf8JsonWriter(stream, options))
        {
            writer.WriteStartObject();

            writer.WriteString("encounter", encounter);
            writer.WriteNumber("difficulty", difficulty);
            writer.WriteBoolean("is_completed", is_completed);

            if (variants != null) writer.WriteString("variants", variants);
            if (additional != null) writer.WriteString("additional", additional);

            writer.WriteBoolean("uploaded", uploaded);

            if (upload_notes != null) writer.WriteString("upload_notes", upload_notes);

            writer.WriteBoolean("is_warlock", is_warlock);
            writer.WriteBoolean("is_titan", is_titan);
            writer.WriteBoolean("is_hunter", is_hunter);
            writer.WriteBoolean("is_archived", is_archived);

            writer.WriteString("raid", Table.Stringify_Raid(raid));

            writer.WriteEndObject();
            writer.Flush();
        }
    }
}
