using System.Text.Json;

namespace serialization.Interfaces.Actions;

public interface IJsonActionPerformer<T>
{
    Task<ICollection<T>> TryReadAsync(string path);
    Task TryWriteAsync(string path, ICollection<T> entities);

    void Configure(JsonSerializerOptions readOptions = null, JsonSerializerOptions writeOptions = null);
}