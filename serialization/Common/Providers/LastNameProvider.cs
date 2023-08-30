using serialization.Interfaces.Providers;

namespace serialization.Common.Providers;

public class LastNameProvider : ILastNameProvider
{
    private static readonly List<string> _lastNames = new List<string>
    {
        "Smith",
        "Johnson",
        "Williams",
        "Brown",
        "Jones",
        "Garcia",
        "Miller",
        "Davis",
        "Rodriguez",
        "Martinez",
        "Wilson",
        "Anderson",
        "Taylor",
        "Thomas",
        "Moore",
        "Jackson",
        "Martin",
        "Lee",
        "Perez",
        "Thompson"
    };
    
    public string Provide()
    {
        var random = new Random();
        int index = random.Next(_lastNames.Count);
        return _lastNames[index];
    }

    public ICollection<string> ProvideArrange(int count)
    {
        var lastNames = new List<string>();
        for (int i = 0; i < count; i++)
        {
            lastNames.Add(Provide());
        }
        return lastNames;
    }
}