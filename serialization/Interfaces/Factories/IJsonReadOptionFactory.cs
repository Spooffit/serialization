using System.Text.Json;

namespace serialization.Interfaces.Factories;

public interface IJsonReadOptionFactory
{
    JsonSerializerOptions GetReadSerializerOptions();
}