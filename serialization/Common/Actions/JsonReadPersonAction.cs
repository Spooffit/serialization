using System.Text.Json;
using serialization.Interfaces.Actions;
using serialization.Models;

namespace serialization.Common.Actions;

public class JsonReadPersonAction : IJsonReadAction<Person>
{
    public async Task<ICollection<Person>> ExecuteAsync(string path, JsonSerializerOptions? options = null)
    {
        ICollection<Person>? persons;

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            if (options is null)
            {
                persons = await JsonSerializer.DeserializeAsync<ICollection<Person>>(fs);
            }
            else
            {
                persons = await JsonSerializer.DeserializeAsync<ICollection<Person>>(fs, options);
            }
            Console.WriteLine("Data has been read from file.");
            Console.WriteLine();
        }

        return persons;
    }
}