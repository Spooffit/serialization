using System.Text.Json;

namespace serialization.Interfaces.Actions;

public interface IJsonReadAction<T> : IJsonAction
{
    Task<ICollection<T>> ExecuteAsync(string path, JsonSerializerOptions? options = null);
}