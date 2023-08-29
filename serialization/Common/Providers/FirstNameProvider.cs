
using serialization.Interfaces;

namespace serialization.Common.Providers;

public class FirstNameProvider : IFirstNameProvider
{
    private static readonly List<string> _firstNames = new List<string>
    {
        "Liam",
        "Noah",
        "Oliver",
        "William",
        "Elijah",
        "James",
        "Benjamin",
        "Lucas",
        "Henry",
        "Alexander",
        "Emma",
        "Olivia",
        "Ava",
        "Isabella",
        "Sophia",
        "Mia",
        "Charlotte",
        "Amelia",
        "Harper",
        "Evelyn"
    };
    
    public string Provide()
    {
        var random = new Random();
        int index = random.Next(_firstNames.Count);
        return _firstNames[index];
    }

    public ICollection<string> ProvideArrange(int count)
    {
        var firstNames = new List<string>();
        for (int i = 0; i < count; i++)
        {
            firstNames.Add(Provide());
        }
        return firstNames;
    }
}