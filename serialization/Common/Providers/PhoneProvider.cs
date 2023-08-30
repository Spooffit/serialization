using serialization.Interfaces.Providers;

namespace serialization.Common.Providers;

public class PhoneProvider : IPhoneProvider
{
    private static readonly List<string> _phones = new List<string>
    {
        "1234567890",
        "9876543210",
        "5555555555",
        "9999999999",
        "1111111111",
        "7777777777",
        "8888888888",
        "4444444444",
        "6666666666",
        "2222222222",
        "5554443333",
        "7778889999",
        "9998887777",
        "2223334444",
        "1112223333",
        "5556667777",
        "8889990000",
        "1231231234",
        "9879879876",
        "5432109876"
    };
    
    public string Provide()
    {
        var random = new Random();
        int index = random.Next(_phones.Count);
        return _phones[index];
    }

    public ICollection<string> ProvideArrange(int count)
    {
        var phones = new List<string>();
        for (int i = 0; i < count; i++)
        {
            phones.Add(Provide());
        }
        return phones;
    }
}