using System.Text.Json;
using serialization.Interfaces.Actions;
using serialization.Models;

namespace serialization.Common.Actions;

public class JsonWritePersonAction : IJsonWriteAction<Person>
{
    public async Task ExecuteAsync(string path, ICollection<Person> entities, JsonSerializerOptions? options = null)
    {
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            if (options is null)
            {
                await JsonSerializer.SerializeAsync(fs, entities);
            }
            else
            {
                await JsonSerializer.SerializeAsync(fs, entities, options);
            }
            Console.WriteLine("Data has been saved to file.");
        }

        entities.Clear();
        Console.WriteLine("Data has been cleared from memory.");
    }
}