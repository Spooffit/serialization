using serialization.Enums;
using serialization.Interfaces.Factories;
using serialization.Interfaces.Providers;
using serialization.Models;

namespace serialization.Common.Factories;

public class PersonFactory : IPersonFactory
{
    private readonly IChildFactory _childFactory;
    
    private readonly IIdProvider _idProvider;
    private readonly IFirstNameProvider _firstNameProvider;
    private readonly ILastNameProvider _lastNameProvider;
    private readonly IPhoneProvider _phoneProvider;
    private readonly ICreditCardNumberProvider _creditCardNumberProvider;
    private readonly ISequenceIdProvider _sequenceIdProvider;
    private readonly IBirthDateProvider _birthDateProvider;

    public PersonFactory(
        IIdProvider idProvider,
        IChildFactory childFactory,
        IFirstNameProvider firstNameProvider, 
        ILastNameProvider lastNameProvider, 
        IPhoneProvider phoneProvider, 
        ICreditCardNumberProvider creditCardNumberProvider, 
        ISequenceIdProvider sequenceIdProvider, 
        IBirthDateProvider birthDateProvider)
    {
        _idProvider = idProvider;
        _childFactory = childFactory;
        _firstNameProvider = firstNameProvider;
        _lastNameProvider = lastNameProvider;
        _phoneProvider = phoneProvider;
        _creditCardNumberProvider = creditCardNumberProvider;
        _sequenceIdProvider = sequenceIdProvider;
        _birthDateProvider = birthDateProvider;
    }
    
    public Person Produce()
    {
        return new Person
        {
            Id = _idProvider.Provide(),
            TransportId = Guid.NewGuid(),
            FirstName = _firstNameProvider.Provide(),
            LastName = _lastNameProvider.Provide(),
            SequenceId = _sequenceIdProvider.Provide(),
            CreditCardNumbers = _creditCardNumberProvider.ProvideArrange(new Random().Next(1, 21)).ToArray(),
            Age = new Random().Next(18, 61),
            Phones = _phoneProvider.ProvideArrange(new Random().Next(1, 21)).ToArray(),
            BirthDate = _birthDateProvider.Provide().ToUnixTimeSeconds(),
            Salary = new Random().Next(1000, 10001),
            IsMarred = new Random().Next(0, 2) == 1,
            Gender = (Gender)new Random().Next(0, 2),
            Children = _childFactory.ProduceArrange(new Random().Next(1, 5)).ToArray()
        };
    }

    public ICollection<Person> ProduceArrange(int count)
    {
        var persons = new List<Person>();
        for (int i = 0; i < count; i++)
        {
            persons.Add(Produce());
        }
        return persons;
    }
}