using serialization.Interfaces;

namespace serialization.Common.Providers;

public class BirthDateProvider : IBirthDateProvider
{
    public DateTimeOffset Provide(bool isChild)
    {
        Random random = new Random();
        
        int minAge = isChild ? 2 : 18;
        int maxAge = isChild ? 17 : 60;
        
        int randomAge = random.Next(minAge, maxAge + 1);

        DateTimeOffset birthDate = DateTimeOffset.UtcNow.AddYears(-randomAge);
        return birthDate;
    }
    

    public DateTimeOffset Provide()
    {
        return Provide(false);
    }

    public ICollection<DateTimeOffset> ProvideArrange(int count)
    {
        var ages = new List<DateTimeOffset>();
        for (int i = 0; i < count; i++)
        {
            ages.Add(Provide());
        }
        return ages;
    }
}