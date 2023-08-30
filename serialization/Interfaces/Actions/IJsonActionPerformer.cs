using System.Text.Json;

namespace serialization.Interfaces.Actions;

public interface IJsonActionPerformer<T>
{
    Task<ICollection<T>> ReadAsync(string path);
    Task WriteAsync(string path, ICollection<T> entities);

    void Configure(JsonSerializerOptions readOptions = null, JsonSerializerOptions writeOptions = null);
}