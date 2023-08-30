namespace serialization.Interfaces.Providers;

public interface IBirthDateProvider : IProvider<DateTimeOffset>
{
    public DateTimeOffset Provide(bool isChild);
}