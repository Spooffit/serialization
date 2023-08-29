using serialization.Enums;
using serialization.Interfaces;
using serialization.Models;

namespace serialization.Common.Factories;

public class ChildFactory : IChildFactory
{
    private readonly IIdProvider _idProvider;
    private readonly IFirstNameProvider _firstNameProvider;
    private readonly ILastNameProvider _lastNameProvider;
    private readonly IBirthDateProvider _birthDateProvider;
    
    public ChildFactory(
        IIdProvider idProvider,
        IFirstNameProvider firstNameProvider, 
        ILastNameProvider lastNameProvider, 
        IBirthDateProvider birthDateProvider)
    {
        _idProvider = idProvider;
        _firstNameProvider = firstNameProvider;
        _lastNameProvider = lastNameProvider;
        _birthDateProvider = birthDateProvider;
    }
    
    public Child Produce()
    {
        return new Child
        {
            Id = _idProvider.Provide(),
            FirstName = _firstNameProvider.Provide(),
            LastName = _lastNameProvider.Provide(),
            BirthDate = _birthDateProvider.Provide(true).ToUnixTimeSeconds(),
            Gender = (Gender)new Random().Next(0, 2),
        };
    }

    public ICollection<Child> ProduceArrange(int count)
    {
        var children = new List<Child>();
        for (int i = 0; i < count; i++)
        {
            children.Add(Produce());
        }
        return children;
    }
}