using System.Text.Json;

namespace serialization.Interfaces.Factories;

public interface IJsonWriteOptionFactory
{
    JsonSerializerOptions GetWriteSerializerOptions();
}