using System.Text.Json;
using serialization.Interfaces.Actions;
using serialization.Models;

namespace serialization.Common.Actions;

public class JsonActionPersonPerformer : IJsonActionPerformer<Person>
{
    private JsonSerializerOptions _readOptions;
    private JsonSerializerOptions _writeOptions;
    
    private readonly IJsonReadAction<Person> _jsonReadAction;
    private readonly IJsonWriteAction<Person> _jsonWriteAction;
    
    public JsonActionPersonPerformer(
        IJsonReadAction<Person> jsonReadAction, 
        IJsonWriteAction<Person> jsonWriteAction)
    {
        _jsonReadAction = jsonReadAction;
        _jsonWriteAction = jsonWriteAction;
    }
    
    public async Task<ICollection<Person>> TryReadAsync(string path)
    {
        try
        {
            return await ReadAsync(path);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Read action was failed with message: \n {e.Message}");
            throw;
        }
    }

    public async Task TryWriteAsync(string path, ICollection<Person> entities)
    {
        try
        {
            await WriteAsync(path, entities);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Write action was failed with message: \n {e.Message}");
            throw;
        }
    }
    
    public void Configure(JsonSerializerOptions readOptions = null, JsonSerializerOptions writeOptions = null)
    {
        _readOptions = readOptions;
        _writeOptions = writeOptions;
    }
    
    private async Task<ICollection<Person>> ReadAsync(string path)
    {
        var result = await _jsonReadAction.ExecuteAsync(path, _readOptions);
        return result;
    }

    private async Task WriteAsync(string path, ICollection<Person> entities)
    {
        await _jsonWriteAction.ExecuteAsync(path, entities, _writeOptions);
    }
}