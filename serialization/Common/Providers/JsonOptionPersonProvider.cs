using System.Text.Json;
using serialization.Interfaces.Providers;

namespace serialization.Common.Providers;

public class JsonOptionPersonProvider : IJsonOptionProvider
{
    public JsonSerializerOptions GetReadSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public JsonSerializerOptions GetWriteSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}