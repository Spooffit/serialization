using System.Text.Json;

namespace serialization.Interfaces.Actions;

public interface IJsonWriteAction<T> : IJsonAction
{
   Task ExecuteAsync(string path, ICollection<T> entities, JsonSerializerOptions? options = null);
}