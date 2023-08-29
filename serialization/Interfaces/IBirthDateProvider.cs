namespace serialization.Interfaces;

public interface IBirthDateProvider : IProvider<DateTimeOffset>
{
    public DateTimeOffset Provide(bool isChild);
}