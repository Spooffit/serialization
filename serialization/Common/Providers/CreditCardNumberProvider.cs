using serialization.Interfaces;

namespace serialization.Common.Providers;

public class CreditCardNumberProvider : ICreditCardNumberProvider
{
    private static readonly List<string> _creditCards = new List<string>
    {
        "1234 5678 9012 3456",
        "9876 5432 1098 7654",
        "5555 5555 5555 4444",
        "9999 9999 9999 8888",
        "1111 1111 1111 2222",
        "7777 7777 7777 6666",
        "8888 8888 8888 9999",
        "4444 4444 4444 3333",
        "6666 6666 6666 5555",
        "2222 2222 2222 3333",
        "5432 1098 7654 3210",
        "7777 8888 9999 0000",
        "9999 8888 7777 5555",
        "2222 3333 4444 5555",
        "1111 2222 3333 4444",
        "5555 6666 7777 8888",
        "8888 9999 0000 1111",
        "1231 2312 3412 3412",
        "9876 9876 9876 5432",
        "5432 1098 7654 3210"
    };
    
    public string Provide()
    {
        var random = new Random();
        int index = random.Next(_creditCards.Count);
        return _creditCards[index];
    }

    public ICollection<string> ProvideArrange(int count)
    {
        var creditCardNumbers = new List<string>();
        for (int i = 0; i < count; i++)
        {
            creditCardNumbers.Add(Provide());
        }
        return creditCardNumbers;
    }
}